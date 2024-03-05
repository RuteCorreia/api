import 'dart:typed_data';

class DadosResponsavel {
  String? data;
  String? uf;
  String? cidade;
  String? nomeCompleto;
  String? documento;
  String? telefone;
  Uint8List? assinaturaResponsavel;

  DadosResponsavel({
    this.data,
    this.uf,
    this.cidade,
    this.nomeCompleto,
    this.documento,
    this.telefone,
    this.assinaturaResponsavel,
  });

  Map<String, dynamic> toMap() {
    return {
      'data': data,
      'uf': uf,
      'cidade': cidade,
      'nomeCompleto': nomeCompleto,
      'documento': documento,
      'telefone': telefone,
      'assinaturaResponsavel': assinaturaResponsavel,
    };
  }

  factory DadosResponsavel.fromJson(Map<String, dynamic>? json) {
    if(json==null) return DadosResponsavel();
    return DadosResponsavel(
      data: json['data'],
      uf: json['uf'],
      cidade: json['cidade'],
      nomeCompleto: json['nomeCompleto'],
      documento: json['documento'],
      telefone: json['telefone'],
      assinaturaResponsavel: json['assinaturaResponsavel'],
    );
  }
}
