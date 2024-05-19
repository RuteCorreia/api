import 'package:flytec/core/enums/dashboard_state.dart';
import 'package:flytec/features/fire_fighting/models/comandante_ocorrencia.dart';
import 'package:flytec/features/fire_fighting/models/coordenador_base_operacional.dart';
import 'package:flytec/features/fire_fighting/models/dados_responsavel.dart';
import 'package:flytec/features/fire_fighting/models/decolagem_pouso_firefighting.dart';
import 'package:flytec/features/fire_fighting/models/local_firefighting.dart';
import 'package:flytec/features/fire_fighting/models/pista_firefighting.dart';

class Firefighting {
  String? refId;
  int? id;
  int? numeroAviso;
  String? cliente;
  String? prefixoAeronave;
  String? uf;
  String? cidade;
  int? data;
  String? horimetroAcionamento;
  int? horarioAcionamento;
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
  int? idDadosResponsavel;
  DadosResponsavel? dadosResponsavel;
  int? idCoordenadorBaseOperacional;
  CoordenadorBaseOperacional? coordenadorBaseOperacional;
  int? idComandanteOcorrencia;
  ComandanteOcorrencia? comandanteOcorrencia;
  DashBoardState? state;
  String? piloto;
  String? executor;
  bool? privado;

  Firefighting(
      {this.refId,
      this.id,
      this.cliente,
      this.numeroAviso,
      this.prefixoAeronave,
      this.uf,
      this.cidade,
      this.privado,
      this.data,
      this.state,
      this.horimetroAcionamento,
      this.horarioAcionamento,
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
      this.idDadosResponsavel,
      this.dadosResponsavel,
      this.piloto,
      this.executor,
      this.idCoordenadorBaseOperacional,
      this.coordenadorBaseOperacional,
      this.idComandanteOcorrencia,
      this.comandanteOcorrencia});

  Map<String, dynamic> toMap() {
    return {
      'piloto': piloto,
      'executor': executor,
      'state': state?.index ?? DashBoardState.Incompleto.index,
      'data': data,
      'refId': refId,
      'numeroAviso': numeroAviso,
      'cliente': cliente,
      'prefixoAeronave': prefixoAeronave,
      'uf': uf,
      'cidade': cidade,
      'privado' : privado! ? 1 : 0,
      'horimetroAcionamento': horimetroAcionamento,
      'horarioAcionamento': horarioAcionamento,
      'observacao': observacao,
      'horarioFinalOperacao': horarioFinalOperacao,
      'horimetroFinalOperacao': horimetroFinalOperacao,
      'horarioCorte': horarioCorte,
      'horimetroCorte': horimetroCorte,
      'capacidadeCargaAeronave': capacidadeCargaAeronave,
      'totalAguaUtilizadaOperacao': totalAguaUtilizadaOperacao,
    };
  }

  factory Firefighting.fromJson(Map<String, dynamic>? json) {
    if (json == null) return Firefighting();

    return Firefighting(
        refId: json['refId'] ?? '',
        id: json['id'] ?? 0,
        cliente: json['cliente'] ?? '',
        numeroAviso: json['numeroAviso'] ?? 0,
        prefixoAeronave: json['prefixoAeronave'] ?? '',
        uf: json['uf'] ?? '',
        cidade: json['cidade'] ?? '',
        piloto: json['piloto'] ?? '',
        executor: json['executor'] ?? '',
        privado: json['privado'] == 1,
        data: json['data'] ?? 0,
        state: DashBoardState.values.firstWhere(
            (state) => state.index == json['state'],
            orElse: () => DashBoardState.Incompleto),
        horimetroAcionamento: json['horimetroAcionamento'] ?? '',
        horarioAcionamento: json['horarioAcionamento'] ?? 0,
        idPistaFirefighting: json['pista_id'] ?? 0,
        idLocalFirefighting: json['localIncendio_id'] ?? 0,
        observacao: json['observacao'] ?? '',
        horarioFinalOperacao: json['horarioFinalOperacao'] ?? 0,
        horimetroFinalOperacao: json['horimetroFinalOperacao'] ?? '',
        horarioCorte: json['horarioCorte'] ?? 0,
        horimetroCorte: json['horimetroCorte'] ?? '',
        capacidadeCargaAeronave: json['capacidadeCargaAeronave'] ?? '',
        totalAguaUtilizadaOperacao: json['totalAguaUtilizadaOperacao'] ?? '',
        idDadosResponsavel: json['dadosResponsavelFirefighting_id'] ?? 0,
        idComandanteOcorrencia: json['comandanteOcorrencia_id'] ?? 0,
        idCoordenadorBaseOperacional:
            json['coordenadorBaseOperacional_id'] ?? 0);
  }
}
