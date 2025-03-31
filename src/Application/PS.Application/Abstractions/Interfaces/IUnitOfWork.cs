using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PS.Application.Abstractions.IRepositories;

namespace PS.Application.Abstractions.Interfaces;
public interface IUnitOfWork
{
    ICountryRepository? CountryRepository { get; }
    ITaxBracketRepository? TaxBracketRepository { get; }
    ITaxBracketLineRepository? TaxBracketLineRepository { get; }
    ITaxRateRepository? TaxRateRepository { get; }
    ITaxCalculationRepository? TaxCalculationRepository { get; }


    Task<int> CompleteAsync();
}
