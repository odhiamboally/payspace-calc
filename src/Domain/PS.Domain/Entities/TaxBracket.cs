using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Domain.Entities;


public class TaxBracket
{
    [Key]
    public int Id { get; set; }
    public int CountryId { get; set; }

    [MaxLength(10)]
    public string? Code { get; set; }

    public Country? Country { get; set; }
    public ICollection<TaxBracketLine> TaxBracketLines { get; set; } = [];
}

