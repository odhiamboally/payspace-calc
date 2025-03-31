using AutoMapper;

using Microsoft.EntityFrameworkCore;

using PS.Application.Abstractions.ICaching;
using PS.Application.Abstractions.Interfaces;
using PS.Application.Abstractions.IServices;
using PS.Application.Dtos.Common;
using PS.Application.Dtos.Country;
using PS.Domain.Entities;
using PS.Infrastructure.Implementations.Interfaces;
using PS.Infrastructure.Utilities;
using PS.Persistence.Migrations;
using PS.Shared.Exceptions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static StackExchange.Redis.Role;

namespace PS.Infrastructure.Implementations.Services;
internal sealed class CountryService : ICountryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;


    public CountryService(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    public Task<Response<int>> AddAsync(CreateCountryRequest createCountryRequest)
    {
        throw new NotImplementedException();
    }

    public Task<Response<bool>> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<List<CountryResponse>>> GetAllAsync()
    {
        try
        {
            var cacheKey = CacheKeyGenerator.GenerateCacheKeyForCursorPage(nameof(Country), 0);
            var cachedCountries = await _cacheService.GetAsync<List<CountryResponse>>(cacheKey);
            if (cachedCountries != null && cachedCountries.Any())
            {
                return Response<List<CountryResponse>>.Success($"{cachedCountries.Count} cached records fetched.", cachedCountries);
            }

            var taxCalculations = await _unitOfWork.CountryRepository!.FindAll().ToListAsync();
            if (!taxCalculations.Any())
                return Response<List<CountryResponse>>.Failure($"No records found. {nameof(TaxCalculation)}");

            var responseList = taxCalculations.Select(response => new CountryResponse
            {
                Id = response.Id,
                Description = response.Description,
                Code = response.Code,
                TaxRegime = response.TaxRegime
            }).ToList();

            // ToDo: Omitted this for unit tesp purposes - See if possible to set this up in test.
            await _cacheService.SetAsync(cacheKey, responseList, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5)); 

            return Response<List<CountryResponse>>.Success($"{responseList.Count} records fetched.", responseList);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<Response<CountryResponse>> GetByIdAsync(int id)
    {
        try
        {
            var cacheKey = CacheKeyGenerator.GenerateCacheKeyForEntity(nameof(Country), 0);
            var cachedCountryResponse = await _cacheService.GetAsync<CountryResponse>(cacheKey);
            if (cachedCountryResponse != null)
            {
                return Response<CountryResponse>.Success($"Cached {cachedCountryResponse} fetched.", cachedCountryResponse);
            }

            var countryResponse = await _unitOfWork.CountryRepository!.FindByCondition(e => e.Id == id).FirstOrDefaultAsync();
            if (countryResponse == null)
                return Response<CountryResponse>.Failure($"No records found. {nameof(Country)}");

            var response = new CountryResponse
            {
                Id = countryResponse.Id,
                Description = countryResponse.Description,
                Code = countryResponse.Code,
                TaxRegime = countryResponse.TaxRegime
                
            };

            await _cacheService.SetAsync(cacheKey, response, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5));

            return Response<CountryResponse>.Success($"Records fetched.", response);

        }
        catch (Exception)
        {
            throw;
        }
    }

    public Task<Response<bool>> UpdateAsync(UpdateCountryRequest updateCountryRequest)
    {
        throw new NotImplementedException();
    }
}
