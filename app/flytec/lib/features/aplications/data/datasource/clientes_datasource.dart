import 'dart:convert';

import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/data/models/clientes_model.dart';
import 'package:http/http.dart' as http;

abstract class IClientDataSource {
  Future<List<ClientesModel>> getClients();
  Future<bool> addCliente({required AddClientParams? addClientParams});
}

class ClienteDataSourceImpl implements IClientDataSource {
  final baseUrl = "https://flytec.keltecnologia.com.br/api/v1";

  @override
  Future<List<ClientesModel>> getClients() async {
    final response = await http.get(Uri.parse("$baseUrl/Cliente"), headers: {
      'Authorization': 'Bearer ${Util.Token}',
    });
    //  getIt<ClienteService>().saveClientesLocal(response.body);
    getIt<GlobalConfigVars>().clientes = clientesModelFromJson(response.body);

    return Future.value(clientesModelFromJson(response.body));
  }

  @override
  Future<bool> addCliente({required AddClientParams? addClientParams}) async {
    final response = await http.post(
      Uri.parse("$baseUrl/Cliente"),
      headers: {
        'Authorization': 'Bearer ${Util.Token}',
        'Content-Type': 'application/json',
      },
      body: jsonEncode({
        "idCliente": 0,
        "nomeCliente": addClientParams!.nome.toString(),
        "idTipoCliente": 0.toString(),
        "cpf": "0",
        "rg": addClientParams.rg.toString(),
        "cnpj": addClientParams.cnpj.toString(),
        "inscricaoEstadual": addClientParams.inscricaoEstadual.toString(),
        "endereco": addClientParams.endereco.toString(),
        "telefone1": addClientParams.telefone1.toString(),
        "telefone2": addClientParams.telefone2.toString(),
        "email": addClientParams.email.toString(),
        "senha": addClientParams.senha.toString(),
        "precificacao": addClientParams.precificacao.toString(),
        "admin": true
      }),
    );
    print(response.body);
    if (response.statusCode == 200) {
      return true;
    } else {
      return false;
    }
  }
}

class AddClientParams {
  final String? nome;
  final int? idTipoCliente;
  final int? cpf;
  final int? rg;
  final int? cnpj;
  final int? inscricaoEstadual;
  final String? endereco;
  final String? telefone1;
  final String? telefone2;
  final String? email;
  final String? senha;
  final String? precificacao;

  AddClientParams(
      {required this.nome,
      required this.idTipoCliente,
      required this.cpf,
      required this.rg,
      required this.cnpj,
      required this.inscricaoEstadual,
      required this.endereco,
      required this.telefone1,
      required this.telefone2,
      required this.email,
      required this.senha,
      required this.precificacao});
}
