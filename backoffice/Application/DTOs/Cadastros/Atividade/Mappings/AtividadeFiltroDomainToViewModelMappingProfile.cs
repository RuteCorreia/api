using Application.DTOs.Cadastros.Atividade.ViewModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.Atividade.Mappings
{
    public class AtividadeFiltroDomainToViewModelMappingProfile : Profile
    {
        public AtividadeFiltroDomainToViewModelMappingProfile()
        {
            CreateMap<Domain.Entidades.Cadastros.Atividade.AtividadeFiltro, AtividadeFiltroViewModel>()
                .ForMember(dest => dest.Aeronave, opt => opt.MapFrom(src => src.PrefixoAeronave))
                .ForMember(dest => dest.Cliente, opt => opt.MapFrom(src => src.Contratante))
                .ForMember(dest => dest.DataInicio, opt => opt.MapFrom(src => src.DataInicial))
                .ForMember(dest => dest.DataFim, opt => opt.MapFrom(src => src.DataFinal));
        }
    }
}
