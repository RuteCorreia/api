import 'package:flytec/core/infrastructure/database/database_instance.dart';
import 'package:flytec/core/infrastructure/database/sql/sql_commands.dart';
import 'package:sqflite/sqflite.dart';
// ignore: depend_on_referenced_packages
import 'package:path/path.dart' show join;

class RelatorioDatabaseInstance<T> implements DatabaseInstance {
  Database? _database;
  RelatorioDatabaseInstance._();

  static final RelatorioDatabaseInstance instance =
      RelatorioDatabaseInstance._();

  @override
  Future<Database?> get database async {
    if (_database != null) return _database;

    return await _initDatabase();
  }

  Future<Database?> _initDatabase() async {
    return await openDatabase(
      join(await getDatabasesPath(), 'relatorios.db'),
      version: 1,
      onCreate: _onCreate,
    );
  }

  Future<void> _onCreate(db, versao) async {
    await db.execute(SQLCommands.createAplicacaoTable);

    await db.execute(SQLCommands.createContratanteTable);

    await db.execute(SQLCommands.createIdentificacaoAreaTratadaTable);

    await db.execute(SQLCommands.createCaracteristicasProdutoAplicadoTable);

    await db.execute(SQLCommands.createRecomendacoesTecnicasTable);

    await db.execute(SQLCommands.createAplicacoesTable);

    await db.execute(SQLCommands.createRelatorioAplicacaoTable);

    await db.execute(SQLCommands.createContratoPrestacaoServicoTable);

    await db.execute(SQLCommands.createDadosResponsavelTable);
  }
}
