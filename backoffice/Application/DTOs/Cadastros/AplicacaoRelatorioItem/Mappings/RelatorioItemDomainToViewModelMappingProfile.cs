using Application.DTOs.Cadastros.AplicacaoRelatorioItem.ViewModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.AplicacaoRelatorioItem.Mappings
{
    public class RelatorioItemDomainToViewModelMappingProfile : Profile
    {
        public RelatorioItemDomainToViewModelMappingProfile()
        {
            CreateMap<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem, RelatorioItemViewModel>()
                .ForMember(dest => dest.HoraFinal, opt => opt.MapFrom(src => src.HoraTermino))
                .ForMember(dest => dest.HorimetroFinal, opt => opt.MapFrom(src => src.HorimetroTermino))
                .ForMember(dest => dest.UmidadeRelativaArInicial, opt => opt.MapFrom(src => src.UrInicial))
                .ForMember(dest => dest.UmidadeRelativaArFinal, opt => opt.MapFrom(src => src.UrFinal))
                .ForMember(dest => dest.ImagemCondicaoClimatica, opt => opt.MapFrom(src => src.ImagemDadosClimaticos));
        }
    }
}
