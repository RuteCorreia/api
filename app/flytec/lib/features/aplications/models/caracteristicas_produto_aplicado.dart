import 'dart:typed_data';

import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/features/bulas/data/models/bula_model.dart';

class CaracteristicasProdutoAplicado {
  String? cultura;
  Uint8List? receiturarioAgronomico;
  String? nomeProduto;
  int? classificacaoToxicologica;
  String? classe;
  String? tipoFormulacao;
  String? alvoBiologico;
  String? doseProdutoHectare;
  String? unidadeDoseProdutoHectare;
  String? adjuvante;
  String? tipoServico;
  String? numeroReceituarioAgronomico;
  String? dataEmissao;
  int? id;

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
    this.numeroReceituarioAgronomico,
    this.dataEmissao,
    this.id,
  });

  Map<String, dynamic> toMap() {
    return {
      'cultura': cultura,
      'receiturarioAgronomico': receiturarioAgronomico,
      'nomeProduto': nomeProduto,
      'classificacaoToxicologica': classificacaoToxicologica.toString(),
      'classe': classe,
      'tipoFormulacao': tipoFormulacao,
      'alvoBiologico': alvoBiologico,
      'doseProdutoHectare': doseProdutoHectare,
      'unidadeDoseProdutoHectare': unidadeDoseProdutoHectare,
      'adjuvante': adjuvante,
      'tipoServico': tipoServico,
      'dataEmissao': dataEmissao,
      'numeroReceituarioAgronomico': numeroReceituarioAgronomico,
    };
  }

  factory CaracteristicasProdutoAplicado.fromJson(Map<String, dynamic>? json) {
    return CaracteristicasProdutoAplicado(
      cultura: json?['cultura'] ?? '',
      receiturarioAgronomico: json?['receiturarioAgronomico'],
      nomeProduto: json?['nomeProduto'] ?? '',
      classificacaoToxicologica:
          int.tryParse(json?['classificacaoToxicologica'] ?? ''),
      classe: json?['classe'] ?? '',
      tipoFormulacao: json?['tipoFormulacao'] ?? '',
      alvoBiologico: json?['alvoBiologico'] ?? '',
      doseProdutoHectare: json?['doseProdutoHectare'] ?? '',
      unidadeDoseProdutoHectare: json?['unidadeDoseProdutoHectare'] ?? '',
      adjuvante: json?['adjuvante'] ?? '',
      tipoServico: json?['tipoServico'] ?? '',
      numeroReceituarioAgronomico: json?['numeroReceituarioAgronomico'] ?? '',
      dataEmissao: json?['dataEmissao'] ?? '',
      id: json?['id'] ?? 0,
    );
  }

  factory CaracteristicasProdutoAplicado.fromBula(BulaModel bulaModel) =>
      CaracteristicasProdutoAplicado(
          nomeProduto: bulaModel.nomeProduto,
          classificacaoToxicologica: bulaModel.idClassificacaoToxicologica,
          classe: bulaModel.classe,
          tipoFormulacao: bulaModel.tipoDeFormulacao,
          tipoServico: bulaModel.idTipoDeServico.toString(),
          adjuvante: bulaModel.adjuvante,
          unidadeDoseProdutoHectare: bulaModel.tipoDeUnidade.toString(),
          doseProdutoHectare: bulaModel.doseProdutoComercial,
          cultura: bulaModel.bulaAplicacoes?.first != null
              ? getIt<GlobalConfigVars>()
                  .culturas[bulaModel.bulaAplicacoes!.first.idCultura! - 1]
                  .nome
              : null,
          alvoBiologico: bulaModel.idAlvoBiologico != null
              ? getIt<GlobalConfigVars>()
                  .alvosBiologicos[bulaModel.idAlvoBiologico! - 1]
                  .nome
              : null);
}
