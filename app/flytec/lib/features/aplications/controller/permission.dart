import 'package:app_settings/app_settings.dart';
import 'package:flutter/material.dart';
import 'package:location/location.dart' as lct;

class CheckPermissionLocation {
  final BuildContext context;
  final Function funcao;

  CheckPermissionLocation(
    this.context,
    this.funcao,
  );

  getPermission({bool canOpen = false}) async {
    lct.Location local = lct.Location();

    lct.PermissionStatus permissao = await local.requestPermission();
    print("===================${permissao.name}");
    if (permissao == lct.PermissionStatus.granted) {
      funcao();
    } else if (permissao == lct.PermissionStatus.deniedForever) {
      // if (exit) await AppSettings.openLocationSettings();
      await AppSettings.openAppSettings();
    } else {
      await AppSettings.openAppSettings();

      funcao();
    }
  }
}
