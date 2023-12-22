using Application.DTOs.Cadastros.Frota.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Frota.Mappings
{
    public class FrotaViewModelToDomainMappingProfile : Profile
    {
        public FrotaViewModelToDomainMappingProfile()
        {
            CreateMap<FrotaViewModel, Domain.Entidades.Cadastros.Frota.Frota>();
        }
    }
}
