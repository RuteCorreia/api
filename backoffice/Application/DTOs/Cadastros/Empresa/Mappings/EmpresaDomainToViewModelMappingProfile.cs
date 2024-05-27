using Application.DTOs.Cadastros.Empresa.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Empresa.Mappings;

public class EmpresaDomainToViewModelMappingProfile : Profile
{
    public EmpresaDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Empresa.Empresa, EmpresaViewModel>();
    }
}
