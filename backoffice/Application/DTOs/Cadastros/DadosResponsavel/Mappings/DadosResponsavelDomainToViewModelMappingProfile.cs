using Application.DTOs.Cadastros.DadosResponsavel.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.DadosResponsavel.Mappings;

public class DadosResponsavelDomainToViewModelMappingProfile : Profile
{
    public DadosResponsavelDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.DadosResponsavel.DadosResponsavel, DadosResponsavelViewModel>();
    }
}
