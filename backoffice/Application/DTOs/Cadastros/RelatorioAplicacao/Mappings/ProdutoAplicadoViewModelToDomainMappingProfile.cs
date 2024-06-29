using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.RelatorioAplicacao.Mappings
{
    public class ProdutoAplicadoViewModelToDomainMappingProfile : Profile
    {
        public ProdutoAplicadoViewModelToDomainMappingProfile()
        {
            CreateMap<ProdutoAplicadoViewModel, Domain.Entidades.Cadastros.CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado>();
        }
    }
}
