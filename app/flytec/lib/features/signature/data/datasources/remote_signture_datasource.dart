import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:http/http.dart' as http;

import '../../../../core/errors/exception.dart';
import '../../../../core/network/network_info.dart';

abstract class RemoteSignatureDataSource {
  Future<String> getSignature();
  Future<void> saveSignature(String imageEncoded);
}

class RemoteSignatureDataSourceImpl implements RemoteSignatureDataSource {
  final http.Client client;
  final NetWorkInfoImpl? netWorkInfoI;
  RemoteSignatureDataSourceImpl(
      {required this.client, required this.netWorkInfoI});

  @override
  Future<String> getSignature() async {
    if (await netWorkInfoI!.isConnected) {
      final response = await client.post(
          Uri.parse(
              "https://flytec.keltecnologia.com.br/api/v1/Auth/getUserSignature"),
          body: {'"${getIt<GlobalConfigVars>().userPayload.jti}"'});
      if (response.statusCode == 200) {
        return Future.value(response.body);
      } else {
        throw ServerException(message: "StatusCode: ${response.statusCode}");
      }
    } else {
      throw NetWorkException(message: "Sem conexão a internet");
    }
  }

  @override
  Future<void> saveSignature(String imageEncoded) async {
    if (await netWorkInfoI!.isConnected) {
      final response = await client.patch(
          Uri.parse(
              "https://flytec.keltecnologia.com.br/api/v1/Auth/saveSignature"),
          body: {"assinatura": imageEncoded});
      if (response.statusCode == 200) {
        return Future.value();
      } else {
        throw ServerException(message: "StatusCode: ${response.statusCode}");
      }
    } else {
      throw NetWorkException(message: "Sem conexão a internet");
    }
  }
}
