using Application.DTOs.Cadastros.Atividade.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Atividade.Mappings
{
    public class AtividadeDomainToViewModelMappingProfile : Profile
    {
        public AtividadeDomainToViewModelMappingProfile()
        {
            CreateMap<Domain.Entidades.Cadastros.Atividade.Atividade, AtividadeViewModel>();
        }
    }
}
