using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entidades.Cadastros.MenuUsuario
{
    public class MenuUsuario
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Usuario")]
        public string IdUsuario { get; set; }

        [ForeignKey("SubMenu")]
        public int? IdSubMenu { get; set; }

        [JsonIgnore]
        public virtual SubMenu.SubMenu? SubMenu { get; set; }

        [JsonIgnore]
        public virtual IdentityUser? Usuario { get; set; }

        [JsonIgnore]
        public int? IdCliente { get; set; } = 0;
    }
}
