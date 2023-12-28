import 'package:flutter/foundation.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/veiculante/data/models/alvo_biologico_model.dart';
import 'package:http/http.dart' as http;

import '../../../../core/errors/exception.dart';
import '../../../../core/network/network_info.dart';

abstract class IRemoteVeiculanteDataSource {
  Future<List<VeiculanteModel>> getVeiculantes();
}

class RemoteVeiculanteDataSourceImpl implements IRemoteVeiculanteDataSource {
  final http.Client client;
  final NetWorkInfoImpl? netWorkInfoI;
  RemoteVeiculanteDataSourceImpl(
      {required this.client, required this.netWorkInfoI});

  @override
  Future<List<VeiculanteModel>> getVeiculantes() async {
    if (await netWorkInfoI!.isConnected) {
      final response = await client.get(
        Uri.parse("https://flytec.keltecnologia.com.br/api/v1/Veiculante"),
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ${Util.Token}',
        },
      );
      debugPrint("VEICULANTES");
      debugPrint(response.body);
      if (response.statusCode == 200) {
        return Future.value(
          veiculanteModelFromJson(response.body),
        );
      } else {
        throw ServerException(message: "StatusCode: ${response.statusCode}");
      }
    } else {
      throw NetWorkException(message: "Sem conexão a internet");
    }
  }
}
