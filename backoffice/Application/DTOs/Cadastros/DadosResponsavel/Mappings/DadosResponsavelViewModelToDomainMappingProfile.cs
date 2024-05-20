using Application.DTOs.Cadastros.DadosResponsavel.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.DadosResponsavel.Mappings;

public class DadosResponsavelViewModelToDomainMappingProfile : Profile
{
    public DadosResponsavelViewModelToDomainMappingProfile()
    {
        CreateMap<DadosResponsavelViewModel, Domain.Entidades.Cadastros.DadosResponsavel.DadosResponsavel>();
    }
}
