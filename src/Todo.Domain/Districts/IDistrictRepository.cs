using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Provinces;
using Volo.Abp.Domain.Repositories;

namespace Todo.Districts
{
    public interface IDistrictRepository : IRepository<District, Guid>
    {
        Task<District> FindByNameAsync(District _district);

        Task<District> FindByCodeAsync(District _district);

        Task<List<District>> GetListDistrictsAsync(Guid id);

    }
}
