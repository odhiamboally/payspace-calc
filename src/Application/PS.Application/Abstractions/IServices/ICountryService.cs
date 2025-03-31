using PS.Application.Dtos.Common;
using PS.Application.Dtos.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Application.Abstractions.IServices;
public interface ICountryService
{
    Task<Response<List<CountryResponse>>> GetAllAsync();
    Task<Response<CountryResponse>> GetByIdAsync(int id);
    Task<Response<int>> AddAsync(CreateCountryRequest createCountryRequest);
    Task<Response<bool>> UpdateAsync(UpdateCountryRequest updateCountryRequest);
    Task<Response<bool>> DeleteAsync(int id);
}
