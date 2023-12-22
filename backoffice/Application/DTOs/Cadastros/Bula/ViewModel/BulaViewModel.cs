using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.Bula.ViewModel;

public class BulaViewModel
{
    public int IdBula { get; set; }
    public string NomeProduto { get; set; }
    public int? IdCultura { get; set; }
    public int? IdClassificacaoToxicologica { get; set; }
    public string Classe { get; set; }
    public string TipoDeFormulacao { get; set; }
    public int? IdAlvoBiologico { get; set; }
    public int? DoseProdutoComercial { get; set; }
    public string Adjuvante { get; set; }
    public int? IdTipoDeServico { get; set; }
}
