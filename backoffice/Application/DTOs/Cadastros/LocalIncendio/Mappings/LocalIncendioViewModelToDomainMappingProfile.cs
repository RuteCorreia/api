using Application.DTOs.Cadastros.LocalIncendio.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.LocalIncendio.Mappings
{
    public class LocalIncendioViewModelToDomainMappingProfile : Profile
    {
        public LocalIncendioViewModelToDomainMappingProfile()
        {
            CreateMap<LocalIncendioViewModel, Domain.Entidades.Cadastros.LocalIncendio.LocalIncendio>();
        }
    }
}
