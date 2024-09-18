using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.Bula.ViewModel
{
    public class RecomendacaoViewModel
    {
        public int? IdBula { get; set; }
        public int IdCultura { get; set; }
        public int IdAlvoBiologico { get; set; }
        public string DoseProdutoComercial { get; set; }
        public int IdTipoDeUnidade { get; set; }
    }
}
