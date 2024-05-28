class ContratoPrestacaoServico {
  int? id;
  String? distanciaPista;
  String? preco;
  String? extensao;
  String? valorTotal;
  int? vencimento;
  String? nomePiloto;
  String? executor;

  ContratoPrestacaoServico({
    this.distanciaPista,
    this.preco,
    this.extensao,
    this.valorTotal,
    this.vencimento,
    this.nomePiloto,
    this.id,
    this.executor,
  });

  factory ContratoPrestacaoServico.fromJson(Map<String, dynamic>? json) {
    if (json == null) return ContratoPrestacaoServico();

    return ContratoPrestacaoServico(
      distanciaPista: json['distanciaPista'] ?? '',
      preco: json['preco'] ?? '',
      extensao: json['extensao'] ?? '',
      valorTotal: json['valorTotal'] ?? '',
      vencimento: json['vencimento'] ?? 0,
      nomePiloto: json['nomePiloto'] ?? '',
      executor: json['executor'] ?? '',
    );
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['distanciaPista'] = distanciaPista;
    data['preco'] = preco;
    data['extensao'] = extensao;
    data['valorTotal'] = valorTotal;
    data['vencimento'] = vencimento;
    data['nomePiloto'] = nomePiloto;
    data['executor'] = executor;
    return data;
  }
}
