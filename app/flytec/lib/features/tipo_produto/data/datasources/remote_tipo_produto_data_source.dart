import 'package:flutter/foundation.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/tipo_produto/data/models/tipo_produto_model.dart';
import 'package:http/http.dart' as http;

import '../../../../core/errors/exception.dart';
import '../../../../core/network/network_info.dart';

abstract class IRemoteTipoProdutoDataSource {
  Future<List<TipoProdutoModel>> getTipoDeProdutos();
}

class RemoteTipoProdutoDataSourceImpl implements IRemoteTipoProdutoDataSource {
  final http.Client client;
  final NetWorkInfoImpl? netWorkInfoI;
  RemoteTipoProdutoDataSourceImpl(
      {required this.client, required this.netWorkInfoI});

  @override
  Future<List<TipoProdutoModel>> getTipoDeProdutos() async {
    if (await netWorkInfoI!.isConnected) {
      final response = await client.get(
        Uri.parse("https://flytec.keltecnologia.com.br/api/v1/TipoProduto"),
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ${Util.Token}',
        },
      );
      debugPrint("TIPO DE PRODUTOS");
      debugPrint(response.body.toString());
      if (response.statusCode == 200) {
        return Future.value(
          tipoProdutoModelFromJson(response.body),
        );
      } else {
        throw ServerException(message: "StatusCode: ${response.statusCode}");
      }
    } else {
      throw NetWorkException(message: "Sem conexão a internet");
    }
  }
}
