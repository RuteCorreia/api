using Application.DTOs.Cadastros.BulaAplicacao.ViewModel;
using Domain.Entidades.Cadastros.Empresa;
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
    public string? DoseProdutoComercial { get; set; }
    public string Adjuvante { get; set; }
    public int? IdTipoDeServico { get; set; }
    public int? TipoDeUnidade { get; set; }
    public List<BulaAplicacaoViewModel> BulaAplicacoes { get; set; }

}
