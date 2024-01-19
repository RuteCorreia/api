using Application.DTOs.Cadastros.Estados.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Estados.Mappings
{
    public class EstadosViewModelToDomainMappingProfile : Profile
    {
        public EstadosViewModelToDomainMappingProfile()
        {
            CreateMap<EstadosViewModel, Domain.Entidades.Cadastros.Estados.Estados>();
        }
    }
}
