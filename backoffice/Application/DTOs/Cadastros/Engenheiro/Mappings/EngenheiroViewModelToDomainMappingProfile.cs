using Application.DTOs.Cadastros.Engenheiro.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Engenheiro.Mappings
{
    public class EngenheiroViewModelToDomainMappingProfile : Profile
    {
        public EngenheiroViewModelToDomainMappingProfile()
        {
            CreateMap<EngenheiroViewModel, Domain.Entidades.Cadastros.Engenheiro.Engenheiro>();
        }
    }
}
