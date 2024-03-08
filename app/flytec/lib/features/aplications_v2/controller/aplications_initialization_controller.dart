import 'package:flytec/core/infrastructure/database/database_instance.dart';
import 'package:flytec/core/infrastructure/database/sql/database_instances/relatorio_database_instance.dart';
import 'package:flytec/core/infrastructure/database/sql/sql_database_provider.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/features/aplications_v2/models/aplicacao.dart';

class AplicationsInitializationController {
  final DatabaseInstance _databaseInstance = RelatorioDatabaseInstance.instance;
  SQLDatabaseProvider get _sqlDatabaseProvider =>
      SQLDatabaseProvider(_databaseInstance);

  List<Aplicacao>? _reportsAplications;

  List<Aplicacao>? get reportsAplications => _reportsAplications;

  Future<void> initialize() async {
    await _obtainReportsAplications();
  }

  Future<void> _obtainReportsAplications() async {
    final refUsuario =
        '${getIt<GlobalConfigVars>().userPayload.nrUsuario}_${getIt<GlobalConfigVars>().userPayload.name}';
    final reports =
        await _sqlDatabaseProvider.obtainTableElementsList("Aplicacao");
    _reportsAplications = reports
        .map((e) => Aplicacao.fromJson(e))
        .where((element) => element.refUsuario == refUsuario)
        .toList();
  }
}
