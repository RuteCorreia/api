using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entidades.Cadastros.Pilotos
{
    public class Piloto
    {
        public int IdPiloto { get; set; }
        public int? IdEmpresa { get; set; }

        [Required]
        public string NomePiloto { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }

        [Required]
        public string CDAC { get; set; }

        [Required]
        public byte[] Assinatura { get; set; }

        [Required]
        public string PorcentagemComissao { get; set; }
    }
}
