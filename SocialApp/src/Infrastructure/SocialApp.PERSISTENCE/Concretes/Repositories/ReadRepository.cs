using Microsoft.EntityFrameworkCore;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.DOMAIN.Models.Common;
using SocialApp.PERSISTENCE.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.PERSISTENCE.Concretes.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : BaseModel
{
    private readonly AppDbContext _context;

    public ReadRepository(AppDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public IQueryable<T> GetAll()
    {
        return Table.AsQueryable();
    }

    public IQueryable<T>? GetByCondition(Expression<Func<T,bool>> expression)
    {
        return Table.Where(expression);
    }

    public async Task<IQueryable<T>?> GetByConditionAsync(Expression<Func<T, bool>> expression)
    {
        IQueryable<T> result = Table.Where(expression);
        return result;
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await Table.SingleOrDefaultAsync(x => x.Id == id);
    }
   
}
