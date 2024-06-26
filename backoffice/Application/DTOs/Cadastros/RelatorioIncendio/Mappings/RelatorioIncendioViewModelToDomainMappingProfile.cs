using Application.DTOs.Cadastros.RelatorioIncendio.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.RelatorioIncendio.Mappings
{
    public class RelatorioIncendioViewModelToDomainMappingProfile : Profile
    {
        public RelatorioIncendioViewModelToDomainMappingProfile()
        {
            CreateMap<RelatorioIncendioViewModel, Domain.Entidades.Cadastros.RelatorioIncendio.RelatorioIncendio>();
        }
    }
}
