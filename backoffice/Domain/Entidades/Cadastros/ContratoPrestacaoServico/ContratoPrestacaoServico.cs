using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.ContratoPrestacaoServico;

public class ContratoPrestacaoServico
{
    [Key]
    public int Id { get; set; }
    public string DistanciaPista { get; set; }
    public string Preco { get; set; }
    public string UnidadePreco { get; set; }
    public string Extensao { get; set; }
    public string ValorTotal { get; set; }
    public string Vencimento { get; set; }
    public string NomePiloto { get; set; }
    public string Executor { get; set; }
    
    [ForeignKey("Empresa")]
    public int? IdEmpresa { get; set; }

    [JsonIgnore]
    public virtual Empresa.Empresa? Empresa { get; set; }
}
