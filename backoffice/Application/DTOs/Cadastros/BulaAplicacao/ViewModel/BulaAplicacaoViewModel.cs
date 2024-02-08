using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.BulaAplicacao.ViewModel
{
    public class BulaAplicacaoViewModel
    {
        [Key]
        public int IdBulaAplicacao { get; set; }
        public int? IdCultura { get; set; }
        public int? IdAlvoBiologico { get; set; }
        public int? IdBula { get; set; }
        public string? DoseProdutoComercial { get; set; }
        public int? TipoDeUnidade { get; set; }
    }
}
