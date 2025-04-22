using Application.DTOs.Cadastros.FrotaMotobomba.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.FrotaMotobomba.Mapping
{
    public class FrotaMotobombaMappingProfile : Profile
    {
        public FrotaMotobombaMappingProfile()
        {
            CreateMap<FrotaMotobombaViewModel, Domain.Entidades.Cadastros.FrotaMotobomba.FrotaMotobomba>();
            CreateMap<Domain.Entidades.Cadastros.FrotaMotobomba.FrotaMotobomba, FrotaMotobombaViewModel>()
                .ForMember(dest => dest.NomeMotobomba, opt => opt.MapFrom(src => src.Motobomba != null ? src.Motobomba.Nome : null)); ;
        }
    }
}
