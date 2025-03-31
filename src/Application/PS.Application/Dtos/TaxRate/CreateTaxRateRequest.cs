using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Application.Dtos.TaxRate;
public record CreateTaxRateRequest
{
    public int CountryId { get; init; }
    public string? RateCode { get; init; }
    public decimal Rate { get; init; }
}
