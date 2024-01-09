import 'dart:async';
import 'dart:collection';
import 'dart:developer';
import 'dart:io';
import 'dart:math' as math;

import 'package:flutter/foundation.dart';
import 'package:flutter/gestures.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_svg/svg.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/presentation/widgets/image_selected.dart';
import 'package:flytec/features/home/presentation/widgets/custom_dialog_button.dart';
import 'package:gap/gap.dart';
import 'package:go_router/go_router.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:image_picker/image_picker.dart';
import 'package:location/location.dart' as lct;
import 'package:signature/signature.dart';

import '../../../../auth/presentation/widgets/custom_login_button.dart';
import '../controllers/permission.dart';
import '../steps/aplication_second_step.dart';

class DMS {
  int degrees;
  int minutes;
  double seconds;

  DMS({required this.degrees, required this.minutes, required this.seconds});

  // Função para converter de DMS para DD
  double toDecimalDegrees() {
    double dd = degrees + (minutes / 60) + (seconds / 3600);
    return dd;
  }
}

class CroquisAreaCliente extends StatelessWidget {
  const CroquisAreaCliente({super.key, required this.updateImagePathMap});
  final void Function(String path)? updateImagePathMap;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
        title: const Text(
          "Croqui da área",
          textAlign: TextAlign.center,
          style: TextStyle(),
        ),
      ),
      body: Padding(
        padding: const EdgeInsets.all(8.0),
        child: SingleChildScrollView(
          child: Center(
            child: Column(
              children: [
                CustomCardButton(
                  onTap: () {
                    Navigator.push(context,
                        MaterialPageRoute(builder: (context) {
                      return const DesenharArea();
                    }));
                  },
                  title: "Desenhar área",
                ),
                CustomCardButton(
                  onTap: () {
                    Navigator.push(context,
                        MaterialPageRoute(builder: (context) {
                      return const BuscarGPS();
                    }));
                  },
                  title: "Buscar pelo GPS",
                ),
                CustomCardButton(
                  onTap: () {
                    Navigator.push(context, MaterialPageRoute(
                      builder: (context) {
                        return UplodadFotos(
                          updateImagePathMap: updateImagePathMap,
                        );
                      },
                    ));
                  },
                  title: "Foto do Mapa",
                ),
                CustomCardButton(
                  onTap: () {},
                  title: "Importar Log",
                ),
                Center(
                  child: CustomButton(
                    title: "OK",
                    onClick: () {
                      context.pop();
                    },
                  ),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}

class UplodadFotos extends StatefulWidget {
  const UplodadFotos({super.key, required this.updateImagePathMap});
  final void Function(String path)? updateImagePathMap;

  @override
  State<UplodadFotos> createState() => _UplodadFotosState();
}

class _UplodadFotosState extends State<UplodadFotos> {
  String? _imagePath = '';
  bool _isCut = true;
  Uint8List? _imageData;
  bool _showButtonOk = false;
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
        title: const Text(
          "Upload de fotos",
          textAlign: TextAlign.center,
          style: TextStyle(),
        ),
      ),
      body: Padding(
        padding: const EdgeInsets.all(8.0),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.spaceEvenly,
          children: [
            Column(
              children: [
                Center(
                  child: InkWell(
                    onTap: () async {
                      _isCut = true;
                      _imagePath = '';
                      setState(() {});
                      _imagePath = await Util.obtainImagePathMaps(context);
                      _imageData = await File(_imagePath!).readAsBytes();
                      setState(() {});
                    },
                    child: const Icon(
                      Icons.photo_camera_outlined,
                      color: Colors.green,
                      size: 050,
                    ),
                  ),
                ),
                const SizedBox(height: 20),
                const Text(
                  'Tire uma foto do mapa da área',
                  textAlign: TextAlign.center,
                  style: TextStyle(
                    color: Color(0xFF151515),
                    fontSize: 20,
                    fontFamily: 'Inter',
                    fontWeight: FontWeight.w600,
                    height: 0.07,
                  ),
                ),
              ],
            ),
          
            if (_imagePath!.isNotEmpty)
              Column(
                children: [
                  SizedBox(height: MediaQuery.of(context).size.height * 0.01),
                  ImageSelected(
                    imageMapsPath: _imagePath,
                    imageData: _imageData,
                    onCutImage: (cut, path) {
                      _isCut = cut;
                      widget.updateImagePathMap!(path);
                      _showButtonOk = true;
                      setState(() {});
                    },
                    isCut: _isCut,
                  ),
                  SizedBox(height: MediaQuery.of(context).size.height * 0.01),
                  if (_showButtonOk)
                    Center(
                      child: InkWell(
                        onTap: () {
                          context.pop();
                          context.pop();
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
          ],
        ),
      ),
    );
  }
}

class DesenharArea extends StatefulWidget {
  const DesenharArea({super.key});

  @override
  State<DesenharArea> createState() => _DesenharAreaState();
}

class _DesenharAreaState extends State<DesenharArea> {
  Future<void> exportImage(BuildContext context) async {
    if (_controller.isEmpty) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          key: Key('snackbarPNG'),
          content: Text('No content'),
        ),
      );
      return;
    }

    final Uint8List? data =
        await _controller.toPngBytes(height: 0500, width: 0500);
    if (data == null) {
      return;
    }

    if (!mounted) return;
  }

  Future<void> exportSVG(BuildContext context) async {
    if (_controller.isEmpty) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          key: Key('snackbarSVG'),
          content: Text('No content'),
        ),
      );
      return;
    }

    final SvgPicture data = _controller.toSVG()!;

    if (!mounted) return;
  }

  final SignatureController _controller = SignatureController(
    penStrokeWidth: 4,
    strokeCap: StrokeCap.square,
    penColor: Colors.green,
    exportBackgroundColor: Colors.transparent,
    exportPenColor: Colors.black,
    onDrawStart: () => log('onDrawStart called!'),
    onDrawEnd: () => log('onDrawEnd called!'),
  );
  double gerarNumeroAleatorio() {
    math.Random random = math.Random();
    return random.nextInt(051) + 050; // Gera um número entre 200 e 300
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
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
        title: const Text(
          "Desenhar área",
          textAlign: TextAlign.center,
          style: TextStyle(),
        ),
      ),
      body: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(8.0),
          child: Stack(
            children: [
              Positioned(
                  child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Container(
                    decoration: BoxDecoration(
                        border: Border.all(
                      color: Colors.grey,
                      width: 2,
                    )),
                    child: Signature(
                      key: const Key('signature'),
                      controller: _controller,
                      height: 220,
                      backgroundColor: Colors.white,
                    ),
                  ),
                  const SizedBox(height: 50),
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
                        height: 40,
                        padding: const EdgeInsets.symmetric(horizontal: 8),
                        decoration: ShapeDecoration(
                          shape: RoundedRectangleBorder(
                            side: const BorderSide(
                                width: 2, color: Color(0xFF00B45D)),
                            borderRadius: BorderRadius.circular(8),
                          ),
                        ),
                        child: Row(
                          mainAxisSize: MainAxisSize.min,
                          mainAxisAlignment: MainAxisAlignment.center,
                          crossAxisAlignment: CrossAxisAlignment.center,
                          children: [
                            Container(
                              width: 33,
                              height: 36,
                              clipBehavior: Clip.antiAlias,
                              decoration: const BoxDecoration(),
                              child: const Row(
                                mainAxisSize: MainAxisSize.min,
                                mainAxisAlignment: MainAxisAlignment.center,
                                crossAxisAlignment: CrossAxisAlignment.center,
                                children: [
                                  SizedBox(
                                    width: 33,
                                    height: 32,
                                    child: Stack(children: [
                                      Icon(Icons.arrow_upward),
                                    ]),
                                  ),
                                ],
                              ),
                            ),
                            const Expanded(
                              child: SizedBox(
                                height: 50,
                                child: Padding(
                                  padding: EdgeInsets.all(12),
                                  child: Text(
                                    'Inserir sentido da aplicação (tiro)',
                                    style: TextStyle(
                                      color: Color(0xFF151515),
                                      fontSize: 16,
                                      fontFamily: 'Inter',
                                      fontWeight: FontWeight.w600,
                                    ),
                                  ),
                                ),
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
                        child: Row(
                          mainAxisSize: MainAxisSize.min,
                          mainAxisAlignment: MainAxisAlignment.center,
                          crossAxisAlignment: CrossAxisAlignment.center,
                          children: [
                            Container(
                              width: 33,
                              height: 36,
                              clipBehavior: Clip.antiAlias,
                              decoration: const BoxDecoration(),
                              child: const Row(
                                mainAxisSize: MainAxisSize.min,
                                mainAxisAlignment: MainAxisAlignment.center,
                                crossAxisAlignment: CrossAxisAlignment.center,
                                children: [
                                  SizedBox(
                                    width: 33,
                                    height: 32,
                                    child: Stack(children: [
                                      Icon(Icons.arrow_upward),
                                    ]),
                                  ),
                                ],
                              ),
                            ),
                            const Expanded(
                              child: SizedBox(
                                height: 50,
                                child: Padding(
                                  padding: EdgeInsets.all(12),
                                  child: Text(
                                    'Inserir sentido do vento',
                                    style: TextStyle(
                                      color: Color(0xFF151515),
                                      fontSize: 16,
                                      fontFamily: 'Inter',
                                      fontWeight: FontWeight.w600,
                                      height: 0.09,
                                    ),
                                  ),
                                ),
                              ),
                            ),
                          ],
                        ),
                      ),
                    ),
                  ),
                  const SizedBox(height: 20),
                  Center(
                    child: InkWell(
                      onTap: () {
                        context.pop();
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
              )),
              Positioned(
                child: Padding(
                  padding: const EdgeInsets.all(8.0),
                  child: Image.asset(
                    "assets/images/bussula1.png",
                    width: 70,
                  ),
                ),
              ),
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
                        onDoubleTap: () {
                          setState(() {
                            showTiro1 = false;
                          });
                        },
                        onTap: () {
                          setState(() {
                            angle4 = angle4++;
                          });
                        },
                        child: SizedBox(
                          width: 40,
                          height: 40,
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
                            angle2 = angle2 + 05;
                          });
                        },
                        child: SizedBox(
                          width: 40,
                          height: 40,
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
                            angle3 = angle3 + 05;
                          });
                        },
                        child: SizedBox(
                          width: 40,
                          height: 40,
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
                  },
                  onPanUpdate: (details) {
                    setState(() {
                      x21 = x2Prev1 + details.localPosition.dx;
                      y21 = y2Prev1 + details.localPosition.dy;
                    });
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
                                angle = angle++;
                              });
                            },
                            child: Container(
                              width: 70,
                              height: 70,
                              padding: const EdgeInsets.all(8),
                              color: Colors.transparent,
                              child: const Column(
                                mainAxisAlignment: MainAxisAlignment.center,
                                crossAxisAlignment: CrossAxisAlignment.center,
                                children: [
                                  Icon(Icons.arrow_forward),
                                  Text(
                                    "SENTIDO DO VENTO",
                                    textAlign: TextAlign.center,
                                    style: TextStyle(fontSize: 8),
                                  )
                                ],
                              ),
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
        ),
      ),
      bottomNavigationBar: BottomAppBar(
        color: Colors.white,
        child: Container(
          decoration: const BoxDecoration(color: Colors.white),
          child: Row(
            mainAxisAlignment: MainAxisAlignment.spaceAround,
            mainAxisSize: MainAxisSize.max,
            children: <Widget>[
              //SHOW EXPORTED IMAGE IN NEW ROUTE
              /*  IconButton(
                key: const Key('exportPNG'),
                icon: const Icon(Icons.image),
                color: Colors.blue,
                onPressed: () => exportImage(context),
                tooltip: 'Export Image',
              ),
 */
              IconButton(
                icon: const Icon(Icons.check),
                color: Colors.blue,
                onPressed: () {
                  context.pop();
                },
                tooltip: 'Ok',
              ),
              IconButton(
                icon: const Icon(Icons.undo),
                color: Colors.blue,
                onPressed: () {
                  setState(() => _controller.undo());
                },
                tooltip: 'Undo',
              ),
              IconButton(
                icon: const Icon(Icons.redo),
                color: Colors.blue,
                onPressed: () {
                  setState(() => _controller.redo());
                },
                tooltip: 'Redo',
              ),
              //CLEAR CANVAS
              IconButton(
                key: const Key('clear'),
                icon: const Icon(Icons.clear),
                color: Colors.blue,
                onPressed: () {
                  setState(() => _controller.clear());
                },
                tooltip: 'Clear',
              ),
              // STOP Edit
            ],
          ),
        ),
      ),
    );
  }
}

