using Microsoft.EntityFrameworkCore;

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


internal sealed class TaxBracketRepository : IBaseRepository<TaxBracket>, ITaxBracketRepository
{
    private readonly DBContext _context;
    private readonly DapperContext _dapper;


    public TaxBracketRepository(DBContext context, DapperContext dapper)
    {
        _context = context;
        _dapper = dapper;
    }

    public Task<int> BulkUpdateAsync(List<TaxCalculation> entities)
    {
        throw new NotImplementedException();
    }

    public Task<TaxBracket> CreateAsync(TaxBracket entity)
    {
        throw new NotImplementedException();
    }

    public Task<TaxBracket> DeleteAsync(TaxBracket entity)
    {
        throw new NotImplementedException();
    }

    public IQueryable<TaxBracket> FindAll()
    {
        throw new NotImplementedException();
    }

    public IQueryable<TaxBracket> FindByCondition(Expression<Func<TaxBracket, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public Task<TaxBracket?> FindByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<TaxBracket> GetByCountryIdAsync(int countryId)
    {
        throw new NotImplementedException();
    }

    public async Task<TaxBracket> GetWithTaxBracketLinesAsync(int id)
    {
        return await _context.TaxBrackets!.Include(c => c.TaxBracketLines).FirstOrDefaultAsync(c => c.Id == id)!;
    }

    public Task<TaxBracket> UpdateAsync(TaxBracket entity)
    {
        throw new NotImplementedException();
    }

    
}
