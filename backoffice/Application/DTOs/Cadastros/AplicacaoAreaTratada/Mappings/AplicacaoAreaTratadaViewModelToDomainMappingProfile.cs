using Application.DTOs.Cadastros.AplicacaoAreaTratada.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.AplicacaoAreaTratada.Mappings
{
    public class AplicacaoAreaTratadaViewModelToDomainMappingProfile : Profile
    {
        public AplicacaoAreaTratadaViewModelToDomainMappingProfile()
        {
            CreateMap<AplicacaoAreaTratadaViewModel, Domain.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada>();
        }
    }
}
