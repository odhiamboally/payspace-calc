using PS.Application.Dtos.TaxBracketLine;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Application.Dtos.TaxBracket;


public record TaxBracketResponse
{
    public int Id { get; init; }
    public int CountryId { get; init; }
    public string? Code { get; init; }
    public ICollection<TaxBracketLineResponse>? Lines { get; init; }
}
