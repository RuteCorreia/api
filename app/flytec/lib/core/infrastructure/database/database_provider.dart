abstract class DatabaseProvider {
  Future<int> insert(Map<String, dynamic> values, String table);
  Future<List<Map<String, dynamic>>> obtainTableElementsList(String table);
  Future<int> update(Map<String, dynamic> values, String table,String idTable);
  Future<Map<String,dynamic>> obtainElementTableById(String table,String idTable);
}
