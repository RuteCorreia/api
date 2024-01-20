import 'package:flutter/foundation.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/equipamento/data/models/equipamento_model.dart';
import 'package:http/http.dart' as http;

import '../../../../core/errors/exception.dart';
import '../../../../core/network/network_info.dart';

abstract class IRemoteEquipamentoDataSource {
  Future<List<EquipamentoModel>> getEquipamentos();
}

class RemoteEquipamentoDataSourceImpl implements IRemoteEquipamentoDataSource {
  final http.Client client;
  final NetWorkInfoImpl? netWorkInfoI;
  RemoteEquipamentoDataSourceImpl(
      {required this.client, required this.netWorkInfoI});

  @override
  Future<List<EquipamentoModel>> getEquipamentos() async {
    if (await netWorkInfoI!.isConnected) {
      final response = await client.get(
        Uri.parse("https://flytec.keltecnologia.com.br/api/v1/Equipamento"),
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ${Util.Token}',
        },
      );
      debugPrint("EQUIAPAMENTOS");
      debugPrint(response.body.toString());
      if (response.statusCode == 200) {
        return Future.value(
          equipamentoModelFromJson(response.body),
        );
      } else {
        throw ServerException(message: "StatusCode: ${response.statusCode}");
      }
    } else {
      throw NetWorkException(message: "Sem conexão a internet");
    }
  }
}
