// To parse this JSON data, do
//
//     final identificacaoAreaModel = identificacaoAreaModelFromJson(jsonString);

import 'dart:convert';

IdentificacaoAreaModel identificacaoAreaModelFromJson(String str) =>
    IdentificacaoAreaModel.fromJson(json.decode(str));

String identificacaoAreaModelToJson(IdentificacaoAreaModel data) =>
    json.encode(data.toJson());

class IdentificacaoAreaModel {
  final String? uf;
  final String? cidade;
  final String? localizacao;
  final String? cultura;
  final String? extensao;

  IdentificacaoAreaModel({
    this.uf,
    this.cidade,
    this.localizacao,
    this.cultura,
    this.extensao,
  });

  factory IdentificacaoAreaModel.fromJson(Map<String, dynamic> json) =>
      IdentificacaoAreaModel(
        uf: json["uf"],
        cidade: json["cidade"],
        localizacao: json["localizacao"],
        cultura: json["cultura"],
        extensao: json["extensao"],
      );

  Map<String, dynamic> toJson() => {
        "uf": uf,
        "cidade": cidade,
        "localizacao": localizacao,
        "cultura": cultura,
        "extensao": extensao,
      };
}
