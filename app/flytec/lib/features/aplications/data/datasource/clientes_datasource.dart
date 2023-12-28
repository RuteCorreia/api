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
        "nomeCliente": addClientParams!.nome,
        "idTipoCliente": 0,
        "cpf": addClientParams.cpf,
        "rg": addClientParams.rg,
        "cnpj": addClientParams.cnpj,
        "inscricaoEstadual": addClientParams.inscricaoEstadual,
        "endereco": addClientParams.endereco,
        "telefone1": addClientParams.telefone1,
        "telefone2": addClientParams.telefone2,
        "email": addClientParams.email,
        "senha": addClientParams.senha,
        "precificacao": addClientParams.precificacao,
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
