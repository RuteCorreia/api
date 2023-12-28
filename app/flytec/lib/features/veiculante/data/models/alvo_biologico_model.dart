// To parse this JSON data, do
//
//     final veiculanteModel = veiculanteModelFromJson(jsonString);

import 'dart:convert';

import 'package:flytec/features/veiculante/domain/entities/veiculante_entity.dart';

List<VeiculanteModel> veiculanteModelFromJson(String str) =>
    List<VeiculanteModel>.from(
        json.decode(str).map((x) => VeiculanteModel.fromJson(x)));

String veiculanteModelToJson(List<VeiculanteModel> data) =>
    json.encode(List<dynamic>.from(data.map((x) => x.toJson())));

class VeiculanteModel extends VeiculanteEntity {
  const VeiculanteModel({
    super.idVeiculante,
    super.nome,
  });

  factory VeiculanteModel.fromJson(Map<String, dynamic> json) =>
      VeiculanteModel(
        idVeiculante: json["idVeiculante"],
        nome: json["nome"],
      );

  Map<String, dynamic> toJson() => {
        "idVeiculante": idVeiculante,
        "nome": nome,
      };
}
