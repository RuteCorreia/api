import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';

import 'package:background_locator_2/background_locator.dart';
import 'package:background_locator_2/location_dto.dart';
import 'package:background_locator_2/settings/android_settings.dart';
import 'package:background_locator_2/settings/ios_settings.dart';
import 'package:background_locator_2/settings/locator_settings.dart';

import 'tracking_service.dart';

class TrackingServiceBackgroundLocator2 extends TrackingService {
  @override
  Future<void> initialize() async {
    await BackgroundLocator.initialize();
  }

  @override
  Future<void> updateNotificationText(
      {required String title,
      required String msg,
      required String bigMsg}) async {
    await BackgroundLocator.updateNotificationText(
        title: title, msg: msg, bigMsg: bigMsg);
  }

  @override
  Future<bool> isServiceRunning() async {
    return await BackgroundLocator.isServiceRunning();
  }

  @override
  Future<bool> isRegisterLocationUpdate() async {
    return await BackgroundLocator.isRegisterLocationUpdate();
  }

  @override
  Future<void> registerLocationUpdate(
      Future<void> Function(LocationDto locationDto) callback) async {
    return await BackgroundLocator.registerLocationUpdate(callback,
        iosSettings: const IOSSettings(
            accuracy: LocationAccuracy.NAVIGATION,
            distanceFilter: 0,
            showsBackgroundLocationIndicator: true,
            stopWithTerminate: false),
        autoStop: false,
        androidSettings: const AndroidSettings(
            accuracy: LocationAccuracy.NAVIGATION,
            interval: 1,
            distanceFilter: 0,
            wakeLockTime: 600000,
            androidNotificationSettings: AndroidNotificationSettings(
              notificationChannelName: 'Rastreando vôo',
              notificationTitle: 'Gravação iniciada',
              notificationMsg: 'Gravando o vôo em segundo plano',
              notificationBigMsg:
                  'Gravando vôo. Seu trajeto,tempo e velocidade estão sendo registrados.',
              notificationIconColor: Colors.grey,
              notificationTapCallback: notificationCallback,
            )));
  }

  static void notificationCallback() {
    if (kDebugMode) {
      print('User clicked on the notification');
    }
  }

  @override
  Future<void> unRegisterLocationUpdate() async {
    await BackgroundLocator.unRegisterLocationUpdate();
  }
}
