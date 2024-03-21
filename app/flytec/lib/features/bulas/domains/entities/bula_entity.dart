class BulaEntity {
  int? idBula;
  String? nomeProduto;
  int? idCultura;
  int? idClassificacaoToxicologica;
  String? classe;
  String? tipoDeFormulacao;
  int? idAlvoBiologico;
  String? doseProdutoComercial;
  String? adjuvante;
  int? idTipoDeServico;
  String? tipoDeUnidade;
  dynamic bulaAplicacoes;

  BulaEntity(
      {this.idBula,
      this.nomeProduto,
      this.idCultura,
      this.idClassificacaoToxicologica,
      this.classe,
      this.tipoDeFormulacao,
      this.idAlvoBiologico,
      this.doseProdutoComercial,
      this.adjuvante,
      this.idTipoDeServico,
      this.tipoDeUnidade,
      this.bulaAplicacoes});
}
