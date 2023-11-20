using Application.DTOs.Cadastros.Veiculante.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Veiculante.Mappings
{
    public class VeiculanteViewModelToDomainMappingProfile : Profile
    {
        public VeiculanteViewModelToDomainMappingProfile()
        {
            CreateMap<VeiculanteViewModel, Domain.Entidades.Cadastros.Veiculante.Veiculante>();
        }
    }
}
