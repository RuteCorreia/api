using Application.DTOs.Cadastros.TipoDeServico.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.TipoDeServico.Mappings
{
    public class TipoDeServicoDomainToViewModelMappingProfile : Profile
    {
        public TipoDeServicoDomainToViewModelMappingProfile()
        {
            CreateMap<Domain.Entidades.Cadastros.TipoDeServico.TipoDeServico, TipoDeServicoViewModel>();
        }
    }
}
