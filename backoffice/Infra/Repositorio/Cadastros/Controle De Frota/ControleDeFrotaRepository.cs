using Dapper;
using Domain.Entidades.Cadastros.Atividade;
using Domain.Entidades.Cadastros.Horimetro;
using Domain.Enums;
using Domain.Interfaces.Cadastros.ControleDeFrota;
using Helpers;
using Infra.Configuracao;
using Microsoft.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace Infra.Repositorio.Cadastros.Controle_De_Frota;

public class ControleDeFrotaRepository : IControleDeFrotaRepository
{
    private readonly ContextBase _contextBase;

    public ControleDeFrotaRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task<int> AddAsync(Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota obj)
    {
        await _contextBase.AddAsync(obj);
        await _contextBase.SaveChangesAsync();
        return obj.Id;
    }

    public async Task DeleteAsync(int id)
    {
        var entityToRemove = await GetByIdAsync(id);
        if(!ObjectNullValidation.IsObjectNull(entityToRemove))
        {
            _contextBase.Remove(entityToRemove);
            await _contextBase.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>> GetListByStatusAsync(int idEmpresa, int statusEnvio)
    {
        var query = @"
            SELECT *
            FROM ControleDeFrota WHERE IdEmpresa = @IdEmpresa AND (StatusEnvio =  @statusEnvio OR StatusEnvio = 4)";

        using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
        {
            var parameters = new { IdEmpresa = idEmpresa, StatusEnvio = statusEnvio };
            var result = await connection.QueryAsync<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>(query, parameters);
            return result;
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>> GetListByIdsAsync(List<int> ids, int idEmpresa, int statusEnvio, int isMapa)
    {
        var query = @"SELECT * FROM ControleDeFrota 
            WHERE Id IN @Ids
            AND IdEmpresa = @IdEmpresa 
            AND (StatusEnvio = @statusEnvio OR StatusEnvio = 4)";

        using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
        {
            var parameters = new { Ids = ids, IdEmpresa = idEmpresa, StatusEnvio = statusEnvio };
            var result = await connection.QueryAsync<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>(query, parameters);
            return result;
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>> GetListByIdsAsync(List<int> ids)
    {
        var query = @"SELECT * FROM ControleDeFrota WHERE Id IN @Ids";

        using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
        {
            var parameters = new { Ids = ids };
            var result = await connection.QueryAsync<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>(query, parameters);
            return result;
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>> GetAllAsync(
        DateTime? offsetDate,
        string userName,
        IEnumerable<string>? roleNames,
        int IdEmpresa)
    {
        string query = "SELECT * FROM ControleDeFrota WHERE StatusEnvio <> 4";

        if (offsetDate != null)
        {
            query += " AND (CONVERT(VARCHAR, DataAtualizacao, 120) > CONVERT(VARCHAR, @offsetDate, 120) " +
                     "OR CONVERT(VARCHAR, DataCriacao, 120) > CONVERT(VARCHAR, @offsetDate, 120))";
        }

        query += " AND IdEmpresa = @IdEmpresa";

        if (roleNames != null && (!roleNames.Contains("Administrativo") && !roleNames.Contains("Administrativo") && !roleNames.Contains("Administrador")))
        {
            query += " AND (NomeExecutor = @Executor OR NomePiloto = @Piloto)";
        }


        var parameters = new
        {
            offsetDate,
            Executor = userName,
            Piloto = userName,
            IdEmpresa
        };

        using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
        {
            // Executa a consulta com os parâmetros
            var result = await connection.QueryAsync<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>(query, parameters);
            return result.ToList();
        }
    }

    public async Task<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota> GetByIdAsync(int? id)
    {
        var query = @"SELECT * FROM ControleDeFrota WHERE Id = @Id";

        using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
        {
            var parameters = new { Id = id };
            var result = await connection.QueryFirstOrDefaultAsync<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>(query, parameters);
            return result;
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>> GetListByMesAsync(int idEmpresa, int statusEnvio, DateTime primeiroDiaMes, DateTime ultimoDiaMes)
    {
        var query = @"SELECT * FROM ControleDeFrota 
            WHERE IdEmpresa = @IdEmpresa 
            AND (StatusEnvio = @statusEnvio OR StatusEnvio = 4) 
            AND DataCriacao >= @primeiroDiaMes 
            AND DataCriacao <= @ultimoDiaMes";

        using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
        {
            var parameters = new { IdEmpresa = idEmpresa, StatusEnvio = statusEnvio, primeiroDiaMes = primeiroDiaMes, ultimoDiaMes = ultimoDiaMes };
            var result = await connection.QueryAsync<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>(query, parameters);
            return result;
        }
    }

    public async Task<int?> UpdateAsync(Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota obj)
    {
        var objeto = await _contextBase.ControleDeFrota.FindAsync(obj.Id);
        if (objeto != null)
        {
            objeto.Observacao = obj.Observacao;
            objeto.Data = obj.Data;
            objeto.IdVeiculo = obj.IdVeiculo;
            objeto.NomeVeiculo = obj.NomeVeiculo;
            objeto.DataCriacao = obj.DataCriacao;
            objeto.DataAtualizacao = obj.DataAtualizacao;
            objeto.KmInicial = obj.KmInicial;
            objeto.KmFinal = obj.KmFinal;
            objeto.IdAeronave = obj.IdAeronave;
            objeto.NomeAeronave = obj.NomeAeronave;
            objeto.HorimetroInicial = obj.HorimetroInicial;
            objeto.HorimetroFinal = obj.HorimetroFinal;
            objeto.CombustivelInicial = obj.CombustivelInicial;
            objeto.CombustivelFinal = obj.CombustivelFinal;
            objeto.QtdeCombustivel = obj.QtdeCombustivel;
            objeto.IdPiloto = obj.IdPiloto;
            objeto.NomePiloto = obj.NomePiloto;
            objeto.IdExecutor = obj.IdExecutor;
            objeto.NomeExecutor = obj.NomeExecutor;
            objeto.Extensao = obj.Extensao;
            objeto.Combustivel = obj.Combustivel;
            objeto.IdData = obj.IdData;
            objeto.Imagem = obj.Imagem;
            objeto.NomeRelatorio = obj.NomeRelatorio;
            objeto.StatusEnvio = obj.StatusEnvio;
            objeto.IsDrone = obj.IsDrone;
            objeto.Checklist = obj.Checklist;
            objeto.Abastecimento = obj.Abastecimento;
            objeto.ValorAbastecimento = obj.ValorAbastecimento;
            objeto.DataRevisao = obj.DataRevisao;
            objeto.KmRevisao = obj.KmRevisao;
            objeto.HorasAplicacao = obj.HorasAplicacao;
            objeto.Horimetros = obj.Horimetros;
            objeto.CombustivelRemanescente = obj.CombustivelRemanescente;
            objeto.CombustivelUtilizado = obj.CombustivelUtilizado;

            _contextBase.ControleDeFrota.Update(objeto);
            await _contextBase.SaveChangesAsync(); 
            return objeto.Id;
        }
        return null;
    }

    public async Task<IEnumerable<Atividade>> GetAtividadesByFiltrosAsync(AtividadeFiltro atividadeFiltro)
    {
        var query = new StringBuilder(@"
            SELECT CASE WHEN Horimetros = 'null' THEN NULL ELSE Horimetros END Horimetros, 
                   HorasAplicacao,
                   IsDrone
            FROM ControleDeFrota
            WHERE StatusEnvio IN (0)
                AND NomePiloto LIKE '%' + @Piloto + '%'
                AND IdEmpresa = @IdEmpresa
                AND NomeExecutor LIKE '%' + @Executor + '%'
                AND NomeAeronave LIKE '%' + @PrefixoAeronave + '%'
                AND (@DataInicio IS NULL OR DataCriacao >= @DataInicio)
                AND (@DataFim IS NULL OR DataCriacao <= @DataFim)
        ");

        var parameters = new DynamicParameters();
        parameters.Add("PrefixoAeronave", atividadeFiltro.PrefixoAeronave);
        parameters.Add("Piloto", atividadeFiltro.Piloto);
        parameters.Add("Executor", atividadeFiltro.Executor);
        parameters.Add("IdEmpresa", atividadeFiltro.IdEmpresa);
        parameters.Add("DataInicio", atividadeFiltro.DataInicial);
        parameters.Add("DataFim", atividadeFiltro.DataFinal);

        dynamic result;
        List<Atividade> atividades = new List<Atividade>();

        using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
        {
            result = await connection.QueryAsync<dynamic>(query.ToString(), parameters);
        }

        foreach (var item in result)
        {
            double horasAplicacao = 0, horasIncendio = 0, horasTranslado = 0;
            List<Horimetro> horimetros;

            if (item.IsDrone && item.HorasAplicacao != null)
            {
                horasAplicacao += Convert.ToDouble(item.HorasAplicacao);
                atividades.Add(new Atividade { TotalHorasAplicacao = horasAplicacao });
            }


            if (!item.IsDrone && item.Horimetros != null)
            {
                horimetros = GetHorimetros(item.Horimetros);
                foreach (var horimetro in horimetros)
                {
                    switch (horimetro.Tipo)
                    {
                        case HorimetroTypeEnum.Aplicacao:
                            horasAplicacao += horimetro.Fim - horimetro.Inicio;
                            break;
                        case HorimetroTypeEnum.Incendio:
                            horasIncendio += horimetro.Fim - horimetro.Inicio;
                            break;
                        case HorimetroTypeEnum.Translado:
                            horasTranslado += horimetro.Fim - horimetro.Inicio;
                            break;
                        default:
                            break;
                    }
                }

                atividades.Add(new Atividade
                {
                    TotalHorasAplicacao = horasAplicacao,
                    TotalHorasIncendio = horasIncendio,
                    TotalHorasTranslado = horasTranslado
                });
            }
        }

        return atividades;
    }

    private List<Horimetro> GetHorimetros(dynamic horimetrosJson)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        dynamic deserialezedList = JsonSerializer.Deserialize<List<dynamic>>(horimetrosJson, options);

        List<Horimetro> result = new List<Horimetro>();

        foreach (var item in deserialezedList)
        {
            double inicio = double.Parse(item.GetProperty("inicio").GetString(), new CultureInfo("pt-BR"));
            double fim = double.Parse(item.GetProperty("fim").GetString(), new CultureInfo("pt-BR"));
            int tipoInt = item.GetProperty("tipo").GetInt32();
            HorimetroTypeEnum tipoEnum = (HorimetroTypeEnum)tipoInt;

            result.Add(new Horimetro 
            {
                Inicio = inicio,
                Fim = fim,
                Tipo = tipoEnum,
            });
        }

        return result;
    }
}
