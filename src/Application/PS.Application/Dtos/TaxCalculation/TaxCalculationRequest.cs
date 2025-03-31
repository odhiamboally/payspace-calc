using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Application.Dtos.TaxCalculation;
public record TaxCalculationRequest
{
    public decimal Income { get; init; }
    public string? TaxRegime { get; init; }
}
