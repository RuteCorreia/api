namespace Application.DTOs.Cadastros.Atividade.ViewModel
{
    public class AtividadeFiltroViewModel
    {
        public string? PrefixoAeronave { get; set; }
        public string? Piloto { get; set; }
        public string? Executor { get; set; }
        public string? Contratante { get; set; }
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
    }
}
