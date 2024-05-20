import 'package:flytec/core/infrastructure/database/database_instance.dart';
import 'package:flytec/core/infrastructure/database/sql/sql_commands.dart';
import 'package:sqflite/sqflite.dart';
// ignore: depend_on_referenced_packages
import 'package:path/path.dart' show join;

class RelatorioFirefightingDatabaseInstance<T> implements DatabaseInstance {
  Database? _database;
  RelatorioFirefightingDatabaseInstance._();

  static final RelatorioFirefightingDatabaseInstance instance =
      RelatorioFirefightingDatabaseInstance._();

  @override
  Future<Database?> get database async {
    if (_database != null) return _database;

    return await _initDatabase();
  }

  Future<Database?> _initDatabase() async {
    return await openDatabase(
      join(await getDatabasesPath(), 'relatorios_firefighting.db'),
      version: 1,
      onCreate: _onCreate,
    );
  }

  Future<void> _onCreate(db, versao) async {
    await db.execute(SQLCommands.createFirefightingTable);

    await db.execute(SQLCommands.createPistaFirefightingTable);

    await db.execute(SQLCommands.createLocalFirefightingTable);

    await db.execute(SQLCommands.createDecolagemPousoFirefightingTable);

    await db.execute(SQLCommands.createDadosResponsavelFirefightingTable);

    await db
        .execute(SQLCommands.createCoordenadorBaseOperacionalFirefightingTable);

    await db.execute(SQLCommands.createComandanteOcorrenciaFirefightingTable);
  }
}
