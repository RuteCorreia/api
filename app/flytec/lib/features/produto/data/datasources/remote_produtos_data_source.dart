import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/produto/data/models/produto_model.dart';
import 'package:http/http.dart' as http;

import '../../../../core/errors/exception.dart';
import '../../../../core/network/network_info.dart';

abstract class IRemoteProdutoDataSource {
  Future<List<ProdutoModel>> getProdutos();
}

class RemoteProdutoDataSourceImpl implements IRemoteProdutoDataSource {
  final http.Client client;
  final NetWorkInfoImpl? netWorkInfoI;
  RemoteProdutoDataSourceImpl(
      {required this.client, required this.netWorkInfoI});

  @override
  Future<List<ProdutoModel>> getProdutos() async {
    if (await netWorkInfoI!.isConnected) {
      final response = await client.get(
        Uri.parse("https://flytec.keltecnologia.com.br/api/v1/produto"),
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ${Util.Token}',
        },
      );
      print("PRODUTOS");
      print(response.body);
      if (response.statusCode == 200) {
        return Future.value(
          produtoModelFromJson(response.body),
        );
      } else {
        throw ServerException(message: "StatusCode: ${response.statusCode}");
      }
    } else {
      throw NetWorkException(message: "Sem conexão a internet");
    }
  }
}
