using Microsoft.AspNetCore.Identity;
using SocialApp.DOMAIN.Models.Common;
using SocialApp.DOMAIN.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Abstractions.Repositories;

public interface IWriteRepository<T> : IGenericRepository<T> where T : BaseModel
{
    Task CreateAsync(T t);
    Task DeleteAsync(T t);
    Task SoftDeleteAsync(T t);
    Task UpdateAsync(T t);
    Task SaveAsync();
}
