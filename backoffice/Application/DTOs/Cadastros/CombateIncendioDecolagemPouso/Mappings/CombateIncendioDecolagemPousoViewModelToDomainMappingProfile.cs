using Application.DTOs.Cadastros.CombateIncendioDecolagemPouso.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.CombateIncendioDecolagemPouso.Mappings
{
    public class CombateIncendioDecolagemPousoViewModelToDomainMappingProfile : Profile
    {
        public CombateIncendioDecolagemPousoViewModelToDomainMappingProfile()
        {
            CreateMap<CombateIncendioDecolagemPousoViewModel, Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso>();
        }
    }
}
