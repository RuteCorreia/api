using Application.DTOs.Cadastros.AplicacaoCroquiImportacao.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.AplicacaoCroquiImportacao.Mappings
{
    public class AplicacaoCroquiImportacaoViewModelToDomainMappingProfile : Profile
    {
        public AplicacaoCroquiImportacaoViewModelToDomainMappingProfile()
        {
            CreateMap<AplicacaoCroquiImportacaoViewModel, Domain.Entidades.Cadastros.Aplicacao.AplicacaoCroquiImportacao>();
        }
    }
}
