using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using PS.Application.Dtos.Country;
using PS.Domain.Entities;

namespace PS.Infrastructure.Configurations.MappingProfiles.Country;

public class CountryMappingProfile : Profile
{
    public CountryMappingProfile()
    {
        CreateMap<Domain.Entities.Country, CountryResponse>().ReverseMap();
        CreateMap<Domain.Entities.Country, CreateCountryRequest>().ReverseMap();
        CreateMap<Domain.Entities.Country, UpdateCountryRequest>().ReverseMap();

    }
}
