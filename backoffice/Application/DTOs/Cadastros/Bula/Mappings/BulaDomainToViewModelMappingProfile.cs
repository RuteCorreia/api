using Application.DTOs.Cadastros.Bula.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Bula.Mappings;

public class BulaDomainToViewModelMappingProfile : Profile
{
    public BulaDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Empresa.Bula, BulaViewModel>();
    }
}
