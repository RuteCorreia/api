using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.SubMenu.ViewModel
{
    public class SubMenuViewModel
    {
        public int SubMenuId { get; set; }
        public string Label { get; set; }
        public string Route { get; set; }
        public string Icon { get; set; }
        public int MenuItemId { get; set; }

    }
}
