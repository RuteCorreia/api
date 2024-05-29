using Application.DTOs.Cadastros.Componentes.ViewModel;
using Application.DTOs.Cadastros.Contratante.ViewModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.Contratante.Mappings
{
    public class ContranteDomainToViewModelMappingProfile : Profile
    {
        public ContranteDomainToViewModelMappingProfile()
        {
            CreateMap<Domain.Entidades.Cadastros.Contratante.Contratante, ContratanteViewModel>();
        }
    }
}
