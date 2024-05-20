using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.Mappings;

public class CaracteristicasProdutoAplicadoViewModelToDomainMappingProfile : Profile
{
    public CaracteristicasProdutoAplicadoViewModelToDomainMappingProfile()
    {
        CreateMap<CaracteristicasProdutoAplicadoViewModel, Domain.Entidades.Cadastros.CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado>();
    }
}
