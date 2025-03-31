using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Application.Dtos.TaxCalculation;
public record CreateTaxCalculationRequest
{
    public int CountryId { get; init; }
    public decimal Income { get; init; }
}
