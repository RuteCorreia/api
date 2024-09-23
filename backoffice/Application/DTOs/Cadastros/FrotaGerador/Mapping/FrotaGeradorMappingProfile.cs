using Application.DTOs.Cadastros.FrotaBateria.ViewModel;
using Application.DTOs.Cadastros.FrotaGerador.ViewModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.FrotaGerador.Mapping
{
    public class FrotaGeradorMappingProfile : Profile
    {
        public FrotaGeradorMappingProfile()
        {
            CreateMap<FrotaGeradorViewModel, Domain.Entidades.Cadastros.FrotaGerador.FrotaGerador>();
            CreateMap<Domain.Entidades.Cadastros.FrotaGerador.FrotaGerador, FrotaGeradorViewModel>();
        }
    }
}
