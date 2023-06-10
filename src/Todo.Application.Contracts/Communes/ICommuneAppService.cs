using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Todo.Districts;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Todo.Communes
{
    public interface ICommuneAppService : IApplicationService

    {
        Task<CommuneDto> GetAsync(Guid id);

        Task<PagedResultDto<CommuneDto>> GetListAsync();

        Task<CommuneDto> CreateAsync(CreateUpdateCommuneDto input);

        Task UpdateAsync(Guid id, CreateUpdateCommuneDto input);

        Task DeleteAsync(Guid id);

        Task<ListResultDto<ProvinceLookupDto>> GetProvinceLookupAsync();

        Task<ListResultDto<DistrictLookupDto>> GetDistrictLookupAsync();

        Task<PagedResultDto<CommuneDto>> GetListDeletedAsync();

        Task ConfirmDeleteAsync(Guid id);

        Task RestoreAsync(Guid id);
    }
}
