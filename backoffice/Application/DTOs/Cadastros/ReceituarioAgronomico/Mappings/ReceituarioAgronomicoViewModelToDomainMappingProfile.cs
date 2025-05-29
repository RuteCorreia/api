using Application.DTOs.Cadastros.ReceituarioAgronomico.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.ReceituarioAgronomico.Mappings;

public class ReceituarioAgronomicoViewModelToDomainMappingProfile : Profile
{
    public ReceituarioAgronomicoViewModelToDomainMappingProfile()
    {
        CreateMap<ReceituarioAgronomicoViewModel, Domain.Entidades.Cadastros.ReceituarioAgronomico.ReceituarioAgronomico>();
    }
}
