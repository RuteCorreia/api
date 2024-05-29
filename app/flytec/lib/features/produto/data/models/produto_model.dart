// To parse super JSON data, do
//
//     final produtoModel = produtoModelFromJson(jsonString);

import 'dart:convert';

import 'package:flytec/features/produto/domain/entities/produto_entity.dart';

List<ProdutoModel> produtoModelFromJson(String str) => List<ProdutoModel>.from(
    json.decode(str).map((x) => ProdutoModel.fromJson(x)));

String produtoModelToJson(List<ProdutoModel> data) =>
    json.encode(List<dynamic>.from(data.map((x) => x.toJson())));

class ProdutoModel extends ProdutoEntity {
  const ProdutoModel({
    super.id,
    super.idCultura,
    super.nome,
    super.classificacaoToxicologica,
    super.classe,
    super.tipoDeFormulacao,
    super.tipoServico,
  });

  factory ProdutoModel.fromJson(Map<String, dynamic> json) => ProdutoModel(
        id: json["id"],
        idCultura: json["idCultura"],
        nome: json["nome"],
        classificacaoToxicologica: json["classificacaoToxicologica"],
        classe: json["classe"],
        tipoDeFormulacao: json["tipoDeFormulacao"],
        tipoServico: json["tipoServico"],
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "idCultura": idCultura,
        "nome": nome,
        "classificacaoToxicologica": classificacaoToxicologica,
        "classe": classe,
        "tipoDeFormulacao": tipoDeFormulacao,
        "tipoServico": tipoServico,
      };
}
