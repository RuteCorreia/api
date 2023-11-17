using Application.DTOs.Cadastros.Cliente.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Cliente.Mappings
{
    public class ClienteViewModelToDomainMappingProfile : Profile
    {
        public ClienteViewModelToDomainMappingProfile()
        {
            CreateMap<ClienteViewModel, Domain.Entidades.Cadastros.Cliente.Cliente>();
        }
    }
}
