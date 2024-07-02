using Application.DTOs.Cadastros.CombateIncendioPista.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.CombateIncendioPista.Mappings
{
    public class CombateIncendioPistaViewModelToDomainMappingProfile : Profile
    {
        public CombateIncendioPistaViewModelToDomainMappingProfile()
        {
            CreateMap<CombateIncendioPistaViewModel, Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioPista>();
        }
    }
}
