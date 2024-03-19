
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/bulas/data/models/bula_model.dart';
import 'package:http/http.dart' as http;

import '../../../../core/errors/exception.dart';
import '../../../../core/network/network_info.dart';

abstract class IRemoteBulaDataSource {
  Future<List<BulaModel>> getBulas();
}

class RemoteBulaDataSourceImpl implements IRemoteBulaDataSource {
  final http.Client client;
  final NetWorkInfoImpl? netWorkInfoI;
  RemoteBulaDataSourceImpl({required this.client, required this.netWorkInfoI});

  @override
  Future<List<BulaModel>> getBulas() async {
    if (await netWorkInfoI!.isConnected) {
      final response = await client.get(
        Uri.parse("https://flytec.keltecnologia.com.br/api/v1/bula"),
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ${Util.Token}',
        },
      );
      if (response.statusCode == 200) {
        return Future.value(bulaModelFromJson(response.body));
      } else {
        throw ServerException(message: "StatusCode: ${response.statusCode}");
      }
    } else {
      throw NetWorkException(message: "Sem conexão a internet");
    }
  }
}
