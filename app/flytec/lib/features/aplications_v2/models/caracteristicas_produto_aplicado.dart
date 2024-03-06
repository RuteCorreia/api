import 'dart:typed_data';

import 'package:flytec/features/produto/data/models/produto_model.dart';

class CaracteristicasProdutoAplicado {
  String? cultura;
  Uint8List? receiturarioAgronomico;
  String? nomeProduto;
  String? classificacaoToxicologica;
  String? classe;
  String? tipoFormulacao;
  String? alvoBiologico;
  String? doseProdutoHectare;
  String? unidadeDoseProdutoHectare;
  String? adjuvante;
  String? tipoServico;

  CaracteristicasProdutoAplicado({
    this.cultura,
    this.receiturarioAgronomico,
    this.nomeProduto,
    this.classificacaoToxicologica,
    this.classe,
    this.tipoFormulacao,
    this.alvoBiologico,
    this.doseProdutoHectare,
    this.unidadeDoseProdutoHectare,
    this.adjuvante,
    this.tipoServico,
  });

  Map<String, dynamic> toMap() {
    return {
      'cultura': cultura,
      'receiturarioAgronomico': receiturarioAgronomico,
      'nomeProduto': nomeProduto,
      'classificacaoToxicologica': classificacaoToxicologica,
      'classe': classe,
      'tipoFormulacao': tipoFormulacao,
      'alvoBiologico': alvoBiologico,
      'doseProdutoHectare': doseProdutoHectare,
      'unidadeDoseProdutoHectare': unidadeDoseProdutoHectare,
      'adjuvante': adjuvante,
      'tipoServico': tipoServico,
    };
  }

  factory CaracteristicasProdutoAplicado.fromJson(Map<String, dynamic>? json) {
    if(json==null) return CaracteristicasProdutoAplicado();
    return CaracteristicasProdutoAplicado(
      cultura: json['cultura'],
      receiturarioAgronomico: json['receiturarioAgronomico'],
      nomeProduto: json['nomeProduto'],
      classificacaoToxicologica: json['classificacaoToxicologica'],
      classe: json['classe'],
      tipoFormulacao: json['tipoFormulacao'],
      alvoBiologico: json['alvoBiologico'],
      doseProdutoHectare: json['doseProdutoHectare'],
      unidadeDoseProdutoHectare: json['unidadeDoseProdutoHectare'],
      adjuvante: json['adjuvante'],
      tipoServico: json['tipoServico'],
    );
  }

  factory CaracteristicasProdutoAplicado.fromProduto(ProdutoModel produto) =>
      CaracteristicasProdutoAplicado(
        nomeProduto: produto.nome,
        classificacaoToxicologica: produto.classificacaoToxicologica,
        classe: produto.classe,
        tipoFormulacao: produto.tipoDeFormulacao,
        tipoServico: produto.tipoServico,
      );
}
