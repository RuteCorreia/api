using Application.DTOs.Cadastros.AuxiliarPista.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.AuxiliarPista.Mappings
{
    public class AuxiliarPistaAplicacaoDomainToViewModelMappingProfile : Profile
    {
        public AuxiliarPistaAplicacaoDomainToViewModelMappingProfile()
        {
            CreateMap<Domain.Entidades.Cadastros.AuxiliarPista.AuxiliarPista, AuxiliarPistaViewModel>();
        }
    }
}
