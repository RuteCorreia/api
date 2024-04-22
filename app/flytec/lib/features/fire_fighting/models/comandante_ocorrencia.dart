class ComandanteOcorrencia {
  String? nome;
  int? id;
  String? postoGraduacao;
  String? re;
  String? assinatura;

  ComandanteOcorrencia(
      {this.nome, this.id, this.postoGraduacao, this.re, this.assinatura});

  factory ComandanteOcorrencia.fromJson(Map<String, dynamic>? json) {
    if (json == null) return ComandanteOcorrencia();

    return ComandanteOcorrencia(
        nome: json['nome'] ?? '',
        id: json['id'] ?? 0,
        postoGraduacao: json['postoGraduacao'] ?? '',
        re: json['re'] ?? '',
        assinatura: json['assinatura'] ?? '');
  }

  Map<String, dynamic> toJson() {
    return {
      'nome': nome,
      'id': id,
      'postoGraduacao': postoGraduacao,
      're': re,
      'assinatura': assinatura
    };
  }
}
