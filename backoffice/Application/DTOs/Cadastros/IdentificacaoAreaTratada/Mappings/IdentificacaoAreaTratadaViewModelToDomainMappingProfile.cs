using Application.DTOs.Cadastros.Frota.ViewModel;
using Application.DTOs.Cadastros.IdentificacaoAreaTratada.ViewModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.IdentificacaoAreaTratada.Mappings
{
    public class IdentificacaoAreaTratadaViewModelToDomainMappingProfile : Profile
    {
        public IdentificacaoAreaTratadaViewModelToDomainMappingProfile()
        {
            CreateMap<IdentificacaoAreaTratadaViewModel, Domain.Entidades.Cadastros.IdentificacaoAreaTratada.IdentificacaoAreaTratada>();
        }
    }
}
