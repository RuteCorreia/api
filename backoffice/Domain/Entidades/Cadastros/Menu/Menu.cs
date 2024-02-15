using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades.Cadastros.Menu
{
    public class Menu
    {
        [Key]
        public int MenuItemId { get; set; }
        public string Label { get; set; }
        public string Route { get; set; }
        public string Icon { get; set; }
    }
}
