using Application.DTOs.Cadastros.BulaAplicacao.ViewModel;
using Domain.Entidades.Cadastros.Empresa;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.Bula.ViewModel;

public class BulaViewModel
{
    public int IdBula { get; set; }
    public int IdProduto { get; set; }
    public List<RecomendacaoViewModel> Recomendacoes { get; set; }

}
