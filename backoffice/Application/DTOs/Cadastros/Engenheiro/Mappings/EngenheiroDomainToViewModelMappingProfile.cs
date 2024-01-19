using Application.DTOs.Cadastros.Engenheiro.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Engenheiro.Mappings;

public class EngenheiroDomainToViewModelMappingProfile : Profile
{
    public EngenheiroDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Engenheiro.Engenheiro, EngenheiroViewModel>();
    }
}
