using PS.Application.Dtos.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Infrastructure.Configurations.MappingProfiles.Country;


public class MapToCountryResponse
{
    public static CountryResponse Map(Domain.Entities.Country country)
    {
        return new CountryResponse
        {
            Id = country.Id,
            Description = country.Description,
            Code = country.Code,
            TaxRegime = country.TaxRegime
        };
    }

}

