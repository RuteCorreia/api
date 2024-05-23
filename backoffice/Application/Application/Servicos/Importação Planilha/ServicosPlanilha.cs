using Application.DTOs.Importação_Planilha;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader;
using Application.DTOs.Importação_Planilha.ViewModel;
using Application.DTOs.Cadastros.Adjuvante.Interface;
using Domain.Interfaces.Importação_Planilha;
using Domain.Entidades.Cadastros.Empresa;
using Domain.Entidades.Importação_Planilha;
using Infra.Repositorio.Importação_Planilha;
using Infra.Configuracao;
using Application.DTOs.Cadastros.BulaAplicacao.ViewModel;

namespace Application.Application.Servicos.Importação_Planilha
{
    public class ServicosPlanilha<T> : IServicosPlanilha<T> where T : class
    {
        private readonly IServicosPlanilhaRepository<T> _servicosPlanilhaRepository;

        /// <summary>
        /// Contador de registros faltantes durante o processamento.
        /// </summary>
        private int _qtdeRegistroFalta;

        /// <summary>
        /// Número máximo de registros a serem processados em uma única execução.
        /// </summary>
        private int _maximoRegitroPorProcessamento;

        /// <summary>
        /// Índice da última carga de linha realizada.
        /// </summary>
        private int? _ultimaCargaIndexLinha;

        // Propriedades da planilha Excel

        /// <summary>
        /// Nome da planilha Excel a ser importada.
        /// </summary>
        private string _nomePlanilha;

        /// <summary>
        /// Propriedade que representa o endereço da planilha Excel.
        /// </summary>
        private PropertyInfo _endereceoPlanilha;

        /// <summary>
        /// Propriedade que representa a quantidade de registros na planilha Excel.
        /// </summary>
        private PropertyInfo _qtdRegistroPlanilha;

        /// <summary>
        /// Propriedade que representa a quantidade de registros no banco de dados.
        /// </summary>
        private PropertyInfo _qtdRegistroBanco;

        /// <summary>
        /// Propriedade que representa os dados salvos durante o processamento.
        /// </summary>
        private PropertyInfo _dadosSalvos;

        /// <summary>
        /// Propriedade que armazena o índice da última carga de linha.
        /// </summary>
        private PropertyInfo _ultimaCargaIndexLinhaProp;

        /// <summary>
        /// Dicionário que mapeia índices de coluna para seus respectivos nomes.
        /// </summary>
        private readonly Dictionary<int, string> _colunas;

        // Outras propriedades e variáveis

        /// <summary>
        /// Nome da tabela onde os dados serão armazenados.
        /// </summary>
        private string _nomeTabela;

        /// <summary>
        /// Data específica relacionada à importação.
        /// </summary>
        private string _dataEspecifica;

        /// <summary>
        /// Leitor de dados utilizado para processar a planilha Excel.
        /// </summary>
        private IExcelDataReader _dadosPlanilha;

        /// <summary>
        /// Conjunto de dados que armazena os dados da planilha Excel.
        /// </summary>
        private DataSet _dadosPlanilhaDatSet = new DataSet();

        /// <summary>
        /// Tabela de dados utilizada durante o processamento.
        /// </summary>
        private DataTable _dataTable;

        /// <summary>
        /// Instância responsável por realizar a importação de planilhas.
        /// </summary>
        private ImportacaoPlanilhas _importacao;

        /// <summary>
        /// Configurações específicas da planilha utilizadas durante o processamento.
        /// </summary>
        private ConfiguracoesPlanilhaViewModel _configuracoesPlanilha;

        /// <summary>
        /// Indica se existem modelos sem template associado.
        /// </summary>
        private bool _modelosSemTemplante;

        /// <summary>
        /// Lista que armazena o mapeamento entre colunas e propriedades.
        /// </summary>
        //private List<DeParaTemplatePlanilha> _mapeamentoColunaPropriedade = new List<DeParaTemplatePlanilha>();

