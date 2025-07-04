using Microsoft.EntityFrameworkCore;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.DOMAIN.Models.Common;
using SocialApp.PERSISTENCE.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.PERSISTENCE.Concretes.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T : BaseModel
{
    private readonly AppDbContext _context;
    public DbSet<T> Table => _context.Set<T>();

    public WriteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task SaveAsync() { await _context.SaveChangesAsync(); }

    public async Task CreateAsync(T t)
    {
        await Table.AddAsync(t);
        await SaveAsync();
    }

    public async Task DeleteAsync(T t)
    {
        Table.Remove(t);
        await SaveAsync();
    }

    public async Task UpdateAsync(T t)
    {
        Table.Update(t);
        await SaveAsync();
    }

    public async Task SoftDeleteAsync(T t)
    {
        t.IsDeleted = true;
        await SaveAsync();
    }
}
