using Application.DTOs.Cadastros.Empresa.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Empresa.Mappings
{
    public class EmpresaViewModelToDomainMappingProfile : Profile
    {
        public EmpresaViewModelToDomainMappingProfile()
        {
            CreateMap<EmpresaViewModel, Domain.Entidades.Cadastros.Empresa.Empresa>();
        }
    }
}
