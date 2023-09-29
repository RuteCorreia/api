using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entidades.Cadastros.Executores
{
    public class Executor
    {
        public int IdExecutor { get; set; }
        public int? IdEmpresa { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [PasswordPropertyText]
        public string Senha { get; set; }

        [Required]
        public string CFTA { get; set; }

        public byte[] Assinatura { get; set; }
    }
}
