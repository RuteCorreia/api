using Application.DTOs.Cadastros.Frota.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Frota.Mappings;

public class FrotaDomainToViewModelMappingProfile : Profile
{
    public FrotaDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Frota.Frota, FrotaViewModel>();
    }
}
