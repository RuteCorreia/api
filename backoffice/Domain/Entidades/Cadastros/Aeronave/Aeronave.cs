using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.Aeronave;

public class Aeronave
{
    [Key]
    public int Id { get; set; }
    public string? Fabricante { get; set; }
    public string? Prefixo { get; set; }
    public string? Modelo { get; set; }
    public string? SerialNumber { get; set; }
    public bool Removido { get; set; }
    public ETipoAeronave Tipo { get; set; }

    [ForeignKey("Empresa")]
    public int? IdEmpresa { get; set; }

    [JsonIgnore]
    public virtual Empresa.Empresa? Empresa { get; set; }

}