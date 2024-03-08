import 'package:flytec/features/aplications_v2/enums/report_dashboard_state.dart';
import 'package:flytec/features/aplications_v2/models/caracteristicas_produto_aplicado.dart';
import 'package:flytec/features/aplications_v2/models/contratante.dart';
import 'package:flytec/features/aplications_v2/models/contrato_prestacao_servico.dart';
import 'package:flytec/features/aplications_v2/models/dados_responsavel.dart';
import 'package:flytec/features/aplications_v2/models/identificacao_area_tratada.dart';
import 'package:flytec/features/aplications_v2/models/recomendacoes_tecnicas.dart';
import 'package:flytec/features/aplications_v2/models/relatorio_aplicacao.dart';

class Aplicacao {
  String? piloto;
  String? executor;
  int? id;
  ReportDashBoardState? state;
  String? data;
  Contratante? contratante;
  IdentificacaoAreaTratada? identificacaoAreaTratada;
  CaracteristicasProdutoAplicado? caracteristicasProdutoAplicado;
  RecomendacoesTecnicas? recomendacoesTecnicas;
  RelatorioAplicacao? relatorioAplicacao;
  ContratoPrestacaoServico? contratoPrestacaoServico;
  DadosResponsavel? dadosResponsavel;
  String? refUsuario;

  Aplicacao(
      {this.piloto,
      this.executor,
      this.id,
      this.contratante,
      this.identificacaoAreaTratada,
      this.caracteristicasProdutoAplicado,
      this.recomendacoesTecnicas,
      this.relatorioAplicacao,
      this.contratoPrestacaoServico,
      this.dadosResponsavel,
      this.state,
      this.data,
      this.refUsuario});

  // Create toJson and toMap methods of class
  Map<String, dynamic> toMap() {
    return {
      'piloto': piloto,
      'executor': executor,
      'state': state?.index ?? ReportDashBoardState.Incompleto.index,
      'data': data,
      'refUsuario': refUsuario
    };
  }

  factory Aplicacao.fromJson(Map<String, dynamic> json) {
    return Aplicacao(
        piloto: json['piloto'],
        executor: json['executor'],
        id: json['id'],
        refUsuario: json['refUsuario'],
        state: ReportDashBoardState.values.firstWhere(
            (state) => state.index == json['state'],
            orElse: () => ReportDashBoardState.Incompleto),
        data: json['data']);
  }
}
