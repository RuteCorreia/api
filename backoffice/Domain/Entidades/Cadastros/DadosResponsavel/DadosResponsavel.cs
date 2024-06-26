using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.DadosResponsavel;

public class DadosResponsavel
{
    [Key]
    public int Id { get; set; }
    public string? Data { get; set; }
    public string? UF { get; set; }
    public string? Cidade { get; set; }
    public string NomeCompleto { get; set; }
    public string? Documento { get; set; }
    public string? Telefone { get; set; }
    public string? PostoGraduacao { get; set; }
    public string? Re { get; set; }
    public byte[]? assinaturaResponsavel { get; set; }
    
    [ForeignKey("Empresa")]
    public int? IdEmpresa { get; set; }

    [JsonIgnore]
    public virtual Empresa.Empresa? Empresa { get; set; }
}
