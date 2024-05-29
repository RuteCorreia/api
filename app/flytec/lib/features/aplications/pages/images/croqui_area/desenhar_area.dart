import 'dart:developer';
import 'dart:math' as math;
import 'dart:typed_data';

import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:signature/signature.dart';

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
                        width: double.infinity,
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
                                height: 60,
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
                        width: double.infinity,
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
                            Expanded(
                              child: Container(
                                height: 50,
                                margin: const EdgeInsets.only(top: 8),
                                child: const Padding(
                                  padding: EdgeInsets.all(12),
                                  child: Text(
                                    'Inserir sentido do vento',
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
                  const SizedBox(height: 20),
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
              IconButton(
                icon: const Icon(Icons.check),
                color: Colors.blue,
                onPressed: () {
                  Navigator.pop(context);
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
