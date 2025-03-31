using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Persistence.EntityConfigurations;
public class TaxCalculationConfiguration
{
    public void Configure(EntityTypeBuilder<TaxCalculation> builder)
    {
       
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Income).IsRequired().HasPrecision(31, 12);
        builder.Property(x => x.CalculatedTax).HasPrecision(31, 12);
        builder.Property(x => x.NetPay).HasPrecision(31, 12);

        builder.HasOne(x => x.Country)
            .WithMany(c => c.TaxCalculations)
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.CountryId);



    }
}
