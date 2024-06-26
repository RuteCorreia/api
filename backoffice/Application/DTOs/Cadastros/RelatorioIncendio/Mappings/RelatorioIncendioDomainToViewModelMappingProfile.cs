using Application.DTOs.Cadastros.RelatorioIncendio.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.RelatorioIncendio.Mappings
{
    public class RelatorioIncendioDomainToViewModelMappingProfile : Profile
    {
        public RelatorioIncendioDomainToViewModelMappingProfile()
        {
            CreateMap<Domain.Entidades.Cadastros.RelatorioIncendio.RelatorioIncendio, RelatorioIncendioViewModel>();
        }
    }
}
