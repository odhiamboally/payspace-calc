using Dapper;

using EFCore.BulkExtensions;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PS.Application.Abstractions.IRepositories;
using PS.Domain.Entities;
using PS.Persistence.DataContext;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

using static Dapper.SqlMapper;

namespace PS.Infrastructure.Implementations.Repositories;
internal sealed class TaxCalculationRepository : IBaseRepository<TaxCalculation>, ITaxCalculationRepository
{
    private readonly DBContext _context;
    private readonly DapperContext _dapper;
    private readonly IDbConnection _connection;

    public TaxCalculationRepository(DBContext context, DapperContext dapper)
    {
        if (string.IsNullOrEmpty(context.Database.GetDbConnection().ConnectionString))
        {
            throw new InvalidOperationException("The ConnectionString property has not been initialized. Check your configuration.");
        }

        _dapper = dapper ?? throw new ArgumentNullException(nameof(dapper));
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _connection = _dapper.CreateConnection();
    }

    public IQueryable<TaxCalculation> ADOFindAll()
    {
        using (var transaction = _connection.BeginTransaction())
        {
            try
            {
                var results = _connection.QueryAsync<TaxCalculation>(
                    "Sp_GetAllTaxCalculations",
                    transaction: transaction,
                    commandType: CommandType.StoredProcedure
                );

                transaction.Commit();

                return (IQueryable<TaxCalculation>)results;
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

    public Task<TaxCalculation> CreateAsync(TaxCalculation entity)
    {
        throw new NotImplementedException();
    }

    public Task<TaxCalculation> DeleteAsync(TaxCalculation entity)
    {
        throw new NotImplementedException();
    }

    public IQueryable<TaxCalculation> FindAll()
    {
        return _context!.TaxCalculations!
            .Where(s => 
            s.CalculatedTax == null || s.CalculatedTax == 0 && 
            s.NetPay == null || s.NetPay == 0)
            .OrderBy(e => e.Id)
            .AsNoTracking();
    }

    public IQueryable<TaxCalculation> FindByCondition(Expression<Func<TaxCalculation, bool>> expression)
    {
        return _context!.TaxCalculations!.Where(expression).AsNoTracking();
    }

    public async Task<TaxCalculation?> FindByIdAsync(int id)
    {
        return await _context!.TaxCalculations!.FindAsync(id);
    }

    public IQueryable<TaxCalculation> GetByCountryIdAsync(int countryId)
    {
        throw new NotImplementedException();
    }
     
    public IQueryable<TaxCalculation> GetByIncomeRangeAsync(decimal minIncome, decimal maxIncome)
    {
        throw new NotImplementedException();
    }

    public async Task<TaxCalculation> UpdateAsync(TaxCalculation entity)
    {

        _context.Entry(entity).State = EntityState.Modified; // Mark the entity as modified
        await Task.CompletedTask;
        return entity;
    }

    public async Task<int> BulkUpdateAsync(List<TaxCalculation> entities)
    {
        await _context.BulkUpdateAsync(entities);
        var entityIds = entities.Select(e => e.Id).ToList();
        var updatedCount = await _context.TaxCalculations!.CountAsync(tc => entityIds.Contains(tc.Id));
        return updatedCount;
    }

    public async Task<TaxCalculation> ADOUpdateSingleAsync(TaxCalculation entity)
    {

        var parameters = new DynamicParameters();
        parameters.Add("@Id", entity.Id, DbType.Int32);
        parameters.Add("@CalculatedTax", entity.CalculatedTax, DbType.Decimal);
        parameters.Add("@NetPay", entity.NetPay, DbType.Decimal);
        parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
        parameters.Add("@ErrorMessage", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

        string errorMessage = string.Empty;
        bool isSuccess = false;
        using var sqlConnection = (SqlConnection)_connection;
        try
        {
            await sqlConnection.OpenAsync();
            using var transaction = _connection.BeginTransaction();
            try
            {
                await sqlConnection.ExecuteAsync(
                    "Sp_UpdateTaxCalculation",
                    parameters,
                    transaction: transaction,
                    commandType: CommandType.StoredProcedure
                );

                isSuccess = parameters.Get<bool>("@Success");
                errorMessage = parameters.Get<string>("@ErrorMessage");

                if (!isSuccess)
                {
                    throw new Exception($"Updating Tax Calculation failed: {errorMessage}");
                }

                transaction.Commit();
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
        catch (Exception)
        {

            throw;
        }

        return new TaxCalculation 
        { 
            Id = entity.Id,
            CountryId = entity.CountryId,
            Income = entity.Income,
            CalculatedTax = entity.CalculatedTax,
            NetPay = entity.NetPay

        };
    }

    public async Task<int> RunTaxCalculation()
    {
        string errorMessage = string.Empty;
        bool isSuccess = false;
        int processcount = 0;

        var parameters = new DynamicParameters();
        parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
        parameters.Add("@ErrorMessage", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
        parameters.Add("@ProcessedCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

        using var sqlConnection = (SqlConnection)_connection;
        try
        {
            await sqlConnection.OpenAsync();
            using var transaction = await sqlConnection.BeginTransactionAsync();
            try
            {
                // Execute the stored procedure with Dapper
                await sqlConnection.ExecuteAsync(
                    "Sp_TaxCalculation",
                    parameters,
                    transaction: transaction,
                    commandType: CommandType.StoredProcedure
                );

                isSuccess = parameters.Get<bool>("@Success");
                errorMessage = parameters.Get<string>("@ErrorMessage");
                processcount = parameters.Get<int>("@ProcessedCount");

                await transaction.CommitAsync();
            }
            catch (SqlException ex)
            {
                errorMessage = $"SQL Exception: {ex.Message}";
                isSuccess = false;
                processcount = 0;
                await transaction.RollbackAsync();
                return processcount;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                isSuccess = false;
                processcount = 0;
                return processcount;
            }
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;
            isSuccess = false;
        }

        return processcount;
        
    }

    public async Task<int> SPBulkUpdateAsync(List<TaxCalculation> taxCalculations)
    {
        var taxCalculationTable = new DataTable();
        taxCalculationTable.Columns.Add("Id", typeof(int));
        taxCalculationTable.Columns.Add("CountryId", typeof(int));
        taxCalculationTable.Columns.Add("Income", typeof(decimal));
        taxCalculationTable.Columns.Add("CalculatedTax", typeof(decimal));
        taxCalculationTable.Columns.Add("NetPay", typeof(decimal));

        // Add each TaxCalculation to the DataTable
        foreach (var calculation in taxCalculations)
        {
            taxCalculationTable.Rows.Add(calculation.Id, calculation.CountryId, calculation.Income, calculation.CalculatedTax, calculation.NetPay);
        }

        // Create a parameter for the TVP
        var tvpParameter = new SqlParameter
        {
            ParameterName = "@TaxCalculations",
            SqlDbType = SqlDbType.Structured,
            Value = taxCalculationTable,
            TypeName = "dbo.TaxCalculationType"  // The name of the TVP type
        };

        var resultParameter = new SqlParameter
        {
            ParameterName = "@Result",
            SqlDbType = SqlDbType.Int,
            Direction = ParameterDirection.Output
        };

        return await _context.Database.ExecuteSqlRawAsync("EXEC SP_UpdateTaxCalculations @TaxCalculations", tvpParameter);

    }


}
