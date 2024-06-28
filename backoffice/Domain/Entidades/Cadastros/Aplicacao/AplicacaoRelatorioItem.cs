using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.Aplicacao;

public class AplicacaoRelatorioItem
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("AplicacaoRelatorio")]
    public int? IdAplicacaoRelatorio { get; set; }
    public string? HoraInicio { get; set; }
    public string? HorimetroInicial { get; set; }
    public string? HoraTermino { get; set; }
    public string? HorimetroTermino { get; set; }
    public string? TemperaturaInicial { get; set; }
    public string? TemperaturaFinal { get; set; }
    public string? UrInicial { get; set; }
    public string? UrFinal { get; set; }
    public string? VentoInicial { get; set; }
    public string? VentoFinal { get; set; }
    public string? ImagemDadosClimaticos { get; set; }
    public DateTime? DataAplicacao { get; set; }

    [JsonIgnore]
    public virtual AplicacaoRelatorio? AplicacaoRelatorio { get; set; }
}
