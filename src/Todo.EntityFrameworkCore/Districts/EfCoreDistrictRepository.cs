using System;
using System.Threading.Tasks;
using Todo.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace Todo.Districts;

public class EfCoreDistrictRepository
    : EfCoreRepository<TodoDbContext, District, Guid>,
        IDistrictRepository
{
    public EfCoreDistrictRepository(
        IDbContextProvider<TodoDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<District> FindByNameAsync(District _district)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(district => district.Name == _district.Name && district.ProvinceId == _district.ProvinceId && district.Status < 2);
    }

    public async Task<District> FindByCodeAsync(District _district)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(district => district.Code == _district.Code && district.Status < 2);
    }

    public async Task<List<District>> GetListDistrictsAsync(Guid id)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.Where(x => x.ProvinceId == id).ToListAsync();
    }
}