        /// <summary>
        /// Data de criação interna utilizada durante o processamento.
        /// </summary>
        private string _dataCriacaoInterno;

        /// <summary>
        /// Coluna atual da planilha sendo processada.
        /// </summary>
        private string _colunaPlaninhaAtual;

        /// <summary>
        /// Coluna atual do banco de dados sendo processada.
        /// </summary>
        private string _colunaBancoAtual;

        /// <summary>
        /// Tipo da propriedade sendo manipulada durante o processamento.
        /// </summary>
        private string _tipoPropridade;


        /// <summary>
        /// Inicializa uma instância da classe ServicosPlanilha com as configurações especificadas.
        /// </summary>
        /// <param name="configuracao">Configurações da planilha a serem utilizadas pelo serviço.</param>
        /// <param name="nomePlanilha">Nome identificador da planilha.</param>
        /// <typeparam name="T">Tipo de dados associado à planilha.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// É lançada quando as configurações fornecidas são nulas.
        /// </exception>
        public ServicosPlanilha(ConfiguracoesPlanilhaViewModel configuracao, string nomePlanilha)
        {
            // Verifica se as configurações foram fornecidas
            if (configuracao == null)
            {
                // Lança uma exceção de argumento nulo
                throw new ArgumentNullException(nameof(configuracao));
            }

            // Inicializa as propriedades da classe com os valores fornecidos
            _nomePlanilha = nomePlanilha;
            _dataEspecifica = configuracao.DataPlanilha;
            _configuracoesPlanilha = configuracao;
            _maximoRegitroPorProcessamento = 12500;

            // Obtém o dicionário de colunas associado ao tipo de dados T
            _colunas = ObterDicionarioColunas<T>();

        }

        public static Dictionary<int, string> ObterDicionarioColunas<T>()
        {
            // Get the properties of the type T
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Create a dictionary mapping column indices to property names
            var dictionary = new Dictionary<int, string>();

            for (int i = 0; i < properties.Length; i++)
            {
                dictionary[i] = properties[i].Name;
            }

            return dictionary;
        }

