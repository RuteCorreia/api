using Application.DTOs.Cadastros.TipoDeServico.ViewModel;
using Application.DTOs.Cadastros.TipoDeUnidade.ViewModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.TipoDeUnidade.Mappings
{
    public class TipoDeUnidadeViewModelToDomainMappingProfile : Profile
    {
        public TipoDeUnidadeViewModelToDomainMappingProfile()
        {
            CreateMap<TipoDeUnidadeViewModel, Domain.Entidades.Cadastros.Alvo_Biologico.TipoDeUnidade>();
        }
    }
}
