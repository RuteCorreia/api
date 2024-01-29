// To parse this JSON data, do
//
//     final clientesModel = clientesModelFromJson(jsonString);

import 'dart:convert';

List<ClientesModel> clientesModelFromJson(String str) =>
    List<ClientesModel>.from(
        json.decode(str).map((x) => ClientesModel.fromJson(x)));

String clientesModelToJson(List<ClientesModel> data) =>
    json.encode(List<dynamic>.from(data.map((x) => x.toJson())));

class ClientesModel {
  final int? idCliente;
  final String? nomeCliente;
  final int? idTipoCliente;
  final String? cpf;
  final String? rg;
  final String? cnpj;
  final String? inscricaoEstadual;
  final String? endereco;
  final String? telefone1;
  final String? telefone2;
  final String? email;
  final String? senha;
  final String? precificacao;
  final bool? admin;
  bool isSelected;

  ClientesModel({
    this.idCliente,
    this.nomeCliente,
    this.idTipoCliente,
    this.isSelected = false,
    this.cpf,
    this.rg,
    this.cnpj,
    this.inscricaoEstadual,
    this.endereco,
    this.telefone1,
    this.telefone2,
    this.email,
    this.senha,
    this.precificacao,
    this.admin,
  });

  factory ClientesModel.fromJson(Map<String, dynamic> json) => ClientesModel(
        idCliente: json["idCliente"],
        nomeCliente: json["nomeCliente"],
        idTipoCliente: json["idTipoCliente"],
        cpf: json["cpf"],
        rg: json["rg"],
        cnpj: json["cnpj"],
        inscricaoEstadual: json["inscricaoEstadual"],
        endereco: json["endereco"],
        telefone1: json["telefone1"],
        telefone2: json["telefone2"],
        email: json["email"],
        senha: json["senha"],
        precificacao: json["precificacao"],
        admin: json["admin"],
      );

  Map<String, dynamic> toJson() => {
        "idCliente": idCliente,
        "nomeCliente": nomeCliente,
        "idTipoCliente": idTipoCliente,
        "cpf": cpf,
        "rg": rg,
        "cnpj": cnpj,
        "inscricaoEstadual": inscricaoEstadual,
        "endereco": endereco,
        "telefone1": telefone1,
        "telefone2": telefone2,
        "email": email,
        "senha": senha,
        "precificacao": precificacao,
        "admin": admin,
      };
}