        /// <summary>
        /// Inicia o processamento de dados para o serviço da planilha.
        /// </summary>
        public void IniciarProcessamento()
        {
            try
            {
                // Obtém o endereço da planilha e a configuração de dados salvos
                var endereco = _configuracoesPlanilha.EnderecoPlanilha;
                var dadosSalvo = _configuracoesPlanilha.DadosSalvos;

                // Se os dados estiverem marcados como salvos e o arquivo existir, exclui o arquivo
                if (dadosSalvo && File.Exists(endereco))
                {
                    File.Delete(endereco);
                }

                // Se o arquivo existir, valida os dados; caso contrário, atualiza a marcação de dados salvos
                if (File.Exists(endereco))
                {
                    ProcessarEhValidarDadosPlanilha();
                }
                else
                {
                    //AtualizaDadosSalvo(true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Inicia o processamento e validação dos dados da planilha associada ao serviço antes da inserção no banco de dados.
        /// </summary>
        private void ProcessarEhValidarDadosPlanilha()
        {
            try
            {
                // Abre a planilha para leitura
                using (var dadosPlanilha = File.OpenRead(_configuracoesPlanilha.EnderecoPlanilha))
                {
                    // Realiza a leitura dos dados da planilha
                    ExtrairEhCarregarDadosExcel(dadosPlanilha);

                    // Verifica se há dados na planilha
                    if (!_dadosPlanilha.Read())
                    {
                        return;
                    }

                    // Calcula a quantidade de registros em falta em comparação com o banco de dados
                    _qtdeRegistroFalta = _dadosPlanilha.RowCount - ObtenhaQtdRegistrosBanco();

                    // Verifica se há dados a serem inseridos no banco de dados
                    if (_qtdeRegistroFalta <= 0)
                    {
                        return;
                    }

                    // Realiza a validação do tipo de inserção
                    ValideTipoInsercao();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        /// <summary>
        /// Extrai dados de um arquivo Excel utilizando a biblioteca IExcelDataReader e carrega-os em um DataSet.
        /// </summary>
        /// <param name="dadosPlanilha">FileStream contendo os dados da planilha Excel.</param>
        private void ExtrairEhCarregarDadosExcel(FileStream dadosPlanilha)
        {
            // Cria um leitor de Excel usando a biblioteca IExcelDataReader
            if (_configuracoesPlanilha.EnderecoPlanilha.Contains("xlsx"))
            {
                IExcelDataReader excelLeitura = ExcelReaderFactory.CreateOpenXmlReader(dadosPlanilha);

                // Carrega os dados da planilha para um DataSet e mantém uma referência ao leitor
                _dadosPlanilhaDatSet = excelLeitura.AsDataSet();
                _dadosPlanilha = excelLeitura;
            }
            else
            {
                IExcelDataReader excelLeitura = ExcelReaderFactory.CreateBinaryReader(dadosPlanilha);

                // Carrega os dados da planilha para um DataSet e mantém uma referência ao leitor
                _dadosPlanilhaDatSet = excelLeitura.AsDataSet();
                _dadosPlanilha = excelLeitura;
            }
        }

        /// <summary>
        /// Extrai e carrega dados de um arquivo Excel para um DataSet.
        /// </summary>
        /// <param name="dadosPlanilha">FileStream contendo os dados da planilha Excel.</param>
        /// <summary>
        /// Obtém a quantidade de registros associados ao tipo de planilha no banco de dados.
        /// </summary>
        /// <returns>A quantidade de registros no banco de dados.</returns>
        private int ObtenhaQtdRegistrosBanco()
        {
            int quantidade = 0;

            switch (_nomePlanilha)
            {
                case "Bula":
                    // Create a predicate that always returns true to count all records
                    Expression<Func<Bula, bool>> predicate = b => true;
                    quantidade = _servicosPlanilhaRepository.ContarRegistros(predicate);
                    break;

                default:
                    quantidade = 0;
                    break;
            }

            return quantidade;

        }



        /// <summary>
        /// Realiza a validação do tipo de inserção com base no nome da planilha e executa a inserção correspondente.
        /// </summary>
        private void ValideTipoInsercao()
        {
            // Determina a tabela a ser utilizada com base no nome da planilha
            DataTable tabela = (_nomePlanilha == "Dynamics027") ? _dadosPlanilhaDatSet.Tables[1] : _dadosPlanilhaDatSet.Tables[0];

            // Verifica se a quantidade de registros no banco é zero
            if (_configuracoesPlanilha.QtdRegistroBanco == 0)
            {
                // Realiza a inserção com ou sem template com base no nome da planilha
                RealizeInsercao(tabela);
                return;
            }

            // Obtém a quantidade de registros no banco
            var registroIniciar = ObtenhaQtdRegistrosBanco();

            // Realiza a inserção com ou sem template com base no nome da planilha e na quantidade de registros no banco
            RealizeInsercao(tabela, registroIniciar);
        }

        /// <summary>
        /// Realiza a inserção com ou sem template com base no nome da planilha.
        /// </summary>
        /// <param name="tabela">A tabela a ser utilizada para a inserção.</param>
        /// <param name="registroIniciar">O índice da linha a partir do qual iniciar a inserção.</param>
        private void RealizeInsercao(DataTable tabela, int registroIniciar = 0)
        {
            // Executa a inserção com ou sem template

            InsiraComTemplate(tabela, registroIniciar);

        }

        /// <summary>
        /// Realiza a inserção de registros a partir de uma tabela DataTable, utilizando um template de mapeamento de coluna para propriedade.
        /// Remove a linha de título da tabela, realiza o mapeamento de coluna para propriedade por meio do método 'MapeamentoColunaPropriedadePorPlanilha',
        /// e, se aplicável, insere os registros utilizando o método 'Insira'. Em seguida, processa e salva os registros restantes.
        /// </summary>
        /// <param name="table">A tabela DataTable contendo os dados a serem inseridos.</param>
        /// <param name="registroIniciar">O índice da linha a partir do qual iniciar a inserção.</param>
        private void InsiraComTemplate(DataTable table, int registroIniciar = 0)
        {
            // Atribui a tabela DataTable à variável de instância _dataTable
            _dataTable = table;

            // Remove a linha de título da planilha Excel
            RemoveTituloPLaninhaExecel();

            // Estabelece o valor da propriedade '_modelosSemTemplate' por meio do método 'MapeamentoColunaPropriedadePorPlanilha'
            //MapeamentoColunaPropriedadePorPlanilha();

            // Inicializa as variáveis necessárias
            var registrosSalvar = new List<T>();
            var contadorRegistro = 0;

            // Processa os registros para importação, começando a partir do índice especificado
            ProcessarRegitrosParaImportacao(registroIniciar, registrosSalvar, ref contadorRegistro);

            // Salva os registros processados, se houver algum a ser salvo
            if (registrosSalvar.Count > 0)
            {
                SalveLoteRegistros(registrosSalvar);
            }

            // Calcula a quantidade de registros restantes na tabela
            var registrosRestantes = _dataTable.Rows.Count - ObtenhaQtdRegistrosBanco();
            // Atualiza a configuração da quantidade máxima de registros por processamento
            _maximoRegitroPorProcessamento = registrosRestantes < _maximoRegitroPorProcessamento ? registrosRestantes : _maximoRegitroPorProcessamento;

            // Verifica se atingiu a quantidade máxima de registros por processamento
            if (contadorRegistro >= _maximoRegitroPorProcessamento)
            {
                //AtualizaConfQtdRegistros();
                return;
            }
        }

        /// <summary>
        /// Processa as linhas da tabela DataTable para importação, aplicando regras específicas para cada registro.
        /// Os registros processados são adicionados à lista 'registrosSalvar'. O processamento é limitado pelo número máximo
        /// de registros por processamento (_maximoRegitroPorProcessamento).
        /// </summary>
        /// <param name="registroIniciar">O índice da linha a partir do qual iniciar o processamento.</param>
        /// <param name="registrosSalvar">Lista para armazenar os registros processados.</param>
        /// <param name="contadorRegistro">Contador de registros processados.</param>
        private void ProcessarRegitrosParaImportacao(int registroIniciar, List<T> registrosSalvar, ref int contadorRegistro)
        {
            // Inicializa o índice da linha
            var indexLinha = 0;

            // Define a data de criação interno como o momento atual
            _dataCriacaoInterno = DateTime.Now.ToString();

            // Itera sobre as linhas da tabela até atingir o número máximo de registros por processamento ou o final da tabela
            while (indexLinha < _dataTable.Rows.Count && contadorRegistro < _maximoRegitroPorProcessamento)
            {
                // Obtém a linha atual da tabela
                var linha = _dataTable.Rows[indexLinha];

                // Verifica se a linha atingiu o índice de início especificado
                if (indexLinha >= registroIniciar)
                {
                    // Cria uma instância do tipo T (registro)
                    var registro = Activator.CreateInstance<T>();

                    // Processa as regras específicas para a linha e preenche o registro
                    ProcessaRegras(linha, registro);

                    // Adiciona o registro processado à lista
                    registrosSalvar.Add(registro);

                    // Incrementa o contador de registros processados
                    contadorRegistro++;
                }

                // Incrementa o índice da linha
                indexLinha++;

                // Atualiza o índice da última carga
                _ultimaCargaIndexLinha = indexLinha;
            }
        }

        /// <summary>
        /// Obtém um dicionário que mapeia o nome da propriedade para a PropertyInfo correspondente na classe genérica T.
        /// </summary>
        /// <returns>Um dicionário contendo o nome da propriedade e sua PropertyInfo correspondente.</returns>
        private static Dictionary<string, PropertyInfo> ObterPropriedades()
        {
            return new Dictionary<string, PropertyInfo>
            {
                { "IndexLinha", typeof(T).GetProperty("IndexLinha") },
                { "DataCriacaoInterno", typeof(T).GetProperty("DataCriacaoInterno") },
                { "IdEmpresaResponsavel", typeof(T).GetProperty("IdEmpresaResponsavel") },
            };
        }

        /// <summary>
        /// Obtém um dicionário que mapeia o nome da classe para o seu tipo correspondente.
        /// </summary>
        /// <returns>Um dicionário contendo o nome da classe e seu tipo correspondente.</returns>
        private static Dictionary<string, Type> ObterTiposDeClasse()
        {
            return new Dictionary<string, Type>
            {
                { "Bula", typeof(Bula) },
            };
        }

        /// <summary>
        /// Processa as regras para preenchimento de propriedades de um registro com base nos dados de uma linha da tabela.
        /// </summary>
        /// <param name="linha">A linha da tabela contendo os dados a serem processados.</param>
        /// <param name="registro">O registro a ser preenchido com os dados processados.</param>
        private void ProcessaRegras(DataRow linha, T registro)
        {
            // Itera sobre as colunas da linha
            for (var indexColuna = 0; indexColuna < linha.ItemArray.Length; indexColuna++)
            {
                // Obtém o valor da coluna na linha
                _colunaPlaninhaAtual = linha.ItemArray[indexColuna].ToString();

                // Verifica se o modelo está sem template e, se aplicável, utiliza a variavel global _colunas que tem o mapeamento da classe toda da planilha importada'
                if (_modelosSemTemplante)
                {
                    // Obtém o nome da coluna no banco de dados
                    _colunaBancoAtual = _colunas[indexColuna];

                    // Aplica regras específicas para propriedades simples
                    AplicaRegrasPropriedades(registro);
                }
                //else
                //{
                //    // Obtém o nome da coluna no banco de dados
                //    _colunaBancoAtual = _mapeamentoColunaPropriedade[indexColuna].ColunaBanco;

                //    // Obtém o tipo da propriedade na classe genérica T
                //    _tipoPropridade = typeof(T).GetProperty(_colunaBancoAtual).ToString();

                //    // Aplica regras específicas para propriedades de classes
                //    AplicaRegrasClasses(registro);

                //    // Aplica regras específicas para propriedades simples
                //    AplicaRegrasPropriedades(registro);
                //}
            }
        }

        /// <summary>
        /// Aplica regras específicas para preenchimento de propriedades simples do registro com base nas configurações da planilha.
        /// </summary>
        /// <param name="registro">O registro a ser preenchido com os dados processados.</param>
        private void AplicaRegrasPropriedades(T registro)
        {
            // Obtém as propriedades da classe genérica T
            var propriedades = ObterPropriedades();

            // Obtém os valores de configuração para idImportacao e idCliente
            var idImportacao = _configuracoesPlanilha.IdImportacao;
            var idCliente = _configuracoesPlanilha.IdCliente;

            // Obtém um array de propriedades da classe genérica T
            var propriedadesArray = propriedades.Values.ToArray();

            // Itera sobre as propriedades
            foreach (var propriedade in propriedadesArray)
            {
                // Verifica se a propriedade é nula e continua para a próxima, se for o caso
                if (propriedade == null) { continue; }

                // Aplica regras específicas com base no nome da propriedade
                switch (propriedade.Name)
                {
                    case "IdEmpresaResponsavel":
                        AplicaRegraIdClienteEEmpresa(registro, propriedade, idCliente);
                        break;

                    case "DataPlanilha":
                        AplicaRegraDataPlanilha(registro, propriedade);
                        break;

                    case "DataCriacaoInterno":
                        AplicaRegraDataCriacaoInterno(registro, propriedade);
                        break;

                    default:
                        // Se não houver uma regra específica, define o valor diretamente na propriedade
                        typeof(T).GetProperty(_colunaBancoAtual).SetValue(registro, _colunaPlaninhaAtual);
                        break;
                }
            }
        }

        /// <summary>
        /// Aplica regras específicas para preenchimento de propriedades de classes do registro com base nas configurações da planilha.
        /// </summary>
        /// <param name="registro">O registro a ser preenchido com os dados processados.</param>
        private void AplicaRegrasClasses(T registro)
        {
            // Obtém os tipos de classe
            var classes = ObterTiposDeClasse();

            // Itera sobre as classes
            foreach (var classe in classes)
            {
                // Verifica se a classe é nula e continua para a próxima, se for o caso
                if (classe.Value == null) { continue; }

                // Aplica regras específicas com base no nome da classe
                switch (classe.Key)
                {
                    default:
                        // Se não houver uma regra específica, define o valor diretamente na propriedade da classe genérica T
                        typeof(T).GetProperty(_colunaBancoAtual).SetValue(registro, _colunaPlaninhaAtual);
                        break;
                }
            }
        }

        /// <summary>
        /// Aplica a regra para preenchimento da propriedade de data de criação interno no registro.
        /// </summary>
        /// <param name="registro">O registro a ser preenchido com os dados processados.</param>
        /// <param name="propriedade">A propriedade de data de criação interno a ser preenchida.</param>
        private void AplicaRegraDataCriacaoInterno(T registro, PropertyInfo propriedade)
        {
            // Verifica se a propriedade não é nula
            if (propriedade != null)
            {
                // Define o valor da propriedade de data de criação interno no registro
                propriedade.SetValue(registro, _dataCriacaoInterno);
            }
        }

        /// <summary>
        /// Aplica uma regra para definir o valor de uma propriedade especificada como o IdCliente fornecido.
        /// </summary>
        /// <param name="registro">O objeto no qual a regra será aplicada.</param>
        /// <param name="propriedade">A propriedade a ser definida.</param>
        /// <param name="idCliente">O valor a ser atribuído à propriedade especificada (IdCliente).</param>
        private void AplicaRegraIdClienteEEmpresa(T registro, PropertyInfo propriedade, int idCliente)
        {
            // Verifica se a propriedade não é nula antes de aplicar a regra.
            if (propriedade != null)
            {
                // Define o valor da propriedade especificada como o IdCliente fornecido.
                propriedade.SetValue(registro, idCliente);
            }
        }

        /// <summary>
        /// Aplica uma regra para definir o valor de uma propriedade especificada como uma data específica ou a data de criação interna, dependendo da disponibilidade.
        /// </summary>
        /// <param name="registro">O objeto no qual a regra será aplicada.</param>
        /// <param name="propriedade">A propriedade a ser definida.</param>
        private void AplicaRegraDataPlanilha(T registro, PropertyInfo propriedade)
        {
            // Verifica se a propriedade não é nula antes de aplicar a regra.
            if (propriedade != null)
            {
                // Define o valor da propriedade especificada como a data específica, se disponível, caso contrário, utiliza a data de criação interna.
                propriedade.SetValue(registro, _dataEspecifica ?? _dataCriacaoInterno);
            }
        }

        
        ///// <summary>
        ///// Realiza o mapeamento das colunas para as propriedades da classe genérica T com base no template de importação associado à planilha.
        ///// </summary>
        //private void MapeamentoColunaPropriedadePorPlanilha()
        //{
        //    // Lista para armazenar o mapeamento de colunas para propriedades
        //    var mapeamentoColunaPropriedade = new List<DeParaTemplatePlanilha>();

        //    // Obtém o ID do cliente das configurações da planilha
        //    var idCliente = _configuracoesPlanilha.IdCliente;

        //    // Inicializa a conexão com o banco de dados
        //    using (DAO DB = new DAO())
        //    {
        //        // Obtém o template de importação associado à planilha principal
        //        var template = DB.TemplateImportacao.FirstOrDefault(t => t.IdCliente == idCliente && t.IntPlanilhaImportacao == 0);

        //        // Realiza o mapeamento com base no nome da planilha
        //        switch (_nomePlanilha)
        //        {
        //            case "OrdemServico":
        //                // Obtém o template de importação específico para a planilha "OrdemServico"
        //                var templateOrdemServico = DB.TemplateImportacao.FirstOrDefault(t => t.IdCliente == idCliente && t.IntPlanilhaImportacao == 2);

        //                if (templateOrdemServico != null)
        //                {
        //                    // Obtém o mapeamento de colunas para propriedades associado ao template
        //                    mapeamentoColunaPropriedade = DB.DeParaTemplatePlanilha.Where(d => d.TemplateImportacaoId == templateOrdemServico.Id).ToList();
        //                }
        //                break;
        //            case "TabelaClientes":
        //                // Obtém o template de importação específico para a planilha "TabelaClientes"
        //                var templateTabelaClientes = DB.TemplateImportacao.FirstOrDefault(t => t.IdCliente == idCliente && t.IntPlanilhaImportacao == 3);

        //                if (templateTabelaClientes != null)
        //                {
        //                    // Obtém o mapeamento de colunas para propriedades associado ao template
        //                    mapeamentoColunaPropriedade = DB.DeParaTemplatePlanilha.Where(d => d.TemplateImportacaoId == templateTabelaClientes.Id).ToList();
        //                }
        //                break;
        //            case "BaseMatriz":
        //                // Obtém o mapeamento de colunas para propriedades associado ao template principal
        //                if (template != null)
        //                {
        //                    mapeamentoColunaPropriedade = DB.DeParaTemplatePlanilha.Where(d => d.TemplateImportacaoId == template.Id).ToList();
        //                }
        //                break;
        //            case "TabelaVeiculos":
        //                // Obtém o template de importação específico para a planilha "TabelaVeiculos"
        //                var templateTabelaVeiculos = DB.TemplateImportacao.FirstOrDefault(t => t.IdCliente == idCliente && t.IntPlanilhaImportacao == 4);

        //                if (templateTabelaVeiculos != null)
        //                {
        //                    // Obtém o mapeamento de colunas para propriedades associado ao template
        //                    mapeamentoColunaPropriedade = DB.DeParaTemplatePlanilha.Where(d => d.TemplateImportacaoId == templateTabelaVeiculos.Id).ToList();
        //                }
        //                break;
        //            default:
        //                // Indica que a planilha não possui um template associado
        //                _modelosSemTemplante = true;
        //                break;
        //        }
        //    }

        //    // Atualiza o mapeamento de colunas para propriedades na classe
        //    _mapeamentoColunaPropriedade = mapeamentoColunaPropriedade;
        //}

        /// <summary>
        /// Remove a linha de título da planilha Excel no DataTable.
        /// </summary>
        private void RemoveTituloPLaninhaExecel()
        {
            // Obtém a linha de título da tabela e a remove
            var linhaTitulo = _dataTable.Rows[0];
            _dataTable.Rows.Remove(linhaTitulo);
        }

        /// <summary>
        /// Salva uma lista de registros em lotes na base de dados, utilizando operações em lote para melhor desempenho.
        /// </summary>
        /// <param name="registros">A lista de registros a serem salvos.</param>
        private void SalveLoteRegistros(List<T> registros)
        {


            try
            {
                _servicosPlanilhaRepository.SalvarLoteRegistros(registros);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            // Limpa a lista de registros após o salvamento bem-sucedido.
            registros.Clear();
        }

        
        /// <summary>
        /// Obtém a quantidade de registros na tabela associada ao tipo genérico T.
        /// </summary>
        /// <returns>O número total de registros na tabela.</returns>
        private int ObterQtdRegistros()
        {
            // Obtém a contagem de registros na tabela associada ao tipo genérico T.
            Expression<Func<T, bool>> predicate = b => true;
            var quantidadeRegistros = _servicosPlanilhaRepository.ContarRegistros(predicate);

            // Retorna o número total de registros na tabela.
            return quantidadeRegistros;
        }

        /// <summary>
        /// Atualiza as configurações de quantidade de registros com base nos dados da planilha importada.
        /// </summary>
        //private void AtualizaConfQtdRegistros()
        //{
        //    // Verifica se a importação é nula e a recupera do banco de dados, se necessário.
        //    if (_importacao == null)
        //    {
        //        using (DAO db = new DAO())
        //        {
        //            _importacao = db.ImportacaoPlanilhas.FirstOrDefault(i => i.Id == _configuracoesPlanilha.IdImportacao);
        //        }
        //    }

        //    // Utiliza um objeto DAO para interagir com o banco de dados.
        //    using (var db = new DAO())
        //    {
        //        // Inicia uma transação para garantir a consistência das atualizações.
        //        using (var transaction = db.Database.BeginTransaction())
        //        {
        //            // Obtém a quantidade de registros no banco de dados e na planilha importada.
        //            var qtdRegistrosBanco = ObtenhaQtdRegistrosBanco();
        //            var qtdRegistrosPlanilha = _dataTable.Rows.Count;

        //            try
        //            {
        //                // Obtém a configuração de importação.
        //                var config = _importacao;

        //                // Atualiza as propriedades da configuração com os novos valores.
        //                config.QtdRegistroBanco = qtdRegistrosBanco;
        //                config.QtdRegistroPlanilha = qtdRegistrosPlanilha;
        //                config.DadosSalvo = qtdRegistrosBanco >= qtdRegistrosPlanilha;
        //                config.IndexUltimaLinha = (int)_ultimaCargaIndexLinha;

        //                // Define o estado da entidade como modificado e salva as alterações no banco de dados.
        //                db.Entry(config).State = System.Data.Entity.EntityState.Modified;
        //                db.SaveChanges();

        //                // Atualiza a referência local do objeto _importacao.
        //                _importacao = config;

        //                // Comita a transação após o salvamento bem-sucedido.
        //                transaction.Commit();

        //                // Atualiza as configurações de quantidade de registros do objeto _configuracoesPlanilha.
        //                _configuracoesPlanilha.QtdRegistroBanco = qtdRegistrosBanco;
        //                _configuracoesPlanilha.QtdRegistroBanco = qtdRegistrosBanco;
        //                _configuracoesPlanilha.DadosSalvos = qtdRegistrosBanco >= qtdRegistrosPlanilha;
        //            }
        //            catch (Exception ex)
        //            {
        //                // Em caso de exceção, realiza rollback na transação e lança a exceção.
        //                transaction.Rollback();
        //                throw ex;
        //            }
        //        }
        //    }
        //}

        public void SalvarPlanilha(string base64Planilha)
        {
            new ServicosPlanilhaRepository<Bula>(null).SalvarPlanilha(base64Planilha);
        }

        public ConfiguracoesPlanilhaViewModel BuscarPlanilhaNaFila()
        {
            var planilha = new ServicosPlanilhaRepository<Bula>(null).BuscarPlanilhaNaFila();
            var config = new ConfiguracoesPlanilhaViewModel()
            {
                IdImportacao = planilha.IdImportacao,
                EnderecoPlanilha = planilha.EnderecoPlanilha,
                QtdRegistroBanco = planilha.QtdRegistroBanco,
                QtdRegistroPlanilha = planilha.QtdRegistroPlanilha,
                DadosSalvos = planilha.DadosSalvos,
                IndexLinhaUltimaCarga = planilha.IndexLinhaUltimaCarga,
                DataPlanilha = planilha.DataPlanilha.ToString()
            };
            return config;
        }
    }
}
