using Application.DTOs.Cadastros.Produto.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Produto.Mappings
{
    public class ProdutoViewModelToDomainMappingProfile : Profile
    {
        public ProdutoViewModelToDomainMappingProfile()
        {
            CreateMap<ProdutoViewModel, Domain.Entidades.Cadastros.Produto.Produto>();
        }
    }
}
