
class IdentificacaoAreaTratada {
  String? uf;
  String? cidade;
  String? localizacao;
  String? cultura;
  String? extensao;
  String? croquiArea;
  bool? isPdf;
  int? id;

  IdentificacaoAreaTratada({
    this.uf,
    this.cidade,
    this.localizacao,
    this.cultura,
    this.extensao,
    this.croquiArea,
    this.isPdf,
    this.id,
  });

  Map<String, dynamic> toMap() {
    return {
      'uf': uf,
      'cidade': cidade,
      'localizacao': localizacao,
      'cultura': cultura,
      'extensao': extensao,
      'croquiArea': croquiArea,
      'isPdf': isPdf! ? 1 : 0,
    };
  }

  factory IdentificacaoAreaTratada.fromJson(Map<String, dynamic>? json) {
    return IdentificacaoAreaTratada(
      uf: json?['uf'] ?? '',
      cidade: json?['cidade'] ?? '',
      localizacao: json?['localizacao'] ?? '',
      cultura: json?['cultura'] ?? '',
      extensao: json?['extensao'] ?? '',
      croquiArea: json?['croquiArea'],
      isPdf: json?['isPdf'] == 1,
      id: json?['id'] ?? 0,
    );
  }
}
