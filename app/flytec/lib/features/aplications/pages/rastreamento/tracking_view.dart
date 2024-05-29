import 'dart:async';

import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';

import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/features/aplications/pages/rastreamento/tracking_map_store.dart';
import 'package:flytec/features/aplications/pages/rastreamento/tracking_point.dart';
import 'package:flytec/features/aplications/pages/rastreamento/tracking_state.dart';
import 'package:flytec/features/aplications/pages/rastreamento/tracking_time_store.dart';
import 'package:flytec/features/aplications/pages/rastreamento/tracking_usecase.dart';

import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:flytec/features/aplications/controller/permission.dart';

import 'package:widgets_to_image/widgets_to_image.dart';

import 'tracking_store.dart';

class TrackingView extends StatefulWidget {
  const TrackingView({super.key});

  @override
  State<TrackingView> createState() => _TrackingViewState();
}

class _TrackingViewState extends State<TrackingView> {
  final Completer<GoogleMapController> _mapController =
      Completer<GoogleMapController>();
  CameraPosition cameraPosition = const CameraPosition(
    target: LatLng(-23.57283933300534, -46.77803615315138),
    zoom: 13,
  );

  Polyline polyline = const Polyline(
      polylineId: PolylineId('sprint'), color: Colors.purpleAccent);
  final _wtoiController = WidgetsToImageController();

