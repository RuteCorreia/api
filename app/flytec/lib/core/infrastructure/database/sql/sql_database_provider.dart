import 'package:flytec/core/infrastructure/database/database_instance.dart';
import 'package:flytec/core/infrastructure/database/database_provider.dart';

class SQLDatabaseProvider implements DatabaseProvider {
  late final DatabaseInstance _instance;
  SQLDatabaseProvider(DatabaseInstance instance) {
    _instance = instance;
  }

  @override
  Future<int> insert(Map<String, dynamic> values, String table) async {
    final database = await _instance.database;
    return database!.insert(table, values);
  }

  @override
  Future<List<Map<String, dynamic>>> obtainTableElementsList(
      String table) async {
    final database = await _instance.database;
    return database!.rawQuery('SELECT * FROM $table');
  }

  @override
  Future<int> update(
      Map<String, dynamic> values, String table, String idTable) async {
    final database = await _instance.database;
    return database!
        .update(table, values, where: 'id = ?', whereArgs: [idTable]);
  }
  
  @override
  Future<Map<String, dynamic>?> obtainElementTableById(
      String table, String idTable) async {
      final database = await _instance.database;
      final result = await database!.rawQuery('SELECT * FROM $table WHERE id = ?', [idTable]);
    return result.isNotEmpty ? result.first : null;
  }
}
