using Application.DTOs.Cadastros.AlturaVoo.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.AlturaVoo.Mappings;

public class AlturaVooDomainToViewModelMappingProfile : Profile
{
    public AlturaVooDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Altura_Voo.AlturaVoo, AlturaVooViewModel>();
    }
}
