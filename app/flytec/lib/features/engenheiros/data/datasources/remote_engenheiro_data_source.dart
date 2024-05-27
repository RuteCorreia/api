import 'package:flytec/core/infrastructure/network/endpoints.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/engenheiros/data/models/engenheiro_model.dart';
import 'package:http/http.dart' as http;

import '../../../../core/errors/exception.dart';
import '../../../../core/network/network_info.dart';

abstract class IRemoteEngenheiroDataSource {
  Future<List<EngenheiroModel>> getEngenheiros();
}

class RemoteEngenheiroDataSourceImpl implements IRemoteEngenheiroDataSource {
  final http.Client client;
  final NetWorkInfoImpl? netWorkInfoI;
  RemoteEngenheiroDataSourceImpl(
      {required this.client, required this.netWorkInfoI});

  @override
  Future<List<EngenheiroModel>> getEngenheiros() async {
    if (await netWorkInfoI!.isConnected) {
      final response = await client.get(
        Uri.parse(Endpoints.engenheiro),
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ${Util.Token}',
        },
      );

      if (response.statusCode == 200) {
        return Future.value(
          engenheiroModelFromJson(response.body),
        );
      } else {
        throw ServerException(message: "StatusCode: ${response.statusCode}");
      }
    } else {
      throw NetWorkException(message: "Sem conexão a internet");
    }
  }
}
