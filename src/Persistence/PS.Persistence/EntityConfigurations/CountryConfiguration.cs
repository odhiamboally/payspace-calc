using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Persistence.EntityConfigurations;


public class CountryConfiguration
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Description).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Code).IsRequired().HasMaxLength(3);
        builder.Property(x => x.TaxRegime).IsRequired().HasMaxLength(4);

        builder.HasIndex(x => x.TaxRegime);




    }
}
