import 'package:flytec/features/aplications/enums/report_dashboard_state.dart';
import 'package:flytec/features/aplications/models/caracteristicas_produto_aplicado.dart';
import 'package:flytec/features/aplications/models/contratante.dart';
import 'package:flytec/features/aplications/models/contrato_prestacao_servico.dart';
import 'package:flytec/features/aplications/models/dados_responsavel.dart';
import 'package:flytec/features/aplications/models/identificacao_area_tratada.dart';
import 'package:flytec/features/aplications/models/recomendacoes_tecnicas.dart';
import 'package:flytec/features/aplications/models/relatorio_aplicacao.dart';

class Aplicacao {
  String? piloto;
  String? executor;
  int? id;
  String? refDocument;
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

  int? contratanteId;
  int? identificacaoAreaTratadaId;
  int? caracteristicasProdutoAplicadoId;
  int? recomendacoesTecnicasId;
  int? relatorioAplicacaoId;
  int? contratoPrestacaoServicoId;
  int? dadosResponsavelId;

  Aplicacao(
      {this.piloto,
      this.executor,
      this.id,
      this.refDocument,
      this.contratante,
      this.identificacaoAreaTratada,
      this.caracteristicasProdutoAplicado,
      this.recomendacoesTecnicas,
      this.relatorioAplicacao,
      this.contratoPrestacaoServico,
      this.dadosResponsavel,
      this.state,
      this.data,
      this.refUsuario,
      this.contratanteId,
      this.identificacaoAreaTratadaId,
      this.caracteristicasProdutoAplicadoId,
      this.recomendacoesTecnicasId,
      this.relatorioAplicacaoId,
      this.contratoPrestacaoServicoId,
      this.dadosResponsavelId});

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
        piloto: json['piloto'] ?? '',
        executor: json['executor'] ?? '',
        id: json['id'] ?? 0,
        refUsuario: json['refUsuario'] ?? "'",
        state: ReportDashBoardState.values.firstWhere(
            (state) => state.index == json['state'],
            orElse: () => ReportDashBoardState.Incompleto),
        data: json['data'] ?? '',
        contratanteId: json['contratante_id'],
        identificacaoAreaTratadaId: json['identificacaoAreaTratada_id'],
        caracteristicasProdutoAplicadoId:
            json['caracteristicasProdutoAplicado_id'],
        recomendacoesTecnicasId: json['recomendacoesTecnicas_id'],
        relatorioAplicacaoId: json['relatorioAplicacao_id'],
        contratoPrestacaoServicoId: json['contratoPrestacaoServico_id'],
        dadosResponsavelId: json['dadosResponsavel_id']);
  }
}
