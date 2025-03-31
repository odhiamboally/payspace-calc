using PS.Application.Abstractions.IServices;
using PS.Application.Dtos.Common;
using PS.Application.Dtos.TaxBracket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Infrastructure.Implementations.Services;


internal sealed class TaxBracketService : ITaxBracketService
{
    public TaxBracketService()
    {
            
    }

    public Task<Response<int>> AddAsync(CreateTaxBracketRequest createTaxBracketRequest)
    {
        throw new NotImplementedException();
    }

    public Task<Response<bool>> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Response<List<TaxBracketResponse>>> GetByCountryAsync(int countryId)
    {
        throw new NotImplementedException();
    }

    public Task<Response<bool>> UpdateAsync(UpdateTaxBracketRequest updateTaxBracketRequest)
    {
        throw new NotImplementedException();
    }
}
