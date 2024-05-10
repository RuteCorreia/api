import 'dart:async';
import 'dart:collection';
import 'package:flytec/core/extensions/widget_externsion.dart';
import 'package:flytec/features/aplications/enums/direcao_latitude.dart';
import 'package:flytec/features/aplications/enums/direcao_longitude.dart';
import 'package:flytec/features/aplications/pages/images/croqui_area/models/marker_maps.dart';
import 'package:location/location.dart' as lct;
import 'package:flutter/foundation.dart';
import 'package:flutter/gestures.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:flytec/features/aplications/controller/permission.dart';
import 'package:gap/gap.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:flytec/features/aplications/components/components_exports.dart';

class BuscarGPS extends StatefulWidget {
  const BuscarGPS({super.key});

  @override
  State<BuscarGPS> createState() => _BuscarGPSState();
}

class _BuscarGPSState extends State<BuscarGPS> {
  final Completer<GoogleMapController> _controller =
      Completer<GoogleMapController>();

  Set<Marker> markers = {};

  double converterCoordenadasParaLatitude(
      int graus, int minutos, double segundos, String direcao) {
    // Verificar se a direção é válida

    // Calcular a latitude em graus decimais
    double latitudeDecimal = graus + (minutos / 60) + (segundos / 3600);

    // Se a direção for Sul, tornar a latitude negativa
    if (direcao == 'S') {
      latitudeDecimal = -latitudeDecimal;
    }

    return latitudeDecimal;
  }

  double converterCoordenadasParaLongitude(
      int graus, int minutos, double segundos, String direcao) {
    // Verificar se a direção é válida

    // Calcular a latitude em graus decimais
    double latitudeDecimal = graus + (minutos / 60) + (segundos / 3600);

    // Se a direção for Sul, tornar a latitude negativa
    if (direcao == 'W') {
      latitudeDecimal = -latitudeDecimal;
    }

    return latitudeDecimal;
  }

  final ScrollController _scrollController = ScrollController();

  List<MarkerMaps> _markersMaps = [];

  void _onMapTapMarker(LatLng position) {
    // Adiciona um marcador no local clicado
    setState(() {
      _markersMaps.add(MarkerMaps(
        isExpanded: false,
        marker: Marker(
          markerId: MarkerId(position.toString()),
          onTap: () {},
          draggable: true,
          consumeTapEvents: true,
          position: position,
          visible: true,
          infoWindow: const InfoWindow(
            title: 'Marcador',
            snippet: 'Descrição do marcador',
          ),
        ),
      ));
      markers.add(
        Marker(
          markerId: MarkerId(position.toString()),
          onTap: () {
            _scrollController.animateTo((_markersMaps.length * 100),
                duration: const Duration(milliseconds: 200),
                curve: Curves.ease);
          },
          draggable: true,
          consumeTapEvents: true,
          position: position,
          visible: true,
          infoWindow: const InfoWindow(
            title: 'Marcador',
            snippet: 'Descrição do marcador',
          ),
        ),
      );
    });
    _showDialog(position);
  }

  _showDialog(LatLng position) async {}

  CameraPosition _kGooglePlex = const CameraPosition(
    target: LatLng(-23.57283933300534, -46.77803615315138),
    zoom: 18.4746,
  );
  DirecaoLatitude _direcaoLatitude = DirecaoLatitude.NORTE;
  DirecaoLongitude _direcaoLongitude = DirecaoLongitude.ESTE;

  final Set<Polygon> _poligone = HashSet<Polygon>();
  List<LatLng> points = [];
  late LatLng location;
  TextEditingController latitudeController = TextEditingController();
  TextEditingController longitudeController = TextEditingController();

  TextEditingController grausController = TextEditingController();
  TextEditingController minutesController = TextEditingController();
  TextEditingController segundosController = TextEditingController();

  TextEditingController grausControllerLongitude = TextEditingController();
  TextEditingController minutesControllerLongitude = TextEditingController();
  TextEditingController segundosControllerLongitude = TextEditingController();
  BitmapDescriptor _iconMarker = BitmapDescriptor.defaultMarker;
  @override
  void initState() {
    CheckPermissionLocation(
      context,
      () {
        lct.Location local = lct.Location();
        local.getLocation().then((lc) async {
          setState(() async {
            latitudeController.text = lc.latitude.toString();
            longitudeController.text = lc.longitude.toString();
            _kGooglePlex = CameraPosition(
                target: LatLng(lc.latitude!, lc.longitude!), zoom: 17.44);
            final GoogleMapController controller = await _controller.future;
            await controller
                .animateCamera(CameraUpdate.newCameraPosition(_kGooglePlex));
          });
        });
      },
    ).getPermission();
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((timeStamp) async {
      _iconMarker = await Container(
        height: 10,
        width: 10,
        decoration: BoxDecoration(
            color: const Color(0xFF00B45D).withOpacity(0.6),
            shape: BoxShape.circle,
            border: Border.all()),
      ).toBitmapDescriptor();
      setState(() {});
    });
  }

  double x12 = 150.0,
      x22 = 250.0,
      a12 = 150,
      a22 = 250,
      b12 = 150,
      b22 = 250,
      y12 = 150.0,
      y22 = 250.0,
      x1Prev2 = 150.0,
      x2Prev2 = 250.0,
      y1Prev2 = 150.0,
      y2Prev2 = 150.0;
  double x13 = 150.0,
      x23 = 270.0,
      a13 = 170,
      a23 = 270,
      b13 = 170,
      b23 = 270,
      y13 = 170.0,
      y23 = 270.0,
      x1Prev3 = 170.0,
      x2Prev3 = 270.0,
      y1Prev3 = 170.0,
      y2Prev3 = 170.0;
  double x1 = 050.0,
      x2 = 200.0,
      a1 = 105,
      a2 = 220,
      b1 = 120,
      b2 = 230,
      y1 = 050.0,
      y2 = 200.0,
      x1Prev = 050.0,
      x2Prev = 200.0,
      y1Prev = 050.0,
      y2Prev = 050.0;

