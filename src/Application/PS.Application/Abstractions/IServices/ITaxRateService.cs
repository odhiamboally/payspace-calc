using PS.Application.Dtos.Common;
using PS.Application.Dtos.TaxRate;
using PS.Domain.Entities;

namespace PS.Application.Abstractions.IServices;

public interface ITaxRateService
{
    Task<Response<int>> CreateAsync(CreateTaxRateRequest createTaxRateRequest);
    Task<Response<bool>> DeleteAsync(int id);
    Task<Response<List<TaxRateResponse>>> GetByCountryIdAsync(int countryId);
    Task<Response<bool>> UpdateAsync(int id, UpdateTaxRateRequest updateTaxRateRequest);
}