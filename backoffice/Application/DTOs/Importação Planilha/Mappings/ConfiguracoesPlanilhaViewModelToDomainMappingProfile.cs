using Application.DTOs.Cadastros.Adjuvante.ViewModel;
using Application.DTOs.Importação_Planilha.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Adjuvante.Mappings
{
    public class ConfiguracoesPlanilhaViewModelToDomainMappingProfile : Profile
    {
        public ConfiguracoesPlanilhaViewModelToDomainMappingProfile()
        {
            CreateMap<ConfiguracoesPlanilhaViewModel, Domain.Entidades.Importação_Planilha.ConfiguracaoPlanilha>();
        }
    }
}
