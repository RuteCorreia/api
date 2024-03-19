import 'dart:convert';

import 'package:flytec/features/bulas/domains/entities/bula_entity.dart';

List<BulaModel> bulaModelFromJson(String str) =>
    List<BulaModel>.from(json.decode(str).map((x) => BulaModel.fromJson(x)));

String bulaModelToJson(List<BulaModel> data) =>
    json.encode(List<dynamic>.from(data.map((x) => x.toJson())));

class BulaModel extends BulaEntity {
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
    bulaAplicacoes = json['bulaAplicacoes'];
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
    data['bulaAplicacoes'] = bulaAplicacoes;
    return data;
  }
}
