using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PS.Domain.Entities;
using System.Reflection.Emit;

namespace PS.Persistence.EntityConfigurations;

public class TaxBracketConfiguration
{
    public void Configure(EntityTypeBuilder<TaxBracket> builder)
    {
       
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code).IsRequired().HasMaxLength(10);

        builder.HasOne(x => x.Country)
            .WithMany(c => c.TaxBrackets)
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.CountryId);




    }
}
