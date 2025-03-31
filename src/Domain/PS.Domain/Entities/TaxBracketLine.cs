using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Domain.Entities;

public class TaxBracketLine
{
    [Key]
    public required int Id { get; set; }
    public required int TaxBracketId { get; set; }
    public required int OrderNumber { get; set; }

    [Precision(31, 12)]
    public required decimal LowerLimit { get; set; }

    [Precision(31, 12)]
    public required decimal UpperLimit { get; set; }

    [Precision(31, 12)]
    [Range(0, 100, ErrorMessage = "Rate must be between 0 and 100.")]
    public required decimal Rate { get; set; }

    public TaxBracket? TaxBracket { get; set; }
}
