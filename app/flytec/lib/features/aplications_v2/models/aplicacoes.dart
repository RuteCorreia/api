import 'dart:typed_data';

class Aplicacoes {
  String? dataAplicacao;
  String? horaInicio;
  String? horaFinal;
  String? horimetroInicial;
  String? horimetroFinal;
  Uint8List? imagemCondicaoClimatica;
  String? temperaturaInicial;
  String? temperaturaFinal;
  String? umidadeRelativaArInicial;
  String? umidadeRelativaArFinal;
  String? ventoInicial;
  String? ventoFinal;

  Aplicacoes({
    this.dataAplicacao,
    this.horaInicio,
    this.horaFinal,
    this.horimetroInicial,
    this.horimetroFinal,
    this.imagemCondicaoClimatica,
    this.temperaturaInicial,
    this.temperaturaFinal,
    this.umidadeRelativaArInicial,
    this.umidadeRelativaArFinal,
    this.ventoInicial,
    this.ventoFinal,
  });

  Map<String, dynamic> toMap() {
    return {
      'dataAplicacao': dataAplicacao,
      'horaInicio': horaInicio,
      'horaFinal': horaFinal,
      'horimetroInicial': horimetroInicial,
      'horimetroFinal': horimetroFinal,
      'imagemCondicaoClimatica': imagemCondicaoClimatica,
      'temperaturaInicial': temperaturaInicial,
      'temperaturaFinal': temperaturaFinal,
      'umidadeRelativaArInicial': umidadeRelativaArInicial,
      'umidadeRelativaArFinal': umidadeRelativaArFinal,
      'ventoInicial': ventoInicial,
      'ventoFinal': ventoFinal,
    };
  }

  factory Aplicacoes.fromJson(Map<String, dynamic> json) {
    return Aplicacoes(
      dataAplicacao: json['dataAplicacao'],
      horaInicio: json['horaInicio'],
      horaFinal: json['horaFinal'],
      horimetroInicial: json['horimetroInicial'],
      horimetroFinal: json['horimetroFinal'],
      imagemCondicaoClimatica: json['imagemCondicaoClimatica'],
      temperaturaInicial: json['temperaturaInicial'],
      temperaturaFinal: json['temperaturaFinal'],
      umidadeRelativaArInicial: json['umidadeRelativaArInicial'],
      umidadeRelativaArFinal: json['umidadeRelativaArFinal'],
      ventoInicial: json['ventoInicial'],
      ventoFinal: json['ventoFinal'],
    );
  }
}
