using System;
using System.Threading.Tasks;
using Todo.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;

namespace Todo.Provinces;

public class EfCoreProvinceRepository
    : EfCoreRepository<TodoDbContext, Province, Guid>,
        IProvinceRepository
{
    public EfCoreProvinceRepository(
        IDbContextProvider<TodoDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<Province> FindByNameAsync(string name)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(province => province.Name == name && province.Status < 2);
    }

    public async Task<Province> FindByCodeAsync(string code)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(province => province.Code == code && province.Status < 2);
    }

    public async Task<Province> FindByAreaCodeAsync(string areaCode)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(province => province.AreaCode == areaCode && province.Status < 2);
    }

    public async Task<string> GetNameAsync(Guid id)
    {
        var db = await GetDbSetAsync();
        var province = await db.FirstOrDefaultAsync(x => x.Id == id);
        return province.Name;
    }

    public async Task<List<Province>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet
            .WhereIf(!filter.IsNullOrWhiteSpace(),
                province => province.Name.Contains(filter))
            .OrderBy(sorting)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }
}