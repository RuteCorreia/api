import 'package:flytec/core/infrastructure/database/database_instance.dart';
import 'package:flytec/core/infrastructure/database/sql/database_instances/relatorio_database_instance.dart';
import 'package:flytec/core/infrastructure/database/sql/sql_database_provider.dart';
import 'package:flytec/features/aplications_v2/enums/report_dashboard_state.dart';
import 'package:flytec/features/aplications_v2/models/aplicacao.dart';

class ReportAplicationController {
  final DatabaseInstance _databaseInstance = RelatorioDatabaseInstance.instance;
  SQLDatabaseProvider get _sqlDatabaseProvider =>
      SQLDatabaseProvider(_databaseInstance);

  List<Aplicacao>? _reportsAplications;

  List<Aplicacao>? get reportsAplications => _reportsAplications;

  void setListRelatorioAplicacao(List<Aplicacao>? reportsAplicationsNew) {
    _reportsAplications = reportsAplicationsNew;
  }

  int obtainQuantityReportsByState(ReportDashBoardState state) {
    return _reportsAplications!
        .where((element) => element.state == state)
        .toList()
        .length;
  }

  Future<void> obtainReportsAplications() async {
    final reports =
        await _sqlDatabaseProvider.obtainTableElementsList("Aplicacao");
    _reportsAplications = reports.map((e) => Aplicacao.fromJson(e)).toList();
  }

  Future<int> createRelatorioAplicacao(Aplicacao aplicacao) async {
    int id = await _sqlDatabaseProvider.insert(aplicacao.toMap(), 'Aplicacao');
    await obtainReportsAplications();
    return id;
  }
}
