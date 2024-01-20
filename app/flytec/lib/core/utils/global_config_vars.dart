import 'dart:developer';

import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aeronave/data/models/aeronave_model.dart';
import 'package:flytec/features/altura_voo/data/models/altura_voo_model.dart';
import 'package:flytec/features/alvo_biologico/data/models/alvo_biologico_model.dart';
import 'package:flytec/features/aplications/data/models/clientes_model.dart';
import 'package:flytec/features/auth/data/models/user_payload_model.dart';
import 'package:flytec/features/cultura/data/models/cultura_model.dart';
import 'package:flytec/features/equipamento/data/models/equipamento_model.dart';
import 'package:flytec/features/executor/data/models/excutores_model.dart';
import 'package:flytec/features/piloto/data/models/excutores_model.dart';
import 'package:flytec/features/produto/data/models/produto_model.dart';
import 'package:flytec/features/tipo_produto/data/models/tipo_produto_model.dart';
import 'package:flytec/features/veiculante/data/models/alvo_biologico_model.dart';
import 'package:flytec/features/weather/data/models/weather_model.dart';

class GlobalConfigVars {
  late List<ClientesModel> clientes = [];
  late List<ExecutorModel> executores = [];
  late List<PilotoModel> pilotos = [];
  late List<CulturaModel> culturas = [];
  late List<ProdutoModel> produtos = [];
  late List<AlvoBiologicoModel> alvosBiologicos = [];
  late List<VeiculanteModel> veiculantes = [];
  late List<AeroNaveModel> aeronaves = [];
  late List<EquipamentoModel> equipamentos = [];
  late List<TipoProdutoModel> tiposProdutos = [];
  late List<AlturaVooModel> alturaVoo = [];

  late UserPayloadModel userPayload;
  late WeatherModel weather;

  void setClientes({required List<ClientesModel>? clientesData}) {
    clientes = clientesData!;
  }

  void setEquipamentos({required List<EquipamentoModel>? data}) {
    equipamentos = data!;
  }

  void setAlturaVoo({required List<AlturaVooModel>? data}) {
    alturaVoo = data!;
  }

  void setTipoProdutos({required List<TipoProdutoModel>? data}) {
    tiposProdutos = data!;
  }

  void setVeiculantes({required List<VeiculanteModel>? data}) {
    veiculantes = data!;
  }

  void setAeroNaves({required List<AeroNaveModel>? data}) {
    aeronaves = data!;
  }

  void setProdutos({required List<ProdutoModel>? produtosData}) {
    produtos = produtosData!;
  }

  void setAlvosBilogicos(
      {required List<AlvoBiologicoModel>? alvosBilogicosData}) {
    alvosBiologicos = alvosBilogicosData!;
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
      produtos = produtoModelFromJson(preloadJson["produtos"]);
      aeronaves = aeroNaveModelFromJson(preloadJson["aeronaves"]);
      veiculantes = veiculanteModelFromJson(preloadJson["veiculantes"]);
      equipamentos = equipamentoModelFromJson(preloadJson["equipamentos"]);
      tiposProdutos = tipoProdutoModelFromJson(preloadJson["tipoprodutos"]);
      alturaVoo = alturaVooModelFromJson(preloadJson["alturavoo"]);

      alvosBiologicos =
          alvoBiologicoModelFromJson(preloadJson["alvosBiologicos"]);
      Util.Token = preloadJson["token"];
    } catch (e) {
      log(preloadJson["clientes"]);
    }
  }
}
