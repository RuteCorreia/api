using Application.DTOs.Cadastros.AplicacaoRelatorioItem.ViewModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.AplicacaoRelatorioItem.Mappings
{
    public class AplicacaoRelatorioItemViewModelToDomainMappingProfile : Profile
    {
        public AplicacaoRelatorioItemViewModelToDomainMappingProfile()
        {
            CreateMap<AplicacaoRelatorioItemViewModel, Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem>()
                .ForMember(dest => dest.HoraTermino, opt => opt.MapFrom(src => src.HoraFinal))
                .ForMember(dest => dest.HorimetroTermino, opt => opt.MapFrom(src => src.HorimetroFinal))
                .ForMember(dest => dest.UrInicial, opt => opt.MapFrom(src => src.UmidadeRelativaArInicial))
                .ForMember(dest => dest.UrFinal, opt => opt.MapFrom(src => src.UmidadeRelativaArFinal))
                .ForMember(dest => dest.ImagemDadosClimaticos, opt => opt.MapFrom(src => src.ImagemCondicaoClimatica));
        }
    }
}
