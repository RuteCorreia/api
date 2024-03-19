class ContratoPrestacaoServico {
  String? distanciaPista;
  String? preco;
  String? unidadePreco;
  String? extensao;
  String? valorTotal;
  String? vencimento;
  String? nomePiloto;
  String? executor;
  int? id;

  ContratoPrestacaoServico({
    this.distanciaPista,
    this.preco,
    this.unidadePreco,
    this.extensao,
    this.valorTotal,
    this.vencimento,
    this.nomePiloto,
    this.executor,
    this.id,
  });

  Map<String, dynamic> toMap() {
    return {
      'distanciaPista': distanciaPista,
      'preco': preco,
      'unidadePreco': unidadePreco,
      'extensao': extensao,
      'valorTotal': valorTotal,
      'vencimento': vencimento,
      'nomePiloto': nomePiloto,
      'executor': executor,
    };
  }

  factory ContratoPrestacaoServico.fromJson(Map<String, dynamic>? json) {
    return ContratoPrestacaoServico(
      distanciaPista: json?['distanciaPista'] ?? '',
      preco: json?['preco'] ?? '',
      unidadePreco: json?['unidadePreco'] ?? '',
      extensao: json?['extensao'] ?? '',
      valorTotal: json?['valorTotal'] ?? '',
      vencimento: json?['vencimento'] ?? '',
      nomePiloto: json?['nomePiloto'] ?? '',
      executor: json?['executor'] ?? '',
      id: json?['id'] ?? 0,
    );
  }
}
