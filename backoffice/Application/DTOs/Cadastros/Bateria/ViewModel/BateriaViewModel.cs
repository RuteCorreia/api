namespace Application.DTOs.Cadastros.Bateria.ViewModel
{
    public class BateriaViewModel
    {
        public int Id { get; set; }
        public string NomeBateria { get; set; }
        public string NumeroBateria { get; set; }
        public int CicloAtual { get; set; }
        public int CicloMaximo { get; set; }
    }
}
