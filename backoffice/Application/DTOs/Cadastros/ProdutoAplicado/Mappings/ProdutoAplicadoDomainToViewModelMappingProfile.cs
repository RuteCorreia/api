using Application.DTOs.Cadastros.ProdutoAplicado.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.ProdutoAplicado.Mappings;

public class ProdutoAplicadoDomainToViewModelMappingProfile : Profile
{
    public ProdutoAplicadoDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.ProdutoAplicado.ProdutoAplicado, ProdutoAplicadoCaracteristicasViewModel>();
    }
}
