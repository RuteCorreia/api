import 'dart:convert';

import 'package:flytec/features/bulas/domains/entities/bula_entity.dart';

List<BulaModel> bulaModelFromJson(String str) =>
    List<BulaModel>.from(json.decode(str).map((x) => BulaModel.fromJson(x)));

String bulaModelToJson(List<BulaModel> data) =>
    json.encode(List<dynamic>.from(data.map((x) => x.toJson())));

class BulaModel extends BulaEntity {
  BulaModel(
      {int? idBula,
      String? nomeProduto,
      int? idCultura,
      int? idClassificacaoToxicologica,
      String? classe,
      String? tipoDeFormulacao,
      int? idAlvoBiologico,
      String? doseProdutoComercial,
      String? adjuvante,
      int? idTipoDeServico,
      int? tipoDeUnidade,
      List<BulaAplicacoesEntity>? bulaAplicacoes})
      : super(
            idBula: idBula,
            nomeProduto: nomeProduto,
            idCultura: idCultura,
            idClassificacaoToxicologica: idClassificacaoToxicologica,
            classe: classe,
            tipoDeFormulacao: tipoDeFormulacao,
            idAlvoBiologico: idAlvoBiologico,
            doseProdutoComercial: doseProdutoComercial,
            adjuvante: adjuvante,
            idTipoDeServico: idTipoDeServico,
            tipoDeUnidade: tipoDeUnidade,
            bulaAplicacoes: bulaAplicacoes);

  BulaModel.fromJson(Map<String, dynamic> json) {
    idBula = json['idBula'];
    nomeProduto = json['nomeProduto'];
    idCultura = json['idCultura'];
    idClassificacaoToxicologica = json['idClassificacaoToxicologica'];
    classe = json['classe'];
    tipoDeFormulacao = json['tipoDeFormulacao'];
    idAlvoBiologico = json['idAlvoBiologico'];
    doseProdutoComercial = json['doseProdutoComercial'];
    adjuvante = json['adjuvante'];
    idTipoDeServico = json['idTipoDeServico'];
    tipoDeUnidade = json['tipoDeUnidade'];
    if (json['bulaAplicacoes'] != null) {
      bulaAplicacoes = [];
      json['bulaAplicacoes'].forEach((v) {
        bulaAplicacoes!.add(BulaAplicacoes.fromJson(v));
      });
    }
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['idBula'] = idBula;
    data['nomeProduto'] = nomeProduto;
    data['idCultura'] = idCultura;
    data['idClassificacaoToxicologica'] = idClassificacaoToxicologica;
    data['classe'] = classe;
    data['tipoDeFormulacao'] = tipoDeFormulacao;
    data['idAlvoBiologico'] = idAlvoBiologico;
    data['doseProdutoComercial'] = doseProdutoComercial;
    data['adjuvante'] = adjuvante;
    data['idTipoDeServico'] = idTipoDeServico;
    data['tipoDeUnidade'] = tipoDeUnidade;
    if (bulaAplicacoes != null) {
      final bulas = bulaAplicacoes as List<BulaAplicacoes>;
      data['bulaAplicacoes'] =
          bulas.map((BulaAplicacoes v) => v.toJson()).toList();
    }
    return data;
  }
}

class BulaAplicacoes extends BulaAplicacoesEntity {
  BulaAplicacoes.fromJson(Map<String, dynamic> json) {
    idBulaAplicacao = json['idBulaAplicacao'];
    idCultura = json['idCultura'];
    idAlvoBiologico = json['idAlvoBiologico'];
    idBula = json['idBula'];
    doseProdutoComercial = json['doseProdutoComercial'];
    tipoDeUnidade = json['tipoDeUnidade'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['idBulaAplicacao'] = idBulaAplicacao;
    data['idCultura'] = idCultura;
    data['idAlvoBiologico'] = idAlvoBiologico;
    data['idBula'] = idBula;
    data['doseProdutoComercial'] = doseProdutoComercial;
    data['tipoDeUnidade'] = tipoDeUnidade;
    return data;
  }
}
