using Application.DTOs.Cadastros.Aeronave.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Aeronave.Mappings;

public class AeronaveDomainToViewModelMappingProfile : Profile
{
    public AeronaveDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Aeronave.Aeronave, AeronaveViewModel>();
    }
}
