using Application.DTOs.Cadastros.Menu.ViewModel;
using Application.DTOs.Cadastros.MenuUsuario.ViewModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.MenuUsuario.Mappings
{
    public class MenuUsuarioViewModelToDomainMappingProfile : Profile
    {
        public MenuUsuarioViewModelToDomainMappingProfile()
        {
            CreateMap<MenuUsuarioViewModel, Domain.Entidades.Cadastros.MenuUsuario.MenuUsuario>();

        }
    }
}
