using PS.Application.Dtos.Common;
using PS.Application.Dtos.TaxBracket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Application.Abstractions.IServices;
public interface ITaxBracketService
{
    Task<Response<List<TaxBracketResponse>>> GetByCountryAsync(int countryId);
    Task<Response<int>> AddAsync(CreateTaxBracketRequest createTaxBracketRequest);
    Task<Response<bool>> UpdateAsync(UpdateTaxBracketRequest updateTaxBracketRequest);
    Task<Response<bool>> DeleteAsync(int id);
}
