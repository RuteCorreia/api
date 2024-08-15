using Application.DTOs.Cadastros.Atividade.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Atividade.Mappings
{
    public class AtividadeViewModelToDomainMappingProfile : Profile
    {
        public AtividadeViewModelToDomainMappingProfile()
        {
            CreateMap<AtividadeViewModel, Domain.Entidades.Cadastros.Atividade.Atividade>();
        }
    }
}
