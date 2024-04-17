class CoordenadorBaseOperacional {
  final String? nome;
  final int? id;
  final String? postoGraduacao;
  final String? re;
  final String? assinatura;

  CoordenadorBaseOperacional(
     { this.nome, this.id, this.postoGraduacao, this.re, this.assinatura});

  factory CoordenadorBaseOperacional.fromJson(Map<String, dynamic> json) {
    return CoordenadorBaseOperacional(
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
