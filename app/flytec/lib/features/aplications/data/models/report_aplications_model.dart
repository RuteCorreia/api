import 'dart:convert';

import 'package:flytec/features/aplications/domain/entities/report_aplications_entity.dart';
import 'package:flytec/features/aplications/models/aplicacao.dart';

List<ReportAplicationsModel> reportAplicationsFromJson(String str) =>
    List<ReportAplicationsModel>.from(
        json.decode(str).map((x) => ReportAplicationsModel.fromJson(x)));

String reportAplicationsToJson(List<ReportAplicationsModel> data) =>
    json.encode(List<dynamic>.from(data.map((x) => x.toJson())));

class ReportAplicationsModel extends ReportAplicationEntity {
  ReportAplicationsModel({
    id,
    contratanteId,
    identificacaoAreaTratadaId,
    caracteristicasProdutoAplicadoId,
    recomendacoesTecnicasId,
    relatorioAplicacaoId,
    contratoPrestacaoServicoId,
    dadosResponsavelId,
    piloto,
    executor,
    refDocument,
    data,
    refUsuario,
  }) : super(
          id: id,
          contratanteId: contratanteId,
          identificacaoAreaTratadaId: identificacaoAreaTratadaId,
          caracteristicasProdutoAplicadoId: caracteristicasProdutoAplicadoId,
          recomendacoesTecnicasId: recomendacoesTecnicasId,
          relatorioAplicacaoId: relatorioAplicacaoId,
          contratoPrestacaoServicoId: contratoPrestacaoServicoId,
          dadosResponsavelId: dadosResponsavelId,
          piloto: piloto,
          executor: executor,
          refDocument: refDocument,
          data: data,
          refUsuario: refUsuario,
        );

  Map<String, dynamic> toJson() {
    return {
      'id': super.id,
      'contratanteId': super.contratanteId,
      'identificacaoAreaTratadaId': super.identificacaoAreaTratadaId,
      'caracteristicasProdutoAplicadoId':
          super.caracteristicasProdutoAplicadoId,
      'recomendacoesTecnicasId': super.recomendacoesTecnicasId,
      'relatorioAplicacaoId': super.relatorioAplicacaoId,
      'contratoPrestacaoServicoId': super.contratoPrestacaoServicoId,
      'dadosResponsavelId': super.dadosResponsavelId,
      'piloto': super.piloto,
      'executor': super.executor,
      'refDocument': super.refDocument,
      'data': super.data,
      'refUsuario': super.refUsuario,
    };
  }

  factory ReportAplicationsModel.fromJson(Map<String, dynamic> json) {
    return ReportAplicationsModel(
      id: json['id'],
      contratanteId: json['contratanteId'],
      identificacaoAreaTratadaId: json['identificacaoAreaTratadaId'],
      caracteristicasProdutoAplicadoId:
          json['caracteristicasProdutoAplicadoId'],
      recomendacoesTecnicasId: json['recomendacoesTecnicasId'],
      relatorioAplicacaoId: json['relatorioAplicacaoId'],
      contratoPrestacaoServicoId: json['contratoPrestacaoServicoId'],
      dadosResponsavelId: json['dadosResponsavelId'],
      piloto: json['piloto'],
      executor: json['executor'],
      refDocument: json['refDocument'],
      data: json['data'],
      refUsuario: json['refUsuario'],
    );
  }

  factory ReportAplicationsModel.fromAplicacao(Aplicacao aplicacao) {
    return ReportAplicationsModel(
        id: aplicacao.id,
        caracteristicasProdutoAplicadoId:
            aplicacao.caracteristicasProdutoAplicadoId,
        contratanteId: aplicacao.contratanteId,
        contratoPrestacaoServicoId: aplicacao.contratoPrestacaoServicoId,
        dadosResponsavelId: aplicacao.dadosResponsavelId,
        executor: aplicacao.executor,
        identificacaoAreaTratadaId: aplicacao.identificacaoAreaTratadaId,
        piloto: aplicacao.piloto,
        recomendacoesTecnicasId: aplicacao.recomendacoesTecnicasId,
        refDocument: aplicacao.refDocument,
        refUsuario: aplicacao.refUsuario,
        relatorioAplicacaoId: aplicacao.relatorioAplicacaoId);
  }
}
