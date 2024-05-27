using Application.DTOs.Cadastros.Adjuvante.ViewModel;
using Application.DTOs.Importação_Planilha.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Adjuvante.Mappings;

public class ConfiguracoesPlanilhaToViewModelMappingProfile : Profile
{
    public ConfiguracoesPlanilhaToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Importação_Planilha.ConfiguracaoPlanilha, ConfiguracoesPlanilhaViewModel>();
    }
}
