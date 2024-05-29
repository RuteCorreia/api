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
    public class ContratanteViewModelToDomainMappingProfile : Profile
    {
        public ContratanteViewModelToDomainMappingProfile()
        {
            CreateMap<ContratanteViewModel, Domain.Entidades.Cadastros.Contratante.Contratante>();
        }
    }
}
