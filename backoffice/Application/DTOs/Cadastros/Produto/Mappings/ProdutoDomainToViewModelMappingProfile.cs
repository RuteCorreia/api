using Application.DTOs.Cadastros.Produto.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Produto.Mappings;

public class ProdutoDomainToViewModelMappingProfile : Profile
{
    public ProdutoDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Produto.Produto, ProdutoViewModel>();
    }
}
