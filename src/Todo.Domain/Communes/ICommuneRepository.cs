using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Provinces;
using Volo.Abp.Domain.Repositories;

namespace Todo.Communes
{
    public interface ICommuneRepository : IRepository<Commune, Guid>
    {
        Task<Commune> FindByNameAsync(Commune _commune);

        Task<Commune> FindByCodeAsync(Commune _commune);

        Task<List<Commune>> GetListCommunesAsync(Guid id);

    }
}
