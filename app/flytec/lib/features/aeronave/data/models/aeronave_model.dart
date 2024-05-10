// To parse super JSON data, do
//
//     final aeroNaveModel = aeroNaveModelFromJson(jsonString);

import 'dart:convert';

import 'package:flytec/features/aeronave/domain/entities/aeronave_entity.dart';

List<AeroNaveModel> aeroNaveModelFromJson(String str) =>
    List<AeroNaveModel>.from(
        json.decode(str).map((x) => AeroNaveModel.fromJson(x)));

String aeroNaveModelToJson(List<AeroNaveModel> data) =>
    json.encode(List<dynamic>.from(data.map((x) => x.toJson())));

class AeroNaveModel extends AeroNaveEntity {
  const AeroNaveModel({
    super.id,
    super.idEmpresa,
    super.prefixo,
    super.combustivel,
    super.capacidadeDeCarga,
    super.horimetro,
    super.modelo,
    super.serialNumber,
    super.tipo
  });

  factory AeroNaveModel.fromJson(Map<String, dynamic> json) => AeroNaveModel(
        id: json["id"],
        idEmpresa: json["idEmpresa"],
        prefixo: json["prefixo"],
        combustivel: json["combustivel"],
        capacidadeDeCarga: json["capacidadeDeCarga"],
        horimetro: json["horimetro"],
        modelo: json["modelo"],
        serialNumber: json["serialNumber"],
        tipo: json["tipo"]
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "idEmpresa": idEmpresa,
        "prefixo": prefixo,
        "combustivel": combustivel,
        "capacidadeDeCarga": capacidadeDeCarga,
        "horimetro": horimetro,
        "modelo": modelo,
        "serialNumber": serialNumber,
        "tipo": tipo
      };
}
