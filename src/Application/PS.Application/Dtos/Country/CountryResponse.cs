using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Application.Dtos.Country;

public record CountryResponse
{
    public int Id { get; init; }
    public string? Description { get; init; }
    public string? Code { get; init; }
    public string? TaxRegime { get; init; }


}
