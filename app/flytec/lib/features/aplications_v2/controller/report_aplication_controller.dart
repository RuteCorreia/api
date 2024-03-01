import 'package:flytec/core/infrastructure/database/database_instance.dart';
import 'package:flytec/core/infrastructure/database/sql/database_instances/relatorio_database_instance.dart';
import 'package:flytec/core/infrastructure/database/sql/sql_database_provider.dart';

class ReportAplicationController {
  final DatabaseInstance _databaseInstance = RelatorioDatabaseInstance.instance;
  SQLDatabaseProvider get _sqlDatabaseProvider =>
      SQLDatabaseProvider(_databaseInstance);

  static int? _idRelatorioAplicacoes;
  static int? get idRelatorioAplicacoes => _idRelatorioAplicacoes;

  void setNewIdRelatorioAplicacoes(int? idRelatorioAplicacoes) {
    _idRelatorioAplicacoes = idRelatorioAplicacoes;
  }

  void dispose() {
    _idRelatorioAplicacoes = null;
  }
}
