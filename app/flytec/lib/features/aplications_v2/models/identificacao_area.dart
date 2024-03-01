import 'dart:typed_data';

class IdentificacaoArea {
  String uf;
  String cidade;
  String localizacao;
  String cultura;
  String extensao;
  Uint8List fotoArea;

  IdentificacaoArea({
    required this.uf,
    required this.cidade,
    required this.localizacao,
    required this.cultura,
    required this.extensao,
    required this.fotoArea,
  });
}
