using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PS.Application.Abstractions.ICaching;
using PS.Application.Abstractions.Interfaces;
using PS.Application.Abstractions.IServices;

namespace PS.Infrastructure.Implementations.Interfaces;


internal sealed class ServiceManager : IServiceManager
{
    public ICountryService CountryService { get; }
    public ITaxBracketService TaxBracketService { get; }
    public ITaxCalculationService TaxCalculationService { get; }
    public ITaxRateService TaxRateService { get; }
    public ICacheService CacheService { get; }



    public ServiceManager(
        ICountryService countryService, 
        ITaxBracketService taxBracketService, 
        ITaxCalculationService taxCalculationService, 
        ITaxRateService taxRateService,
        ICacheService cacheService)
    {
        CountryService = countryService;
        TaxBracketService = taxBracketService;
        TaxCalculationService = taxCalculationService;
        TaxRateService = taxRateService;
        CacheService = cacheService;
    }
}

