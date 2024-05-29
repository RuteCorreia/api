class ReportAplicationEntity {
  int? id;
  int? contratanteId;
  int? identificacaoAreaTratadaId;
  int? caracteristicasProdutoAplicadoId;
  int? recomendacoesTecnicasId;
  int? relatorioAplicacaoId;
  int? contratoPrestacaoServicoId;
  int? dadosResponsavelId;
  String? piloto;
  String? executor;
  String? refDocument;
  String? data;
  String? refUsuario;

  ReportAplicationEntity({
    this.id,
    this.contratanteId,
    this.identificacaoAreaTratadaId,
    this.caracteristicasProdutoAplicadoId,
    this.recomendacoesTecnicasId,
    this.relatorioAplicacaoId,
    this.contratoPrestacaoServicoId,
    this.dadosResponsavelId,
    this.piloto,
    this.executor,
    this.refDocument,
    this.data,
    this.refUsuario,
  });
}
