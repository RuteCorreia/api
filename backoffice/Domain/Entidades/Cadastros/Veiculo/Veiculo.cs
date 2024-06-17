using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.Veiculo;

public class Veiculo
{
    [Key]
    public int Id { get; set; }
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public string Placa { get; set; }
    public int KM_Inicial { get; set; }
    public int KM_Atual { get; set; }
    public int KM_Inspecao { get; set; }
    public int KM_EntreRevisoes { get; set; }
    public int CapacidadeLitros { get; set; }
    public int QtdAtualLitros { get; set; }

    [ForeignKey("Empresa")]
    public int? IdEmpresa { get; set; }

    [JsonIgnore]
    public virtual Empresa.Empresa? Empresa { get; set; }
}
