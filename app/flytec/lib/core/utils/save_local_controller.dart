import 'dart:convert';

import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/executor/data/models/excutores_model.dart';
import 'package:flytec/features/piloto/data/models/excutores_model.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../../features/aplications/data/models/clientes_model.dart';

class SaveLocalDataController {
  final SharedPreferences? preferences;
  SaveLocalDataController({required this.preferences});
  String clientesPreloadModelToJson(List<ClientesModel>? clientes) =>
      json.encode(List<dynamic>.from(clientes!.map((x) => x.toJson())));

  Map<String, dynamic> initializeLocalData() {
    var preloadData = {
      "clientes":
          clientesPreloadModelToJson(getIt<GlobalConfigVars>().clientes),
      "token": Util.Token,
      "executores": executorModelToJson(getIt<GlobalConfigVars>().executores),
      "pilotos": pilotoModelToJson(getIt<GlobalConfigVars>().pilotos)
    };
    return preloadData;
  }

  Future salvarLocalPreloadData(
      {required Map<String, dynamic> preloadData}) async {
    var prefers = await SharedPreferences.getInstance();
    return prefers.setString("preload", json.encode(preloadData));
  }

  Future getLocalPreloadData() async {
    var perf = await SharedPreferences.getInstance();
    var p = perf.getString("preload");
    return p;
  }
}
