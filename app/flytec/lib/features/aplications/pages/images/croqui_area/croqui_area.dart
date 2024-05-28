// ignore_for_file: use_build_context_synchronously
import 'dart:io';
import 'dart:typed_data';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/pages/images/croqui_area/buscar_gps.dart';
import 'package:flytec/features/aplications/pages/images/croqui_area/desenhar_area.dart';
import 'package:flytec/features/aplications/components/components_exports.dart';
import 'package:flutter/material.dart';
import 'package:flytec/features/aplications/pages/images/upload_foto.dart';
import 'package:flytec/features/aplications/pages/rastreamento/tracking_page.dart';

class CroquiArea extends StatefulWidget {
  final void Function(Uint8List data, bool isPdf)? updateImageData;
  const CroquiArea({super.key, required this.updateImageData});

  @override
  State<CroquiArea> createState() => _CroquiAreaState();
}

class _CroquiAreaState extends State<CroquiArea> {
  Uint8List? _imageData;

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
                  onTap: () async {
                    _imageData = null;
                    setState(() {});
                    final archive =
                        await Util.obtainImagePathMaps(context, isPdf: true);
                    if (archive.path!.isEmpty) return;
                    _imageData = await File(archive.path!).readAsBytes();
                    if (!archive.isImage) {
                      widget.updateImageData!(_imageData!, true);
                      Navigator.pop(context);
                      return;
                    }

                    Navigator.push(context, MaterialPageRoute(
                      builder: (context) {
                        return UploadFotos(
                          updateImageData: (Uint8List data) =>
                              widget.updateImageData!(data, false),
                          imageData: _imageData,
                          onOkButton: () {
                            Navigator.pop(context);
                            Navigator.pop(context);
                          },
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
                CustomCardButton(
                  onTap: () {
                    Navigator.of(context).push(MaterialPageRoute(
                      builder: (context) =>
                          TrackingPage(updateImageData: widget.updateImageData),
                    ));
                  },
                  title: "Gravar a área",
                ),
                Center(
                  child: CustomButton(
                    title: "OK",
                    onClick: () {
                      Navigator.pop(context);
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
