using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Communes;
using Todo.Districts;
using Todo.Permissions;
using Volo.Abp.Account;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Todo.Provinces
{
    [Authorize(TodoPermissions.Provinces.Default)]
    public class ProvinceAppService : TodoAppService, IProvinceAppService
    {
        private readonly IProvinceRepository _provinceRepository;
        private readonly IDistrictRepository _districtRepository;
        private readonly ICommuneRepository _communeRepository;

        public ProvinceAppService(IProvinceRepository provinceRepository, IDistrictRepository districtRepository, ICommuneRepository communeRepository)
        {
            _provinceRepository = provinceRepository;
            _districtRepository = districtRepository;
            _communeRepository = communeRepository;
        }
        public async Task<ProvinceDto> GetAsync(Guid id)
        {
            var province = await _provinceRepository.GetAsync(id);
            return ObjectMapper.Map<Province, ProvinceDto>(province);
        }

        public async Task<PagedResultDto<ProvinceDto>> GetListAsync(GetProvinceListDto input)
        {

            var provinces = await _provinceRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter);
            var filteredProvinces = provinces.Where(p => p.Status < 2).ToList();
            var totalCount = input.Filter == null
                ? await _provinceRepository.CountAsync(p => p.Status < 2)
                : await _provinceRepository.CountAsync(province => province.Status < 2 && province.Name.Contains(input.Filter));
            return new PagedResultDto<ProvinceDto>(
                totalCount,
                ObjectMapper.Map<List<Province>, List<ProvinceDto>>(filteredProvinces)
            );
        }

        public async Task<PagedResultDto<ProvinceDto>> GetListDeletedAsync(GetProvinceListDto input)
        {

            var provinces = await _provinceRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter);
            var filteredProvinces = provinces.Where(p => p.Status == 2).ToList();
            var totalCount = input.Filter == null
                ? await _provinceRepository.CountAsync(p => p.Status == 2)
                : await _provinceRepository.CountAsync(province => province.Status == 2 && province.Name.Contains(input.Filter));

            return new PagedResultDto<ProvinceDto>(
                totalCount,
                ObjectMapper.Map<List<Province>, List<ProvinceDto>>(filteredProvinces)
            );
        }

        [Authorize(TodoPermissions.Provinces.Create)]
        public async Task<ProvinceDto> CreateAsync(CreateUpdateProvinceDto input)
        {
            var province = ObjectMapper.Map<CreateUpdateProvinceDto, Province>(input);
            var provinceNameExist = await _provinceRepository.FindByNameAsync(province.Name);
            var provinceCodeExist = await _provinceRepository.FindByCodeAsync(province.Code);
            var provinceAreaCodeExist = await _provinceRepository.FindByAreaCodeAsync(province.AreaCode);
            if (provinceCodeExist != null)
            {
                throw new CodeAlreadyExistsException(province.Code);
            }
            if (provinceNameExist != null)
            {
                throw new NameAlreadyExistsException(province.Name);
            }
            if (provinceAreaCodeExist != null)
            {
                throw new AreaCodeAlreadyExistsException(province.AreaCode);
            }

            await _provinceRepository.InsertAsync(province);

            return ObjectMapper.Map<Province, ProvinceDto>(province);
        }

        [Authorize(TodoPermissions.Provinces.Edit)]
        public async Task UpdateAsync(Guid id, CreateUpdateProvinceDto input)
        {
            var province = await _provinceRepository.GetAsync(id);
            province.Name = input.Name;
            province.Code = input.Code;
            province.AreaCode = input.AreaCode;
            province.Status = 1;
            province.Description = input.Description;
            province.OrderBy = input.OrderBy;
            var provinceNameExist = await _provinceRepository.FindByNameAsync(province.Name);
            var provinceCodeExist = await _provinceRepository.FindByCodeAsync(province.Code);
            var provinceAreaCodeExist = await _provinceRepository.FindByAreaCodeAsync(province.AreaCode);
            if (provinceCodeExist != null && provinceCodeExist.Id != province.Id)
            {
                throw new CodeAlreadyExistsException(province.Code);
            }
            if (provinceNameExist != null && provinceNameExist.Id != province.Id)
            {
                throw new NameAlreadyExistsException(province.Name);
            }
            if (provinceAreaCodeExist != null && provinceAreaCodeExist.Id != province.Id)
            {
                throw new AreaCodeAlreadyExistsException(province.AreaCode);
            }
            await _provinceRepository.UpdateAsync(province);
        }

        [Authorize(TodoPermissions.Provinces.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            var province = await _provinceRepository.GetAsync(id);
            var districts = await _districtRepository.GetListDistrictsAsync(id);
            foreach ( var district in districts) {
                district.Status = 3;
                var communes = await _communeRepository.GetListCommunesAsync(district.Id);
                foreach (var commune in communes)
                {
                    commune.Status = 3;
                }
            }
            province.Status = 2;
            await _districtRepository.UpdateManyAsync(districts);
            await _provinceRepository.UpdateAsync(province);
        }

        public async Task ConfirmDeleteAsync(Guid id)
        {
            var districts = await _districtRepository.GetListDistrictsAsync(id);
            foreach (var district in districts)
            {
                var communes = await _communeRepository.GetListCommunesAsync(district.Id);
                await _communeRepository.DeleteManyAsync(communes);
            }
            await _districtRepository.DeleteManyAsync(districts);
            await _provinceRepository.DeleteAsync(id);
        }

        public async Task RestoreAsync(Guid id)
        {
            var province = await _provinceRepository.GetAsync(id);
            var provinceNameExist = await _provinceRepository.FindByNameAsync(province.Name);
            var provinceCodeExist = await _provinceRepository.FindByCodeAsync(province.Code);
            var provinceAreaCodeExist = await _provinceRepository.FindByAreaCodeAsync(province.AreaCode);
            if (provinceCodeExist != null)
            {
                throw new CodeAlreadyExistsException(province.Code);
            }
            if (provinceNameExist != null)
            {
                throw new NameAlreadyExistsException(province.Name);
            }
            if (provinceAreaCodeExist != null)
            {
                throw new AreaCodeAlreadyExistsException(province.AreaCode);
            }
            var districts = await _districtRepository.GetListDistrictsAsync(id);
            foreach (var district in districts)
            {
                district.Status = 2;
            }
            province.Status = 1;
            await _districtRepository.UpdateManyAsync(districts);
            await _provinceRepository.UpdateAsync(province);
        }
    }
}
