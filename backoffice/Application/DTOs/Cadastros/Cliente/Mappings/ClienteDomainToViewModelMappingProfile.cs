using Application.DTOs.Cadastros.Cliente.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Cliente.Mappings;

public class ClienteDomainToViewModelMappingProfile : Profile
{
    public ClienteDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Cliente.Cliente, ClienteViewModel>();
    }
}
