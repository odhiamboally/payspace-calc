using AutoMapper;
using PS.Application.Dtos.TaxRate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Infrastructure.Configurations.MappingProfiles.TaxRate;


public class TaxRateMappingProfile : Profile
{
    public TaxRateMappingProfile()
    {
        CreateMap<Domain.Entities.TaxRate, TaxRateResponse>().ReverseMap();
        CreateMap<Domain.Entities.TaxRate, CreateTaxRateRequest>().ReverseMap();
        CreateMap<Domain.Entities.TaxRate, UpdateTaxRateRequest>().ReverseMap();
        
    }
}
