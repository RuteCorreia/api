using Application.DTOs.Cadastros.AuxiliarPista.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.AuxiliarPista.Mappings
{
    public class AuxiliarPistaAplicacaoViewModelToDomainMappingProfile : Profile
    {
        public AuxiliarPistaAplicacaoViewModelToDomainMappingProfile()
        {
            CreateMap<AuxiliarPistaViewModel, Domain.Entidades.Cadastros.AuxiliarPista.AuxiliarPista>();
        }
    }
}
