using Application.DTOs.Cadastros.Tipo_Produto.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Tipo_Produto.Mappings
{
    public class TipoProdutoViewModelToDomainMappingProfile : Profile
    {
        public TipoProdutoViewModelToDomainMappingProfile()
        {
            CreateMap<TipoProdutoViewModel, Domain.Entidades.Cadastros.Tipo_Produto.TipoProduto>();
        }
    }
}
