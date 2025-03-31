using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PS.Application.Dtos.Country;
using PS.Application.Dtos.TaxCalculation;
using PS.Domain.Entities;

namespace PS.Infrastructure.Configurations.MappingProfiles.TaxCalculations;
public class MapToTaxCalculation
{
    public static TaxCalculation Map(TaxCalculationResponse taxCalculationResponse)
    {
        return new TaxCalculation
        {
            Id = taxCalculationResponse.Id,
            CountryId = taxCalculationResponse.CountryId,
            Income = taxCalculationResponse.Income,
            CalculatedTax = taxCalculationResponse.CalculatedTax,
            NetPay = taxCalculationResponse.NetPay,
        };
    }
}
