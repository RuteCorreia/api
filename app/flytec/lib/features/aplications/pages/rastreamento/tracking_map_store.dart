import 'package:flutter_bloc/flutter_bloc.dart';

import 'package:flytec/features/aplications/pages/rastreamento/tracking_point.dart';

class TrackingMapStore extends Cubit<List<TrackingPoint>> {
  TrackingMapStore() : super([]);

  addPoints(TrackingPoint point) {
    final points = [...state, point];
    emit(points);
  }
}
