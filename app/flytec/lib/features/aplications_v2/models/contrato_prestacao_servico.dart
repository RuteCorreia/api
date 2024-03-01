class ContratoPrestacaoServicos {
  int distanciaPista;
  int preco;
  String precoUnidade;
  String extensaoHorasHa;
  String valorTotal;
  DateTime vencimento;
  String nomePiloto;
  String executor;

  ContratoPrestacaoServicos({
    required this.distanciaPista,
    required this.preco,
    required this.precoUnidade,
    required this.extensaoHorasHa,
    required this.valorTotal,
    required this.vencimento,
    required this.nomePiloto,
    required this.executor,
  });
}
