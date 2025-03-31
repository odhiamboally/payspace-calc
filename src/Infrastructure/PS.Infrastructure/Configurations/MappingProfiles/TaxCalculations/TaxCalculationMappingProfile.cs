using AutoMapper;
using PS.Application.Dtos.TaxCalculation;
using PS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Infrastructure.Configurations.MappingProfiles.TaxCalculations;


public class TaxCalculationMappingProfile : Profile
{
    public TaxCalculationMappingProfile()
    {
        CreateMap<Domain.Entities.TaxCalculation, TaxCalculationResponse>().ReverseMap();
        CreateMap<Domain.Entities.TaxCalculation, CreateTaxCalculationRequest>().ReverseMap();
        CreateMap<Domain.Entities.TaxCalculation, UpdateTaxCalculationRequest>().ReverseMap();
        
    }
}
