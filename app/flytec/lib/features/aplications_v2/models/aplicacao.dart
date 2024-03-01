import 'dart:typed_data';

class Aplicacao {
  DateTime? dataAplicacao;
  DateTime? horarioInicio;
  String? horimetroInicial;
  DateTime? horarioTermino;
  String? horimetroFinal;
  Uint8List? imagemCondicoesClimaticas;
  String? temperaturaInicial;
  String? temperaturaFinal;
  String? urArInicial;
  String? urArFinal;
  String? ventoInicial;
  String? ventoFinal;

  Aplicacao({
     this.dataAplicacao,
     this.horarioInicio,
     this.horimetroInicial,
     this.horarioTermino,
     this.horimetroFinal,
     this.imagemCondicoesClimaticas,
     this.temperaturaInicial,
     this.temperaturaFinal,
     this.urArInicial,
     this.urArFinal,
     this.ventoInicial,
     this.ventoFinal,
  });
  Map<String,dynamic> toMap() {
    return {
      'dataAplicacao': dataAplicacao.toString(),
      'horarioInicio': horarioInicio.toString(),
      'horimetroInicial': horimetroInicial,
      'horarioTermino': horarioTermino.toString(),
      'horimetroFinal': horimetroFinal,
      'imagemCondicoesClimaticas': imagemCondicoesClimaticas,
      'temperaturaInicial': temperaturaInicial,
      'temperaturaFinal': temperaturaFinal,
      'urArInicial': urArInicial,
      'urArFinal': urArFinal,
      'ventoInicial': ventoInicial,
      'ventoFinal': ventoFinal,
    };
  }
}
