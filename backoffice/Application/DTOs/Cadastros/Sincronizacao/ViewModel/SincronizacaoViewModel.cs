using Application.DTOs.Cadastros.Aeronave.ViewModel;
using Application.DTOs.Cadastros.AlturaVoo.ViewModel;
using Application.DTOs.Cadastros.AlvoBiologico.ViewModel;
using Application.DTOs.Cadastros.Bateria.ViewModel;
using Application.DTOs.Cadastros.Bula.ViewModel;
using Application.DTOs.Cadastros.Cliente.ViewModel;
using Application.DTOs.Cadastros.Cultura.ViewModel;
using Application.DTOs.Cadastros.Engenheiro.ViewModel;
using Application.DTOs.Cadastros.Equipamento.ViewModel;
using Application.DTOs.Cadastros.Executor.ViewModel;
using Application.DTOs.Cadastros.Gerador.ViewModel;
using Application.DTOs.Cadastros.Motobomba.ViewModel;
using Application.DTOs.Cadastros.Piloto.ViewModel;
using Application.DTOs.Cadastros.Pistas.ViewModel;
using Application.DTOs.Cadastros.Produto.ViewModel;
using Application.DTOs.Cadastros.Tipo_Produto.ViewModel;
using Application.DTOs.Cadastros.TipoDeFormulacao.ViewModel;
using Application.DTOs.Cadastros.TipoDeServico.ViewModel;
using Application.DTOs.Cadastros.TipoDeUnidade.ViewModel;
using Application.DTOs.Cadastros.Veiculante.ViewModel;
using Application.DTOs.Cadastros.Veiculo.ViewModel;

namespace Application.DTOs.Cadastros.Sincronizacao.ViewModel;

public class SincronizacaoViewModel
{
    public IEnumerable<AeronaveViewModel> Aeronaves { get; set; }
    public IEnumerable<AlvoBiologicoViewModel> AlvosBiologicos { get; set; }
    public IEnumerable<AlturaVooViewModel> Alturas { get; set; }
    public IEnumerable<BateriaViewModel> Baterias { get; set; }
    public IEnumerable<BulaAppViewModel> Bulas { get; set; }
    public IEnumerable<ClienteViewModel> Clientes { get; set; }
    public IEnumerable<CulturaViewModel> Culturas { get; set; }
    public IEnumerable<EquipamentoViewModel> Equipamentos { get; set; }
    public IEnumerable<GeradorViewModel> Geradores { get; set; }
    public IEnumerable<MotobombaViewModel> Motobombas { get; set; }
    public IEnumerable<PistaViewModel> Pistas { get; set; }
    public IEnumerable<ProdutoViewModel> Produtos { get; set; }
    public IEnumerable<TipoDeFormulacaoViewModel> TiposDeFormulacao { get; set; }
    public IEnumerable<TipoProdutoViewModel> TiposProduto { get; set; }
    public IEnumerable<TipoDeServicoViewModel> TiposDeServico { get; set; }
    public IEnumerable<TipoDeUnidadeViewModel> TiposDeUnidade { get; set; }
    public IEnumerable<PilotoViewModel> Pilotos { get; set; }
    public IEnumerable<ExecutorViewModel> Executores { get; set; }
    public IEnumerable<EngenheiroViewModel> Engenheiros { get; set; }
    public IEnumerable<VeiculanteViewModel> Veiculantes { get; set; }
    public IEnumerable<VeiculoViewModel> Veiculos { get; set; }
}
