using Domain.Entidades.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.Aplicacao;

public class Aplicacao
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Empresa")]
    public int? IdEmpresa { get; set; }
    public string StatusEnvio { get; set; }

    [ForeignKey("Piloto")]
    public Guid? IdPiloto { get; set; }

    [ForeignKey("Executor")]
    public Guid? IdExecutor { get; set; }

    [ForeignKey("Cliente")]
    public int? IdCliente { get; set; }

    [ForeignKey("Cultura")]
    public int? IdCultura { get; set; }

    [JsonIgnore]
    public virtual Empresa.Empresa? Empresa { get; set; }
    [JsonIgnore]
    public virtual Usuario? Piloto { get; set; }
    [JsonIgnore]
    public virtual Usuario? Executor { get; set; }
    [JsonIgnore]
    public virtual Cliente.Cliente? Cliente { get; set; }
    [JsonIgnore]
    public virtual Cultura.Cultura? Cultura { get; set; }

}
