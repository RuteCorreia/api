using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entidades.Cadastros.MenuUsuario
{
    public class MenuUsuario
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Usuario")]
        public int? IdUsuario { get; set; }

        [ForeignKey("Menu")]
        public int? IdMenu { get; set; }

        [JsonIgnore]
        public virtual Menu.Menu? Menu { get; set; }

        [JsonIgnore]
        public virtual User.ApplicationUser? Usuario { get; set; }
    }
}
