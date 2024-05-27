import 'package:flytec/core/infrastructure/network/endpoints.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/piloto/data/models/excutores_model.dart';
import 'package:http/http.dart' as http;

import '../../../../core/errors/exception.dart';
import '../../../../core/network/network_info.dart';

abstract class IRemotePilotoDataSource {
  Future<List<PilotoModel>> getPilotos();
}

class RemotePilotoDataSourceImpl implements IRemotePilotoDataSource {
  final http.Client client;
  final NetWorkInfoImpl? netWorkInfoI;
  RemotePilotoDataSourceImpl(
      {required this.client, required this.netWorkInfoI});

  @override
  Future<List<PilotoModel>> getPilotos() async {
    if (await netWorkInfoI!.isConnected) {
      final response = await client.get(
        Uri.parse(Endpoints.piloto),
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ${Util.Token}',
        },
      );

      if (response.statusCode == 200) {
        return Future.value(
          pilotoModelFromJson(response.body),
        );
      } else {
        throw ServerException(message: "StatusCode: ${response.statusCode}");
      }
    } else {
      throw NetWorkException(message: "Sem conexão a internet");
    }
  }
}
