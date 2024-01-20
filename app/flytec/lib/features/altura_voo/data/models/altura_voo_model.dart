// To parse this JSON data, do
//
//     final alturaVooModel = alturaVooModelFromJson(jsonString);

import 'dart:convert';

import 'package:flytec/features/altura_voo/domain/entities/tipo_produto_entity.dart';

List<AlturaVooModel> alturaVooModelFromJson(String str) =>
    List<AlturaVooModel>.from(
        json.decode(str).map((x) => AlturaVooModel.fromJson(x)));

String alturaVooModelToJson(List<AlturaVooModel> data) =>
    json.encode(List<dynamic>.from(data.map((x) => x.toJson())));

class AlturaVooModel extends AlturaVooEntity {
  const AlturaVooModel({
    super.id,
    super.nome,
  });

  factory AlturaVooModel.fromJson(Map<String, dynamic> json) => AlturaVooModel(
        id: json["id"],
        nome: json["nome"],
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "nome": nome,
      };
}
