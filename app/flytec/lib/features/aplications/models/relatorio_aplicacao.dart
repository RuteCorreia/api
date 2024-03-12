import 'package:flytec/features/aplications/models/aplicacoes.dart';

class RelatorioAplicacao {
  String? cultura;
  String? produtoAplicado;
  String? dosagem;
  String? unidadeDosagem;
  String? volumeAplicacao;
  String? unidadeVolumeAplicacao;
  String? totalAreaAplicada;
  String? localizacaoPistaCodigoICAO;
  String? lat;
  String? long;
  String? observacoes;
  String? relatorioDGPS;
  String? densidade;
  List<Aplicacoes?>? aplicacoes;
  int? id;

  RelatorioAplicacao({
    this.cultura,
    this.produtoAplicado,
    this.dosagem,
    this.unidadeDosagem,
    this.volumeAplicacao,
    this.unidadeVolumeAplicacao,
    this.totalAreaAplicada,
    this.localizacaoPistaCodigoICAO,
    this.lat,
    this.long,
    this.observacoes,
    this.relatorioDGPS,
    this.aplicacoes,
    this.densidade,
    this.id,
  });

  Map<String, dynamic> toMap() {
    return {
      'cultura': cultura,
      'produtoAplicado': produtoAplicado,
      'dosagem': dosagem,
      'unidadeDosagem': unidadeDosagem,
      'volumeAplicacao': volumeAplicacao,
      'unidadeVolumeAplicacao': unidadeVolumeAplicacao,
      'totalAreaAplicada': totalAreaAplicada,
      'localizacaoPistaCodigoICAO': localizacaoPistaCodigoICAO,
      'lat': lat,
      'long': long,
      'densidade': densidade,
      'observacoes': observacoes,
      'relatorioDGPS': relatorioDGPS,
    };
  }

  factory RelatorioAplicacao.fromJson(Map<String, dynamic>? json) {
    if (json == null) return RelatorioAplicacao();
    return RelatorioAplicacao(
      cultura: json['cultura'],
      produtoAplicado: json['produtoAplicado'],
      dosagem: json['dosagem'],
      unidadeDosagem: json['unidadeDosagem'],
      volumeAplicacao: json['volumeAplicacao'],
      unidadeVolumeAplicacao: json['unidadeVolumeAplicacao'],
      totalAreaAplicada: json['totalAreaAplicada'],
      localizacaoPistaCodigoICAO: json['localizacaoPistaCodigoICAO'],
      lat: json['lat'],
      long: json['long'],
      densidade: json['densidade'],
      observacoes: json['observacoes'],
      relatorioDGPS: json['relatorioDGPS'],
      id: json['id'],
    );
  }
}
