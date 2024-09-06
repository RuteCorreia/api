using Application.DTOs.ExportExcel.ViewModel;

namespace Application.DTOs.Cadastros.RelatorioMapa.ViewModel
{
    public class RelatorioInfo
    {
        public List<RelatorioInfoViewModel> RelatoriosInfo { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
    }
}
