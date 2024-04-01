import 'package:flytec/core/infrastructure/network/endpoints.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/cultura/data/models/cultura_model.dart';
import 'package:http/http.dart' as http;

import '../../../../core/errors/exception.dart';
import '../../../../core/network/network_info.dart';

abstract class IRemoteCulturaDataSource {
  Future<List<CulturaModel>> getCulturas();
}

class RemoteCulturaDataSourceImpl implements IRemoteCulturaDataSource {
  final http.Client client;
  final NetWorkInfoImpl? netWorkInfoI;
  RemoteCulturaDataSourceImpl(
      {required this.client, required this.netWorkInfoI});

  @override
  Future<List<CulturaModel>> getCulturas() async {
    if (await netWorkInfoI!.isConnected) {
      final response = await client.get(
        Uri.parse(Endpoints.cultura),
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ${Util.Token}',
        },
      );

      if (response.statusCode == 200) {
        return Future.value(
          culturaModelFromJson(response.body),
        );
      } else {
        throw ServerException(message: "StatusCode: ${response.statusCode}");
      }
    } else {
      throw NetWorkException(message: "Sem conexão a internet");
    }
  }
}
