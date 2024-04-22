using Domain.Enums;
using System.ComponentModel.DataAnnotations;

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
   
}