import 'package:flytec/core/infrastructure/database/database_instance.dart';
import 'package:flytec/core/infrastructure/database/sql/database_instances/relatorio_database_instance.dart';
import 'package:flytec/core/infrastructure/database/sql/sql_database_provider.dart';
import 'package:flytec/features/aplications_v2/enums/report_dashboard_state.dart';
import 'package:flytec/features/aplications_v2/models/relatorio_aplicacao.dart';

class ReportAplicationController {
  final DatabaseInstance _databaseInstance = RelatorioDatabaseInstance.instance;
  SQLDatabaseProvider get _sqlDatabaseProvider =>
      SQLDatabaseProvider(_databaseInstance);

  List<RelatorioAplicacoes>? _reportsAplications;

  List<RelatorioAplicacoes>? get reportsAplications => _reportsAplications;

  void setListRelatorioAplicacoes(
      List<RelatorioAplicacoes>? reportsAplicationsNew) {
    _reportsAplications = reportsAplicationsNew;
  }

  int obtainQuantityReportsByState(ReportDashBoardState state) {
    return _reportsAplications!
        .where((element) => element.state == state)
        .toList()
        .length;
  }
}
