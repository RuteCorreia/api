import 'dart:typed_data';

class DadosResponsavel {
  DateTime data;
  String uf;
  String cidade;
  String nomeCompleto;
  String documento;
  String telefone;
  Uint8List assinaturaResponsavel;

  DadosResponsavel({
    required this.data,
    required this.uf,
    required this.cidade,
    required this.nomeCompleto,
    required this.documento,
    required this.telefone,
    required this.assinaturaResponsavel,
  });
}
