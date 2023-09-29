using Entities.Entidades.Cadastros.Alvo_Biologico;
using Entities.Entidades.Cadastros.Cultura;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entidades.Cadastros.Empresa
{
    public class Bula
    {
        public int IdBula { get; set; }
        public string NomeProduto { get; set; }

        [ForeignKey("Cultura")]
        public int? IdCultura { get; set; }

        public int? IdClassificacaoToxicologica { get; set; }
        public string Classe { get; set; }
        public string TipoDeFormulacao { get; set; }

        [ForeignKey("AlvoBiologico")]
        public int? IdAlvoBiologico { get; set; }
        public int? DoseProdutoComercial { get; set; }
        public string Adjuvante { get; set; }
        public int? IdTipoDeServico { get; set; }
        public virtual Cultura.Cultura Cultura { get; set; }
        public virtual AlvoBiologico AlvoBiologico { get; set; }
    }
}
