using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entidades.Cadastros.SubMenu
{
    public class SubMenu
    {
        public int SubMenuId { get; set; }
        public string Label { get; set; }
        public string Route { get; set; }

        [ForeignKey("MenuItem")]
        public int MenuItemId { get; set; }

        [JsonIgnore]
        public virtual Entidades.Cadastros.Menu.Menu MenuItem { get; set; }

    }
}
