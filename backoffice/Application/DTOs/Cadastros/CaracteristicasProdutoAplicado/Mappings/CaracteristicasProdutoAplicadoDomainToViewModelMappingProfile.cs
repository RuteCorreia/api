using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.Mappings;

public class CaracteristicasProdutoAplicadoDomainToViewModelMappingProfile : Profile
{
    public CaracteristicasProdutoAplicadoDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado, CaracteristicasProdutoAplicadoViewModel>();
    }
}
