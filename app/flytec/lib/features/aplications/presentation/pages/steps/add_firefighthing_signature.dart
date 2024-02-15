import 'dart:developer';

import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:signature/signature.dart';

import '../../../../auth/presentation/widgets/custom_login_button.dart';
import 'dart:typed_data';


class AddFireFightingSignatureStep extends StatefulWidget {
  const AddFireFightingSignatureStep({super.key});

  @override
  State<AddFireFightingSignatureStep> createState() =>
      _AddFireFightingSecondStepState();
}

class _AddFireFightingSecondStepState
    extends State<AddFireFightingSignatureStep> {
  final SignatureController _controller = SignatureController(
    penStrokeWidth: 2,
    penColor: Colors.green,
    exportBackgroundColor: Colors.transparent,
    exportPenColor: Colors.black,
    onDrawStart: () => log('onDrawStart called!'),
    onDrawEnd: () => log('onDrawEnd called!'),
  );

  @override
  void initState() {
    super.initState();
    _controller.addListener(() => log('Value changed'));
  }

  @override
  void dispose() {
    // IMPORTANT to dispose of the controller
    _controller.dispose();
    super.dispose();
  }

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
        await _controller.toPngBytes(height: 600, width: 600);
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

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Assinatura"),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: SingleChildScrollView(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const SizedBox(height: 16),
              Container(
                decoration: BoxDecoration(
                    border: Border.all(
                  color: Colors.grey,
                  width: 2,
                )),
                child: Signature(
                  key: const Key('signature'),
                  controller: _controller,
                  height: 400,
                  backgroundColor: Colors.white,
                ),
              ),
              Opacity(
                opacity: 0.90,
                child: InkWell(
                  onTap: () {
                    setState(() => _controller.clear());
                  },
                  child: Container(
                    width: 328,
                    height: 40,
                    padding: const EdgeInsets.all(8),
                    clipBehavior: Clip.antiAlias,
                    decoration: ShapeDecoration(
                      color: Colors.black.withOpacity(0.1599999964237213),
                      shape: const RoundedRectangleBorder(
                        side: BorderSide(width: 1, color: Colors.grey),
                      ),
                    ),
                    child: Row(
                      mainAxisSize: MainAxisSize.min,
                      mainAxisAlignment: MainAxisAlignment.start,
                      crossAxisAlignment: CrossAxisAlignment.center,
                      children: [
                        Expanded(
                          child: Column(
                            mainAxisSize: MainAxisSize.min,
                            mainAxisAlignment: MainAxisAlignment.center,
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [
                              Container(
                                width: double.infinity,
                                padding: const EdgeInsets.only(left: 8),
                                child: const Row(
                                  mainAxisSize: MainAxisSize.min,
                                  mainAxisAlignment: MainAxisAlignment.center,
                                  crossAxisAlignment: CrossAxisAlignment.center,
                                  children: [
                                    Expanded(
                                      child: Column(
                                        mainAxisSize: MainAxisSize.min,
                                        mainAxisAlignment:
                                            MainAxisAlignment.center,
                                        crossAxisAlignment:
                                            CrossAxisAlignment.center,
                                        children: [
                                          SizedBox(
                                            width: double.infinity,
                                            child: Text(
                                              'Limpar',
                                              style: TextStyle(
                                                color: Color.fromARGB(
                                                    255, 121, 118, 118),
                                                fontSize: 14,
                                                fontFamily: 'Inter',
                                                fontWeight: FontWeight.w600,
                                                height: 0.11,
                                              ),
                                            ),
                                          ),
                                        ],
                                      ),
                                    ),
                                  ],
                                ),
                              ),
                            ],
                          ),
                        ),
                        const SizedBox(width: 16),
                        Container(
                          width: 24,
                          height: 24,
                          clipBehavior: Clip.antiAlias,
                          decoration: const BoxDecoration(),
                          child: const Row(
                            mainAxisSize: MainAxisSize.min,
                            mainAxisAlignment: MainAxisAlignment.center,
                            crossAxisAlignment: CrossAxisAlignment.center,
                            children: [
                              SizedBox(
                                width: 24,
                                height: 24,
                                child: Stack(children: [Icon(Icons.close)]),
                              ),
                            ],
                          ),
                        ),
                      ],
                    ),
                  ),
                ),
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

class CustomText extends StatelessWidget {
  const CustomText({super.key, required this.text});
  final String text;
  @override
  Widget build(BuildContext context) {
    return Text(
      text,
      style: const TextStyle(
        color: Color(0xFF00B45D),
        fontSize: 14,
        fontFamily: 'Inter',
        fontWeight: FontWeight.w700,
        height: 0.11,
      ),
    );
  }
}
