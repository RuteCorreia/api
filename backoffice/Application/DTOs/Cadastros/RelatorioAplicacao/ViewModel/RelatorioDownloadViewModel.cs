using Application.DTOs.Cadastros.DataFormat.ViewModel;

namespace Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel
{
    public class RelatorioDownloadViewModel
    {
        public int Id { get; set; }
        public DataFormatViewModel? ReceituarioAgronomico { get; set; }
    }
}
