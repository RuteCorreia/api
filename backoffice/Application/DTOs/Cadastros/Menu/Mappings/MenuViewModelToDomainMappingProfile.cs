using Application.DTOs.Cadastros.Bula.ViewModel;
using Application.DTOs.Cadastros.Menu.ViewModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.Menu.Mappings
{
    public class MenuViewModelToDomainMappingProfile : Profile
    {
        public MenuViewModelToDomainMappingProfile()
        {
            CreateMap<MenuViewModel, Domain.Entidades.Cadastros.Menu.Menu>();

        }
    }
}
