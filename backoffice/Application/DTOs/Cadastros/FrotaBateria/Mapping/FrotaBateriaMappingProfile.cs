using Application.DTOs.Cadastros.FrotaBateria.ViewModel;
using Application.DTOs.Cadastros.Gerador.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.FrotaBateria.Mapping
{
    public class FrotaBateriaMappingProfile : Profile
    {
        public FrotaBateriaMappingProfile()
        {
            CreateMap<FrotaBateriaViewModel, Domain.Entidades.Cadastros.FrotaBateria.FrotaBateria>();
            CreateMap<Domain.Entidades.Cadastros.FrotaBateria.FrotaBateria, FrotaBateriaViewModel>()
                .ForMember(dest => dest.NumeroBateria, opt => opt.MapFrom(src => src.Bateria != null ? src.Bateria.NumeroBateria : null)); ;
        }
    }
}
