// To parse this JSON data, do
//
//     final aplicacaoModel = aplicacaoModelFromJson(jsonString);

import 'dart:convert';

List<AplicacaoModel> aplicacaoModelFromJson(String str) =>
    List<AplicacaoModel>.from(
        json.decode(str).map((x) => AplicacaoModel.fromJson(x)));

String aplicacaoModelToJson(List<AplicacaoModel> data) =>
    json.encode(List<dynamic>.from(data.map((x) => x.toJson())));

class AplicacaoModel {
  final String? data;
  final String? horarioInicial;
  final String? horimetroInicial;
  final String? horarioTermino;
  final String? horimetroFinal;
  final String? temperaturaInicial;
  final String? temperaturaFinal;
  final String? umidadeInicial;
  final String? umidadeFinal;
  final String? ventoInicial;
  final String? ventoFinal;

  AplicacaoModel({
    this.data,
    this.horarioInicial,
    this.horimetroInicial,
    this.horarioTermino,
    this.horimetroFinal,
    this.temperaturaInicial,
    this.temperaturaFinal,
    this.umidadeInicial,
    this.umidadeFinal,
    this.ventoInicial,
    this.ventoFinal,
  });

  factory AplicacaoModel.fromJson(Map<String, dynamic> json) => AplicacaoModel(
        data: json["data"],
        horarioInicial: json["horarioInicial"],
        horimetroInicial: json["horimetroInicial"],
        horarioTermino: json["horarioTermino"],
        horimetroFinal: json["horimetroFinal"],
        temperaturaInicial: json["temperaturaInicial"],
        temperaturaFinal: json["temperaturaFinal"],
        umidadeInicial: json["umidadeInicial"],
        umidadeFinal: json["umidadeFinal"],
        ventoInicial: json["ventoInicial"],
        ventoFinal: json["ventoFinal"],
      );

  Map<String, dynamic> toJson() => {
        "data": data,
        "horarioInicial": horarioInicial,
        "horimetroInicial": horimetroInicial,
        "horarioTermino": horarioTermino,
        "horimetroFinal": horimetroFinal,
        "temperaturaInicial": temperaturaInicial,
        "temperaturaFinal": temperaturaFinal,
        "umidadeInicial": umidadeInicial,
        "umidadeFinal": umidadeFinal,
        "ventoInicial": ventoInicial,
        "ventoFinal": ventoFinal,
      };
}
