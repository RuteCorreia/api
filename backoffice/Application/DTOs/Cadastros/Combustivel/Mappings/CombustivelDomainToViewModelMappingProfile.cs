using Application.DTOs.Cadastros.Combustivel.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Combustivel.Mappings;

public class CombustivelDomainToViewModelMappingProfile : Profile
{
    public CombustivelDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Combustivel.Combustivel, CombustivelViewModel>();
    }
}
