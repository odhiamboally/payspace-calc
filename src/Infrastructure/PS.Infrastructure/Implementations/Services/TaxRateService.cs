using PS.Application.Abstractions.IServices;
using PS.Application.Dtos.Common;
using PS.Application.Dtos.TaxRate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Infrastructure.Implementations.Services;


internal sealed class TaxRateService : ITaxRateService
{
    public TaxRateService()
    {
            
    }

    public Task<Response<int>> CreateAsync(CreateTaxRateRequest createTaxRateRequest)
    {
        throw new NotImplementedException();
    }

    public Task<Response<bool>> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Response<List<TaxRateResponse>>> GetByCountryIdAsync(int countryId)
    {
        throw new NotImplementedException();
    }

    public Task<Response<bool>> UpdateAsync(int id, UpdateTaxRateRequest updateTaxRateRequest)
    {
        throw new NotImplementedException();
    }
}
