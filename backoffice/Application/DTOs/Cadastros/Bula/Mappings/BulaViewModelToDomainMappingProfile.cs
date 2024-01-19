using Application.DTOs.Cadastros.Bula.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Bula.Mappings
{
    public class BulaViewModelToDomainMappingProfile : Profile
    {
        public BulaViewModelToDomainMappingProfile()
        {
            CreateMap<BulaViewModel, Domain.Entidades.Cadastros.Empresa.Bula>();
        }
    }
}
