using PS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Application.Abstractions.IRepositories;
public interface ITaxCalculationRepository : IBaseRepository<TaxCalculation>
{
    IQueryable<TaxCalculation> ADOFindAll();
    Task<int> RunTaxCalculation();
    IQueryable<TaxCalculation> GetByCountryIdAsync(int countryId);
    IQueryable<TaxCalculation> GetByIncomeRangeAsync(decimal minIncome, decimal maxIncome);
    Task<TaxCalculation> ADOUpdateSingleAsync(TaxCalculation taxCalculation);
    Task<int> SPBulkUpdateAsync(List<TaxCalculation> taxCalculations);
}
