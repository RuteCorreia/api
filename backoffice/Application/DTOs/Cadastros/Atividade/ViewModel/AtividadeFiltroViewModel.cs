using Newtonsoft.Json;

namespace Application.DTOs.Cadastros.Atividade.ViewModel
{
    public class AtividadeFiltroViewModel
    {
        public string? Aeronave { get; set; }
        public string? Piloto { get; set; }
        public string? Executor { get; set; }
        public string? Cliente { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}
