using Application.DTOs.Cadastros.Estados.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Estados.Mappings;

public class EstadosDomainToViewModelMappingProfile : Profile
{
    public EstadosDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Estados.Estados, EstadosViewModel>();
    }
}
