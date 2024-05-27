using Application.DTOs.Cadastros.Bula.ViewModel;
using Application.DTOs.Cadastros.BulaAplicacao.ViewModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.BulaAplicacao.Mappings
{
    public class BulaAplicacaoViewModelToDomainMappingProfile : Profile
    {
        public BulaAplicacaoViewModelToDomainMappingProfile()
        {
            CreateMap<BulaAplicacaoViewModel, Domain.Entidades.Cadastros.Empresa.BulaAplicacao>();
        }
    }
}
