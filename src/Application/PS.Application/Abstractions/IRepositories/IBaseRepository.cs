using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using PS.Domain.Entities;

namespace PS.Application.Abstractions.IRepositories;

public interface IBaseRepository<T> where T : class
{
    Task<T> CreateAsync(T entity);
    Task<T> DeleteAsync(T entity);
    IQueryable<T> FindAll();
    
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
    Task<T?> FindByIdAsync(int id);
    Task<T> UpdateAsync(T entity);
    Task<int> BulkUpdateAsync(List<TaxCalculation> entities);


}
