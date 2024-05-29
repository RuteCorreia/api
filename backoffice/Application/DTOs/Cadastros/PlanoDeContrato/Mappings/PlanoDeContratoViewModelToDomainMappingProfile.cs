using Application.DTOs.Cadastros.PlanoDeContrato.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.PlanoDeContrato.Mappings
{
    public class PlanoDeContratoViewModelToDomainMappingProfile : Profile
    {
        public PlanoDeContratoViewModelToDomainMappingProfile()
        {
            CreateMap<PlanoDeContratoViewModel, Domain.Entidades.Cadastros.Empresa.PlanoDeContrato>();
        }
    }
}
