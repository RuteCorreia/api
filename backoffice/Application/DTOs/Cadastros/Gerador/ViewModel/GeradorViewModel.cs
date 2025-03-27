namespace Application.DTOs.Cadastros.Gerador.ViewModel
{
    public class GeradorViewModel
    {
        public int Id { get; set; }
        public string NomeGerador { get; set; }
        public long QuantidadeHoras { get; set; }
        public DateTime DataUltimaTrocaOleo { get; set; }
        public long QuantidadeHorasTroca { get; set; }
        public IEnumerable<string>? Checklist { get; set; }
    }
}
