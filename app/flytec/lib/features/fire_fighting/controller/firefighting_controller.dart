import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:flytec/core/enums/dashboard_state.dart';
import 'package:flytec/core/infrastructure/database/database_instance.dart';
import 'package:flytec/core/infrastructure/database/sql/database_instances/relatorio_firefighting_database_instance.dart';
import 'package:flytec/core/infrastructure/database/sql/sql_database_provider.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/features/fire_fighting/models/dados_responsavel.dart';
import 'package:flytec/features/fire_fighting/models/decolagem_pouso_firefighting.dart';
import 'package:flytec/features/fire_fighting/models/firefighting.dart';
import 'package:flytec/features/fire_fighting/models/local_firefighting.dart';
import 'package:flytec/features/fire_fighting/models/pista_firefighting.dart';

class FirefightingController extends ChangeNotifier {
  final DatabaseInstance _databaseInstance =
      RelatorioFirefightingDatabaseInstance.instance;
  SQLDatabaseProvider get _sqlDatabaseProvider =>
      SQLDatabaseProvider(_databaseInstance);

  Future<Map<String, dynamic>?> getElementById(int id, String table) async {
    return await _sqlDatabaseProvider.obtainElementTableById(table, id);
  }

  Firefighting? firefightingSelected;
  final ValueNotifier<List<Firefighting>?> firefightingList =
      ValueNotifier<List<Firefighting>?>([]);

  void setUpdateUpdateView(VoidCallback updateView) {
    notifyListeners();
  }

  void setFirefightingSelected(Firefighting firefighting) {
    firefightingSelected = firefighting;
    notifyListeners();
  }

  Future<void> obtainReportsFirefightings() async {
    List<Firefighting> firefightingListResult;
    final reports =
        await _sqlDatabaseProvider.obtainTableElementsList("Firefighting");
    
    final listFirefighting =
        reports.map((e) => Firefighting.fromJson(e)).toList().where((element) {
      final refUsuario =
          '${getIt<GlobalConfigVars>().userPayload.nrUsuario}_${element.id}';
      return element.refId == refUsuario;
    }).toList();
    firefightingListResult = [];
    if (reports.isEmpty) {
      firefightingList.value = [];
      notifyListeners();
      return;
    }
    for (int i = 0; i < listFirefighting.length; i++) {
      final firefighting = listFirefighting[i];
      final idNew =
          '${getIt<GlobalConfigVars>().userPayload.nrUsuario}_${firefighting.id}';
      firefighting.refId = idNew;
      final getPistaFirefightingDb =
          await _sqlDatabaseProvider.obtainElementTableById(
              "PistaFirefighting", firefighting.idPistaFirefighting);
      PistaFirefighting pista =
          PistaFirefighting.fromJson(getPistaFirefightingDb);
      pista.id = firefighting.idPistaFirefighting;
      firefighting.pista = pista;

      final getLocalIncendioDb =
          await _sqlDatabaseProvider.obtainElementTableById(
              "LocalFirefighting", firefighting.idLocalFirefighting);
      LocalFirefighting localIncendio =
          LocalFirefighting.fromJson(getLocalIncendioDb);
      localIncendio.id = firefighting.idLocalFirefighting;
      firefighting.localIncendio = localIncendio;

      final getDecolagemPousoFirefightingListDb = await _sqlDatabaseProvider
          .obtainTableElementsList("DecolagemPousoFirefighting");

      final listDecolagemPousoFirefightingListDb =
          getDecolagemPousoFirefightingListDb
              .map((e) => DecolagemPousoFirefighting.fromJson(e))
              .toList();

      final selectedListDecolagemPousoFirefightingList =
          listDecolagemPousoFirefightingListDb
              .where((element) => element.idFirefighting == firefighting.id)
              .toList();

      firefighting.decolagemPousoFirefightingList =
          selectedListDecolagemPousoFirefightingList;

      final getDadosResponsavelFirefightingDb =
          await _sqlDatabaseProvider.obtainElementTableById(
              "DadosResponsavelFirefighting", firefighting.idDadosResponsavel);
      DadosResponsavel dadosResponsavel =
          DadosResponsavel.fromJson(getDadosResponsavelFirefightingDb);
      dadosResponsavel.id = firefighting.idDadosResponsavel;
      firefighting.dadosResponsavel = dadosResponsavel;

      firefightingListResult.add(firefighting);
    }
    firefightingList.value = firefightingListResult;
    notifyListeners();
  }

  Future<int?> createElementInTable(
      Map<String, dynamic> data, String table) async {
    int? id = await _sqlDatabaseProvider.insert(data, table);
    await obtainReportsFirefightings();
    return id;
  }

  Future<void> updateElementInTable(
      int id, Map<String, dynamic> data, String table) async {
    await _sqlDatabaseProvider.update(data, table, id.toString());
    await obtainReportsFirefightings();
  }

  int obtainQuantityReportsByState(DashBoardState state) {
    return firefightingList.value!
        .where((element) => element.state == state)
        .toList()
        .length;
  }
}
