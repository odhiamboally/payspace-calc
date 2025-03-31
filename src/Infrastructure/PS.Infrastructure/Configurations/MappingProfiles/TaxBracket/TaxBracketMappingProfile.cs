using AutoMapper;
using PS.Application.Dtos.TaxBracket;
using PS.Application.Dtos.TaxBracketLine;
using PS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Infrastructure.Configurations.MappingProfiles.TaxBracket;


public class TaxBracketMappingProfile : Profile
{
    public TaxBracketMappingProfile()
    {
        CreateMap<Domain.Entities.TaxBracket, TaxBracketResponse>().ReverseMap();
        CreateMap<Domain.Entities.TaxBracket, CreateTaxBracketRequest>().ReverseMap();
        CreateMap<Domain.Entities.TaxBracket, UpdateTaxBracketRequest>().ReverseMap();

      
        CreateMap<TaxBracketLine, TaxBracketLineResponse>().ReverseMap();
        CreateMap<TaxBracketLine, CreateTaxBracketLineRequest>().ReverseMap();
        CreateMap<TaxBracketLine, UpdateTaxBracketLineRequest>().ReverseMap();
        
    }
}
