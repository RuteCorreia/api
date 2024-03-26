import 'package:flutter/material.dart';
import 'package:flytec/core/infrastructure/database/database_instance.dart';
import 'package:flytec/core/infrastructure/database/sql/database_instances/relatorio_database_instance.dart';
import 'package:flytec/core/infrastructure/database/sql/sql_database_provider.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/features/aplications/enums/report_dashboard_state.dart';
import 'package:flytec/features/aplications/models/aplicacao.dart';
import 'package:flytec/features/aplications/models/aplicacoes.dart';
import 'package:flytec/features/aplications/models/caracteristicas_produto_aplicado.dart';
import 'package:flytec/features/aplications/models/contratante.dart';
import 'package:flytec/features/aplications/models/contrato_prestacao_servico.dart';
import 'package:flytec/features/aplications/models/dados_responsavel.dart';
import 'package:flytec/features/aplications/models/identificacao_area_tratada.dart';
import 'package:flytec/features/aplications/models/recomendacoes_tecnicas.dart';
import 'package:flytec/features/aplications/models/relatorio_aplicacao.dart';

class ReportAplicationController extends ChangeNotifier {
  final DatabaseInstance _databaseInstance = RelatorioDatabaseInstance.instance;
  VoidCallback? _updateView;
  SQLDatabaseProvider get _sqlDatabaseProvider =>
      SQLDatabaseProvider(_databaseInstance);
  ReportAplicationController({required VoidCallback? updateView})
      : _updateView = updateView;

  List<Aplicacao>? _listaAplicacao = [];

  List<Aplicacao>? get listaAplicacao => _listaAplicacao;

  Aplicacao? _aplicacaoSelected;

  Aplicacao? get aplicacaoSelected => _aplicacaoSelected;

  VoidCallback? get updateView => _updateView;

  void setUpdateUpdateView(VoidCallback updateView) {
    _updateView = updateView;
  }

  void setAplicacaoSelected(Aplicacao? aplicacao) {
    _aplicacaoSelected = aplicacao;
    _updateView!();
  }

  void setListRelatorioAplicacao(List<Aplicacao>? listaAplicacaoNew) {
    _listaAplicacao = listaAplicacaoNew;
    _updateView!();
    notifyListeners();
  }

  int obtainQuantityReportsByState(ReportDashBoardState state) {
    return _listaAplicacao!
        .where((element) => element.state == state)
        .toList()
        .length;
  }

  Future<void> obtainReportsAplications() async {
    List<Aplicacao> listaAplicacaoResult = [];
    final idUsuario = getIt<GlobalConfigVars>().userPayload.nrUsuario;
    final refUsuario =
        '${idUsuario}_${getIt<GlobalConfigVars>().userPayload.name}';
    final reports =
        await _sqlDatabaseProvider.obtainTableElementsList("Aplicacao");
    final listaAplicacao = reports
        .map((e) => Aplicacao.fromJson(e))
        .toList()
        .where((element) => element.refUsuario == refUsuario)
        .toList();
    for (int i = 0; i < listaAplicacao.length; i++) {
      final aplicacao = listaAplicacao[i];
      final idNew = '${idUsuario}_${aplicacao.id}';
      aplicacao.refDocument = idNew; 
      final getContratanteDb = await _sqlDatabaseProvider
          .obtainElementTableById("Contratante", aplicacao.contratanteId);
      Contratante contratante = Contratante.fromJson(getContratanteDb);
      aplicacao.contratante = contratante;

      final getIdentificacaoAreaTratadaDb =
          await _sqlDatabaseProvider.obtainElementTableById(
              "IdentificacaoAreaTratada", aplicacao.identificacaoAreaTratadaId);
      IdentificacaoAreaTratada identificacaoAreaTratada =
          IdentificacaoAreaTratada.fromJson(getIdentificacaoAreaTratadaDb);
      aplicacao.identificacaoAreaTratada = identificacaoAreaTratada;

      final getCaracteristicasProdutoAplicado =
          await _sqlDatabaseProvider.obtainElementTableById(
              "CaracteristicasProdutoAplicado",
              aplicacao.caracteristicasProdutoAplicadoId);
      CaracteristicasProdutoAplicado caracteristicasProdutoAplicado =
          CaracteristicasProdutoAplicado.fromJson(
              getCaracteristicasProdutoAplicado);
      aplicacao.caracteristicasProdutoAplicado = caracteristicasProdutoAplicado;

      final getRecomendacoesTecnicas =
          await _sqlDatabaseProvider.obtainElementTableById(
              "RecomendacoesTecnicas", aplicacao.recomendacoesTecnicasId);
      RecomendacoesTecnicas recomendacoesTecnicas =
          RecomendacoesTecnicas.fromJson(getRecomendacoesTecnicas);
      aplicacao.recomendacoesTecnicas = recomendacoesTecnicas;

      final getRelatorioAplicacao =
          await _sqlDatabaseProvider.obtainElementTableById(
              "RelatorioAplicacao", aplicacao.relatorioAplicacaoId);
      RelatorioAplicacao relatorioAplicacao =
          RelatorioAplicacao.fromJson(getRelatorioAplicacao);
      List<Aplicacoes?> aplicacoes = await getAplicacoesByRelatorioAplicacao(
          aplicacao.relatorioAplicacaoId);
      relatorioAplicacao.aplicacoes =
          aplicacoes.map((aplicacoes) => aplicacoes).toList();
      aplicacao.relatorioAplicacao = relatorioAplicacao;
      final getContratoPrestacaoServico =
          await _sqlDatabaseProvider.obtainElementTableById(
              "ContratoPrestacaoServico", aplicacao.contratoPrestacaoServicoId);
      ContratoPrestacaoServico contratoPrestacaoServico =
          ContratoPrestacaoServico.fromJson(getContratoPrestacaoServico);
      aplicacao.contratoPrestacaoServico = contratoPrestacaoServico;

      final getDadosResponsavel =
          await _sqlDatabaseProvider.obtainElementTableById(
              "DadosResponsavel", aplicacao.dadosResponsavelId);
      DadosResponsavel dadosResponsavel =
          DadosResponsavel.fromJson(getDadosResponsavel);
      aplicacao.dadosResponsavel = dadosResponsavel;

      listaAplicacaoResult.add(aplicacao);
    }

    _listaAplicacao = listaAplicacaoResult;
    _updateView!();
    notifyListeners();
  }

  Future<Map<String, dynamic>?> getElementById(int id, String table) async {
    return await _sqlDatabaseProvider.obtainElementTableById(table, id);
  }

  Future<int?> createElementInTable(
      Map<String, dynamic> data, String table) async {
    int? id = await _sqlDatabaseProvider.insert(data, table);
    await obtainReportsAplications();
    return id;
  }

  Future<void> updateElementInTable(
      int id, Map<String, dynamic> data, String table) async {
    await _sqlDatabaseProvider.update(data, table, id.toString());
    await obtainReportsAplications();
  }

  Future<List<Aplicacoes?>> getAplicacoesByRelatorioAplicacao(
      int? idAplicacao) async {
    if (idAplicacao == null) return [];
    final getAplicacoesByDb =
        await _sqlDatabaseProvider.obtainTableElementsList("Aplicacoes");
    final listAplicacoes = getAplicacoesByDb.map((aplicacoes) {
      if (aplicacoes['relatorioAplicacaoId'] != idAplicacao) {
        return null;
      }
      return Aplicacoes.fromJson(aplicacoes);
    }).toList();
    listAplicacoes.removeWhere((element) => element == null);
    return listAplicacoes;
  }
}
