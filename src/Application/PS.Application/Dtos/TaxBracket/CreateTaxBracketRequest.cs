using PS.Application.Dtos.TaxBracketLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Application.Dtos.TaxBracket;


public record CreateTaxBracketRequest
{
    public int CountryId { get; init; }
    public string? Code { get; init; }
    public ICollection<CreateTaxBracketLineRequest>? TaxBracketLines { get; init; }
}
