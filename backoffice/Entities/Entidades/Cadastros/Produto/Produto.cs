using Entities.Entidades.Cadastros.Cultura;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Entidades.Cadastros.Produtos
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Cultura")]
        public int? IdCultura { get; set; }
        public string Nome { get; set; }
        public string ClassificacaoToxicologica { get; set; }
        public string Classe { get; set; }
        public string TipoDeFormulacao { get; set; }
        public string TipoServico { get; set; }
        [JsonIgnore]
        public virtual Cultura.Cultura? Cultura { get; set; }
    }
}
