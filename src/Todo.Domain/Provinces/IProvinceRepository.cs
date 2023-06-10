using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Todo.Provinces
{
    public interface IProvinceRepository : IRepository<Province, Guid>
    {
        Task<Province> FindByNameAsync(string name);

        Task<Province> FindByCodeAsync(string code);

        Task<Province> FindByAreaCodeAsync(string areaCode);

        Task<string> GetNameAsync(Guid id);

        Task<List<Province>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}
