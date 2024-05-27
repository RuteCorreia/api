using Application.DTOs.Cadastros.Veiculante.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Veiculante.Mappings;

public class VeiculanteDomainToViewModelMappingProfile : Profile
{
    public VeiculanteDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Veiculante.Veiculante, VeiculanteViewModel>();
    }
}
