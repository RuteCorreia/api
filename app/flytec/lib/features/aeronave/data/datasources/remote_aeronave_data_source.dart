import 'package:flutter/foundation.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aeronave/data/models/aeronave_model.dart';
import 'package:http/http.dart' as http;

import '../../../../core/errors/exception.dart';
import '../../../../core/network/network_info.dart';

abstract class IRemoteAeroNaveDataSource {
  Future<List<AeroNaveModel>> getAeroNaves();
}

class RemoteAeroNaveDataSourceImpl implements IRemoteAeroNaveDataSource {
  final http.Client client;
  final NetWorkInfoImpl? netWorkInfoI;
  RemoteAeroNaveDataSourceImpl(
      {required this.client, required this.netWorkInfoI});

  @override
  Future<List<AeroNaveModel>> getAeroNaves() async {
    if (await netWorkInfoI!.isConnected) {
      final response = await client.get(
        Uri.parse("https://flytec.keltecnologia.com.br/api/v1/Aeronave"),
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ${Util.Token}',
        },
      );
      debugPrint("AERONAVES");
      debugPrint(response.body.toString());
      if (response.statusCode == 200) {
        return Future.value(
          aeroNaveModelFromJson(response.body),
        );
      } else {
        throw ServerException(message: "StatusCode: ${response.statusCode}");
      }
    } else {
      throw NetWorkException(message: "Sem conexão a internet");
    }
  }
}
