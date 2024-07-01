using Application.DTOs.Cadastros.CombateIncendioPista.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.CombateIncendioPista.Mappings
{
    public class CombateIncendioPistaDomainToViewModelMappingProfile : Profile
    {
        public CombateIncendioPistaDomainToViewModelMappingProfile()
        {
            CreateMap<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioPista, CombateIncendioPistaViewModel>();
        }
    }
}
