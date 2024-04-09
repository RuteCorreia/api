// To parse super JSON data, do
//
//     final pilotoModel = pilotoModelFromJson(jsonString);

import 'dart:convert';

import 'package:flytec/features/piloto/domain/entities/piloto_entity.dart';

List<PilotoModel> pilotoModelFromJson(String str) => List<PilotoModel>.from(
    json.decode(str).map((x) => PilotoModel.fromJson(x)));

String pilotoModelToJson(List<PilotoModel> data) =>
    json.encode(List<dynamic>.from(data.map((x) => x.toJson())));

class PilotoModel extends PilotoEntity {
  const PilotoModel({
    super.idPiloto,
    super.idEmpresa,
    super.nomePiloto,
    super.email,
    super.senha,
    super.cdac,
    super.assinatura,
    super.porcentagemComissao,
  });

  factory PilotoModel.fromJson(Map<String, dynamic> json) => PilotoModel(
        idPiloto: json["idPiloto"],
        idEmpresa: json["idEmpresa"],
        nomePiloto: json["nome"],
        email: json["email"],
        senha: json["senha"],
        cdac: json["cdac"],
        assinatura: json["assinatura"],
        porcentagemComissao: json["porcentagemComissao"],
      );

  Map<String, dynamic> toJson() => {
        "idPiloto": idPiloto,
        "idEmpresa": idEmpresa,
        "nomePiloto": nomePiloto,
        "email": email,
        "senha": senha,
        "cdac": cdac,
        "assinatura": assinatura,
        "porcentagemComissao": porcentagemComissao,
      };
}
