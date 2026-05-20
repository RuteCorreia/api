using Application.DTOs.Cadastros.Classe.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Classe.Mappings
{
    public class ClasseDomainToViewModelMappingProfile : Profile
    {
        public ClasseDomainToViewModelMappingProfile()
        {
            CreateMap<Domain.Entidades.Cadastros.Classe.Classe, ClasseViewModel>();
        }
    }
}
