using Microsoft.EntityFrameworkCore;
using SocialApp.DOMAIN.Models.Common;

namespace SocialApp.APPLICATION.Abstractions.Repositories
{
    public interface IGenericRepository<T> where T : BaseModel
    {
       DbSet<T> Table { get; }
    }
}