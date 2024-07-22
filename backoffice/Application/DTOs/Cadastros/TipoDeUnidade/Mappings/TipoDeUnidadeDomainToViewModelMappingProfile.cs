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
    public class TipoDeUnidadeDomainToViewModelMappingProfile : Profile
    {
        public TipoDeUnidadeDomainToViewModelMappingProfile()
        {
            CreateMap<Domain.Entidades.Cadastros.Alvo_Biologico.TipoDeUnidade, TipoDeUnidadeViewModel>();
        }
    }
}
