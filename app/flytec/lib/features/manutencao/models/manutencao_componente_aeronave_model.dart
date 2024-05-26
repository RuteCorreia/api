class ManutencaoComponenteAeronaveModel {
  final int? id;
  final String? prefAeronave;
  final String? componente;
  final String? observacao;
  final List<String>? imagens;

  ManutencaoComponenteAeronaveModel(
      {this.componente,
      this.observacao,
      this.imagens,
      this.id,
      this.prefAeronave});
}
