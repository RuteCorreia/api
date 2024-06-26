using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.RelatorioAplicacao.Mappings
{
    public class AreaTratadaDomainToViewModelMappingProfile : Profile
    {
        public AreaTratadaDomainToViewModelMappingProfile()
        {
            CreateMap<Domain.Entidades.Cadastros.IdentificacaoAreaTratada.IdentificacaoAreaTratada, AreaTratadaViewModel>();
        }
    }
}
