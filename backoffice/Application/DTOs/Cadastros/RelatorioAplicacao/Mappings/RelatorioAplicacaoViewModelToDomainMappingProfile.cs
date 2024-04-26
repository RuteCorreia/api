using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;
using Application.DTOs.Cadastros.SubMenu.ViewModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.RelatorioAplicacao.Mappings
{
    public class RelatorioAplicacaoViewModelToDomainMappingProfile : Profile
    {
        public RelatorioAplicacaoViewModelToDomainMappingProfile()
        {
            CreateMap<RelatorioAplicacaoViewModel, Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>();
        }
    }
}
