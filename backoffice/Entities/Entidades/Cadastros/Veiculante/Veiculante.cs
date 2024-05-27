using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entidades.Cadastros.Veiculante
{
    public class Veiculante
    {
        [Key]
        public int IdVeiculante { get; set; }
        public string Nome { get; set; }
    }
}
