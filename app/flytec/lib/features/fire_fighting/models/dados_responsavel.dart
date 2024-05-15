class DadosResponsavel {
  String? nome;
  int? id;
  String? documento;
  String? assinatura;

  DadosResponsavel({this.nome, this.id, this.documento, this.assinatura});

  factory DadosResponsavel.fromJson(Map<String, dynamic>? json) {
    if (json == null) return DadosResponsavel();

    return DadosResponsavel(
        nome: json['nome'] ?? '',
        id: json['id'] ?? 0,
        documento: json['documento'] ?? '',
        assinatura: json['assinatura'] ?? '');
  }

  Map<String, dynamic> toJson() {
    return {
      'nome': nome,
      'id': id,
      'documento': documento,
      'assinatura': assinatura
    };
  }
}
