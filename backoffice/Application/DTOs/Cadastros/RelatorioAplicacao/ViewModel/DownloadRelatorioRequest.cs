namespace Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel
{
    public class DownloadRelatorioRequest
    {
        public List<int> Ids { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
    }
}
