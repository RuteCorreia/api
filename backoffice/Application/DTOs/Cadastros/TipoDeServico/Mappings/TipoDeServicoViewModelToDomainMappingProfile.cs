using Application.DTOs.Cadastros.TipoDeServico.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.TipoDeServico.Mappings
{
    public class TipoDeServicoViewModelToDomainMappingProfile : Profile
    {
        public TipoDeServicoViewModelToDomainMappingProfile()
        {
            CreateMap<TipoDeServicoViewModel, Domain.Entidades.Cadastros.TipoDeServico.TipoDeServico>();
        }
    }
}
