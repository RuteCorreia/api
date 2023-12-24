import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/executor/data/models/excutores_model.dart';
import 'package:http/http.dart' as http;

import '../../../../core/errors/exception.dart';
import '../../../../core/network/network_info.dart';

abstract class IRemoteExecutorDataSource {
  Future<List<ExecutorModel>> getExecutores();
}

class RemoteExecutorDataSourceImpl implements IRemoteExecutorDataSource {
  final http.Client client;
  final NetWorkInfoImpl? netWorkInfoI;
  RemoteExecutorDataSourceImpl(
      {required this.client, required this.netWorkInfoI});

  @override
  Future<List<ExecutorModel>> getExecutores() async {
    if (await netWorkInfoI!.isConnected) {
      final response = await client.get(
        Uri.parse("https://flytec.keltecnologia.com.br/api/v1/Executor"),
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ${Util.Token}',
        },
      );
      print("EXECUTORES");
      print(response.body);
      if (response.statusCode == 200) {
        return Future.value(
          executorModelFromJson(response.body),
        );
      } else {
        throw ServerException(message: "StatusCode: ${response.statusCode}");
      }
    } else {
      throw NetWorkException(message: "Sem conexão a internet");
    }
  }
}
