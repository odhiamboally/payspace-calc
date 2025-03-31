using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PS.Application.Abstractions.IRepositories;
using PS.Domain.Entities;
using PS.Persistence.DataContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PS.Infrastructure.Implementations.Repositories;
internal sealed class CountryRepository : IBaseRepository<Country>, ICountryRepository
{
    private readonly DBContext _context;
    private readonly DapperContext _dapper;
    private readonly IDbConnection _connection;


    public CountryRepository(DBContext context, DapperContext dapper)
    {
        if (string.IsNullOrEmpty(context.Database.GetDbConnection().ConnectionString))
        {
            throw new InvalidOperationException("The ConnectionString property has not been initialized. Check your configuration.");
        }

        _dapper = dapper ?? throw new ArgumentNullException(nameof(dapper));
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _connection = _dapper.CreateConnection();
    }

    public Task<Country> CreateAsync(Country entity)
    {
        throw new NotImplementedException();
    }

    public Task<Country> DeleteAsync(Country entity)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Country> FindAll()
    {
        throw new NotImplementedException();
    }

    public IQueryable<Country> FindByCondition(Expression<Func<Country, bool>> expression)
    {
        return _context!.Countries!.Where(expression).AsNoTracking();
    }

    public async Task<Country?> FindByIdAsync(int id)
    {
        return await _context!.Countries!.FindAsync(id);
    }

    public async Task<Country?> FindByIdWithRelatedDataAsync(int id)
    {
        return await _context.Countries!
            .Include(c => c.TaxBrackets)
            .Include(c => c.TaxRates)
            .Include(c => c.TaxCalculations)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public Task<Country?> ADOFindByIdAsync(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id, DbType.Int32);

        using (var transaction = _connection.BeginTransaction())
        {
            try
            {
                var results = _connection.QueryFirstOrDefaultAsync<Country>(
                    "Sp_GetCountry",
                    transaction: transaction,
                    commandType: CommandType.StoredProcedure
                );

                transaction.Commit();

                return results;
            }
            catch (SqlException)
            {
                transaction.Rollback();
                throw;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }

    public Task<Country> UpdateAsync(Country entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> BulkUpdateAsync(List<TaxCalculation> entities)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Country> FindWithRelatedDatasAsync(List<int> countryIds)
    {
        var countries = _context.Countries!
            .Where(c => countryIds.Contains(c.Id)) // Filter by the list of countryIds
            .Include(c => c.TaxRates)
            .Include(c => c.TaxBrackets)
                .ThenInclude(tb => tb.TaxBracketLines);
            //.Include(c => c.TaxCalculations);
            
        return countries;
    }

    public IQueryable<Country> FindWithRelatedDatasAsync()
    {
        var countries = _context.Countries!
            .Include(c => c.TaxRates)
            .Include(c => c.TaxBrackets)
                .ThenInclude(tb => tb.TaxBracketLines)
            .Include(c => c.TaxCalculations);

        return countries;
    }
}
