using Application.DTOs.Cadastros.Piloto.ViewModel;
using AutoMapper;
using Domain.Entidades.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.Piloto.Mappings
{
    public class PilotoMappingProfile : Profile
    {
        public PilotoMappingProfile()
        {
            CreateMap<Domain.Entidades.Cadastros.Piloto.Piloto, PilotoViewModel>()
                .ForMember(dest => dest.Assinatura, opt => opt.MapFrom(src => Convert.ToBase64String(src.Assinatura)))
                .ForMember(dest => dest.IdPiloto, opt => opt.MapFrom(src => src.IdPiloto.ToString()));
            CreateMap<PilotoViewModel, Domain.Entidades.Cadastros.Piloto.Piloto>();
        }
    }
}
