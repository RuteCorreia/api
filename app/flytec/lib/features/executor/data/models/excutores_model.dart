// To parse this JSON data, do
//
//     final executorModel = executorModelFromJson(jsonString);

import 'dart:convert';

import 'package:flytec/features/executor/domain/entities/executor_entity.dart';

List<ExecutorModel> executorModelFromJson(String str) =>
    List<ExecutorModel>.from(
        json.decode(str).map((x) => ExecutorModel.fromJson(x)));

String executorModelToJson(List<ExecutorModel> data) =>
    json.encode(List<dynamic>.from(data.map((x) => x.toJson())));

class ExecutorModel extends ExecutorEntity {
  const ExecutorModel({
    super.idExecutor,
    super.nome,
    super.email,
    super.senha,
    super.cfta,
    super.assinatura,
  });

  factory ExecutorModel.fromJson(Map<String, dynamic> json) => ExecutorModel(
        idExecutor: json["id"],
        nome: json["nome"],
        email: json["email"],
        senha: json["senha"],
        cfta: json["cfta"],
        assinatura: json["assinatura"],
      );

  Map<String, dynamic> toJson() => {
        "idExecutor": idExecutor,
        "nome": nome,
        "email": email,
        "senha": senha,
        "cfta": cfta,
        "assinatura": assinatura,
      };
}
