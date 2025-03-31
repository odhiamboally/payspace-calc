using PS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Application.Abstractions.IRepositories;
public interface ITaxRateRepository : IBaseRepository<TaxRate>
{
    IQueryable<TaxRate> GetByCountryIdAsync(int countryId);
    Task<TaxRate> GetByRateCodeAsync(string rateCode);
}
