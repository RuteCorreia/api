using Application.DTOs.Cadastros.MenuUsuario.ViewModel;
using Application.DTOs.Cadastros.SubMenu.ViewModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.SubMenu.Mappings
{
    public class SubMenuViewModelToDomainMappingProfile : Profile
    {
        public SubMenuViewModelToDomainMappingProfile()
        {
            CreateMap<SubMenuViewModel, Domain.Entidades.Cadastros.SubMenu.SubMenu>();

        }
    }
}
