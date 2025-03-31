using PS.Application.Abstractions.IRepositories;
using PS.Domain.Entities;
using PS.Persistence.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PS.Infrastructure.Implementations.Repositories;
internal sealed class TaxRateRepository : IBaseRepository<TaxRate>, ITaxRateRepository
{
    private readonly DBContext _context;
    private readonly DapperContext _dapper;

    public TaxRateRepository(DBContext context, DapperContext dapper)
    {
        _context = context;
        _dapper = dapper;
    }

    public Task<int> BulkUpdateAsync(List<TaxCalculation> entities)
    {
        throw new NotImplementedException();
    }

    public Task<TaxRate> CreateAsync(TaxRate entity)
    {
        throw new NotImplementedException();
    }

    public Task<TaxRate> DeleteAsync(TaxRate entity)
    {
        throw new NotImplementedException();
    }

    public IQueryable<TaxRate> FindAll()
    {
        throw new NotImplementedException();
    }

    public IQueryable<TaxRate> FindByCondition(Expression<Func<TaxRate, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public Task<TaxRate?> FindByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<TaxRate> GetByCountryIdAsync(int countryId)
    {
        throw new NotImplementedException();
    }

    public Task<TaxRate> GetByRateCodeAsync(string rateCode)
    {
        throw new NotImplementedException();
    }

    public Task<TaxRate> UpdateAsync(TaxRate entity)
    {
        throw new NotImplementedException();
    }
}
