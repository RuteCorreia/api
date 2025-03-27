using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.FrotaGerador
{
    public class FrotaGerador
    {
        public int Id { get; set; }
        [ForeignKey("Gerador")]
        public int? IdGerador { get; set; }
        [ForeignKey("ControleDeFrota")]
        public int? IdFrota { get; set; }
        public DateTime? CreatedAt { get; set; }
        public double? HoraInicio { get; set; }
        public double? HoraFim { get; set; }
        public double? HorasUso { get; set; }
        public DateTime? DataTrocaOleo { get; set; }
        [ForeignKey("Empresa")]
        public int? IdEmpresa { get; set; }
        public string? CheckList { get; set; }
        [JsonIgnore]
        public virtual Empresa.Empresa? Empresa { get; set; }
        [JsonIgnore]
        public virtual Gerador.Gerador? Gerador { get; set; }
        [JsonIgnore]
        public virtual Controle_De_Frota.ControleDeFrota? ControleDeFrota { get; set; }
    }
}
