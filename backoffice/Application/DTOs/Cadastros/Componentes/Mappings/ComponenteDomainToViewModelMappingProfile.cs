using Application.DTOs.Cadastros.Componentes.ViewModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.Componentes.Mappings
{
    public class ComponenteDomainToViewModelMappingProfile : Profile
    {
        public ComponenteDomainToViewModelMappingProfile()
        {
            CreateMap<Domain.Entidades.Cadastros.Componentes.Componentes, ComponentesViewModel>();
        }
    }
}
