using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Application.Dtos.TaxBracket;


public record UpdateTaxBracketRequest
{
    public int Id { get; init; }
    public string? Code { get; init; }
}
