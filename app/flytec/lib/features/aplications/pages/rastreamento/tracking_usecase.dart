import 'dart:async';
import 'dart:typed_data';

abstract class TrackingUseCase {
  Future<void> getMyLocation(Completer<dynamic> mapController);
  Future<void> startRecording();
  Future<void> stopRecording();
  Future<bool> updateImagem(Uint8List? imageData);

  Future<void> loadTracking();
  Future<void> checkPermissionsStatus();
  Future<void> requestPermissions();
}
