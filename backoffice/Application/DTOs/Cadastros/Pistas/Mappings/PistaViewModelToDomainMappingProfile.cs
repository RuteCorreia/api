using Application.DTOs.Cadastros.Pistas.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Pistas.Mappings
{
    public class PistaViewModelToDomainMappingProfile : Profile
    {
        public PistaViewModelToDomainMappingProfile()
        {
            CreateMap<PistaViewModel, Domain.Entidades.Cadastros.Pistas.Pista>();
        }
    }
}
