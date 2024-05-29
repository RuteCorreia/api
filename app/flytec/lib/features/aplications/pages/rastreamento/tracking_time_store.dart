import 'package:flutter_bloc/flutter_bloc.dart';

class TrackingTimeStore extends Cubit<Duration> {
  TrackingTimeStore() : super(Duration.zero);

  update(Duration duration) {
    emit(duration);
  }
}
