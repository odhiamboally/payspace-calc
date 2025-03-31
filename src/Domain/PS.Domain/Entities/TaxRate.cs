using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Domain.Entities;


public class TaxRate
{
    [Key]
    public required int Id { get; set; }
    public required int CountryId { get; set; }

    [MaxLength(10)]
    public required string RateCode { get; set; }

    [Required]
    [Precision(31, 12)]
    [Range(0, 100, ErrorMessage = "Rate must be between 0 and 100.")]
    public required decimal Rate { get; set; }

    public Country? Country { get; set; }
}
