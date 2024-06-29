using Application.DTOs.Cadastros.AplicacaoRelatorio.ViewModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.AplicacaoRelatorio.Mappings
{
    public class StringAplicacaoRelatorioDomainToViewModelMappingProfile : Profile
    {
        public StringAplicacaoRelatorioDomainToViewModelMappingProfile()
        {
            CreateMap<StringAplicacaoRelatorioViewModel, Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio>()
                .ForMember(dest => dest.KG_LT, opt => opt.MapFrom(src => src.UnidadeDosagem))
                .ForMember(dest => dest.Alteracoes_Observacoes, opt => opt.MapFrom(src => src.Observacoes))
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Lat))
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Long));
        }
    }
}
