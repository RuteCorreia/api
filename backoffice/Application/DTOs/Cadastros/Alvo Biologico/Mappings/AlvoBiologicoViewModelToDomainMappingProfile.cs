using Application.DTOs.Cadastros.AlvoBiologico.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.AlvoBiologico.Mappings
{
    public class AlvoBiologicoViewModelToDomainMappingProfile : Profile
    {
        public AlvoBiologicoViewModelToDomainMappingProfile()
        {
            CreateMap<AlvoBiologicoViewModel, Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico>();
        }
    }
}
