using Application.DTOs.Cadastros.Veiculo.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Veiculo.Mappings;

public class VeiculoViewModelToDomainMappingProfile : Profile
{
    public VeiculoViewModelToDomainMappingProfile()
    {
        CreateMap<VeiculoViewModel, Domain.Entidades.Cadastros.Veiculo.Veiculo>();
    }
}