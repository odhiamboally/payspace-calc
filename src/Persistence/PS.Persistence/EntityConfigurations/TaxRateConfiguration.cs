using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Persistence.EntityConfigurations;


public class TaxRateConfiguration
{
    public void Configure(EntityTypeBuilder<TaxRate> builder)
    {
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.RateCode).IsRequired().HasMaxLength(10);
        builder.Property(x => x.Rate).IsRequired().HasPrecision(31, 12);

        builder.HasOne(x => x.Country)
            .WithMany(c => c.TaxRates)
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.CountryId);


    }
}
