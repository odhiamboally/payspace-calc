using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Application.Dtos.TaxCalculation;

public record TaxCalculationResponse
{
    public int Id { get; init; }
    public int CountryId { get; init; }
    public decimal Income { get; init; }
    public decimal? CalculatedTax { get; init; }
    public decimal? NetPay { get; init; }
}
