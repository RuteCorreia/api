using Application.DTOs.Cadastros.ProdutoAplicado.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.ProdutoAplicado.Mappings;

public class ProdutoAplicadoViewModelToDomainMappingProfile : Profile
{
    public ProdutoAplicadoViewModelToDomainMappingProfile()
    {
        CreateMap<ProdutoAplicadoCaracteristicasViewModel, Domain.Entidades.Cadastros.ProdutoAplicado.ProdutoAplicado>();
    }
}
