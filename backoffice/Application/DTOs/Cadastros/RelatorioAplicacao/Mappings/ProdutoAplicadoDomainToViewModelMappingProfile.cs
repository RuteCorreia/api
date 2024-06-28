using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.RelatorioAplicacao.Mappings
{
    public class ProdutoAplicadoDomainToViewModelMappingProfile : Profile
    {
        public ProdutoAplicadoDomainToViewModelMappingProfile()
        {
            CreateMap<Domain.Entidades.Cadastros.CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado, ProdutoAplicadoViewModel>();
        }
    }
}
