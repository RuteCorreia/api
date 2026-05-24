using Application.DTOs.Cadastros.TipoDeServico.ViewModel;

namespace Application.DTOs.Cadastros.TipoDeFormulacao.ViewModel
{
    public class TipoDeFormulacaoViewModel
    {
        public int Id { get; set; }
        public string? NomeFormulacao { get; set; }
        public int? IdRef { get; set; }
    }
}
