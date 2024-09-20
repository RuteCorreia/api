namespace Application.DTOs.Cadastros.Gerador.ViewModel
{
    public class GeradorViewModel
    {
        public int Id { get; set; }
        public string NomeGerador { get; set; }
        public int QuantidadeHoras { get; set; }
        public DateTime DataUltimaTrocaOleo { get; set; }
        public int QuantidadeHorasTroca { get; set; }
    }
}
