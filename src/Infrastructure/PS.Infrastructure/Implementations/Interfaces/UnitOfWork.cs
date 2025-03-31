using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using PS.Application.Abstractions.Interfaces;
using PS.Application.Abstractions.IRepositories;
using PS.Persistence.DataContext;

namespace PS.Infrastructure.Implementations.Interfaces;


public class UnitOfWork : IUnitOfWork
{
    public ICountryRepository? CountryRepository { get; private set; }
    public ITaxBracketRepository? TaxBracketRepository { get; private set; }
    public ITaxBracketLineRepository? TaxBracketLineRepository { get; private set; }
    public ITaxRateRepository? TaxRateRepository { get; private set; }
    public ITaxCalculationRepository? TaxCalculationRepository { get; private set; }

    private readonly DBContext _context;

    public UnitOfWork(

        ICountryRepository countryRepository,
        ITaxBracketRepository taxBracketRepository,
        ITaxBracketLineRepository taxBracketLineRepository,
        ITaxRateRepository taxRateRepository,
        ITaxCalculationRepository taxCalculationRepository,

        DBContext context
    )
    {
        CountryRepository = countryRepository;
        TaxBracketRepository = taxBracketRepository;
        TaxBracketLineRepository = taxBracketLineRepository;
        TaxRateRepository = taxRateRepository;
        TaxCalculationRepository = taxCalculationRepository;
        _context = context;
    }



    public Task<int> CompleteAsync()
    {
        var result = _context.SaveChangesAsync();
        return result;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);

    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _context.Dispose();
        }
    }
}


