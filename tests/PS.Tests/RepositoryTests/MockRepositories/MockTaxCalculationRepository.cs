using PS.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Tests.RepositoryTests.MockRepositories;

internal sealed class MockTaxCalculationRepository
{
    public static List<TaxCalculation> GetMockTaxCalculations()
    {
        return new List<TaxCalculation>
        {
            new TaxCalculation 
            { 
                Id = 1, 
                CountryId = 1, 
                Income = -5000,
                CalculatedTax = 0,
                NetPay = 0,
            },
            new TaxCalculation 
            { 
                Id = 2, 
                CountryId = 2, 
                Income = 11600,
                CalculatedTax = 0,
                NetPay = 0,
            },
            new TaxCalculation 
            { 
                Id = 3, 
                CountryId = 3, 
                Income = 200000,
                CalculatedTax = 0,
                NetPay = 0,
            },
            new TaxCalculation
            {
                Id = 4,
                CountryId = 4,
                Income = 30000,
                CalculatedTax = 0,
                NetPay = 0,
            },
            new TaxCalculation
            {
                Id = 5,
                CountryId = 5,
                Income = 0,
                CalculatedTax = 0,
                NetPay = 0,
            },
            new TaxCalculation
            {
                Id = 6,
                CountryId = 6,
                Income = 0,
                CalculatedTax = 0,
                NetPay = 0,
            }

        };
    }
}

