using Entities.Entidades.Cadastros.Estados;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Entidades.Cadastros.Aplicacao
{
    public class AplicacaoAreaTratada
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Aplicacao")]
        public int? IdAplicacao { get; set; }

        [ForeignKey("Estado")]
        public int? IdEstado { get; set; }

        [ForeignKey("Cidade")]
        public int? IdCidade { get; set; }
        public string Localizacao { get; set; }
        public decimal? Extensao { get; set; }

        [JsonIgnore]
        public virtual Aplicacao? Aplicacao { get; set; }
        [JsonIgnore]
        public virtual Estados.Estados? Estado { get; set; }
        [JsonIgnore]
        public virtual Cidades.Cidades? Cidade { get; set; }
    }
}