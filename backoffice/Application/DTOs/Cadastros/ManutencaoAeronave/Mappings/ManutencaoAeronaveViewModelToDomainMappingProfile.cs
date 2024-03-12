using Application.DTOs.Cadastros.Frota.ViewModel;
using Application.DTOs.Cadastros.ManutencaoAeronave.ViewModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.ManutencaoAeronave.Mappings
{
    public class ManutencaoAeronaveViewModelToDomainMappingProfile : Profile
    {
        public ManutencaoAeronaveViewModelToDomainMappingProfile()
        {
            CreateMap<ManutencaoAeronaveViewModel, Domain.Entidades.Cadastros.ManutencaoAeronave.ManutencaoAeronave>();
        }
    }
}
