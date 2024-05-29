// ignore_for_file: unused_local_variable

import 'dart:async';
import 'dart:convert';
import 'dart:io';
import 'dart:isolate';
import 'dart:ui';

import 'package:app_settings/app_settings.dart';
import 'package:background_locator_2/location_dto.dart';
import 'package:flutter/foundation.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/features/aplications/pages/rastreamento/tracking_point.dart';
import 'package:flytec/features/aplications/pages/rastreamento/tracking_service.dart';
import 'package:flytec/features/aplications/pages/rastreamento/tracking_store.dart';
import 'package:flytec/features/aplications/pages/rastreamento/tracking_time_store.dart';
import 'package:flytec/features/aplications/pages/rastreamento/tracking_usecase.dart';
import 'package:flytec/features/aplications/pages/rastreamento/track_model.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:location/location.dart';
import 'package:permission_handler/permission_handler.dart';

import 'file_manager.dart';
import 'location_ios.dart';
import 'tracking_map_store.dart';

class TrackingController extends TrackingUseCase {
  static const String _isolateName = "LocatorIsolate";
  bool _executando = false;
  StreamSubscription? _stream;
  ReceivePort port = ReceivePort();
  Timer? _tmTempo;
  final Function(Uint8List data, bool isPdf)? updateImageDataCallback;
  final TrackingService service;

  TrackingController({required this.service, this.updateImageDataCallback});

  @override
  Future<void> getMyLocation(Completer<dynamic> mapController) async {
    Location local = Location();
    await local.getLocation().then((position) async {
      var atual = CameraPosition(
          target: LatLng(position.latitude!, position.longitude!), zoom: 17.44);
      final GoogleMapController controller = await mapController.future;
      await controller.animateCamera(CameraUpdate.newCameraPosition(atual));
    });
  }

  @override
  Future<void> startRecording() async {
    if (_executando) {
      if (kDebugMode) {
        print("Gravação iniciada");
      }
      return;
    }
    _executando = true;
    getIt.get<TrackingStore>().setGravando(true);

    await checkPermissionsStatus();
    final store = getIt<TrackingStore>();
    if (store.state.permissao) {
      if (store.state.inicio == null) {
        store.setInicio(DateTime.now());
      }

      _atualizarTempo();

      await _cancelarServicoLocalizacao(); // cancelar caso fique ativo, só por desencargo
      await _initialize();
      try {
        await _load();
      } catch (e) {
        if (kDebugMode) {
          print("$e");
        }
      }

      await _startLocator();
      await _updateIsRunning();
    }
  }

  _load() async {
    final log = await FileManager.readLogFile();
    final store = getIt<TrackingStore>();
    if (log.length <= 3) {
      store.setTrack(TrackModel());
    } else {
      var log0 = verificarArquivo(log);
      var track = TrackModel.fromJson(log0);
      store.setTrack(track);
      if ((track.pontos?.first.time ?? 0.0) > 0) {
        var startTime = DateTime.fromMillisecondsSinceEpoch(
            track.pontos!.first.time.floor());
        store.setInicio(startTime);
      }
    }
  }

  @pragma('vm:entry-point')
  static Future<void> callback(LocationDto locationDto) async {
    final json = jsonEncode(locationDto.toJson());
    FileManager.writeToLogFile(json);

    final SendPort? send = IsolateNameServer.lookupPortByName(_isolateName);
    send?.send(json);
  }

//Optional
  @pragma('vm:entry-point')
  Future<void> _startLocator() async {
    await service.registerLocationUpdate(
      callback,
    );
  }

  Future<void> _initPlatformState() async {
    await _updateIsRunning();
    if (!_executando) {
      if (kDebugMode) {
        print('Serviço BG Geo: Inicializando...');
      }
      await service.initialize();
    } else {
      if (kDebugMode) {
        print('Serviço BG Geo: Já foi iniciado.');
      }
    }
  }

  Future<bool> _updateIsRunning() async {
    final isRunning = await service.isServiceRunning();
    _executando = await service.isRegisterLocationUpdate() && _stream != null;

    getIt.get<TrackingStore>().setGravando(_executando);
    if (kDebugMode) {
      print('Geolocalização executando ${isRunning.toString()}');
    }
    if (kDebugMode) {
      print('Excutando leitura  ${_executando.toString()}');
    }

    return _executando;
  }

