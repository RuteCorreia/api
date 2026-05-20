using Application.DTOs.Cadastros.Classe.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Classe.Mappings
{
    public class ClasseViewModelToDomainMappingProfile : Profile
    {
        public ClasseViewModelToDomainMappingProfile()
        {
            CreateMap<ClasseViewModel, Domain.Entidades.Cadastros.Classe.Classe>();
            CreateMap<ClasseCreateViewModel, Domain.Entidades.Cadastros.Classe.Classe>();
        }
    }
}
