using Entities.Entidades.Cadastros.Aeronaves;
using Entities.Entidades.Cadastros.Altura_Voo;
using Entities.Entidades.Cadastros.Tipo_Produto;
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
    public class AplicacaoRecomendacoesTecnicas
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Aplicacao")]
        public int? IdAplicacao { get; set; }

        [ForeignKey("Veiculante")]
        public int? IdVeiculante { get; set; }
        public int? QtdeVeiculante { get; set; }
        public int? LarguraFaixa { get; set; }
        public int? VolumeAplicacao { get; set; }

        [ForeignKey("Aeronave")]
        public int? IdAeronave { get; set; }

        [ForeignKey("AlturaVoo")]
        public int? IdAlturaVoo { get; set; }
        public string AlturaVooCustom { get; set; }
        public int? Temperatura { get; set; }
        public int? UrDoAR { get; set; }
        public int? VelocidadeVento { get; set; }

        [ForeignKey("TipoProduto")]
        public int? IdTipoDeProduto { get; set; }

        [ForeignKey("Equipamento")]
        public int? IdEquipamento { get; set; }
        public int? Angulo { get; set; }

        [JsonIgnore]
        public virtual Aplicacao? Aplicacao { get; set; }
        [JsonIgnore]
        public virtual Veiculante.Veiculante? Veiculante { get; set; }
        [JsonIgnore]
        public virtual Aeronave? Aeronave { get; set; }
        [JsonIgnore]
        public virtual AlturaVoo? AlturaVoo { get; set; }
        [JsonIgnore]
        public virtual TipoProduto? TipoProduto { get; set; }
        [JsonIgnore]
        public virtual Equipamento.Equipamento? Equipamento { get; set; }
    }
}
