import 'package:flutter/material.dart';
import 'package:flytec/core/infrastructure/database/database_instance.dart';
import 'package:flytec/core/infrastructure/database/sql/database_instances/relatorio_database_instance.dart';
import 'package:flytec/core/infrastructure/database/sql/sql_database_provider.dart';
import 'package:flytec/features/aplications_v2/enums/report_dashboard_state.dart';
import 'package:flytec/features/aplications_v2/models/aplicacao.dart';

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

  Future<Map<String, dynamic>> getElementById(int id, String table) async {
    return await _sqlDatabaseProvider.obtainElementTableById(
        table, id.toString());
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
}
