using Application.DTOs.Cadastros.AlturaVoo.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.AlturaVoo.Mappings
{
    public class AlturaVooViewModelToDomainMappingProfile : Profile
    {
        public AlturaVooViewModelToDomainMappingProfile()
        {
            CreateMap<AlturaVooViewModel, Domain.Entidades.Cadastros.Altura_Voo.AlturaVoo>();
        }
    }
}
