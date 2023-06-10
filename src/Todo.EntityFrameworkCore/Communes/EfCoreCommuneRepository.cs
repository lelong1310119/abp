using System;
using System.Threading.Tasks;
using Todo.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace Todo.Communes;

public class EfCoreCommuneRepository
    : EfCoreRepository<TodoDbContext, Commune, Guid>,
        ICommuneRepository
{
    public EfCoreCommuneRepository(
        IDbContextProvider<TodoDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<Commune> FindByNameAsync(Commune _commune)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(commune => commune.Name == _commune.Name && commune.DistrictId == _commune.DistrictId && commune.Status < 2);
    }

    public async Task<Commune> FindByCodeAsync(Commune _commune)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(commune => commune.Code == _commune.Code && commune.Status < 2);
    }

    public async Task<List<Commune>> GetListCommunesAsync(Guid id)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.Where(x => x.DistrictId == id).ToListAsync();
    }
}