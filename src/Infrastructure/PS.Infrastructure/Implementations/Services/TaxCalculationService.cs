using AutoMapper;
using Azure.Core;

using Microsoft.EntityFrameworkCore;

using PS.Application.Abstractions.ICaching;
using PS.Application.Abstractions.Interfaces;
using PS.Application.Abstractions.IRepositories;
using PS.Application.Abstractions.IServices;
using PS.Application.Dtos.Common;
using PS.Application.Dtos.Country;
using PS.Application.Dtos.TaxCalculation;
using PS.Domain.Entities;
using PS.Infrastructure.Configurations.MappingProfiles.TaxCalculations;
using PS.Infrastructure.Implementations.Interfaces;
using PS.Infrastructure.Implementations.Repositories;
using PS.Infrastructure.Utilities;
using PS.Shared.Exceptions;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Infrastructure.Implementations.Services;


internal sealed class TaxCalculationService : ITaxCalculationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;

    public TaxCalculationService(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    public async Task<Response<List<TaxCalculationResponse>>> CalculateTaxAsync(bool calculate)
    {
        try
        {
            List<TaxCalculationResponse> taxCalculationResponses = [];
            List<TaxCalculation> taxCalculations = [];
            Dictionary<int, Country> countryDict = [];

            taxCalculations = await _unitOfWork.TaxCalculationRepository!.FindAll().ToListAsync();

            // Fetch all countries and their related data in a single query
            var countryIds = taxCalculations.Select(t => t.CountryId).Distinct().ToList();
            var countries = await _unitOfWork.CountryRepository!.FindWithRelatedDatasAsync(countryIds).ToListAsync();
            countryDict = countries.ToDictionary(c => c.Id); 


            // Calculate tax for each
            foreach (var calculation in taxCalculations)
            {
                if (!countryDict.TryGetValue(calculation.CountryId, out var country))
                {
                    throw new NotFoundException($"Country not found.|{calculation.CountryId}");
                }

                decimal tax = 0;
                decimal netPay = calculation.Income;
                var taxRates = country.TaxRates.ToDictionary(r => r.RateCode, r => r.Rate); // Index by RateCode for fast lookup

                switch (country.TaxRegime)
                {
                    case "PROG":
                        tax = CalculateProgressiveTax(calculation.Income, tax, country.TaxBrackets);
                        break;

                    case "FLAT":

                        var flatRate = taxRates.GetValueOrDefault("FLATRATE", 0);
                        var threshold = taxRates.GetValueOrDefault("THRES", 0);
                        tax = calculation.Income > threshold ? flatRate : 0;
                        break;

                    case "PERC":
                        var percentage = taxRates.GetValueOrDefault("TAXPERC", 0);
                        tax = calculation.Income * (percentage / 100);
                        break;

                    default:
                        throw new NotSupportedException($"Unsupported tax regime: {country.TaxRegime}");
                }

                netPay -= tax;

                taxCalculationResponses.Add(new TaxCalculationResponse
                {
                    Id = calculation.Id,
                    CountryId = country.Id,
                    Income = calculation.Income,
                    CalculatedTax = tax,
                    NetPay = netPay
                });
            }

            return Response<List<TaxCalculationResponse>>.Success("Tax Calculations completed successfully.", taxCalculationResponses);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Response<List<TaxCalculationResponse>>> CalculateTaxAsync_(bool calculate)
    {
        try
        {
            List<TaxCalculationResponse> taxCalculationResponses = new();
            var countries = await _unitOfWork.CountryRepository!
                .FindWithRelatedDatasAsync()
                .ToListAsync();

            foreach (var country in countries)
            {
                foreach (var calculation in country.TaxCalculations)
                {
                    decimal tax = 0;
                    decimal netPay = calculation.Income;
                    var taxRates = country.TaxRates.ToDictionary(r => r.RateCode, r => r.Rate);

                    switch (country.TaxRegime)
                    {
                        case "PROG":
                            tax = CalculateProgressiveTax(calculation.Income, tax, country.TaxBrackets);
                            break;

                        case "FLAT":
                            var flatRate = taxRates.GetValueOrDefault("FLATRATE", 0);
                            var threshold = taxRates.GetValueOrDefault("THRES", 0);
                            tax = calculation.Income > threshold ? flatRate : 0;
                            break;

                        case "PERC":
                            var percentage = taxRates.GetValueOrDefault("TAXPERC", 0);
                            tax = calculation.Income * (percentage / 100);
                            break;

                        default:
                            throw new NotSupportedException($"Unsupported tax regime: {country.TaxRegime}");
                    }

                    netPay -= tax;

                    taxCalculationResponses.Add(new TaxCalculationResponse
                    {
                        Id = calculation.Id,
                        CountryId = country.Id,
                        Income = calculation.Income,
                        CalculatedTax = tax,
                        NetPay = netPay
                    });
                }
            }

            return Response<List<TaxCalculationResponse>>.Success("Tax Calculations completed successfully.", taxCalculationResponses);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Response<List<TaxCalculationResponse>>> _CalculateTaxAsync(bool calculate)
    {
        try
        {
            var taxCalculationResponses = new ConcurrentBag<TaxCalculationResponse>();
            var countries = await _unitOfWork.CountryRepository!
                .FindWithRelatedDatasAsync()
                .ToListAsync();

            Parallel.ForEach(countries, country =>
            {
                foreach (var calculation in country.TaxCalculations)
                {
                    decimal tax = 0;
                    decimal netPay = calculation.Income;
                    var taxRates = country.TaxRates.ToDictionary(r => r.RateCode, r => r.Rate);

                    switch (country.TaxRegime)
                    {
                        case "PROG":
                            tax = CalculateProgressiveTax(calculation.Income, tax, country.TaxBrackets);
                            break;

                        case "FLAT":
                            var flatRate = taxRates.GetValueOrDefault("FLATRATE", 0);
                            var threshold = taxRates.GetValueOrDefault("THRES", 0);
                            tax = calculation.Income > threshold ? flatRate : 0;
                            break;

                        case "PERC":
                            var percentage = taxRates.GetValueOrDefault("TAXPERC", 0);
                            tax = calculation.Income * (percentage / 100);
                            break;

                        default:
                            throw new NotSupportedException($"Unsupported tax regime: {country.TaxRegime}");
                    }

                    netPay -= tax;

                    taxCalculationResponses.Add(new TaxCalculationResponse
                    {
                        Id = calculation.Id,
                        CountryId = country.Id,
                        Income = calculation.Income,
                        CalculatedTax = tax,
                        NetPay = netPay
                    });
                }
            });

            return Response<List<TaxCalculationResponse>>.Success(
                "Tax Calculations completed successfully.",
                taxCalculationResponses.ToList()
            );
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Response<List<TaxCalculationResponse>>> FindAll()
    {
        try
        {
            var cacheKey = CacheKeyGenerator.GenerateCacheKeyForCursorPage(nameof(TaxCalculation), 0);
            var cachedTaxCalculations = await _cacheService.GetAsync<List<TaxCalculationResponse>>(cacheKey);
            if (cachedTaxCalculations != null && cachedTaxCalculations.Any())
            {
                return Response<List<TaxCalculationResponse>>.Success($"{cachedTaxCalculations.Count} cached records fetched.", cachedTaxCalculations);
            }

            var taxCalculations = await _unitOfWork.TaxCalculationRepository!.FindAll().ToListAsync();
            if (!taxCalculations.Any())
                return Response<List<TaxCalculationResponse>>.Failure($"No records found. {nameof(TaxCalculation)}");

            var responseList = taxCalculations.Select(response => new TaxCalculationResponse
            {
                Id = response.Id,
                CountryId = response.CountryId,
                Income = response.Income,
                CalculatedTax = response.CalculatedTax,
                NetPay = response.NetPay,

            }).ToList();

            await _cacheService.SetAsync(cacheKey, responseList, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5));

            return Response<List<TaxCalculationResponse>>.Success($"{responseList.Count} records fetched.", responseList);

        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<Response<int>> UpdateTaxCalculationAsync(UpdateTaxCalculationRequest updateTaxCalculationRequest)
    {
        try
        {
            if (updateTaxCalculationRequest.TaxCalculations == null || !updateTaxCalculationRequest.TaxCalculations.Any())
            {
                return Response<int>.Failure("No tax calculations provided for update.");
            }

            // Map all calculations to entities in bulk
            var taxCalculations = updateTaxCalculationRequest.TaxCalculations
                .Select(MapToTaxCalculation.Map)
                .ToList();

            //var unitOfWorkResponse = await _unitOfWork.TaxCalculationRepository!.BulkUpdateAsync(taxCalculations); // 7000ms
            var unitOfWorkResponse = await _unitOfWork.TaxCalculationRepository!.SPBulkUpdateAsync(taxCalculations); // 5500ms

            if (unitOfWorkResponse > 0)
            {
                return Response<int>.Success("Tax Calculation Updated Successfully", unitOfWorkResponse);
            }

            return Response<int>.Failure("There was a pronlem updating Tax Calculations");
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<Response<bool>> ADOUpdateSingleTaxCalculationAsync(int id, UpdateTaxCalculationRequest updateTaxCalculationRequest) 
    {
        try
        {

            var taxCalculation = _mapper.Map<TaxCalculation>(updateTaxCalculationRequest);
            var updatedEntity = await _unitOfWork.TaxCalculationRepository!.ADOUpdateSingleAsync(taxCalculation);
            var unitOfWorkResponse = await _unitOfWork.CompleteAsync();
            if (unitOfWorkResponse > 0) 
            {
                var updateResponse = _mapper.Map<TaxCalculationResponse>(updatedEntity);
                return Response<bool>.Success("Tax Calculation Updated Successfully", true);
            }
        

            return Response<bool>.Failure("There was a pronlem updating Tax Calculation");
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<Response<int>> RunTaxCalculation()
    {
        try
        {
            var response = await _unitOfWork.TaxCalculationRepository!.RunTaxCalculation();
            return response > 0
                ? Response<int>.Success("Tax Calculation Completed Successfully", response)
                : Response<int>.Failure("Tax Calculation Failed");
        }
        catch (Exception)
        {

            throw;
        }
    }

    private decimal CalculateProgressiveTax(decimal income, decimal tax, ICollection<TaxBracket> brackets)
    {
        // Flatten all TaxBracketLines and order them by LowerLimit
        var taxBracketLines = brackets.SelectMany(tb => tb.TaxBracketLines)
                                       .OrderBy(tbl => tbl.LowerLimit)
                                       .ToList();

        foreach (var line in taxBracketLines)
        {
            if (income > line.UpperLimit)
            {
                // Tax for the full range of this bracket
                tax += (line.UpperLimit - line.LowerLimit) * (line.Rate / 100);
            }
            else
            {
                // Tax for the portion of income within this bracket
                tax += (income - line.LowerLimit) * (line.Rate / 100);
                break; // No further brackets apply
            }
        }

        return tax;
    }

}
