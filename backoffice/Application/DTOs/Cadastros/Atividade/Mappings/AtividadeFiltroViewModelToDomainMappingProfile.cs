using Application.DTOs.Cadastros.Atividade.ViewModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.Atividade.Mappings
{
    public class AtividadeFiltroViewModelToDomainMappingProfile : Profile
    {
        public AtividadeFiltroViewModelToDomainMappingProfile()
        {
            CreateMap<AtividadeFiltroViewModel, Domain.Entidades.Cadastros.Atividade.AtividadeFiltro>()
                .ForMember(dest => dest.PrefixoAeronave, opt => opt.MapFrom(src => src.Aeronave))
                .ForMember(dest => dest.Contratante, opt => opt.MapFrom(src => src.Cliente))
                .ForMember(dest => dest.DataInicial, opt => opt.MapFrom(src => src.DataInicio))
                .ForMember(dest => dest.DataFinal, opt => opt.MapFrom(src => src.DataFim));
        }
    }
}
