using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Persistence.EntityConfigurations;


public class TaxBracketLineConfiguration
{
    public void Configure(EntityTypeBuilder<TaxBracketLine> builder)
    {
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.LowerLimit).IsRequired().HasPrecision(31, 12);
        builder.Property(x => x.UpperLimit).IsRequired().HasPrecision(31, 12);
        builder.Property(x => x.Rate).IsRequired().HasPrecision(31, 12);

        builder.HasOne(x => x.TaxBracket)
            .WithMany(tb => tb.TaxBracketLines)
            .HasForeignKey(x => x.TaxBracketId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.TaxBracketId);




    }
}
