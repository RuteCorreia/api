// To parse this JSON data, do
//
//     final tipoProdutoModel = tipoProdutoModelFromJson(jsonString);

import 'dart:convert';

import 'package:flytec/features/tipo_produto/domain/entities/tipo_produto_entity.dart';

List<TipoProdutoModel> tipoProdutoModelFromJson(String str) =>
    List<TipoProdutoModel>.from(
        json.decode(str).map((x) => TipoProdutoModel.fromJson(x)));

String tipoProdutoModelToJson(List<TipoProdutoModel> data) =>
    json.encode(List<dynamic>.from(data.map((x) => x.toJson())));

class TipoProdutoModel extends TipoProdutoEntity {
  const TipoProdutoModel({
    super.id,
    super.nome,
  });

  factory TipoProdutoModel.fromJson(Map<String, dynamic> json) =>
      TipoProdutoModel(
        id: json["id"],
        nome: json["nome"],
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "nome": nome,
      };
}
