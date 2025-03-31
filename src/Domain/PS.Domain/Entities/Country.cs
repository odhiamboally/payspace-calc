using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Domain.Entities;


public class Country
{
    [Key]
    public int Id { get; set; }

    [MaxLength(50)]
    public string? Description { get; set; }

    [MaxLength(3)]
    public string? Code { get; set; }

    [MaxLength(4)]
    public string? TaxRegime { get; set; }

    public ICollection<TaxBracket> TaxBrackets { get; set; } = [];
    public ICollection<TaxRate> TaxRates { get; set; } = [];
    public ICollection<TaxCalculation> TaxCalculations { get; set; } = [];
}

