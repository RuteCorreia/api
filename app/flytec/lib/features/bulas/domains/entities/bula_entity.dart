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
  int? tipoDeUnidade;
  List<BulaAplicacoesEntity>? bulaAplicacoes;

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

class BulaAplicacoesEntity{
  int? idBulaAplicacao;
  int? idCultura;
  int? idAlvoBiologico;
  int? idBula;
  String? doseProdutoComercial;
  int? tipoDeUnidade;

  BulaAplicacoesEntity(
      {this.idBulaAplicacao,
      this.idCultura,
      this.idAlvoBiologico,
      this.idBula,
      this.doseProdutoComercial,
      this.tipoDeUnidade});
}
