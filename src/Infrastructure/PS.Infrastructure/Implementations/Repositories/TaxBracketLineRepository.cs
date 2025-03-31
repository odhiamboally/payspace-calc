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


internal sealed class TaxBracketLineRepository : IBaseRepository<TaxBracketLine>, ITaxBracketLineRepository
{
    private readonly DBContext _context;
    private readonly DapperContext _dapper;

    public TaxBracketLineRepository(DBContext context, DapperContext dapper)
    {
        _context = context;
        _dapper = dapper;
    }

    public Task<int> BulkUpdateAsync(List<TaxCalculation> entities)
    {
        throw new NotImplementedException();
    }

    public Task<TaxBracketLine> CreateAsync(TaxBracketLine entity)
    {
        throw new NotImplementedException();
    }

    public Task<TaxBracketLine> DeleteAsync(TaxBracketLine entity)
    {
        throw new NotImplementedException();
    }

    public IQueryable<TaxBracketLine> FindAll()
    {
        throw new NotImplementedException();
    }

    public IQueryable<TaxBracketLine> FindByCondition(Expression<Func<TaxBracketLine, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public Task<TaxBracketLine?> FindByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<TaxBracketLine> GetByTaxBracketIdAsync(int taxBracketId)
    {
        throw new NotImplementedException();
    }

    public Task<TaxBracketLine> UpdateAsync(TaxBracketLine entity)
    {
        throw new NotImplementedException();
    }
}
