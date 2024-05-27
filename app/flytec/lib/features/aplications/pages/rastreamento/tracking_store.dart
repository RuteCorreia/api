import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flytec/features/aplications/pages/rastreamento/tracking_state.dart';
import 'package:flytec/features/aplications/pages/rastreamento/track_model.dart';

class TrackingStore extends Cubit<TrackingState> {
  TrackingStore()
      : super(TrackingState(
            gravando: false, permissao: false, inicio: null, track: null));

  setGravando(bool gravando) {
    emit(TrackingState(
        gravando: gravando,
        permissao: state.permissao,
        inicio: state.inicio,
        track: state.track));
  }

  void setInicio(DateTime inicio) {
    emit(TrackingState(
      gravando: state.gravando,
      permissao: state.permissao,
      inicio: inicio,
      track: state.track,
    ));
  }

  void setTrack(TrackModel? track) {
    emit(TrackingState(
        gravando: state.gravando,
        permissao: state.permissao,
        inicio: state.inicio,
        track: track));
  }

  void setPermissao(bool permissao) {
    emit(TrackingState(
        gravando: state.gravando,
        inicio: state.inicio,
        track: state.track,
        permissao: permissao));
  }
}
