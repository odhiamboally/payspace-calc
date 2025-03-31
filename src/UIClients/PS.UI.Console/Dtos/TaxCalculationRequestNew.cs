using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.UI.Console.Dtos;
public record TaxCalculationRequestNew
{
    public decimal Income { get; init; }
    public string? TaxRegime { get; init; }
    public List<TaxCalculationResponse>? TaxCalculations { get; init; }
}
