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
    public class BulaAplicacaoDomainToViewModelMappingProfile : Profile
    {
        public BulaAplicacaoDomainToViewModelMappingProfile()
        {
            CreateMap<Domain.Entidades.Cadastros.Empresa.BulaAplicacao, BulaAplicacaoViewModel>();
        }
    }
}
