import 'package:flutter/material.dart';
import 'package:flytec/core/infrastructure/database/database_instance.dart';
import 'package:flytec/core/infrastructure/database/sql/database_instances/relatorio_database_instance.dart';
import 'package:flytec/core/infrastructure/database/sql/sql_database_provider.dart';
import 'package:flytec/features/aplications_v2/enums/report_dashboard_state.dart';
import 'package:flytec/features/aplications_v2/models/aplicacao.dart';
import 'package:flytec/features/aplications_v2/models/contratante.dart';

class ReportAplicationController {
  final DatabaseInstance _databaseInstance = RelatorioDatabaseInstance.instance;
  final VoidCallback? _updateView;
  SQLDatabaseProvider get _sqlDatabaseProvider =>
      SQLDatabaseProvider(_databaseInstance);
  ReportAplicationController({required VoidCallback? updateView})
      : _updateView = updateView;
  List<Aplicacao>? _listaAplicacao;

  List<Aplicacao>? get listaAplicacao => _listaAplicacao;

  Aplicacao? _aplicacaoSelected;

  Aplicacao? get aplicacaoSelected => _aplicacaoSelected;

  VoidCallback? get updateView => _updateView;

  void setAplicacaoSelected(Aplicacao? aplicacao) {
    _aplicacaoSelected = aplicacao;
    _updateView!();
  }

  void setListRelatorioAplicacao(List<Aplicacao>? listaAplicacaoNew) {
    _listaAplicacao = listaAplicacaoNew;
    _updateView!();
  }

  int obtainQuantityReportsByState(ReportDashBoardState state) {
    return _listaAplicacao!
        .where((element) => element.state == state)
        .toList()
        .length;
  }

  Future<void> obtainReportsAplications() async {
    final reports =
        await _sqlDatabaseProvider.obtainTableElementsList("Aplicacao");
    _listaAplicacao = reports.map((e) => Aplicacao.fromJson(e)).toList();
    _updateView!();
  }

  Future<int> createAplicacao(Aplicacao aplicacao) async {
    int id = await _sqlDatabaseProvider.insert(aplicacao.toMap(), 'Aplicacao');
    await obtainReportsAplications();
    return id;
  }

  Future<void> updateContratanteAplicacao(
      int idContratante, int idAplicacao) async {
    await _sqlDatabaseProvider.update(
        {'contratante_id': idContratante}, 'Aplicacao', idAplicacao.toString());
    await obtainReportsAplications();
  }

  Future<int> createContrante(Contratante contratante) async {
    return await _sqlDatabaseProvider.insert(
        contratante.toMap(), 'Contratante');
  }

  Future<Map<String, dynamic>> getElementById(int id, String table) async {
    return await _sqlDatabaseProvider.obtainElementTableById(
        table, id.toString());
  }
}
