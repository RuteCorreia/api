using Application.DTOs.Cadastros.PlanoDeContrato.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.PlanoDeContrato.Mappings;

public class PlanoDeContratoDomainToViewModelMappingProfile : Profile
{
    public PlanoDeContratoDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Empresa.PlanoDeContrato, PlanoDeContratoViewModel>();
    }
}
