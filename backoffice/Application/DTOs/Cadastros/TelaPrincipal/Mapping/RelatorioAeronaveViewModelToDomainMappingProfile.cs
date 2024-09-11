using Application.DTOs.Cadastros.TelaPrincipal.ViewModel;
using AutoMapper;
using Domain.Entidades.Cadastros.TelaPrincipal;

namespace Application.DTOs.Cadastros.TelaPrincipal.Mapping
{
    public class RelatorioAeronaveViewModelToDomainMappingProfile : Profile
    {
        public RelatorioAeronaveViewModelToDomainMappingProfile()
        {
            CreateMap<RelatorioAeronaveViewModel, RelatorioAeronave>();

        }
    }
}
