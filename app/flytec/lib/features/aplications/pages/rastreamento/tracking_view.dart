import 'dart:async';
import 'dart:typed_data';

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
    CheckPermissionLocation(
      context,
      () {
        final controller = getIt.get<TrackingUseCase>();
        controller.getMyLocation(_mapController);
      },
    ).getPermission();

    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
        title: const Text(
          "Rastreamento",
          textAlign: TextAlign.center,
        ),
      ),
      body: Center(
        child: Stack(
          children: [
            Stack(
              children: [
                BlocBuilder<TrackingMapStore, List<TrackingPoint>>(
                  builder: (context, state) {
                    Polyline polyline = Polyline(
                        polylineId: const PolylineId('sprint'),
                        color: Colors.purpleAccent,
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

                    return WidgetsToImage(
                      controller: _wtoiController,
                      child: GoogleMap(
                          mapType: MapType.satellite,
                          initialCameraPosition: cameraPosition,
                          onMapCreated: (GoogleMapController mapController) {
                            _mapController.complete(mapController);
                          },
                          polylines: {polyline}),
                    );
                  },
                ),
              ],
            ),
            Positioned(
                child: Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                BlocBuilder<TrackingTimeStore, Duration>(
                  builder: (context, state) {
                    return timeDisplay(state);
                  },
                ),
              ],
            )),
            Positioned(
                bottom: 0,
                left: (MediaQuery.of(context).size.width / 2) - 25,
                child: BlocBuilder<TrackingStore, TrackingState>(
                  builder: (context, state) {
                    return state.gravando
                        ? _stopButtom()
                        : (state.track?.pontos?.isEmpty ?? true)
                            ? _recordButtom()
                            : _atualizarImagem();
                  },
                ))
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
              final controller = getIt.get<TrackingUseCase>();
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
    return IconButton.outlined(
        onPressed: () async {
          await getIt.get<TrackingUseCase>().startRecording();
        },
        icon: Container(
          width: 60,
          height: 60,
          decoration: BoxDecoration(
              shape: BoxShape.circle,
              color: Colors.red,
              boxShadow: kElevationToShadow[4]),
          child: const Center(
              child: Text(
            "Gravar",
            style: TextStyle(fontWeight: FontWeight.bold, color: Colors.white),
          )),
        ));
  }

  _stopButtom() {
    return IconButton.outlined(
        onPressed: () async {
          await getIt.get<TrackingUseCase>().stopRecording();
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

  _atualizarImagem() {
    return IconButton.outlined(
        onPressed: () async {
          Uint8List? imageData = await _wtoiController.capture();
          if (imageData != null) {
            try {
              bool ok =
                  await getIt.get<TrackingUseCase>().updateImagem(imageData);
              if (ok) {
                Navigator.of(context).pop();
              }
            } catch (e) {
              if (kDebugMode) {
                print(e);
              }
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
            "Salvar",
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
}
