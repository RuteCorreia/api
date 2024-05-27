using Application.DTOs.Cadastros.Veiculo.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Veiculo.Mappings;

public class VeiculoDomainToViewModelMappingProfile : Profile
{
    public VeiculoDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Veiculo.Veiculo, VeiculoViewModel>();
    }
}
