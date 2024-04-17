import 'package:flytec/features/fire_fighting/models/comandante_ocorrencia.dart';
import 'package:flytec/features/fire_fighting/models/coordenador_base_operacional.dart';
import 'package:flytec/features/fire_fighting/models/decolagem_pouso_firefighting.dart';
import 'package:flytec/features/fire_fighting/models/local_firefighting.dart';
import 'package:flytec/features/fire_fighting/models/pista_firefighting.dart';

class Firefighting {
  String? refId;
  int? id;
  int? numeroAviso;
  String? prefixoAeronave;
  String? uf;
  String? cidade;
  int? data;
  String? horimetroAcionamento;
  PistaFirefighting? pista;
  int? idPistaFirefighting;
  LocalFirefighting? localIncendio;
  int? idLocalFirefighting;
  List<int?>? idDecolagemPousoFirefightingList;
  List<DecolagemPousoFirefighting?>? decolagemPousoFirefightingList;
  String? observacao;
  int? horarioFinalOperacao;
  String? horimetroFinalOperacao;
  int? horarioCorte;
  String? horimetroCorte;
  String? capacidadeCargaAeronave;
  String? totalAguaUtilizadaOperacao;
  int? idCoordenadorBaseOperacional;
  CoordenadorBaseOperacional? coordenadorBaseOperacional;
  int? idComandanteOcorrencia;
  ComandanteOcorrencia? comandanteOcorrencia;

  Firefighting(
      {this.refId,
      this.id,
      this.numeroAviso,
      this.prefixoAeronave,
      this.uf,
      this.cidade,
      this.data,
      this.horimetroAcionamento,
      this.pista,
      this.idPistaFirefighting,
      this.localIncendio,
      this.idLocalFirefighting,
      this.idDecolagemPousoFirefightingList,
      this.decolagemPousoFirefightingList,
      this.observacao,
      this.horarioFinalOperacao,
      this.horimetroFinalOperacao,
      this.horarioCorte,
      this.horimetroCorte,
      this.capacidadeCargaAeronave,
      this.totalAguaUtilizadaOperacao,
      this.idCoordenadorBaseOperacional,
      this.coordenadorBaseOperacional,
      this.idComandanteOcorrencia,
      this.comandanteOcorrencia});

  factory Firefighting.fromJson(Map<String, dynamic> json) {
    return Firefighting(
        refId: json['refId'] ?? '',
        id: json['id'] ?? 0,
        numeroAviso: json['numeroAviso'] ?? 0,
        prefixoAeronave: json['prefixoAeronave'] ?? '',
        uf: json['uf'] ?? '',
        cidade: json['cidade'] ?? '',
        data: json['data'] ?? 0,
        horimetroAcionamento: json['horimetroAcionamento'] ?? '',
        idPistaFirefighting: json['pista_id'] ?? 0,
        idLocalFirefighting: json['localIncendio_id'] ?? 0,
        observacao: json['observacao'] ?? '',
        horarioFinalOperacao: json['horarioFinalOperacao'] ?? 0,
        horimetroFinalOperacao: json['horimetroFinalOperacao'] ?? '',
        horarioCorte: json['horarioCorte'] ?? 0,
        horimetroCorte: json['horimetroCorte'] ?? '',
        capacidadeCargaAeronave: json['capacidadeCargaAeronave'] ?? '',
        totalAguaUtilizadaOperacao: json['totalAguaUtilizadaOperacao'] ?? '',
        idCoordenadorBaseOperacional:
            json['coordenadorBaseOperacional_id'] ?? 0);
  }
}
