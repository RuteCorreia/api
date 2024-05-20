using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.Mappings;

public class CaracteristicasProdutoAplicadoViewModelToDomain : Profile
{
    public CaracteristicasProdutoAplicadoViewModelToDomain()
    {
        CreateMap<CaracteristicasProdutoAplicadoViewModel, Domain.Entidades.Cadastros.CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado>();
    }
}
