import 'track_model.dart';

class TrackingState {
  final bool gravando;
  final bool permissao;
  final DateTime? inicio;
  final TrackModel? track;
  final bool salvando;

  TrackingState(
      {required this.gravando,
      required this.permissao,
      required this.inicio,
      required this.track,
      required this.salvando});
}
