using Application.DTOs.Cadastros.Aeronave.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Aeronave.Mappings
{
    public class AeronaveViewModelToDomainMappingProfile : Profile
    {
        public AeronaveViewModelToDomainMappingProfile()
        {
            CreateMap<AeronaveViewModel, Domain.Entidades.Cadastros.Aeronave.Aeronave>();
        }
    }
}