  double x11 = 130.0,
      x21 = 230.0,
      y11 = 130.0,
      y21 = 230.0,
      x1Prev1 = 130.0,
      x2Prev1 = 230.0,
      y1Prev1 = 130.0,
      y2Prev1 = 130.0;
  double angle = 0.0;
  double angle2 = 0.0;
  double angle3 = 0.0;
  double angle4 = 0.0;
  double angle5 = 0.0;

  bool showSentidoVento = false;
  bool showTiro1 = false;
  bool showTiro2 = false;
  bool showTiro3 = false;
  bool showTiro4 = false;
  bool showTiro5 = false;
  bool showTiro6 = false;
  bool showTiro7 = false;

  List<Widget> lista = [];
  bool closePoligon = false;

  void _setPositionPoligon(LatLng position) {
    setState(() {
      markers.add(
        Marker(
          markerId: MarkerId('AREA: ${position.toString()}'),
          onTap: () {},
          draggable: true,
          icon: _iconMarker,
          consumeTapEvents: true,
          position: position,
          visible: true,
          infoWindow: const InfoWindow(
            title: 'Marcador',
            snippet: 'Descrição do marcador',
          ),
        ),
      );
      points.add(position);
      _poligone.add(Polygon(
          fillColor: const Color(0xFF00B45D).withOpacity(0.2),
          strokeWidth: 2,
          visible: true,
          geodesic: true,
          strokeColor: const Color(0xFF00B45D),
          polygonId: const PolygonId("1"),
          zIndex: 0,
          points: points));
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
        title: const Text(
          "Buscar pelo GPS",
          textAlign: TextAlign.center,
        ),
        actions: [
          IconButton(
              onPressed: () {
                setState(() {
                  if (showTiro1) {
                    showTiro1 = false;
                  } else if (showTiro2) {
                    showTiro2 = false;
                  } else if (showTiro3) {
                    showTiro3 = false;
                  } else if (showTiro4) {
                    showTiro4 = false;
                  } else if (showTiro5) {
                    showTiro5 = false;
                  } else if (showTiro6) {
                    showTiro6 = false;
                  } else if (showTiro7) {
                    showTiro7 = false;
                  }
                });
              },
              icon: const Icon(Icons.close)),
          IconButton(
              onPressed: () {
                setState(() {
                  closePoligon = !closePoligon;
                  ScaffoldMessenger.of(context).showSnackBar(SnackBar(
                      content: Text(closePoligon
                          ? "Área do polígono foi travada"
                          : "Área do polígono foi destravada")));
                });
              },
              icon: Icon(
                Icons.check_circle_outline,
                color: closePoligon ? Colors.red : Colors.white,
              )),
          IconButton(
            onPressed: () {
              setState(() {
                if (points.isEmpty) {
                  return;
                }
                points.removeLast();
                if (points.isNotEmpty) {
                  markers.remove(
                    Marker(
                        markerId: MarkerId(points.toString()),
                        position: points.last),
                  );
                }
                final lastElementAreaMarker = markers.lastWhere(
                    (element) => element.markerId.value.contains('AREA'),
                    orElse: () => markers.firstWhere(
                        (element) => element.markerId.value.contains('AREA'),
                        orElse: () => const Marker(markerId: MarkerId(''))));
                if (lastElementAreaMarker.markerId.value.isNotEmpty) {
                  markers.remove(lastElementAreaMarker);
                }

                _poligone.add(Polygon(
                    visible: true,
                    geodesic: true,
                    fillColor: Colors.red.withOpacity(0.2),
                    strokeWidth: 2,
                    strokeColor: Colors.red,
                    polygonId: const PolygonId("1"),
                    points: points));
              });
            },
            icon: const Icon(Icons.undo),
          ),
        ],
      ),
      body: ListView(
        controller: _scrollController,
        children: [
          Stack(
            children: [
              Positioned(
                  child: SizedBox(
                height: 360,
                child: GoogleMap(
                  indoorViewEnabled: true,
                  scrollGesturesEnabled: !closePoligon,
                  gestureRecognizers: <Factory<OneSequenceGestureRecognizer>>{
                    Factory<OneSequenceGestureRecognizer>(
                      () => EagerGestureRecognizer(),
                    ),
                  },
                  compassEnabled: true,
                  myLocationEnabled: true,
                  onLongPress: (position) {
                    _onMapTapMarker(position);
                  },
                  onTap: (argument) async {
                    if (closePoligon) {
                      return;
                    }
                    _setPositionPoligon(
                        LatLng(argument.latitude, argument.longitude));
                  },
                  zoomGesturesEnabled: true,
                  polygons: _poligone,
                  mapType: MapType.satellite,
                  markers: markers,
                  initialCameraPosition: _kGooglePlex,
                  onMapCreated: (GoogleMapController controller) {
                    _controller.complete(controller);
                  },
                ),
              )),
              Positioned(
                left: x1,
                top: y1,
                child: GestureDetector(
                  onPanDown: (d) {
                    x1Prev = x1;
                    y1Prev = y1;
                  },
                  onPanUpdate: (details) {
                    setState(() {
                      x1 = x1Prev + details.localPosition.dx;
                      y1 = y1Prev + details.localPosition.dy;
                    });
                  },
                  child: Visibility(
                    visible: showTiro1,
                    child: Transform.rotate(
                      angle: angle4,
                      child: InkWell(
                        onTap: () {
                          setState(() {
                            angle4+= 0.1;
                          });
                        },
                        child: SizedBox(
                          width: 50,
                          height: 50,
                          child: Image.asset("assets/images/tiro.png"),
                        ),
                      ),
                    ),
                  ),
                ),
              ),
              Positioned(
                left: x12,
                top: y12,
                child: GestureDetector(
                  onPanDown: (d) {
                    x1Prev2 = x12;
                    y1Prev2 = y12;
                  },
                  onPanUpdate: (details) {
                    setState(() {
                      x12 = x1Prev2 + details.localPosition.dx;
                      y12 = y1Prev2 + details.localPosition.dy;
                    });
                  },
                  child: Visibility(
                    visible: showTiro2,
                    child: Transform.rotate(
                      angle: angle2,
                      child: InkWell(
                        onTap: () {
                          setState(() {
                            angle2  += 0.1;
                          });
                        },
                        child: SizedBox(
                          width: 50,
                          height: 50,
                          child: Image.asset("assets/images/tiro.png"),
                        ),
                      ),
                    ),
                  ),
                ),
              ),
              Positioned(
                left: x13,
                top: y13,
                child: GestureDetector(
                  onPanDown: (d) {
                    x1Prev3 = x13;
                    y1Prev3 = y13;
                  },
                  onPanUpdate: (details) {
                    setState(() {
                      x13 = x1Prev3 + details.localPosition.dx;
                      y13 = y1Prev3 + details.localPosition.dy;
                    });
                  },
                  child: Visibility(
                    visible: showTiro3,
                    child: Transform.rotate(
                      angle: angle3,
                      child: InkWell(
                        onTap: () {
                          setState(() {
                            angle3 += 0.1;
                          });
                        },
                        child: SizedBox(
                          width: 50,
                          height: 50,
                          child: Image.asset("assets/images/tiro.png"),
                        ),
                      ),
                    ),
                  ),
                ),
              ),
              Positioned(
                left: x21,
                top: y21,
                child: GestureDetector(
                  onPanDown: (d) {
                    x2Prev1 = x21;
                    y2Prev1 = y21;
                    debugPrint('--> AQUI');
                  },
                  onPanUpdate: (details) {
                    setState(() {
                      x21 = x2Prev1 + details.localPosition.dx;
                      y21 = y2Prev1 + details.localPosition.dy;
                    });
                                        debugPrint('--> AQUI 2');

                  },
                  child: GestureDetector(
                    /*    onScaleUpdate: (details) {
                  setState(() {
                    angle = details.rotation;
                  });
                }, */
                    child: Transform.rotate(
                      angle: angle,
                      child: Visibility(
                        visible: showSentidoVento,
                        child: Transform.rotate(
                          angle: angle,
                          child: InkWell(
                            onTap: () {
                              setState(() {
                                angle += 0.1;
                              });
                            },
                            child: Image.asset(
                              "assets/images/vento.png",
                              width: 60 ,
                              height: 60  ,
                            ),
                          ),
                        ),
                      ),
                    ),
                  ),
                ),
              )
            ],
          ),
          const SizedBox(height: 20),
          SingleChildScrollView(
            controller: _scrollController,
            child: Column(
              children: [
                Center(
                  child: GestureDetector(
                    onTap: () {
                      setState(() {
                        if (!showTiro1) {
                          showTiro1 = true;
                        } else if (!showTiro2) {
                          showTiro2 = true;
                        } else if (!showTiro3) {
                          showTiro3 = true;
                        } else if (!showTiro4) {
                          showTiro4 = true;
                        } else if (!showTiro5) {
                          showTiro5 = true;
                        } else if (!showTiro6) {
                          showTiro6 = true;
                        }
                      });
                    },
                    child: Container(
                      width: 328,
                      height: 50,
                      padding: const EdgeInsets.symmetric(horizontal: 8),
                      decoration: ShapeDecoration(
                        shape: RoundedRectangleBorder(
                          side: const BorderSide(
                              width: 2, color: Color(0xFF00B45D)),
                          borderRadius: BorderRadius.circular(8),
                        ),
                      ),
                      child: const Row(
                        mainAxisSize: MainAxisSize.min,
                        mainAxisAlignment: MainAxisAlignment.start,
                        crossAxisAlignment: CrossAxisAlignment.center,
                        children: [
                          Row(
                            mainAxisAlignment: MainAxisAlignment.center,
                            crossAxisAlignment: CrossAxisAlignment.center,
                            children: [
                              Icon(
                                Icons.arrow_upward,
                                size: 20,
                              ),
                              Icon(
                                Icons.arrow_downward,
                                size: 20,
                              ),
                            ],
                          ),
                          Text(
                            'Inserir sentido da aplicação (tiro)',
                            style: TextStyle(
                              color: Color(0xFF151515),
                              fontSize: 15,
                              fontFamily: 'Inter',
                              fontWeight: FontWeight.w600,
                            ),
                          ),
                        ],
                      ),
                    ),
                  ),
                ),
                const SizedBox(height: 15),
                Center(
                  child: InkWell(
                    onTap: () {
                      setState(() {
                        showSentidoVento = !showSentidoVento;
                      });
                    },
                    child: Container(
                      width: 328,
                      height: 40,
                      padding: const EdgeInsets.symmetric(horizontal: 8),
                      decoration: ShapeDecoration(
                        shape: RoundedRectangleBorder(
                          side: const BorderSide(
                              width: 2, color: Color(0xFF00B45D)),
                          borderRadius: BorderRadius.circular(8),
                        ),
                      ),
                      child: const Row(
                        mainAxisSize: MainAxisSize.min,
                        mainAxisAlignment: MainAxisAlignment.start,
                        crossAxisAlignment: CrossAxisAlignment.center,
                        children: [
                          Stack(children: [
                            Icon(Icons.arrow_upward),
                          ]),
                          Text(
                            'Inserir sentido do vento',
                            style: TextStyle(
                              color: Color(0xFF151515),
                              fontSize: 16,
                              fontFamily: 'Inter',
                              fontWeight: FontWeight.w600,
                            ),
                          ),
                        ],
                      ),
                    ),
                  ),
                ),
                if (_markersMaps.isNotEmpty) ...[
                  const SizedBox(height: 10),
                  const Padding(
                    padding: EdgeInsets.all(12),
                    child: Row(
                      children: [
                        Icon(
                          Icons.fmd_good_sharp,
                          color: Color(0xFF00B45D),
                        ),
                        Text(
                          'Marcadores',
                          style: TextStyle(
                            color: Color(0xFF00B45D),
                            fontSize: 16,
                            fontFamily: 'Inter',
                            fontWeight: FontWeight.w600,
                             
                          ),
                        ),
                      ],
                    ),
                  ),
                  ListView.builder(
                    controller: _scrollController,
                    shrinkWrap: true,
                    itemCount: _markersMaps.length,
                    itemBuilder: (context, index) {
                      return Padding(
                        padding: const EdgeInsets.symmetric(
                            horizontal: 16.0, vertical: 4.0),
                        child: AnimatedContainer(
                          duration: const Duration(milliseconds: 300),
                          width: 328,
                          height: !_markersMaps[index].isExpanded ? 55 : 200,
                          padding: const EdgeInsets.symmetric(
                              horizontal: 8, vertical: 4.0),
                          decoration: ShapeDecoration(
                            shape: RoundedRectangleBorder(
                              side: const BorderSide(
                                  width: 2, color: Color(0xFF00B45D)),
                              borderRadius: BorderRadius.circular(8),
                            ),
                          ),
                          child: Column(
                            children: [
                              Row(
                                mainAxisAlignment:
                                    MainAxisAlignment.spaceBetween,
                                children: [
                                  Text(
                                      'Lat: ${markers.toList()[index].position.latitude}\nLong: ${markers.toList()[index].position.longitude}'),
                                  Row(
                                    children: [
                                      InkWell(
                                          onTap: () {
                                            setState(() {
                                              _markersMaps[index].isExpanded =
                                                  !_markersMaps[index]
                                                      .isExpanded;
                                            });
                                          },
                                          child: const Icon(
                                              Icons.open_in_full_rounded,
                                              size: 18,
                                              color: Color(0xFF00B45D))),
                                      InkWell(
                                          onTap: () {
                                            setState(() {
                                              final item = _markersMaps[index];
                                              markers.remove(item.marker);
                                              _markersMaps.removeAt(index);
                                            });
                                          },
                                          child: const Icon(Icons.close,
                                              color: Colors.red)),
                                    ],
                                  )
                                ],
                              ),
                              if (_markersMaps[index].isExpanded) ...[
                                const Divider(),
                                Container(
                                  width: double.infinity,
                                  height: 95,
                                  margin: const EdgeInsets.only(bottom: 05),
                                  padding: const EdgeInsets.symmetric(
                                      horizontal: 16, vertical: 05),
                                  decoration: ShapeDecoration(
                                    shape: RoundedRectangleBorder(
                                      side: const BorderSide(
                                          width: 1, color: Color(0xFF636363)),
                                      borderRadius: BorderRadius.circular(05),
                                    ),
                                  ),
                                  child: TextField(
                                    onSubmitted: (value) {
                                      _markersMaps[index].observation = value;
                                      setState(() {});
                                    },
                                    onChanged: (value) {
                                      _markersMaps[index].observation = value;
                                      setState(() {});
                                    },
                                    controller: _markersMaps[index].controller,
                                    decoration: const InputDecoration(
                                        border: InputBorder.none,
                                        hintStyle: TextStyle(
                                          color: Color.fromARGB(
                                              255, 121, 118, 118),
                                          fontSize: 16,
                                          fontFamily: 'Inter',
                                          fontWeight: FontWeight.w500,
                                        )),
                                  ),
                                ),
                                Center(
                                  child: InkWell(
                                    onTap: () {
                                      _markersMaps[index].controller =
                                          TextEditingController(
                                              text: _markersMaps[index]
                                                  .observation);
                                      setState(() {});
                                    },
                                    child: Container(
                                      height: 30,
                                      padding: const EdgeInsets.symmetric(
                                          vertical: 4.0, horizontal: 8.0),
                                      decoration: ShapeDecoration(
                                        color: Colors.green,
                                        shape: RoundedRectangleBorder(
                                          borderRadius:
                                              BorderRadius.circular(05),
                                        ),
                                      ),
                                      alignment: Alignment.center,
                                      child: const Text(
                                        'Salvar observação',
                                        style: TextStyle(
                                          color: Colors.white,
                                          fontSize: 12,
                                          fontFamily: 'Inter',
                                          fontWeight: FontWeight.w700,
                                        ),
                                      ),
                                    ),
                                  ),
                                )
                              ]
                            ],
                          ),
                        ),
                      );
                    },
                  )
                ],
                const SizedBox(height: 10),
                Padding(
                  padding: const EdgeInsets.all(15),
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      const CustomText(text: "Latitude"),
                      const SizedBox(height: 14),
                      Container(
                        width: double.infinity,
                        height: 50,
                        margin: const EdgeInsets.only(bottom: 05),
                        padding: const EdgeInsets.symmetric(
                            horizontal: 16, vertical: 05),
                        decoration: ShapeDecoration(
                          shape: RoundedRectangleBorder(
                            side: const BorderSide(
                                width: 1, color: Color(0xFF636363)),
                            borderRadius: BorderRadius.circular(05),
                          ),
                        ),
                        child: TextField(
                          controller: latitudeController,
                          decoration: const InputDecoration(
                              hintText: "",
                              border: InputBorder.none,
                              hintStyle: TextStyle(
                                color: Color.fromARGB(255, 121, 118, 118),
                                fontSize: 16,
                                fontFamily: 'Inter',
                                fontWeight: FontWeight.w500,
                                 
                              )),
                        ),
                      ),
                      const SizedBox(height: 15),
                      const CustomText(text: "Longitude"),
                      const SizedBox(height: 14),
                      Container(
                        width: double.infinity,
                        height: 50,
                        margin: const EdgeInsets.only(bottom: 05),
                        padding: const EdgeInsets.symmetric(
                            horizontal: 16, vertical: 05),
                        decoration: ShapeDecoration(
                          shape: RoundedRectangleBorder(
                            side: const BorderSide(
                                width: 1, color: Color(0xFF636363)),
                            borderRadius: BorderRadius.circular(05),
                          ),
                        ),
                        child: TextField(
                          controller: longitudeController,
                          decoration: const InputDecoration(
                              hintText: "",
                              border: InputBorder.none,
                              hintStyle: TextStyle(
                                color: Color.fromARGB(255, 121, 118, 118),
                                fontSize: 16,
                                fontFamily: 'Inter',
                                fontWeight: FontWeight.w500,
                                 
                              )),
                        ),
                      ),
                      const SizedBox(height: 14),
                      Center(
                        child: InkWell(
                          onTap: () async {
                            _kGooglePlex = CameraPosition(
                                target: LatLng(
                                    double.tryParse(latitudeController.text) ??
                                        0.0,
                                    double.tryParse(longitudeController.text) ??
                                        0.0),
                                zoom: 17.0);
                            final GoogleMapController controller =
                                await _controller.future;
                            await controller.animateCamera(
                                CameraUpdate.newCameraPosition(_kGooglePlex));
                          },
                          child: Container(
                            height: 40,
                            padding: const EdgeInsets.only(
                                top: 8, left: 20, right: 24, bottom: 8),
                            decoration: ShapeDecoration(
                              color: Colors.green,
                              shape: RoundedRectangleBorder(
                                side: const BorderSide(
                                  width: 2,
                                  color: Colors.green,
                                ),
                                borderRadius: BorderRadius.circular(05),
                              ),
                            ),
                            child: Row(
                              mainAxisSize: MainAxisSize.min,
                              mainAxisAlignment: MainAxisAlignment.center,
                              crossAxisAlignment: CrossAxisAlignment.center,
                              children: [
                                Container(
                                  width: 24,
                                  height: 24,
                                  clipBehavior: Clip.antiAlias,
                                  decoration: const BoxDecoration(),
                                  child: Stack(children: [
                                    SvgPicture.asset(
                                      "assets/images/checkbox.svg",
                                    )
                                  ]),
                                ),
                                const SizedBox(width: 8),
                                const Text(
                                  'BUSCAR PELO GPS',
                                  style: TextStyle(
                                    color: Colors.white,
                                    fontSize: 14,
                                    fontFamily: 'Inter',
                                    fontWeight: FontWeight.w700,
                                    height: 0.11,
                                  ),
                                ),
                              ],
                            ),
                          ),
                        ),
                      ),
                      const SizedBox(
                        height: 14,
                      ),
                      Center(
                        child: InkWell(
                          onTap: () async {
                            showModalBottomSheet(
                                context: context,
                                builder: (ctx) {
                                  return StatefulBuilder(
                                      builder: (context, update) {
                                    return Container(
                                      height: 600,
                                      width: double.infinity,
                                      color: Colors.white,
                                      child: Padding(
                                        padding: EdgeInsets.only(
                                            left: 15,
                                            right: 15,
                                            top: 15,
                                            bottom: MediaQuery.of(context)
                                                .viewInsets
                                                .bottom),
                                        child: SingleChildScrollView(
                                          child: Column(
                                            crossAxisAlignment:
                                                CrossAxisAlignment.start,
                                            children: [
                                              const Gap(20),
                                              const Text("Latitude"),
                                              Row(
                                                children: [
                                                  Expanded(
                                                    child: Column(
                                                      crossAxisAlignment:
                                                          CrossAxisAlignment
                                                              .start,
                                                      children: [
                                                        const Gap(20),
                                                        const CustomText(
                                                            text: "Graus º"),
                                                        const Gap(10),
                                                        Container(
                                                          width: 70,
                                                          height: 50,
                                                          margin:
                                                              const EdgeInsets
                                                                  .only(
                                                                  bottom: 05),
                                                          padding:
                                                              const EdgeInsets
                                                                  .symmetric(
                                                                  horizontal:
                                                                      10,
                                                                  vertical: 05),
                                                          decoration:
                                                              ShapeDecoration(
                                                            shape:
                                                                RoundedRectangleBorder(
                                                              side: const BorderSide(
                                                                  width: 1,
                                                                  color: Color(
                                                                      0xFF636363)),
                                                              borderRadius:
                                                                  BorderRadius
                                                                      .circular(
                                                                          05),
                                                            ),
                                                          ),
                                                          child: TextField(
                                                            controller:
                                                                grausController,
                                                            keyboardType:
                                                                TextInputType
                                                                    .number,
                                                            decoration:
                                                                const InputDecoration(
                                                                    hintText:
                                                                        "",
                                                                    border:
                                                                        InputBorder
                                                                            .none,
                                                                    hintStyle:
                                                                        TextStyle(
                                                                      color: Color.fromARGB(
                                                                          255,
                                                                          121,
                                                                          118,
                                                                          118),
                                                                      fontSize:
                                                                          16,
                                                                      fontFamily:
                                                                          'Inter',
                                                                      fontWeight:
                                                                          FontWeight
                                                                              .w500,
                                                                      height:
                                                                          0.09,
                                                                    )),
                                                          ),
                                                        ),
                                                      ],
                                                    ),
                                                  ),
                                                  Expanded(
                                                    child: Column(
                                                      crossAxisAlignment:
                                                          CrossAxisAlignment
                                                              .start,
                                                      children: [
                                                        const Gap(20),
                                                        const CustomText(
                                                            text: "Minutos"),
                                                        const Gap(10),
                                                        Container(
                                                          width: 70,
                                                          height: 50,
                                                          margin:
                                                              const EdgeInsets
                                                                  .only(
                                                                  bottom: 05),
                                                          padding:
                                                              const EdgeInsets
                                                                  .symmetric(
                                                                  horizontal:
                                                                      10,
                                                                  vertical: 05),
                                                          decoration:
                                                              ShapeDecoration(
                                                            shape:
                                                                RoundedRectangleBorder(
                                                              side: const BorderSide(
                                                                  width: 1,
                                                                  color: Color(
                                                                      0xFF636363)),
                                                              borderRadius:
                                                                  BorderRadius
                                                                      .circular(
                                                                          05),
                                                            ),
                                                          ),
                                                          child: TextField(
                                                            controller:
                                                                minutesController,
                                                            keyboardType:
                                                                TextInputType
                                                                    .number,
                                                            decoration:
                                                                const InputDecoration(
                                                                    hintText:
                                                                        "",
                                                                    border:
                                                                        InputBorder
                                                                            .none,
                                                                    hintStyle:
                                                                        TextStyle(
                                                                      color: Color.fromARGB(
                                                                          255,
                                                                          121,
                                                                          118,
                                                                          118),
                                                                      fontSize:
                                                                          16,
                                                                      fontFamily:
                                                                          'Inter',
                                                                      fontWeight:
                                                                          FontWeight
                                                                              .w500,
                                                                      height:
                                                                          0.09,
                                                                    )),
                                                          ),
                                                        ),
                                                      ],
                                                    ),
                                                  ),
                                                  Expanded(
                                                    child: Column(
                                                      crossAxisAlignment:
                                                          CrossAxisAlignment
                                                              .start,
                                                      children: [
                                                        const Gap(20),
                                                        const CustomText(
                                                            text: "Segundos"),
                                                        const Gap(10),
                                                        Container(
                                                          width: 70,
                                                          height: 50,
                                                          margin:
                                                              const EdgeInsets
                                                                  .only(
                                                                  bottom: 05),
                                                          padding:
                                                              const EdgeInsets
                                                                  .symmetric(
                                                                  horizontal:
                                                                      10,
                                                                  vertical: 05),
                                                          decoration:
                                                              ShapeDecoration(
                                                            shape:
                                                                RoundedRectangleBorder(
                                                              side: const BorderSide(
                                                                  width: 1,
                                                                  color: Color(
                                                                      0xFF636363)),
                                                              borderRadius:
                                                                  BorderRadius
                                                                      .circular(
                                                                          05),
                                                            ),
                                                          ),
                                                          child: TextField(
                                                            controller:
                                                                segundosController,
                                                            keyboardType:
                                                                TextInputType
                                                                    .number,
                                                            decoration:
                                                                const InputDecoration(
                                                                    hintText:
                                                                        "",
                                                                    border:
                                                                        InputBorder
                                                                            .none,
                                                                    hintStyle:
                                                                        TextStyle(
                                                                      color: Color.fromARGB(
                                                                          255,
                                                                          121,
                                                                          118,
                                                                          118),
                                                                      fontSize:
                                                                          16,
                                                                      fontFamily:
                                                                          'Inter',
                                                                      fontWeight:
                                                                          FontWeight
                                                                              .w500,
                                                                      height:
                                                                          0.09,
                                                                    )),
                                                          ),
                                                        ),
                                                      ],
                                                    ),
                                                  ),
                                                  Column(
                                                    children: [
                                                      const Text("N"),
                                                      Checkbox(
                                                          materialTapTargetSize:
                                                              MaterialTapTargetSize
                                                                  .padded,
                                                          visualDensity:
                                                              VisualDensity
                                                                  .comfortable,
                                                          value:
                                                              _direcaoLatitude ==
                                                                  DirecaoLatitude
                                                                      .NORTE,
                                                          onChanged: (_) {
                                                            update(() {
                                                              _direcaoLatitude =
                                                                  DirecaoLatitude
                                                                      .NORTE;
                                                            });
                                                          })
                                                    ],
                                                  ),
                                                  Column(
                                                    children: [
                                                      const Text("S"),
                                                      Checkbox(
                                                          materialTapTargetSize:
                                                              MaterialTapTargetSize
                                                                  .padded,
                                                          visualDensity:
                                                              VisualDensity
                                                                  .comfortable,
                                                          value:
                                                              _direcaoLatitude ==
                                                                  DirecaoLatitude
                                                                      .SUL,
                                                          onChanged: (_) {
                                                            update(() {
                                                              _direcaoLatitude =
                                                                  DirecaoLatitude
                                                                      .SUL;
                                                            });
                                                          })
                                                    ],
                                                  ),
                                                ],
                                              ),
                                              const Text("Longitude"),
                                              Row(
                                                children: [
                                                  Expanded(
                                                    child: Column(
                                                      crossAxisAlignment:
                                                          CrossAxisAlignment
                                                              .start,
                                                      children: [
                                                        const Gap(20),
                                                        const CustomText(
                                                            text: "Graus º"),
                                                        const Gap(10),
                                                        Container(
                                                          width: 70,
                                                          height: 50,
                                                          margin:
                                                              const EdgeInsets
                                                                  .only(
                                                                  bottom: 05),
                                                          padding:
                                                              const EdgeInsets
                                                                  .symmetric(
                                                                  horizontal:
                                                                      10,
                                                                  vertical: 05),
                                                          decoration:
                                                              ShapeDecoration(
                                                            shape:
                                                                RoundedRectangleBorder(
                                                              side: const BorderSide(
                                                                  width: 1,
                                                                  color: Color(
                                                                      0xFF636363)),
                                                              borderRadius:
                                                                  BorderRadius
                                                                      .circular(
                                                                          05),
                                                            ),
                                                          ),
                                                          child: TextField(
                                                            controller:
                                                                grausControllerLongitude,
                                                            keyboardType:
                                                                TextInputType
                                                                    .number,
                                                            decoration:
                                                                const InputDecoration(
                                                                    hintText:
                                                                        "",
                                                                    border:
                                                                        InputBorder
                                                                            .none,
                                                                    hintStyle:
                                                                        TextStyle(
                                                                      color: Color.fromARGB(
                                                                          255,
                                                                          121,
                                                                          118,
                                                                          118),
                                                                      fontSize:
                                                                          16,
                                                                      fontFamily:
                                                                          'Inter',
                                                                      fontWeight:
                                                                          FontWeight
                                                                              .w500,
                                                                      height:
                                                                          0.09,
                                                                    )),
                                                          ),
                                                        ),
                                                      ],
                                                    ),
                                                  ),
                                                  Expanded(
                                                    child: Column(
                                                      crossAxisAlignment:
                                                          CrossAxisAlignment
                                                              .start,
                                                      children: [
                                                        const Gap(20),
                                                        const CustomText(
                                                            text: "Minutos"),
                                                        const Gap(10),
                                                        Container(
                                                          width: 70,
                                                          height: 50,
                                                          margin:
                                                              const EdgeInsets
                                                                  .only(
                                                                  bottom: 05),
                                                          padding:
                                                              const EdgeInsets
                                                                  .symmetric(
                                                                  horizontal:
                                                                      10,
                                                                  vertical: 05),
                                                          decoration:
                                                              ShapeDecoration(
                                                            shape:
                                                                RoundedRectangleBorder(
                                                              side: const BorderSide(
                                                                  width: 1,
                                                                  color: Color(
                                                                      0xFF636363)),
                                                              borderRadius:
                                                                  BorderRadius
                                                                      .circular(
                                                                          05),
                                                            ),
                                                          ),
                                                          child: TextField(
                                                            controller:
                                                                minutesControllerLongitude,
                                                            keyboardType:
                                                                TextInputType
                                                                    .number,
                                                            decoration:
                                                                const InputDecoration(
                                                                    hintText:
                                                                        "",
                                                                    border:
                                                                        InputBorder
                                                                            .none,
                                                                    hintStyle:
                                                                        TextStyle(
                                                                      color: Color.fromARGB(
                                                                          255,
                                                                          121,
                                                                          118,
                                                                          118),
                                                                      fontSize:
                                                                          16,
                                                                      fontFamily:
                                                                          'Inter',
                                                                      fontWeight:
                                                                          FontWeight
                                                                              .w500,
                                                                      height:
                                                                          0.09,
                                                                    )),
                                                          ),
                                                        ),
                                                      ],
                                                    ),
                                                  ),
                                                  Expanded(
                                                    child: Column(
                                                      crossAxisAlignment:
                                                          CrossAxisAlignment
                                                              .start,
                                                      children: [
                                                        const Gap(20),
                                                        const CustomText(
                                                            text: "Segundos"),
                                                        const Gap(10),
                                                        Container(
                                                          width: 70,
                                                          height: 50,
                                                          margin:
                                                              const EdgeInsets
                                                                  .only(
                                                                  bottom: 05),
                                                          padding:
                                                              const EdgeInsets
                                                                  .symmetric(
                                                                  horizontal:
                                                                      10,
                                                                  vertical: 05),
                                                          decoration:
                                                              ShapeDecoration(
                                                            shape:
                                                                RoundedRectangleBorder(
                                                              side: const BorderSide(
                                                                  width: 1,
                                                                  color: Color(
                                                                      0xFF636363)),
                                                              borderRadius:
                                                                  BorderRadius
                                                                      .circular(
                                                                          05),
                                                            ),
                                                          ),
                                                          child: TextField(
                                                            controller:
                                                                segundosControllerLongitude,
                                                            keyboardType:
                                                                TextInputType
                                                                    .number,
                                                            decoration:
                                                                const InputDecoration(
                                                                    hintText:
                                                                        "",
                                                                    border:
                                                                        InputBorder
                                                                            .none,
                                                                    hintStyle:
                                                                        TextStyle(
                                                                      color: Color.fromARGB(
                                                                          255,
                                                                          121,
                                                                          118,
                                                                          118),
                                                                      fontSize:
                                                                          16,
                                                                      fontFamily:
                                                                          'Inter',
                                                                      fontWeight:
                                                                          FontWeight
                                                                              .w500,
                                                                      height:
                                                                          0.09,
                                                                    )),
                                                          ),
                                                        ),
                                                      ],
                                                    ),
                                                  ),
                                                  Column(
                                                    children: [
                                                      const Text("W"),
                                                      Checkbox(
                                                          materialTapTargetSize:
                                                              MaterialTapTargetSize
                                                                  .padded,
                                                          visualDensity:
                                                              VisualDensity
                                                                  .comfortable,
                                                          value:
                                                              _direcaoLongitude ==
                                                                  DirecaoLongitude
                                                                      .OESTE,
                                                          onChanged: (_) {
                                                            update(() {
                                                              _direcaoLongitude =
                                                                  DirecaoLongitude
                                                                      .OESTE;
                                                            });
                                                          })
                                                    ],
                                                  ),
                                                  Column(
                                                    children: [
                                                      const Text("E"),
                                                      Checkbox(
                                                          materialTapTargetSize:
                                                              MaterialTapTargetSize
                                                                  .padded,
                                                          visualDensity:
                                                              VisualDensity
                                                                  .comfortable,
                                                          value:
                                                              _direcaoLongitude ==
                                                                  DirecaoLongitude
                                                                      .ESTE,
                                                          onChanged: (_) {
                                                            update(() {
                                                              _direcaoLongitude =
                                                                  DirecaoLongitude
                                                                      .ESTE;
                                                            });
                                                          })
                                                    ],
                                                  ),
                                                ],
                                              ),
                                              const Gap(10),
                                              Center(
                                                child: ElevatedButton(
                                                    style: const ButtonStyle(),
                                                    onPressed: () async {
                                                      latitudeController
                                                          .text = converterCoordenadasParaLatitude(
                                                              int.parse(
                                                                  grausController
                                                                      .text),
                                                              int.parse(
                                                                  minutesController
                                                                      .text),
                                                              double.parse(
                                                                  segundosController
                                                                      .text),
                                                              _direcaoLatitude ==
                                                                      DirecaoLatitude
                                                                          .NORTE
                                                                  ? "N"
                                                                  : "S")
                                                          .toStringAsFixed(6);

                                                      longitudeController
                                                          .text = converterCoordenadasParaLongitude(
                                                              int.parse(
                                                                  grausControllerLongitude
                                                                      .text),
                                                              int.parse(
                                                                  minutesControllerLongitude
                                                                      .text),
                                                              double.parse(
                                                                  segundosControllerLongitude
                                                                      .text),
                                                              _direcaoLongitude ==
                                                                      DirecaoLongitude
                                                                          .ESTE
                                                                  ? "E"
                                                                  : "W")
                                                          .toStringAsFixed(6);

                                                      _kGooglePlex =
                                                          CameraPosition(
                                                              target: LatLng(
                                                                converterCoordenadasParaLatitude(
                                                                    int.parse(
                                                                        grausController
                                                                            .text),
                                                                    int.parse(
                                                                        minutesController
                                                                            .text),
                                                                    double.parse(
                                                                        segundosController
                                                                            .text),
                                                                    _direcaoLatitude ==
                                                                            DirecaoLatitude.NORTE
                                                                        ? "N"
                                                                        : "S"),
                                                                converterCoordenadasParaLongitude(
                                                                    int.parse(
                                                                        grausControllerLongitude
                                                                            .text),
                                                                    int.parse(
                                                                        minutesControllerLongitude
                                                                            .text),
                                                                    double.parse(
                                                                        segundosControllerLongitude
                                                                            .text),
                                                                    _direcaoLongitude ==
                                                                            DirecaoLongitude.ESTE
                                                                        ? "E"
                                                                        : "W"),
                                                              ),
                                                              zoom: 18.0);
                                                      final GoogleMapController
                                                          controller =
                                                          await _controller
                                                              .future;
                                                      controller
                                                          .animateCamera(CameraUpdate
                                                              .newCameraPosition(
                                                                  _kGooglePlex))
                                                          .then((value) {
                                                        setState(() {});
                                                        Navigator.pop(context);
                                                      });
                                                    },
                                                    child: const Text(
                                                        "BUSCAR LOCALIZAÇÃO")),
                                              )
                                            ],
                                          ),
                                        ),
                                      ),
                                    );
                                  });
                                });
                          },
                          child: Container(
                            height: 40,
                            padding: const EdgeInsets.only(
                                top: 8, left: 15, right: 15, bottom: 8),
                            decoration: ShapeDecoration(
                              color: Colors.green,
                              shape: RoundedRectangleBorder(
                                side: const BorderSide(
                                  width: 2,
                                  color: Colors.green,
                                ),
                                borderRadius: BorderRadius.circular(05),
                              ),
                            ),
                            child: Row(
                              mainAxisSize: MainAxisSize.min,
                              mainAxisAlignment: MainAxisAlignment.center,
                              crossAxisAlignment: CrossAxisAlignment.center,
                              children: [
                                Container(
                                  width: 24,
                                  height: 24,
                                  clipBehavior: Clip.antiAlias,
                                  decoration: const BoxDecoration(),
                                  child: Stack(children: [
                                    SvgPicture.asset(
                                      "assets/images/checkbox.svg",
                                    )
                                  ]),
                                ),
                                const SizedBox(width: 8),
                                const Flexible(
                                  child: Text(
                                    'BUSCAR POR GRAU, MINUTOS E SEGUNDOS',
                                    style: TextStyle(
                                      color: Colors.white,
                                      fontSize: 12,
                                      fontFamily: 'Inter',
                                      fontWeight: FontWeight.w700,
                                      height: 0.11,
                                    ),
                                  ),
                                ),
                              ],
                            ),
                          ),
                        ),
                      ),
                      const SizedBox(height: 10),
                      Center(
                        child: InkWell(
                          onTap: () {
                            Navigator.pop(context);
                          },
                          child: Container(
                            height: 40,
                            padding: const EdgeInsets.only(
                                top: 8, left: 20, right: 24, bottom: 8),
                            decoration: ShapeDecoration(
                              color: Colors.green,
                              shape: RoundedRectangleBorder(
                                side: const BorderSide(
                                  width: 2,
                                  color: Colors.green,
                                ),
                                borderRadius: BorderRadius.circular(05),
                              ),
                            ),
                            child: Row(
                              mainAxisSize: MainAxisSize.min,
                              mainAxisAlignment: MainAxisAlignment.center,
                              crossAxisAlignment: CrossAxisAlignment.center,
                              children: [
                                Container(
                                  width: 24,
                                  height: 24,
                                  clipBehavior: Clip.antiAlias,
                                  decoration: const BoxDecoration(),
                                  child: Stack(children: [
                                    SvgPicture.asset(
                                      "assets/images/checkbox.svg",
                                    )
                                  ]),
                                ),
                                const SizedBox(width: 8),
                                const Text(
                                  'OK',
                                  style: TextStyle(
                                    color: Colors.white,
                                    fontSize: 14,
                                    fontFamily: 'Inter',
                                    fontWeight: FontWeight.w700,
                                    height: 0.11,
                                  ),
                                ),
                              ],
                            ),
                          ),
                        ),
                      )
                    ],
                  ),
                )
              ],
            ),
          )
        ],
      ),
    );
  }
}
