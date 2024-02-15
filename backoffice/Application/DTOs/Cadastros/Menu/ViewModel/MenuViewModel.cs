using Application.DTOs.Cadastros.SubMenu.ViewModel;
using Domain.Entidades.Cadastros.SubMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.Menu.ViewModel
{
    public class MenuViewModel
    {
        public int MenuItemId { get; set; }
        public string Label { get; set; }
        public string Route { get; set; }
        public string Icon { get; set; }
        public List<SubMenuViewModel> SubMenus { get; set; } = new List<SubMenuViewModel>();
    }
}
