using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Permissions;
using Volo.Abp.Account;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Todo.Provinces;
using Todo.Communes;

namespace Todo.Districts
{
    [Authorize(TodoPermissions.Districts.Default)]
    public class DistrictAppService : TodoAppService, IDistrictAppService
    {
        private readonly IDistrictRepository _districtRepository;
        private readonly ICommuneRepository _communeRepository;
        private readonly IProvinceRepository _provinceRepository;

        public DistrictAppService(IDistrictRepository districtRepository , IProvinceRepository provinceRepository, ICommuneRepository communeRepository)
        {
            _districtRepository = districtRepository;
            _provinceRepository = provinceRepository;
            _communeRepository = communeRepository;
        }
        public async Task<DistrictDto> GetAsync(Guid id)
        {

            var district = await _districtRepository.GetAsync(id);
            var result = ObjectMapper.Map<District, DistrictDto>(district);
            result.ProvinceName = await _provinceRepository.GetNameAsync(district.ProvinceId);
            return result;
        }

        public async Task<PagedResultDto<DistrictDto>> GetListAsync()
        {

            var districts = await _districtRepository.GetListAsync();
            var filteredDistricts = districts.Where(p => p.Status < 2).ToList();
            var totalCount = filteredDistricts.Count();
            var list = ObjectMapper.Map<List<District>, List<DistrictDto>>(filteredDistricts);
            foreach( var district in list ) {
                district.ProvinceName = await _provinceRepository.GetNameAsync(district.ProvinceId);
            }

            return new PagedResultDto<DistrictDto>(
                totalCount,
                list   
            );
        }

        [Authorize(TodoPermissions.Districts.Create)]
        public async Task<DistrictDto> CreateAsync(CreateUpdateDistrictDto input)
        {
            var district = ObjectMapper.Map<CreateUpdateDistrictDto, District>(input);
            var districtNameExist = await _districtRepository.FindByNameAsync(district);
            var districtCodeExist = await _districtRepository.FindByCodeAsync(district);
            if (districtCodeExist != null)
            {
                throw new DistrictCodeAlreadyExistsException(district.Code);
            }
            if (districtNameExist != null)
            {
                throw new DistrictNameAlreadyExistsException(district.Name);
            }

            await _districtRepository.InsertAsync(district);

            return ObjectMapper.Map<District, DistrictDto>(district);
        }

        [Authorize(TodoPermissions.Districts.Edit)]
        public async Task UpdateAsync(Guid id, CreateUpdateDistrictDto input)
        {
            var district = await _districtRepository.GetAsync(id);
            district.Name = input.Name;
            district.Code = input.Code;
            district.ProvinceId = input.ProvinceId;
            district.Status = 1;
            district.Description = input.Description;
            district.OrderBy = input.OrderBy;
            var districtNameExist = await _districtRepository.FindByNameAsync(district);
            var districtCodeExist = await _districtRepository.FindByCodeAsync(district);
            if (districtCodeExist != null && districtCodeExist.Id != district.Id)
            {
                throw new DistrictCodeAlreadyExistsException(district.Code);
            }
            if (districtNameExist != null && districtNameExist.Id != district.Id)
            {
                throw new DistrictNameAlreadyExistsException(district.Name);
            }
            await _districtRepository.UpdateAsync(district);
        }

        [Authorize(TodoPermissions.Districts.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            var district = await _districtRepository.GetAsync(id);
            var communes = await _communeRepository.GetListCommunesAsync(id);
            foreach (var commune in communes)
            {
                commune.Status = 3;
            }
            district.Status = 2;
            await _communeRepository.UpdateManyAsync(communes);
            await _districtRepository.UpdateAsync(district);
        }

        public async Task<ListResultDto<ProvinceLookupDto>> GetProvinceLookupAsync()
        {
            var provinces = await _provinceRepository.GetListAsync();
            var filter = provinces.Where(x => x.Status < 2).ToList();
            var provinceDtos = ObjectMapper.Map<List<Province>, List<ProvinceLookupDto>>(filter);
            return new ListResultDto<ProvinceLookupDto>(provinceDtos);
        }

        public async Task<PagedResultDto<DistrictDto>> GetListDeletedAsync()
        {

            var districts = await _districtRepository.GetListAsync();
            var filteredDistricts = districts.Where(p => p.Status == 2).ToList();
            var totalCount = filteredDistricts.Count();
            var list = ObjectMapper.Map<List<District>, List<DistrictDto>>(filteredDistricts);
            foreach (var district in list)
            {
                district.ProvinceName = await _provinceRepository.GetNameAsync(district.ProvinceId);
            }

            return new PagedResultDto<DistrictDto>(
                totalCount,
                list
            );
        }

        public async Task ConfirmDeleteAsync(Guid id)
        {
            var communes = await _communeRepository.GetListCommunesAsync(id);
            await _communeRepository.DeleteManyAsync(communes);
            await _districtRepository.DeleteAsync(id);
        }

        public async Task RestoreAsync(Guid id)
        {
            var district = await _districtRepository.GetAsync(id);
            var districtNameExist = await _districtRepository.FindByNameAsync(district);
            var districtCodeExist = await _districtRepository.FindByCodeAsync(district);
            if (districtCodeExist != null)
            {
                throw new DistrictCodeAlreadyExistsException(districtCodeExist.Code);
            }
            if (districtNameExist != null)
            {
                throw new DistrictNameAlreadyExistsException(districtNameExist.Name);
            }
            var communes = await _communeRepository.GetListCommunesAsync(id);
            foreach (var commune in communes)
            {
                commune.Status = 2;
            }
            district.Status = 1;
            await _communeRepository.UpdateManyAsync(communes);
            await _districtRepository.UpdateAsync(district);
        }
    }
}
