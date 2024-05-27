using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.AplicacaoCroquiImportacao.ViewModel;

public class AplicacaoCroquiImportacaoViewModel
{
    public int Id { get; set; }
    public int? IdAplicacaoCroqui { get; set; }
    public string Arquivo { get; set; }
}
