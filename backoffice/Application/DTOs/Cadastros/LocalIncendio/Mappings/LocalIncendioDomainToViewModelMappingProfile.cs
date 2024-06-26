using Application.DTOs.Cadastros.LocalIncendio.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.LocalIncendio.Mappings
{
    public class LocalIncendioDomainToViewModelMappingProfile : Profile
    {
        public LocalIncendioDomainToViewModelMappingProfile()
        {
            CreateMap<Domain.Entidades.Cadastros.LocalIncendio.LocalIncendio, LocalIncendioViewModel>();
        }
    }
}
