using Application.DTOs.Cadastros.AplicacaoCroqui.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.AplicacaoCroqui.Mappings
{
    public class AplicacaoCroquiViewModelToDomainMappingProfile : Profile
    {
        public AplicacaoCroquiViewModelToDomainMappingProfile()
        {
            CreateMap<AplicacaoCroquiViewModel, Domain.Entidades.Cadastros.Aplicacao.AplicacaoCroqui>();
        }
    }
}
