using Application.DTOs.Cadastros.Combustivel.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Combustivel.Mappings
{
    public class CombustivelViewModelToDomainMappingProfile : Profile
    {
        public CombustivelViewModelToDomainMappingProfile()
        {
            CreateMap<CombustivelViewModel, Domain.Entidades.Cadastros.Combustivel.Combustivel>();
        }
    }
}
