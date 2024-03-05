import 'package:flytec/core/infrastructure/database/database_instance.dart';
import 'package:flytec/core/infrastructure/database/sql/database_instances/relatorio_database_instance.dart';
import 'package:flytec/core/infrastructure/database/sql/sql_database_provider.dart';
import 'package:flytec/features/aplications_v2/enums/report_dashboard_state.dart';
import 'package:flytec/features/aplications_v2/models/aplicacao.dart';

class ReportAplicationController {
  final DatabaseInstance _databaseInstance = RelatorioDatabaseInstance.instance;
  SQLDatabaseProvider get _sqlDatabaseProvider =>
      SQLDatabaseProvider(_databaseInstance);

  List<Aplicacao>? _listaAplicacao;

  List<Aplicacao>? get listaAplicacao => _listaAplicacao;

  void setListRelatorioAplicacao(List<Aplicacao>? listaAplicacaoNew) {
    _listaAplicacao = listaAplicacaoNew;
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
  }

  Future<int> createAplicacao(Aplicacao aplicacao) async {
    int id = await _sqlDatabaseProvider.insert(aplicacao.toMap(), 'Aplicacao');
    await obtainReportsAplications();
    return id;
  }
}
