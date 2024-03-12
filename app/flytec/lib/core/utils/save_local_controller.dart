import 'dart:convert';

import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aeronave/data/models/aeronave_model.dart';
import 'package:flytec/features/altura_voo/data/models/altura_voo_model.dart';
import 'package:flytec/features/alvo_biologico/data/models/alvo_biologico_model.dart';
import 'package:flytec/features/cultura/data/models/cultura_model.dart';
import 'package:flytec/features/equipamento/data/models/equipamento_model.dart';
import 'package:flytec/features/executor/data/models/excutores_model.dart';
import 'package:flytec/features/piloto/data/models/excutores_model.dart';
import 'package:flytec/features/produto/data/models/produto_model.dart';
import 'package:flytec/features/tipo_produto/data/models/tipo_produto_model.dart';
import 'package:flytec/features/veiculante/data/models/alvo_biologico_model.dart';
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
      "culturas": culturaModelToJson(getIt<GlobalConfigVars>().culturas),
      "produtos": produtoModelToJson(getIt<GlobalConfigVars>().produtos),
      "pilotos": pilotoModelToJson(getIt<GlobalConfigVars>().pilotos),
      "aeronaves": aeroNaveModelToJson(getIt<GlobalConfigVars>().aeronaves),
      "equipamentos":
          equipamentoModelToJson(getIt<GlobalConfigVars>().equipamentos),
      "tipoprodutos":
          tipoProdutoModelToJson(getIt<GlobalConfigVars>().tiposProdutos),
      "veiculantes":
          veiculanteModelToJson(getIt<GlobalConfigVars>().veiculantes),
      "alturavoo": alturaVooModelToJson(getIt<GlobalConfigVars>().alturaVoo),
      "alvosBiologicos":
          alvoBiologicoModelToJson(getIt<GlobalConfigVars>().alvosBiologicos)
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
