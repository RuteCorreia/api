namespace Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel
{
    public class DownloadRelatorioRequest
    {
        public List<RelatorioDownloadViewModel> Ids { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
    }
}
