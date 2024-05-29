// To parse this JSON data, do
//
//     final equipamentoModel = equipamentoModelFromJson(jsonString);

import 'dart:convert';

import '../../domain/entities/equipamento_entity.dart';

List<EquipamentoModel> equipamentoModelFromJson(String str) =>
    List<EquipamentoModel>.from(
        json.decode(str).map((x) => EquipamentoModel.fromJson(x)));

String equipamentoModelToJson(List<EquipamentoModel> data) =>
    json.encode(List<dynamic>.from(data.map((x) => x.toJson())));

class EquipamentoModel extends EquipamentoEntity {
  const EquipamentoModel({
    super.id,
    super.nome,
  });

  factory EquipamentoModel.fromJson(Map<String, dynamic> json) =>
      EquipamentoModel(
        id: json["id"],
        nome: json["nome"],
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "nome": nome,
      };
}
