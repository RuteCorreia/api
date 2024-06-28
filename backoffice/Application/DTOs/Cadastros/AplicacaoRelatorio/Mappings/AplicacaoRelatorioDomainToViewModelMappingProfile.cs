using Application.DTOs.Cadastros.AplicacaoRelatorio.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.AplicacaoRelatorio.Mappings;

public class AplicacaoRelatorioDomainToViewModelMappingProfile : Profile
{
    public AplicacaoRelatorioDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio, AplicacaoRelatorioViewModel>()
            .ForMember(dest => dest.UnidadeDosagem, opt => opt.MapFrom(src => src.KG_LT))
            .ForMember(dest => dest.Observacoes, opt => opt.MapFrom(src => src.Alteracoes_Observacoes))
            .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Latitude))
            .ForMember(dest => dest.Long, opt => opt.MapFrom(src => src.Longitude));
    }
}
