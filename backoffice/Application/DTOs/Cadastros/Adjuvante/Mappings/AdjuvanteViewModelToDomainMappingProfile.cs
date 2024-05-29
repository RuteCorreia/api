using Application.DTOs.Cadastros.Adjuvante.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Adjuvante.Mappings
{
    public class AdjuvanteViewModelToDomainMappingProfile : Profile
    {
        public AdjuvanteViewModelToDomainMappingProfile()
        {
            CreateMap<AdjuvanteViewModel, Domain.Entidades.Cadastros.Adjuvante.Adjuvante>();
        }
    }
}
