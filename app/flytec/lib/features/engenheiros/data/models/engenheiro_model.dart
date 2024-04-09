import 'dart:convert';

import 'package:flytec/features/engenheiros/domain/entities/engenheiro_entity.dart';

List<EngenheiroModel> engenheiroModelFromJson(String str) =>
    List<EngenheiroModel>.from(
        json.decode(str).map((x) => EngenheiroModel.fromJson(x)));

String engenheiroModelToJson(List<EngenheiroModel> data) =>
    json.encode(List<dynamic>.from(data.map((x) => x.toJson())));

class EngenheiroModel extends EngenheiroEntity {
  const EngenheiroModel({
    super.idEngenheiro,
    super.nomeEngenheiro,
    super.email,
    super.senha,
    super.crea,
    super.assinatura,
  });

  factory EngenheiroModel.fromJson(Map<String, dynamic> json) =>
      EngenheiroModel(
        idEngenheiro: json["id"],
        nomeEngenheiro: json["nome"],
        email: json["email"],
        senha: json["senha"],
        crea: json["crea"],
        assinatura: json["assinatura"],
      );

  Map<String, dynamic> toJson() => {
        "idEngenheiro": idEngenheiro,
        "nomeEngenheiro": nomeEngenheiro,
        "email": email,
        "senha": senha,
        "crea": crea,
        "assinatura": assinatura,
      };
}
