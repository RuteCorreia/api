// To parse this JSON data, do
//
//     final culturaModel = culturaModelFromJson(jsonString);

import 'dart:convert';

import 'package:flytec/features/cultura/domain/entities/cultura_entity.dart';

List<CulturaModel> culturaModelFromJson(String str) => List<CulturaModel>.from(
    json.decode(str).map((x) => CulturaModel.fromJson(x)));

String culturaModelToJson(List<CulturaModel> data) =>
    json.encode(List<dynamic>.from(data.map((x) => x.toJson())));

class CulturaModel extends CulturaEntity {
  const CulturaModel({
    super.idCultura,
    super.nome,
    super.alvoBiologico,
  });

  factory CulturaModel.fromJson(Map<String, dynamic> json) => CulturaModel(
        idCultura: json["idCultura"],
        nome: json["nome"],
        alvoBiologico: json["alvoBiologico"],
      );

  Map<String, dynamic> toJson() => {
        "idCultura": idCultura,
        "nome": nome,
        "alvoBiologico": alvoBiologico,
      };
}
