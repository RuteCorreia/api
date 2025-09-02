namespace Application.DTOs.Cadastros.Sincronizacao.ViewModel;

public class SincronizacaoRelatorioParams
{
    public List<int> IdsAplicacao { get; set; }
    public List<int> IdsFrota { get; set; }
    public DateTime DataUltimaAtualizacao { get; set; }
}
