using Application.DTOs.Cadastros.Classe.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Classe.Mappings
{
    public class ClasseViewModelToDomainMappingProfile : Profile
    {
        public ClasseViewModelToDomainMappingProfile()
        {
            CreateMap<ClasseViewModel, Domain.Entidades.Cadastros.Classe.Classe>().ReverseMap();
            CreateMap<ClasseCreateViewModel, Domain.Entidades.Cadastros.Classe.Classe>();
        }
    }
}
