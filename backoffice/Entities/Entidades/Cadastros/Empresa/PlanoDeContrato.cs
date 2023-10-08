using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entidades.Cadastros.Empresa
{
    public class PlanoDeContrato
    {
        [Key]
        public int IdPlano { get; set; }
        public string NomeDoPlano { get; set; }
    }
}
