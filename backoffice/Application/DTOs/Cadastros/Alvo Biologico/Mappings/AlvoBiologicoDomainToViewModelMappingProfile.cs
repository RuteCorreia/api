using Application.DTOs.Cadastros.AlvoBiologico.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.AlvoBiologico.Mappings;

public class AlvoBiologicoDomainToViewModelMappingProfile : Profile
{
    public AlvoBiologicoDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico, AlvoBiologicoViewModel>();
    }
}
