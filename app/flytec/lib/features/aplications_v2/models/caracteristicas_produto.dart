import 'dart:typed_data';

class CaracteristicasProduto {
  String cultura;
  Uint8List fotoReceituarioAgronomico;
  String nomeProduto;
  String classificacaoToxicologica;
  String classe;
  String tipoFormulacao;
  String alvoBiologico;
  String doseProdutoHectare;
  String unidadeDoseProdutoHectare;
  String adjuvante;
  String tipoServico;

  CaracteristicasProduto({
    required this.cultura,
    required this.fotoReceituarioAgronomico,
    required this.nomeProduto,
    required this.classificacaoToxicologica,
    required this.classe,
    required this.tipoFormulacao,
    required this.alvoBiologico,
    required this.doseProdutoHectare,
    required this.unidadeDoseProdutoHectare,
    required this.adjuvante,
    required this.tipoServico,
  });
}
