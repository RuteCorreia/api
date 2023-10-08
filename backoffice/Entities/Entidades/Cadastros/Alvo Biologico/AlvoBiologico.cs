using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Entidades.Cadastros.Alvo_Biologico
{
    public class AlvoBiologico
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Produto")]
        public int? IdProduto { get; set; }
        public string Nome { get; set; }
        public string DoseProdutoPorHectare { get; set; }
        [JsonIgnore]
        public virtual Produtos.Produto? Produto { get; set; }
    }
}