  @override
  void initState() {
    final controller = getIt<TrackingUseCase>();
    CheckPermissionLocation(
      context,
      () {
        controller.getMyLocation(_mapController);
      },
    ).getPermission();
    controller.checkPermissionsStatus();
    controller.loadTracking();

    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.black,
      appBar: AppBar(
        centerTitle: true,
        title: const Text(
          "Rastreamento",
          textAlign: TextAlign.center,
        ),
      ),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: [
            Stack(
              children: [
                BlocBuilder<TrackingMapStore, List<TrackingPoint>>(
                  builder: (context, state) {
                    Polyline polyline = Polyline(
                        polylineId: const PolylineId('sprint'),
                        color: Colors.green,
                        width: 5,
                        points: state
                            .map((e) => LatLng(e.latitude, e.longitude))
                            .toList());
                    if (state.isNotEmpty) {
                      _mapController.future.then((ctn) async =>
                          ctn.animateCamera(CameraUpdate.newCameraPosition(
                              CameraPosition(
                                  target: LatLng(state.last.latitude,
                                      state.last.longitude),
                                  zoom: await ctn.getZoomLevel()))));
                    }

                    return BlocBuilder<TrackingStore, TrackingState>(
                      builder: (context, state) {
                        return Stack(
                          children: [
                            WidgetsToImage(
                              controller: _wtoiController,
                              child: AspectRatio(
                                aspectRatio: 1,
                                child: GoogleMap(
                                    mapType: MapType.satellite,
                                    compassEnabled: true,
                                    mapToolbarEnabled: false,
                                    initialCameraPosition: cameraPosition,
                                    onMapCreated:
                                        (GoogleMapController mapController) {
                                      _mapController.complete(mapController);
                                    },
                                    polylines: {polyline}),
                              ),
                            ),
                          ],
                        );
                      },
                    );
                  },
                ),
              ],
            ),
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                BlocBuilder<TrackingTimeStore, Duration>(
                  builder: (context, state) {
                    return timeDisplay(state);
                  },
                ),
              ],
            ),
            BlocBuilder<TrackingStore, TrackingState>(
              builder: (context, state) {
                return !state.permissao
                    ? mensagemPermissao()
                    : state.salvando
                        ? Center(
                            child: CircularProgressIndicator(
                              valueColor: AlwaysStoppedAnimation(
                                  Theme.of(context).indicatorColor),
                            ),
                          )
                        : _contoles();
              },
            )
          ],
        ),
      ),
      floatingActionButton: Column(
        mainAxisAlignment: MainAxisAlignment.start,
        children: [
          const SizedBox(
            height: 100,
          ),
          FloatingActionButton(
            mini: true,
            backgroundColor: Colors.white.withOpacity(0.8),
            onPressed: () {
              final controller = getIt<TrackingUseCase>();
              controller.getMyLocation(_mapController);
            },
            child: const Icon(
              Icons.my_location,
              color: Colors.grey,
            ),
          ),
          const SizedBox(
            height: 100,
          ),
        ],
      ),
    );
  }

  _recordButtom() {
    return Builder(builder: (context) {
      final store = context.read<TrackingStore>();
      final continuar = store.state.track?.pontos?.isNotEmpty ?? false;
      return IconButton.outlined(
          onPressed: () async {
            await getIt<TrackingUseCase>().startRecording();
          },
          icon: Container(
            width: 60,
            height: 60,
            decoration: BoxDecoration(
                shape: BoxShape.circle,
                color: Colors.red,
                boxShadow: kElevationToShadow[4]),
            child: Center(
                child: Text(
              continuar ? "Continue" : "Gravar",
              style: const TextStyle(
                  fontWeight: FontWeight.bold, color: Colors.white),
            )),
          ));
    });
  }

  _stopButtom() {
    return IconButton.outlined(
        onPressed: () async {
          await getIt<TrackingUseCase>().stopRecording();
        },
        icon: Container(
          width: 60,
          height: 60,
          decoration: BoxDecoration(
              shape: BoxShape.circle,
              color: Colors.black,
              boxShadow: kElevationToShadow[4]),
          child: const Center(
              child: Text(
            "Parar",
            style: TextStyle(fontWeight: FontWeight.bold, color: Colors.white),
          )),
        ));
  }

  _capturarImagem() {
    return IconButton.outlined(
        onPressed: () async {
          final mapController = await _mapController.future;
          final store = context.read<TrackingStore>();
          store.setSalvando(true);
          Uint8List? imageData = await _wtoiController.capture();

          if (imageData != null) {
            try {
              bool ok = await getIt<TrackingUseCase>().updateImagem(imageData);
              if (ok) {
                Navigator.of(context).pop();
                Navigator.of(context).pop();
              }
              store.setSalvando(false);
            } catch (e) {
              if (kDebugMode) {
                print(e);
              }
              store.setSalvando(false);
            }
          }
        },
        icon: Container(
          width: 60,
          height: 60,
          decoration: BoxDecoration(
              shape: BoxShape.circle,
              color: Colors.blue,
              boxShadow: kElevationToShadow[4]),
          child: const Center(
              child: Text(
            "Usar",
            style: TextStyle(fontWeight: FontWeight.bold, color: Colors.white),
          )),
        ));
  }

  Widget timeDisplay(Duration duration) {
    return duration.inMilliseconds == 0
        ? Container()
        : Padding(
            padding: const EdgeInsets.all(8.0),
            child: Material(
              color: Colors.white.withOpacity(0.8),
              borderRadius: BorderRadius.circular(8),
              child: Padding(
                padding: const EdgeInsets.all(8.0),
                child: Text("$duration"),
              ),
            ),
          );
  }

  _contoles() {
    return Builder(
      builder: (context) {
        final state = context.read<TrackingStore>().state;
        return state.gravando
            ? _stopButtom()
            : (!state.gravando && state.inicio == null)
                ? _recordButtom()
                : Row(
                    mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                    children: [_capturarImagem(), _recordButtom()],
                  );
      },
    );
  }

  mensagemPermissao() {
    return Expanded(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Row(
            children: [
              Expanded(
                  child: Container(
                child: Stack(
                  alignment: Alignment.center,
                  children: [
                    Column(
                      children: [
                        SizedBox(
                          height: 50,
                        ),
                        Material(
                          borderRadius: BorderRadius.circular(8),
                          child: Padding(
                            padding: const EdgeInsets.all(16.0),
                            child: Column(
                              crossAxisAlignment: CrossAxisAlignment.center,
                              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                              children: [
                                const SizedBox(
                                  height: 20,
                                ),
                                const Text(
                                  "Para gravar a navegação é necessário autorizar o acesso a localização durante todo o tempo.",
                                  textAlign: TextAlign.center,
                                ),
                                const SizedBox(
                                  height: 20,
                                ),
                                OutlinedButton(
                                    onPressed: () async {
                                      final controller =
                                          getIt<TrackingUseCase>();
                                      try {
                                        await controller.requestPermissions();
                                        await controller
                                            .checkPermissionsStatus();
                                      } catch (e) {
                                        if (kDebugMode) {
                                          print(e);
                                        }
                                      }
                                    },
                                    child: Text("Autorizar"))
                              ],
                            ),
                          ),
                        ),
                      ],
                    ),
                    const Positioned(
                        top: 20,
                        child: Row(
                          mainAxisAlignment: MainAxisAlignment.center,
                          children: [
                            Material(
                              shape: StadiumBorder(),
                              color: Colors.white,
                              child: Padding(
                                padding: EdgeInsets.all(8.0),
                                child: Icon(
                                  Icons.location_disabled_outlined,
                                  color: Colors.black,
                                  size: 54,
                                ),
                              ),
                            )
                          ],
                        ))
                  ],
                ),
              ))
            ],
          ),
        ],
      ),
    );
  }
}
