using PS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Application.Abstractions.IRepositories;


public interface ITaxBracketRepository : IBaseRepository<TaxBracket>
{
    IQueryable<TaxBracket> GetByCountryIdAsync(int countryId);
    Task<TaxBracket> GetWithTaxBracketLinesAsync(int id);

}
