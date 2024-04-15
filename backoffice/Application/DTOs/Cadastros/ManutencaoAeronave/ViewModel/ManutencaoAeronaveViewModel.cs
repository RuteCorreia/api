using Application.DTOs.Cadastros.ManutencaoAeronaveItemsRevisao.ViewModel;

namespace Application.DTOs.Cadastros.ManutencaoAeronave.ViewModel
{
    public class ManutencaoAeronaveViewModel
    {
        public int Id { get; set; }
        public int? IdAeronave { get; set; }
        public string? HorimetroInicial { get; set; }
        public string? HorasRevisao { get; set; }
        public string? HorasInspecao { get; set; }
        public string? DocumentoBase64 { get; set; }
        public string? PrefixoAeronave { get; set; }
        public byte[]? Documento { get; set; }
        public IEnumerable<ManutencaoAeronaveItemsRevisaoViewModel> ItensRevisao { get; set; }
    }
}
