using Application.DTOs.Cadastros.Cidades.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Cidades.Mappings
{
    public class CidadeDomainToViewModelMappingProfile : Profile
    {
        public CidadeDomainToViewModelMappingProfile()
        {
            CreateMap<Domain.Entidades.Cadastros.Cidades.Cidades, CidadeViewModel>();
        }
    }
}
