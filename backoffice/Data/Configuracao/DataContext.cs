using Entities.Entidades.Cadastros.Adjuvante;
using Entities.Entidades.Cadastros.Aeronaves;
using Entities.Entidades.Cadastros.Altura_Voo;
using Entities.Entidades.Cadastros.Alvo_Biologico;
using Entities.Entidades.Cadastros.Aplicacao;
using Entities.Entidades.Cadastros.Cidades;
using Entities.Entidades.Cadastros.Cliente;
using Entities.Entidades.Cadastros.CombateIncendio;
using Entities.Entidades.Cadastros.Combustivel;
using Entities.Entidades.Cadastros.Controle_De_Frota;
using Entities.Entidades.Cadastros.Cultura;
using Entities.Entidades.Cadastros.Empresa;
using Entities.Entidades.Cadastros.Engenheiros;
using Entities.Entidades.Cadastros.Equipamento;
using Entities.Entidades.Cadastros.Estados;
using Entities.Entidades.Cadastros.Executores;
using Entities.Entidades.Cadastros.Frota;
using Entities.Entidades.Cadastros.Pilotos;
using Entities.Entidades.Cadastros.Pistas;
using Entities.Entidades.Cadastros.Precificacao;
using Entities.Entidades.Cadastros.Produtos;
using Entities.Entidades.Cadastros.Tipo_Produto;
using Entities.Entidades.Cadastros.Veiculante;
using Entities.Entidades.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        #region Básicos
        public DbSet<Estados> Estados { get; set; }
        public DbSet<Cidades> Cidades { get; set; }
        public DbSet<AlturaVoo> AlturaVoo { get; set; }
        public DbSet<Adjuvante> Adjuvante { get; set; }
        public DbSet<Equipamento> Equipamento { get; set; }
        public DbSet<Combustivel> Combustivel { get; set; }
        public DbSet<Veiculante> Veiculante { get; set; }
        public DbSet<Cultura> Cultura { get; set; }
        #endregion

        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<PlanoDeContrato> PlanoDeContrato { get; set; }
        public DbSet<Bula> Bula { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Piloto> Piloto { get; set; }
        public DbSet<Engenheiro> Engenheiro { get; set; }
        public DbSet<Executor> Executor { get; set; }
        public DbSet<Precificacao> Precificacao { get; set; }
        public DbSet<Pista> Pista { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<AlvoBiologico> AlvoBiologico { get; set; }
        public DbSet<Frota> Frota { get; set; }
        public DbSet<Aeronave> Aeronave { get; set; }
        public DbSet<TipoProduto> TipoProduto { get; set; }
        public DbSet<Aplicacao> Aplicacao { get; set; }
        public DbSet<AplicacaoAreaTratada> AplicacaoAreaTratada { get; set; }
        public DbSet<AplicacaoCroqui> AplicacaoCroqui { get; set; }
        public DbSet<AplicacaoCroquiImportacao> AplicacaoCroquiImportacao { get; set; }
        public DbSet<AplicacaoCaracteristicas> AplicacaoCaracteristicas { get; set; }
        public DbSet<AplicacaoRecomendacoesTecnicas> AplicacaoRecomendacoesTecnicas { get; set; }
        public DbSet<AplicacaoRelatorio> AplicacaoRelatorio { get; set; }
        public DbSet<AplicacaoRelatorioItem> AplicacaoRelatorioItem { get; set; }
        public DbSet<AplicacaoLog> AplicacaoLog { get; set; }
        public DbSet<AplicacaoContrato> AplicacaoContrato { get; set; }
        public DbSet<CombateIncendio> CombateIncendio { get; set; }
        public DbSet<CombateIncendioDecolagemPouso> CombateIncendioDecolagemPouso { get; set; }
        public DbSet<ControleDeFrota> ControleDeFrota { get; set; }
    }
}
