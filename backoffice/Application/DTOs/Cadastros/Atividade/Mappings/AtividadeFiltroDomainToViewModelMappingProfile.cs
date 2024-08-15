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
            CreateMap<Domain.Entidades.Cadastros.Atividade.AtividadeFiltro, AtividadeFiltroViewModel>();
        }
    }
}
