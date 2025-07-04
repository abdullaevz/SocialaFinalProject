using SocialApp.DOMAIN.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Abstractions.Repositories;

public interface IReadRepository<T>:IGenericRepository<T> where T : BaseModel
{
    IQueryable<T> GetAll();
    Task<T?> GetByIdAsync(int id);
    IQueryable<T>? GetByCondition(Expression<Func<T, bool>> expression);
    Task<IQueryable<T>?> GetByConditionAsync(Expression<Func<T, bool>> expression);
}