enum DirecaoLatitude { NORTE, SUL }

enum DirecaoLongitude { ESTE, OESTE }

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

  void _onMapTapMarker(LatLng position) {
    // Adiciona um marcador no local clicado
    setState(() {
      markers.add(
        Marker(
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
      );
    });
    _showDialog(position);
  }

  _showDialog(LatLng position) async {
    String description = await showDialog(
      context: context,
      builder: (BuildContext context) {
        TextEditingController controller = TextEditingController();
        return AlertDialog(
          title: const Text('Adicionar Descrição'),
          content: TextField(
            controller: controller,
            decoration: const InputDecoration(hintText: 'Digite a descrição'),
          ),
          actions: <Widget>[
            ElevatedButton(
              child: const Text('Cancelar'),
              onPressed: () {
                Navigator.of(context).pop();
              },
            ),
            ElevatedButton(
              child: const Text('Adicionar'),
              onPressed: () {
                if (controller.text.isNotEmpty) {
                  setState(() {
                    markers.add(Marker(
                      markerId: MarkerId(position.toString()),
                      position: position,
                      infoWindow: InfoWindow(
                          title: 'Marcador', snippet: controller.text),
                    ));
                  });
                }
                Navigator.of(context).pop(controller.text);
              },
            ),
          ],
        );
      },
    );
  }

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

  @override
  void initState() {
    CheckPermissionLocation(
      context,
      () {
        lct.Location local = lct.Location();
        local.getLocation().then((lc) async {
          print("============LATITUDE=========== ${lc.latitude}");
          print("============LONGITUDE=========== ${lc.longitude}");

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
                markers.remove(
                  Marker(
                    markerId: MarkerId(points.toString()),
                    position: points.last,
                  ),
                );
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
      body: Container(
        child: ListView(
          // physics: const NeverScrollableScrollPhysics(),
          children: [
            Stack(
              children: [
                Positioned(
                    child: SizedBox(
                  height: 360,
                  child: GoogleMap(
                    indoorViewEnabled: true,
                    scrollGesturesEnabled: true,
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
                    onTap: (argument) {
                      if (closePoligon) {
                        return;
                      }
                      setState(() {
                        points
                            .add(LatLng(argument.latitude, argument.longitude));
                        _poligone.add(Polygon(
                            fillColor: Colors.red.withOpacity(0.2),
                            strokeWidth: 2,
                            strokeColor: Colors.red,
                            polygonId: const PolygonId("1"),
                            points: points));
                      });
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
                              angle4++;
                            });
                          },
                          child: SizedBox(
                            width: 40,
                            height: 40,
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
                              angle2 = angle2++;
                            });
                          },
                          child: SizedBox(
                            width: 40,
                            height: 40,
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
                              angle3 = angle3++;
                            });
                          },
                          child: SizedBox(
                            width: 40,
                            height: 40,
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
                    },
                    onPanUpdate: (details) {
                      setState(() {
                        x21 = x2Prev1 + details.localPosition.dx;
                        y21 = y2Prev1 + details.localPosition.dy;
                      });
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
                                  angle++;
                                });
                              },
                              child: Container(
                                width: 100,
                                height: 100,
                                padding: const EdgeInsets.all(20),
                                color: Colors.transparent,
                                child: Column(
                                  mainAxisAlignment: MainAxisAlignment.center,
                                  crossAxisAlignment: CrossAxisAlignment.center,
                                  children: [
                                    Image.asset(
                                      "assets/images/vento.png",
                                      width: 50,
                                    )
                                  ],
                                ),
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
                        child: Row(
                          mainAxisSize: MainAxisSize.min,
                          mainAxisAlignment: MainAxisAlignment.center,
                          crossAxisAlignment: CrossAxisAlignment.center,
                          children: [
                            Container(
                              width: 40,
                              height: 36,
                              clipBehavior: Clip.antiAlias,
                              decoration: const BoxDecoration(),
                              child: const Row(
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
                            ),
                            Expanded(
                              child: Container(
                                height: 55,
                                margin: const EdgeInsets.only(top: 5),
                                child: const Padding(
                                  padding: EdgeInsets.all(12),
                                  child: Text(
                                    'Inserir sentido da aplicação (tiro)',
                                    style: TextStyle(
                                      color: Color(0xFF151515),
                                      fontSize: 15,
                                      fontFamily: 'Inter',
                                      fontWeight: FontWeight.w600,
                                    ),
                                  ),
                                ),
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
                        child: Row(
                          mainAxisSize: MainAxisSize.min,
                          mainAxisAlignment: MainAxisAlignment.center,
                          crossAxisAlignment: CrossAxisAlignment.center,
                          children: [
                            Container(
                              width: 33,
                              height: 36,
                              clipBehavior: Clip.antiAlias,
                              decoration: const BoxDecoration(),
                              child: const Row(
                                mainAxisSize: MainAxisSize.min,
                                mainAxisAlignment: MainAxisAlignment.center,
                                crossAxisAlignment: CrossAxisAlignment.center,
                                children: [
                                  SizedBox(
                                    width: 33,
                                    height: 32,
                                    child: Stack(children: [
                                      Icon(Icons.arrow_upward),
                                    ]),
                                  ),
                                ],
                              ),
                            ),
                            const Expanded(
                              child: SizedBox(
                                height: 50,
                                child: Padding(
                                  padding: EdgeInsets.all(12),
                                  child: Text(
                                    'Inserir sentido do vento',
                                    style: TextStyle(
                                      color: Color(0xFF151515),
                                      fontSize: 16,
                                      fontFamily: 'Inter',
                                      fontWeight: FontWeight.w600,
                                      height: 0.09,
                                    ),
                                  ),
                                ),
                              ),
                            ),
                          ],
                        ),
                      ),
                    ),
                  ),
                  const SizedBox(height: 20),
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
                                  height: 0.09,
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
                                  height: 0.09,
                                )),
                          ),
                        ),
                        const SizedBox(height: 14),
                        Center(
                          child: InkWell(
                            onTap: () async {
                              _kGooglePlex = CameraPosition(
                                  target: LatLng(
                                      double.tryParse(
                                              latitudeController.text) ??
                                          0.0,
                                      double.tryParse(
                                              longitudeController.text) ??
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
                                                                    vertical:
                                                                        05),
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
                                                                      border: InputBorder
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
                                                                            FontWeight.w500,
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
                                                                    vertical:
                                                                        05),
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
                                                                      border: InputBorder
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
                                                                            FontWeight.w500,
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
                                                                    vertical:
                                                                        05),
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
                                                                      border: InputBorder
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
                                                                            FontWeight.w500,
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
                                                            value: _direcaoLatitude ==
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
                                                            value: _direcaoLatitude ==
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
                                                                    vertical:
                                                                        05),
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
                                                                      border: InputBorder
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
                                                                            FontWeight.w500,
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
                                                                    vertical:
                                                                        05),
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
                                                                      border: InputBorder
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
                                                                            FontWeight.w500,
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
                                                                    vertical:
                                                                        05),
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
                                                                      border: InputBorder
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
                                                                            FontWeight.w500,
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
                                                            value: _direcaoLongitude ==
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
                                                            value: _direcaoLongitude ==
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
                                                      style:
                                                          const ButtonStyle(),
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
                                                          context.pop();
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
                              context.pop();
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
      ),
    );
  }
}
