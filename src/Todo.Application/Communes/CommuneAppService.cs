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
using Todo.Districts;
using Volo.Abp.ObjectMapping;
using static Todo.Permissions.TodoPermissions;

namespace Todo.Communes
{
    [Authorize(TodoPermissions.Communes.Default)]
    public class CommuneAppService : TodoAppService, ICommuneAppService
    {
        private readonly ICommuneRepository _communeRepository;
        private readonly IDistrictRepository _districtRepository;
        private readonly IProvinceRepository _provinceRepository;

        public CommuneAppService(ICommuneRepository communeRepository, IDistrictRepository districtRepository, IProvinceRepository provinceRepository)
        {
            _communeRepository = communeRepository;
            _districtRepository = districtRepository;
            _provinceRepository = provinceRepository;
        }
        public async Task<CommuneDto> GetAsync(Guid id)
        {

            var commune = await _communeRepository.GetAsync(id);
            var result = ObjectMapper.Map<Commune, CommuneDto>(commune);
            var district = await _districtRepository.GetAsync(result.DistrictId);
            result.DistrictName = district.Name;
            result.ProvinceName = await _provinceRepository.GetNameAsync(district.ProvinceId);
            return result;
        }

        public async Task<PagedResultDto<CommuneDto>> GetListAsync()
        {

            var communes = await _communeRepository.GetListAsync();
            var filteredCommunes = communes.Where(p => p.Status < 2).ToList();
            var totalCount = filteredCommunes.Count();
            var list = ObjectMapper.Map<List<Commune>, List<CommuneDto>>(filteredCommunes);
            foreach (var commune in list)
            {
                var district = await _districtRepository.GetAsync(commune.DistrictId);
                commune.DistrictName = district.Name;
                commune.ProvinceName = await _provinceRepository.GetNameAsync(district.ProvinceId);
            }
            return new PagedResultDto<CommuneDto>(
                totalCount,
                list
            );
        }

        [Authorize(TodoPermissions.Communes.Create)]
        public async Task<CommuneDto> CreateAsync(CreateUpdateCommuneDto input)
        {
            var commune = ObjectMapper.Map<CreateUpdateCommuneDto, Commune>(input);
            var communeNameExist = await _communeRepository.FindByNameAsync(commune);
            var communeCodeExist = await _communeRepository.FindByCodeAsync(commune);
            if (communeCodeExist != null)
            {
                throw new CommuneCodeAlreadyExistsException(commune.Code);
            }
            if (communeNameExist != null)
            {
                throw new CommuneNameAlreadyExistsException(commune.Name);
            }

            await _communeRepository.InsertAsync(commune);

            return ObjectMapper.Map<Commune, CommuneDto>(commune);
        }

        [Authorize(TodoPermissions.Communes.Edit)]
        public async Task UpdateAsync(Guid id, CreateUpdateCommuneDto input)
        {
            var commune = await _communeRepository.GetAsync(id);
            commune.Name = input.Name;
            commune.Code = input.Code;
            commune.DistrictId = input.DistrictId;
            commune.Status = 1;
            commune.Description = input.Description;
            commune.OrderBy = input.OrderBy;
            var communeNameExist = await _communeRepository.FindByNameAsync(commune);
            var communeCodeExist = await _communeRepository.FindByCodeAsync(commune);
            if (communeCodeExist != null && communeCodeExist.Id != commune.Id)
            {
                throw new CommuneCodeAlreadyExistsException(commune.Code);
            }
            if (communeNameExist != null && communeNameExist.Id != commune.Id)
            {
                throw new CommuneNameAlreadyExistsException(commune.Name);
            }
            await _communeRepository.UpdateAsync(commune);
        }

        [Authorize(TodoPermissions.Communes.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            var commune = await _communeRepository.GetAsync(id);
            commune.Status = 2;
            await _communeRepository.UpdateAsync(commune);
        }

        public async Task<ListResultDto<ProvinceLookupDto>> GetProvinceLookupAsync()
        {
            var provinces = await _provinceRepository.GetListAsync();
            var filter = provinces.Where(x => x.Status < 2).ToList();
            var provinceDtos = ObjectMapper.Map<List<Province>, List<ProvinceLookupDto>>(filter);
            foreach(var province in provinceDtos)
            {
                var districts = await _districtRepository.GetListAsync();
                var filterDistrict = districts.Where(x => x.Status < 2 && province.Id == x.ProvinceId).ToList();
                var districtDtos = ObjectMapper.Map<List<District>, List<DistrictLookupDto>>(filterDistrict);
                province.Districts = new ListResultDto<DistrictLookupDto>(districtDtos);
            }  
            return new ListResultDto<ProvinceLookupDto>(provinceDtos);
        }

        public async Task<ListResultDto<DistrictLookupDto>> GetDistrictLookupAsync()
        {
            var districts = await _districtRepository.GetListAsync();
            var filter = districts.Where(x => x.Status < 2).ToList();
            var districtDtos = ObjectMapper.Map<List<District>, List<DistrictLookupDto>>(filter);
            return new ListResultDto<DistrictLookupDto>(districtDtos);
        }

        public async Task<PagedResultDto<CommuneDto>> GetListDeletedAsync()
        {

            var communes = await _communeRepository.GetListAsync();
            var filteredCommunes = communes.Where(p => p.Status == 2).ToList();
            var totalCount = filteredCommunes.Count();
            var list = ObjectMapper.Map<List<Commune>, List<CommuneDto>>(filteredCommunes);
            foreach (var commune in list)
            {
                var district = await _districtRepository.GetAsync(commune.DistrictId);
                commune.DistrictName = district.Name;
                commune.ProvinceName = await _provinceRepository.GetNameAsync(district.ProvinceId);
            }
            return new PagedResultDto<CommuneDto>(
                totalCount,
                list
            );
        }

        public async Task ConfirmDeleteAsync(Guid id)
        {
            await _communeRepository.DeleteAsync(id);
        }

        public async Task RestoreAsync(Guid id)
        {
            var commnue = await _communeRepository.GetAsync(id);
            var communeNameExist = await _communeRepository.FindByNameAsync(commnue);
            var communeCodeExist = await _communeRepository.FindByCodeAsync(commnue);
            if (communeCodeExist != null)
            {
                throw new CommuneCodeAlreadyExistsException(communeCodeExist.Code);
            }
            if (communeNameExist != null)
            {
                throw new CommuneNameAlreadyExistsException(communeNameExist.Name);
            }
            commnue.Status = 1;
            await _communeRepository.UpdateAsync(commnue);
        }
    }
}
