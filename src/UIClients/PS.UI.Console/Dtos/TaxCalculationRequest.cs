using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.UI.Console.Dtos;


public sealed record TaxCalculationRequest
{
    public decimal Income { get; init; }
    public string? TaxRegime { get; init; }
}
