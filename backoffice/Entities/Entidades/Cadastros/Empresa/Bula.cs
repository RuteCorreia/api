using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entidades.Cadastros.Empresa
{
    public class Bula
    {
        public int IdBula { get; set; }
        public string NomeProduto { get; set; }
        public int? IdCultura { get; set; }
        public int? IdClassificacaoToxicologica { get; set; }
        public string Classe { get; set; }
        public string TipoDeFormulacao { get; set; }
        public int? IdAlvoBiologico { get; set; }
        public int? DoseProdutoComercial { get; set; }
        public string Adjuvante { get; set; }
        public int? TipoDeServico { get; set; }

    }
}
