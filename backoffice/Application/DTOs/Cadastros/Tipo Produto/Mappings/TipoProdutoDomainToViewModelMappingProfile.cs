using Application.DTOs.Cadastros.Tipo_Produto.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Tipo_Produto.Mappings;

public class TipoProdutoDomainToViewModelMappingProfile : Profile
{
    public TipoProdutoDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Tipo_Produto.TipoProduto, TipoProdutoViewModel>();
    }
}
