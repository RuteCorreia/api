using Application.DTOs.Cadastros.ReceituarioAgronomico.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.ReceituarioAgronomico.Mappings;

public class ReceituarioAgronomicoDomainToViewModelMappingProfile : Profile
{
    public ReceituarioAgronomicoDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.ReceituarioAgronomico.ReceituarioAgronomico, ReceituarioAgronomicoViewModel>();
    }
}
