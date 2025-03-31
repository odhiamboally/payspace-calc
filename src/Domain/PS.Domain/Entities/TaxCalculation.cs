using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Domain.Entities;

public class TaxCalculation
{
    [Key]
    public required int Id { get; set; }
    public required int CountryId { get; set; }

    [Precision(31, 12)]
    public required decimal Income { get; set; }

    [Precision(31, 12)]
    [DefaultValue(0)]
    public decimal? CalculatedTax { get; set; } = decimal.Zero;

    [Precision(31, 12)]
    [DefaultValue(0)]
    public decimal? NetPay { get; set; } = decimal.Zero;

    public Country? Country { get; set; }
}
