using Application.DTOs.Cadastros.Adjuvante.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Adjuvante.Mappings;

public class AdjuvanteDomainToViewModelMappingProfile : Profile
{
    public AdjuvanteDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Adjuvante.Adjuvante, AdjuvanteViewModel>();
    }
}
