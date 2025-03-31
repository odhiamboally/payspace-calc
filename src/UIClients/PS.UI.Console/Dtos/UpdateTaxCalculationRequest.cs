using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.UI.Console.Dtos;


public sealed record UpdateTaxCalculationRequest
{
    public int Id { get; init; }
    public decimal CalculatedTax { get; init; }
    public decimal NetPay { get; init; }
    public List<TaxCalculationResponse>? TaxCalculations { get; init; }
}
