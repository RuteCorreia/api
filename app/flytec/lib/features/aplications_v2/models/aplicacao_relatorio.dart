import 'package:flytec/features/aplications_v2/models/aplicacao.dart';

class AplicacaoRelatorio {
  String cultura;
  String produtoAplicado;
  String dosagem;
  String unidadeDosagem;
  String volumeAplicacao;
  String unidadeVolumeAplicacao;
  String totalAreaAplicadaHa;
  String localizacaoPistaCodigoICAO;
  String latitudeSul;
  String longitudeOeste;
  String densidade;
  String alteracoesPlanejamentoObservacoes;
  String relatorioDGPSLogs;
  List<Aplicacao> aplicacoes;

  AplicacaoRelatorio({
    required this.cultura,
    required this.produtoAplicado,
    required this.dosagem,
    required this.unidadeDosagem,
    required this.volumeAplicacao,
    required this.unidadeVolumeAplicacao,
    required this.totalAreaAplicadaHa,
    required this.localizacaoPistaCodigoICAO,
    required this.latitudeSul,
    required this.longitudeOeste,
    required this.densidade,
    required this.alteracoesPlanejamentoObservacoes,
    required this.relatorioDGPSLogs,
    required this.aplicacoes,
  });
}
