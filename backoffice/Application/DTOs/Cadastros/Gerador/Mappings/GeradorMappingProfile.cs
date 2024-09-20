using Application.DTOs.Cadastros.Gerador.ViewModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.Gerador.Mappings
{
    public class GeradorMappingProfile : Profile
    {
        public GeradorMappingProfile()
        {
            CreateMap<GeradorViewModel, Domain.Entidades.Cadastros.Gerador.Gerador>();
            CreateMap<Domain.Entidades.Cadastros.Gerador.Gerador, GeradorViewModel>();
        }
    }
}
