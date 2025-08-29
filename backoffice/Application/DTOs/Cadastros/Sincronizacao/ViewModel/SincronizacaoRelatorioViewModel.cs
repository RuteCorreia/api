using Application.DTOs.Cadastros.AlturaVoo.ViewModel;
using Application.DTOs.Cadastros.AlvoBiologico.ViewModel;
using Application.DTOs.Cadastros.Controle_De_Frota.ViewModel;
using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;

namespace Application.DTOs.Cadastros.Sincronizacao.ViewModel;

public class SincronizacaoRelatorioViewModel
{
    public IEnumerable<RelatorioAplicacaoViewModel> RelatoriosAplicacao { get; set; }
    public IEnumerable<ControleDeFrotaViewModel> RelatoriosFrota { get; set; }
    public IEnumerable<ControleDeFrotaViewModel> RelatoriosIncendio { get; set; }
}
