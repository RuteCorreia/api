import 'package:background_locator_2/location_dto.dart';

abstract class TrackingService {
  Future<void> initialize();

  Future<void> updateNotificationText(
      {required String title, required String msg, required String bigMsg});

  Future<bool> isServiceRunning();

  Future<bool> isRegisterLocationUpdate();

  Future<void> registerLocationUpdate(
      Future<void> Function(LocationDto locationDto) callback);

  Future<void> unRegisterLocationUpdate();
}
