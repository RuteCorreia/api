using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entidades.Cadastros.Tipo_Produto
{
    public class TipoProduto
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
