using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entidades.Cadastros.Cultura
{
    public class Cultura
    {
        [Key]
        public int IdCultura { get; set; }
        public string? Nome { get; set; }
        public string? AlvoBiologico { get; set; }
        public string? Status { get; set; }
    }
}
