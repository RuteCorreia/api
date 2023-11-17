using Application.DTOs.Cadastros.Cultura.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Cultura.Mappings;

public class CulturaDomainToViewModelMappingProfile : Profile
{
    public CulturaDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Cultura.Cultura, CulturaViewModel>();
    }
}
