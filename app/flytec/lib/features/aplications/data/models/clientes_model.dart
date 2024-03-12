// To parse this JSON data, do
//
//     final clientesModel = clientesModelFromJson(jsonString);

import 'dart:convert';

import 'package:flytec/features/aplications/models/contratante.dart';

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
  final String? uf;
  final String? cidade;
  bool isSelected;

  ClientesModel({
    this.idCliente,
    this.nomeCliente,
    this.idTipoCliente,
    this.isSelected = false,
    this.cpf,
    this.rg,
    this.cnpj,
    this.cidade,
    this.inscricaoEstadual,
    this.endereco,
    this.telefone1,
    this.telefone2,
    this.uf,
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
        cidade: json["cidade"] ?? "",
        endereco: json["endereco"],
        telefone1: json["telefone1"],
        telefone2: json["telefone2"],
        email: json["email"],
        senha: json["senha"],
        precificacao: json["precificacao"],
        admin: json["admin"],
        uf: json["uf"] ?? "",
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
        "cidade": cidade,
        "email": email,
        "senha": senha,
        "precificacao": precificacao,
        "admin": admin,
        "uf": uf
      };
  factory ClientesModel.fromContratante(Contratante contratante) =>
      ClientesModel(
          nomeCliente: contratante.nome,
          idTipoCliente: contratante.tipoContratante == "Pessoa Física" ? 1 : 2,
          cpf: contratante.cpf,
          rg: contratante.rg,
          cnpj: contratante.cnpj,
          inscricaoEstadual: contratante.inscricaoEstadual,
          cidade: contratante.cidade,
          endereco: contratante.endereco,
          telefone1: "",
          telefone2: "",
          email: "",
          senha: "",
          precificacao: "",
          admin: false,
          uf: contratante.uf,
          idCliente: int.tryParse(contratante.idContrante!));
}
