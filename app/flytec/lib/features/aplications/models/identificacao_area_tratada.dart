import 'dart:typed_data';

class IdentificacaoAreaTratada {
  String? uf;
  String? cidade;
  String? localizacao;
  String? cultura;
  String? extensao;
  Uint8List? croquiArea;
  int? id;

  IdentificacaoAreaTratada({
    this.uf,
    this.cidade,
    this.localizacao,
    this.cultura,
    this.extensao,
    this.croquiArea,
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
      id: json?['id'] ?? 0,
    );
  }
}
