using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.Produto.ViewModel;

public class ProdutoViewModel
{
    public int Id { get; set; }
    public int? IdCultura { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3)]
    [MaxLength(120)]
    public string? Nome { get; set; }
    public string? ClassificacaoToxicologica { get; set; }
    public string? Classe { get; set; }
    public string? TipoDeFormulacao { get; set; }
    public string? TipoServico { get; set; }
    public string? IdEmpresa { get; set; }
    public int? IdTipoDeFormulacao { get; set; }
    public int? IdTipoDeServico { get; set; }
    public int ExclusaoCampo { get; set; }
    public int CampoAdiconado { get; set; }
}
