import 'package:flytec/core/infrastructure/database/database_instance.dart';
import 'package:flytec/core/infrastructure/database/sql/database_instances/relatorio_database_instance.dart';
import 'package:flytec/core/infrastructure/database/sql/sql_database_provider.dart';
import 'package:flytec/features/aplications_v2/enums/report_dashboard_state.dart';
import 'package:flytec/features/aplications_v2/models/aplicacao.dart';
import 'package:flytec/features/aplications_v2/models/relatorio_aplicacao.dart';

class AplicationsInitializationController {
  final DatabaseInstance _databaseInstance = RelatorioDatabaseInstance.instance;
  SQLDatabaseProvider get _sqlDatabaseProvider =>
      SQLDatabaseProvider(_databaseInstance);

  List<RelatorioAplicacoes>? _reportsAplications;

  List<RelatorioAplicacoes>? get reportsAplications => _reportsAplications;

  Future<void> initialize() async {
    await _obtainReportsAplications();
  }

  Future<void> _obtainReportsAplications() async {
    final reports = await _sqlDatabaseProvider
        .obtainTableElementsList("RelatorioAplicacoes");
    _reportsAplications =
        reports.map((e) => RelatorioAplicacoes.fromJson(e)).toList();
  }

  Future<void> teste() async {
    final relatorioAplicacoes = RelatorioAplicacoes(
        data: DateTime.now(), state: ReportDashBoardState.Incompleto);
    final idRelatorioAplicacoes = await _sqlDatabaseProvider.insert(
        relatorioAplicacoes.toMap(), "RelatorioAplicacoes");

    final aplicacao = Aplicacao(
        horimetroFinal: "0.5",
        horimetroInicial: "1.2",
        temperaturaFinal: "31 °C",
        temperaturaInicial: "27,5 °C",
        dataAplicacao: DateTime.now(),
        horarioInicio: DateTime.now().subtract(const Duration(days: 3)),
        horarioTermino: DateTime.now());
    final idRelatorio =
        await _sqlDatabaseProvider.insert(aplicacao.toMap(), "Aplicacao");

    await _sqlDatabaseProvider.update({"identificadorRelatorio": idRelatorio},
        "RelatorioAplicacoes", idRelatorioAplicacoes.toString());
    await _obtainReportsAplications();
  }
}
