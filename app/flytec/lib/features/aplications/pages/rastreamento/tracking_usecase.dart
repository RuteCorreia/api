import 'dart:async';

abstract class TrackingUseCase {
  Future<void> getMyLocation(Completer<dynamic> mapController);
  Future<void> startRecording();
  Future<void> stopRecording();
}
