using Domain.Entidades.Cadastros.Alvo_Biologico;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entidades.Cadastros.Empresa
{
    public class BulaAplicacao
    {
        [Key]
        public int IdBulaAplicacao { get; set; }

        [ForeignKey("Cultura")]
        public int? IdCultura { get; set; }

        [ForeignKey("AlvoBiologico")]
        public int? IdAlvoBiologico { get; set; }

        [ForeignKey("Bula")]
        public int? IdBula { get; set; }
        public string? DoseProdutoComercial { get; set; }
        public int? TipoDeUnidade { get; set; }

        [JsonIgnore]
        public virtual Bula? Bula { get; set; }

        [JsonIgnore]
        public virtual Cultura.Cultura? Cultura { get; set; }
        [JsonIgnore]
        public virtual AlvoBiologico? AlvoBiologico { get; set; }
    }
}
