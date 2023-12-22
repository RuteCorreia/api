using Application.DTOs.Cadastros.Cultura.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Cultura.Mappings
{
    public class CulturaViewModelToDomainMappingProfile : Profile
    {
        public CulturaViewModelToDomainMappingProfile()
        {
            CreateMap<CulturaViewModel, Domain.Entidades.Cadastros.Cultura.Cultura>();
        }
    }
}
