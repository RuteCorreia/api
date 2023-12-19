import 'dart:developer';

import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/data/models/clientes_model.dart';
import 'package:flytec/features/auth/data/models/user_payload_model.dart';
import 'package:flytec/features/cultura/data/models/cultura_model.dart';
import 'package:flytec/features/executor/data/models/excutores_model.dart';
import 'package:flytec/features/piloto/data/models/excutores_model.dart';
import 'package:flytec/features/weather/data/models/weather_model.dart';

class GlobalConfigVars {
  late List<ClientesModel> clientes = [];
  late List<ExecutorModel> executores = [];
  late List<PilotoModel> pilotos = [];
  late List<CulturaModel> culturas = [];

  late UserPayloadModel userPayload;
  late WeatherModel weather;

  void setClientes({required List<ClientesModel>? clientesData}) {
    clientes = clientesData!;
  }

  void setCulturas({required List<CulturaModel>? culturaData}) {
    culturas = culturaData!;
  }

  void setExecutores({required List<ExecutorModel>? executoresData}) {
    executores = executoresData!;
  }

  void setPilotos({required List<PilotoModel>? pilotosData}) {
    pilotos = pilotosData!;
  }

  void setWeatherData({required WeatherModel? weatherData}) {
    weather = weatherData!;
  }

  void setPreloadDataFromJson({
    required Map<String, dynamic> preloadJson,
  }) {
    try {
      print("SETADO COM SUCESSO ");

      clientes = clientesModelFromJson(preloadJson["clientes"]);
      culturas = culturaModelFromJson(preloadJson["culturas"]);
      executores = executorModelFromJson(preloadJson["executores"]);
      pilotos = pilotoModelFromJson(preloadJson["pilotos"]);
      weather = weatherModelFromJson(preloadJson["weather"]);
      Util.Token = preloadJson["token"];
    } catch (e) {
      log(preloadJson["clientes"]);
    }
  }
}
