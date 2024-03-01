import 'package:flytec/features/aplications_v2/models/aplicacao.dart';
import 'package:flytec/features/aplications_v2/models/caracteristicas_produto.dart';
import 'package:flytec/features/aplications_v2/models/contrato_prestacao_servico.dart';
import 'package:flytec/features/aplications_v2/models/dados_responsavel.dart';
import 'package:flytec/features/aplications_v2/models/identificacao_area.dart';
import 'package:flytec/features/aplications_v2/models/identificacao_contratante.dart';

class RelatorioAplicacoes {
  String? piloto;
  String? executor;
  DateTime data;
  int? identificadorRelatorio;

  IdentificacaoContratante? identificacaoContratante;
  IdentificacaoArea? identificacaoArea;
  CaracteristicasProduto? caracteristicasProduto;
  Aplicacao? aplicacaoRelatorio;
  ContratoPrestacaoServicos? contratoPrestacaoServicos;
  DadosResponsavel? dadosResponsavel;

  RelatorioAplicacoes({
    this.piloto,
    this.executor,
    required this.data,
    this.identificadorRelatorio,
    this.identificacaoContratante,
    this.identificacaoArea,
    this.caracteristicasProduto,
    this.aplicacaoRelatorio,
    this.contratoPrestacaoServicos,
    this.dadosResponsavel,
  });

  Map<String, dynamic> toMap() {
    return {
      'piloto': piloto,
      'executor': executor,
      'data': data.millisecondsSinceEpoch.toString(),
    };
  }
}
