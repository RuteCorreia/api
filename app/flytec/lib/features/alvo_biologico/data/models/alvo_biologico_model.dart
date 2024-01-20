// To parse this JSON data, do
//
//     final alvoBiologicoModel = alvoBiologicoModelFromJson(jsonString);

import 'dart:convert';

import 'package:flytec/features/alvo_biologico/domain/entities/alvo_biologico_entity.dart';

List<AlvoBiologicoModel> alvoBiologicoModelFromJson(String str) =>
    List<AlvoBiologicoModel>.from(
        json.decode(str).map((x) => AlvoBiologicoModel.fromJson(x)));

String alvoBiologicoModelToJson(List<AlvoBiologicoModel> data) =>
    json.encode(List<dynamic>.from(data.map((x) => x.toJson())));

class AlvoBiologicoModel extends AlvoBiologicoEntity {
  AlvoBiologicoModel({
    super.id,
    super.idProduto,
    super.nome,
    super.doseProdutoPorHectare,
  });

  factory AlvoBiologicoModel.fromJson(Map<String, dynamic> json) =>
      AlvoBiologicoModel(
        id: json["id"],
        idProduto: json["idProduto"],
        nome: json["nome"],
        doseProdutoPorHectare: json["doseProdutoPorHectare"],
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "idProduto": idProduto,
        "nome": nome,
        "doseProdutoPorHectare": doseProdutoPorHectare,
      };
}
