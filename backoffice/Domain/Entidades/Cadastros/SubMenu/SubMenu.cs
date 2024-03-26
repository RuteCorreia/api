using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.SubMenu
{
    public class SubMenu
    {
        public int SubMenuId { get; set; }
        public string Label { get; set; }
        public string Route { get; set; }
        public string Icon { get; set; }

        [ForeignKey("MenuItem")]
        public int? MenuItemId { get; set; }

        [ForeignKey("SubMenuItem")]
        public int? SubMenuItemId { get; set; }

        [JsonIgnore]
        public virtual Menu.Menu? MenuItem { get; set; }

        [JsonIgnore]
        public virtual SubMenu? SubMenuItem { get; set; }

    }
}
