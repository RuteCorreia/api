using Domain.Entidades.Importação_Planilha;
using Domain.Interfaces.Importação_Planilha;
using Infra.Configuracao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorio.Importação_Planilha
{
    public class ServicosPlanilhaRepository<T> : IServicosPlanilhaRepository<T> where T : class
    {
        private readonly ContextBase _contextBase;

        public ServicosPlanilhaRepository(ContextBase contextBase)
        {
            _contextBase = contextBase;
        }

        /// <summary>
        /// Conta a quantidade de registros de uma entidade específica no banco de dados, com base em um predicado.
        /// </summary>
        /// <typeparam name="TEntity">O tipo da entidade para contagem.</typeparam>
        /// <param name="db">O contexto do banco de dados.</param>
        /// <param name="predicate">O predicado para filtrar os registros.</param>
        /// <returns>A quantidade de registros no banco de dados.</returns>
        public int ContarRegistros<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _contextBase.Set<T>().Count(predicate);

        }

        public async void SalvarLoteRegistros<T1>(List<T1> registros) where T1 : class
        {            
            // Tamanho do lote para operações em lote.
            const int batchSize = 12500;

            // Calcula a quantidade de lotes necessários com base no tamanho do lote.
            var batchCount = Math.Ceiling((double)registros.Count / batchSize);

            // Divide a lista de registros em lotes e realiza operações em lote.
            for (int i = 0; i < batchCount; i++)
            {
                var batch = registros.Skip(i * batchSize).Take(batchSize);

                // Utiliza EFBatchOperation para inserir todos os registros do lote de uma vez.
                _contextBase.ChangeTracker.AutoDetectChangesEnabled = false;
                await _contextBase.AddRangeAsync(batch);
                await _contextBase.SaveChangesAsync();
                _contextBase.ChangeTracker.AutoDetectChangesEnabled = true;

                // Salva as mudanças no banco de dados.
                await _contextBase.SaveChangesAsync();
            }
        }

        public void SalvarPlanilha(string base64Planilha)
        {
            var context = new ContextBase();
            var importacaoPlanilha = new ImportacaoPlanilhas()
            {
                Base64Planilha = base64Planilha,
                DadosSalvo = false,
                DataPlanilha = DateTime.Now,
                Erro = false,
                IndexUltimaLinha = 0,
                IntPlanilhaImportada = 1,
                MenssagensProcessamento = "",
                QtdRegistroBanco = 0,
                QtdRegistroPlanilha = 0,
                DataProcessamento = DateTime.Now,
                NomeCliente = "teste"
            };

            context.ImportacaoPlanilha.Add(importacaoPlanilha);
            context.SaveChanges();
        }


        public ConfiguracaoPlanilha BuscarPlanilhaNaFila()
        {
            var context = new ContextBase();
            var planilha = context.ImportacaoPlanilha.Where(x => x.DadosSalvo == false && x.Erro == false).FirstOrDefault();

            var config = new ConfiguracaoPlanilha()
            {
                IdImportacao = planilha.Id,
                EnderecoPlanilha = planilha.Base64Planilha,
                QtdRegistroBanco = planilha.QtdRegistroBanco,
                QtdRegistroPlanilha = planilha.QtdRegistroPlanilha,
                DadosSalvos = planilha.DadosSalvo,
                IndexLinhaUltimaCarga = planilha.IndexUltimaLinha,
                DataPlanilha = planilha.DataPlanilha.ToString()
            };

            return config;
        }
    }
}
