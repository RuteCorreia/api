using Application.DTOs.Cadastros.CombateIncendio.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.CombateIncendio.Mappings;

public class CombateIncendioDomainToViewModelMappingProfile : Profile
{
    public CombateIncendioDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio, CombateIncendioViewModel>()
            .ForMember(dest => dest.IdExecutor, opt => opt.MapFrom(src => src.IdExecutor.ToString()))
                .ForMember(dest => dest.HorimetroAviao, opt => opt.MapFrom(src => src.HorimetroAviao))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.StatusEnvio));
    }
}
