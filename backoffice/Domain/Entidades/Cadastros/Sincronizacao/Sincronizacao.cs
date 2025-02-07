using Domain.Entidades.Cadastros.Alvo_Biologico;
using Domain.Entidades.Cadastros.Empresa;
using Domain.Entidades.User;

namespace Domain.Entidades.Cadastros.Sincronizacao;

public class Sincronizacao
{
    public virtual IEnumerable<Aeronave.Aeronave>? Aeronaves { get; set; }
    public virtual IEnumerable<AlvoBiologico>? AlvosBiologicos { get; set; }
    public virtual IEnumerable<Altura_Voo.AlturaVoo>? Alturas { get; set; }
    public virtual IEnumerable<Bateria.Bateria>? Baterias { get; set; }
    public virtual IEnumerable<Bula>? Bulas { get; set; }
    public virtual IEnumerable<Cliente.Cliente>? Clientes { get; set; }
    public virtual IEnumerable<Cultura.Cultura>? Culturas { get; set; }
    public virtual IEnumerable<Equipamento.Equipamento>? Equipamentos { get; set; }
    public virtual IEnumerable<Gerador.Gerador>? Geradores { get; set; }
    public virtual IEnumerable<Pistas.Pista>? Pistas { get; set; }
    public virtual IEnumerable<Produto.Produto>? Produtos { get; set; }
    public virtual IEnumerable<TipoDeFormulacao.TipoDeFormulacao>? TiposDeFormulacao { get; set; }
    public virtual IEnumerable<Tipo_Produto.TipoProduto>? TiposProduto { get; set; }
    public virtual IEnumerable<TipoDeServico.TipoDeServico>? TiposDeServico { get; set; }
    public virtual IEnumerable<TipoDeUnidade>? TiposDeUnidade { get; set; }
    public virtual IEnumerable<Usuario>? Pilotos { get; set; }
    public virtual IEnumerable<Usuario>? Executores { get; set; }
    public virtual IEnumerable<Usuario>? Engenheiros { get; set; }
    public virtual IEnumerable<Veiculante.Veiculante>? Veiculantes { get; set; }
    public virtual IEnumerable<Veiculo.Veiculo>? Veiculos { get; set; }
}
