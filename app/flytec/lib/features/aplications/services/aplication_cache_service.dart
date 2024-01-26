import 'dart:convert';
import 'dart:developer';

import 'package:shared_preferences/shared_preferences.dart';

import '../data/models/relatorio_model.dart';

class ReportCacheService {
  List<RelatorioModel> reportList = [];

  Future saveReportOnLocalDevice(dados) async {
    print("salvando relatorio no cache ...");
    var perf = await SharedPreferences.getInstance();
    return perf.setString("relatorios", json.encode(dados));
  }

  Future getReportOnLocalDevice() async {
    log("Buscando relatorios localmente  ...");
    var perf = await SharedPreferences.getInstance();
    var phonesData = perf.get("relatorios");
    return phonesData;
  }

  setReportOnLocalDevice({required List<Map<String, dynamic>> reportData}) {
    try {
      reportList = relatorioModelFromJson(reportData.toString());
    } catch (e) {
      log("Nao foi possivel settar os dados dos telefones");
    }
  }

  updateReportOnLocalDeviceData() {
    log("Atualizando dados dos relatorios ...");
    Map<String, dynamic> reportsJson = {
      "relatorios": relatorioModelToJson(reportList)
    };
    saveReportOnLocalDevice(reportsJson);
  }
}
