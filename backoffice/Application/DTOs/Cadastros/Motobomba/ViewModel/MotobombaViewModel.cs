namespace Application.DTOs.Cadastros.Motobomba.ViewModel
{
    public class MotobombaViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataUltimaTrocaOleo { get; set; }
        public IEnumerable<string>? Checklist { get; set; }
    }
}
