namespace Domain.Entidades.Cadastros.Atividade
{
    public class AtividadeFiltro
    {
        public string? PrefixoAeronave { get; set; }
        public string? Piloto { get; set; }
        public string? Executor { get; set; }
        public string? Contratante { get; set; }
        public int? IdEmpresa { get; set; }
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
    }
}
