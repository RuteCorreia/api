using Application.DTOs.Cadastros.AplicacaoLog.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.AplicacaoLog.Mappings
{
    public class AplicacaoLogViewModelToDomainMappingProfile : Profile
    {
        public AplicacaoLogViewModelToDomainMappingProfile()
        {
            CreateMap<AplicacaoLogViewModel, Domain.Entidades.Cadastros.Aplicacao.AplicacaoLog>();
        }
    }
}
