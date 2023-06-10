using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Todo.Provinces;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Todo.Districts
{
    public interface IDistrictAppService : IApplicationService
    
    {
        Task<DistrictDto> GetAsync(Guid id);

        Task<PagedResultDto<DistrictDto>> GetListAsync();

        Task<DistrictDto> CreateAsync(CreateUpdateDistrictDto input);

        Task UpdateAsync(Guid id, CreateUpdateDistrictDto input);

        Task DeleteAsync(Guid id);

        Task<ListResultDto<ProvinceLookupDto>> GetProvinceLookupAsync();

        Task<PagedResultDto<DistrictDto>> GetListDeletedAsync();

        Task ConfirmDeleteAsync(Guid id);

        Task RestoreAsync(Guid id);
    }
}
