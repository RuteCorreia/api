using Application.DTOs.Cadastros.CombateIncendio.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.CombateIncendio.Mappings
{
    public class CombateIncendioViewModelToDomainMappingProfile : Profile
    {
        public CombateIncendioViewModelToDomainMappingProfile()
        {
            CreateMap<CombateIncendioViewModel, Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio>();
        }
    }
}
