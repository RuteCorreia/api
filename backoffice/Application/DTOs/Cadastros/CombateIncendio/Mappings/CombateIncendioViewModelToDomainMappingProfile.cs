using Application.DTOs.Cadastros.CombateIncendio.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.CombateIncendio.Mappings
{
    public class CombateIncendioViewModelToDomainMappingProfile : Profile
    {
        public CombateIncendioViewModelToDomainMappingProfile()
        {
            CreateMap<CombateIncendioViewModel, Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio>()
                .ForMember(dest => dest.IdExecutor, opt => opt.MapFrom(src => Guid.Parse(src.IdExecutor)));
        }
    }
}
