using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Todo.Provinces
{
    public interface IProvinceAppService : IApplicationService
    
    {
        Task<ProvinceDto> GetAsync(Guid id);

        Task<PagedResultDto<ProvinceDto>> GetListAsync(GetProvinceListDto input);

        Task<ProvinceDto> CreateAsync(CreateUpdateProvinceDto input);

        Task UpdateAsync(Guid id, CreateUpdateProvinceDto input);

        Task DeleteAsync(Guid id);

        Task<PagedResultDto<ProvinceDto>> GetListDeletedAsync(GetProvinceListDto input);

        Task ConfirmDeleteAsync(Guid id);

        Task RestoreAsync(Guid id);
    }
}
