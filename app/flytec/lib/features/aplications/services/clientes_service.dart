import 'dart:convert';
import 'dart:developer';

import 'package:flytec/features/aplications/data/models/clientes_model.dart';
import 'package:shared_preferences/shared_preferences.dart';

class ClienteService {
  List<ClientesModel> clientesModelData = [];

  Future saveClientesLocal(dados) async {
    print("salvando phone no cache ...");
    var perf = await SharedPreferences.getInstance();
    return perf.setString("clientes", json.encode(dados));
  }

  Future getClientesLocalData() async {
    log("Buscando phones localmente  ...");
    var perf = await SharedPreferences.getInstance();
    var phonesData = perf.get("clientes");
    return phonesData;
  }

  setClientesData({required List<Map<String, dynamic>> clintesJson}) {
    try {
      clientesModelData = clientesModelFromJson(clintesJson.toString());
    } catch (e) {
      log("Nao foi possivel settar os dados dos telefones");
    }
  }

  updateClientesData() {
    log("Atualizando dados dos phones ...");
    Map<String, dynamic> phonesJson = {
      "clientes": clientesModelToJson(clientesModelData)
    };
    saveClientesLocal(phonesJson);
  }
}