  _initialize() async {
    if (_stream == null) {
      if (IsolateNameServer.lookupPortByName(_isolateName) != null) {
        IsolateNameServer.removePortNameMapping(_isolateName);
      }

      IsolateNameServer.registerPortWithName(port.sendPort, _isolateName);

      try {
        _stream = port.listen(
          (dynamic data) async {
            await _updateUI(data);
          },
        );
      } catch (e, stacktrace) {
        if (kDebugMode) {
          print(e);
          print(stacktrace);
        }
      }
      await _initPlatformState();
    }
  }

  @pragma('vm:entry-point')
  Future<void> _updateUI(dynamic data) async {
    if (data == null) return;
    final store = getIt.get<TrackingMapStore>();

    try {
      TrackingPoint location = TrackingPoint.fromJson(jsonDecode(data));
      store.addPoints(location);
      await _updateNotificationText();
    } catch (e) {
      if (kDebugMode) {
        print(e);
      }
    }
  }

  Future<void> checkPermissionsStatus() async {
    final store = getIt.get<TrackingStore>();
    if (Platform.isAndroid) {
      var status = await Permission.locationAlways.status;

      if (status.isGranted) {
        store.setPermissao(true);
      } else {
        store.setPermissao(false);
      }
    } else {
      store.setPermissao(await LocationOnIOS().hasPermission());
    }
  }

  _atualizarTempo() {
    final store = getIt.get<TrackingStore>();
    final timeStore = getIt.get<TrackingTimeStore>();
    _tmTempo = Timer.periodic(const Duration(milliseconds: 20), (timer) {
      final dur = store.state.inicio == null
          ? Duration.zero
          : DateTime.now().difference(store.state.inicio!);
      timeStore.update(dur);
    });
  }

  _pararTempo() {
    _tmTempo?.cancel();
    _tmTempo = null;
  }

  Future<void> _cancelarServicoLocalizacao() async {
    await service.unRegisterLocationUpdate();
  }

  @override
  Future<void> stopRecording() async {
    _executando = false;

    try {
      await _cancelarServicoLocalizacao();
    } catch (e) {
      if (kDebugMode) {
        print(e);
      }
    }

    try {
      await _updateIsRunning();
    } catch (e) {
      if (kDebugMode) {
        print(e);
      }
    }
    _pararTempo();
    try {
      await _load();
    } catch (e) {
      if (kDebugMode) {
        print(e);
      }
    }
  }

  String verificarArquivo(String log) {
    return log.replaceAll(",,", ",");
  }

  _updateNotificationText() {
    final storeTime = getIt.get<TrackingTimeStore>();
    service.updateNotificationText(
        title: "Rastreamento",
        msg: "Duração ${storeTime.state}",
        bigMsg: "Rastreamento ativo durante ${storeTime.state}");
  }

  @override
  Future<bool> updateImagem(Uint8List? imageData) async {
    if (updateImageDataCallback != null && imageData != null) {
      updateImageDataCallback!(imageData, false);
      return true;
    } else {
      return false;
    }
  }

  @override
  Future<void> loadTracking() async {
    await _load();
    try {
      await _updateIsRunning();
      final store = getIt.get<TrackingStore>();
      if (store.state.gravando) {
        _atualizarTempo();
      }
    } catch (e) {
      if (kDebugMode) {
        print(e);
      }
    }
  }

  Future<void> requestPermissions() async {
    if (Platform.isAndroid) {
      var status = await Permission.locationAlways.status;
      if (status.isDenied) {
        status = await Permission.location.request();
        if (status.isGranted) {
          status = await Permission.locationAlways.request();
        }
      }

      if (status.isPermanentlyDenied || status.isDenied) {
        var abriu = await openAppSettings();

        if (!abriu) {
          //await AppSettings.openLocationSettings(asAnotherTask: true);
          await AppSettings.openAppSettings(asAnotherTask: true);
        }
      }
      ServiceStatus service = await Permission.locationAlways.serviceStatus;

      if (service.isDisabled) {
        throw "Serviço de geolocalização está desabilitado neste aparelho.";
      } else {
        return;
      }
    } else if (Platform.isIOS) {
      var handler = LocationOnIOS();
      if (!(await handler.hasPermission())) {
        bool ok = await LocationOnIOS().requestPermission();
        if (!ok) {
          throw "Serviço de geolocalização está desabilitado neste aparelho.";
        } else {
          return;
        }
      } else {
        return;
      }
    }
  }
}
