using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entidades.Cadastros.Empresa
{
    public class Empresa
    {
        [Key]
        public int IdEmpresa { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public byte[] Imagem { get; set; }

        [Required]
        public int? PlanoContratado { get; set; }

    }
}
