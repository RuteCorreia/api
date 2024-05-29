using Application.DTOs.Cadastros.CombateIncendioDecolagemPouso.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.CombateIncendioDecolagemPouso.Mappings;

public class CombateIncendioDecolagemPousoDomainToViewModelMappingProfile : Profile
{
    public CombateIncendioDecolagemPousoDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso, CombateIncendioDecolagemPousoViewModel>();
    }
}
