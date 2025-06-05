using Application.DTOs.Cadastros.ReceituarioAgronomico.ViewModel;
using AutoMapper;
using Newtonsoft.Json;

namespace Application.DTOs.Cadastros.ReceituarioAgronomico.Mappings;

public class ReceituarioAgronomicoViewModelToDomainMappingProfile : Profile
{
    public ReceituarioAgronomicoViewModelToDomainMappingProfile()
    {
        CreateMap<ReceituarioAgronomicoViewModel, Domain.Entidades.Cadastros.ReceituarioAgronomico.ReceituarioAgronomico>()
            .ForMember(dest => dest.NomeArquivo,
                       opt => opt.MapFrom(src =>src.NomeArquivo.Data));
    }
}
