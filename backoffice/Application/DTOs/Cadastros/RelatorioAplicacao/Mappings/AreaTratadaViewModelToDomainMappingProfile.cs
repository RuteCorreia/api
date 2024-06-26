using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.RelatorioAplicacao.Mappings
{
    public class AreaTratadaViewModelToDomainMappingProfile : Profile
    {
        public AreaTratadaViewModelToDomainMappingProfile()
        {
            CreateMap<AreaTratadaViewModel, Domain.Entidades.Cadastros.IdentificacaoAreaTratada.IdentificacaoAreaTratada>();
        }
    }
}
