using Application.DTOs.Cadastros.CombateIncendio.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.CombateIncendio.Mappings;

public class CombateIncendioDomainToViewModelMappingProfile : Profile
{
    public CombateIncendioDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio, CombateIncendioViewModel>()
            .ForMember(dest => dest.IdExecutor, opt => opt.MapFrom(src => src.IdExecutor.ToString()));
    }
}
