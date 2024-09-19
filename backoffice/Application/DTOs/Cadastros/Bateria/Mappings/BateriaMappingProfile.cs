using Application.DTOs.Cadastros.Bateria.ViewModel;
using Application.DTOs.Cadastros.BulaAplicacao.ViewModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.Bateria.Mappings
{
    public class BateriaMappingProfile : Profile
    {
        public BateriaMappingProfile()
        {
            CreateMap<Domain.Entidades.Cadastros.Bateria.Bateria, BateriaViewModel>();
            CreateMap<BateriaViewModel, Domain.Entidades.Cadastros.Bateria.Bateria> ();
        }
    }
}
