using PS.Application.Abstractions.ICaching;
using PS.Application.Abstractions.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Application.Abstractions.Interfaces;
public interface IServiceManager
{
    public ICountryService CountryService { get; }
    public ITaxBracketService TaxBracketService { get; }
    public ITaxCalculationService TaxCalculationService { get; }
    public ITaxRateService TaxRateService { get; }
    public ICacheService CacheService { get; }
}
