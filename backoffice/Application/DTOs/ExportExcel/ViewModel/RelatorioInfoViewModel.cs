using Application.DTOs.Cadastros.DataFormat.ViewModel;

namespace Application.DTOs.ExportExcel.ViewModel
{
    public class RelatorioInfoViewModel
    {
        public int? Id { get; set; }
        public string? NomeRelatorio { get; set; }
        public DataFormatViewModel? ReceituarioAgronomico { get; set; }

    }
}
