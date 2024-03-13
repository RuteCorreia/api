import 'dart:typed_data';

import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:flytec/features/aplications/pages/images/image_selected.dart';

class UploadFotos extends StatefulWidget {
  final void Function(Uint8List data)? updateImageData;
  final Uint8List? imageData;
  final VoidCallback onOkButton;
  const UploadFotos({
    super.key,
    required this.updateImageData,
    required this.onOkButton,
    this.imageData,
  });

  @override
  State<UploadFotos> createState() => _UploadFotosState();
}

class _UploadFotosState extends State<UploadFotos> {
  bool _isCut = true;
  bool _showButtonOk = false;
  Uint8List? _imageData;

  @override
  void initState() {
    super.initState();
    _imageData = widget.imageData;
  }

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
          child: Builder(builder: (context) {
            if (_imageData != null) {
              return Column(
                children: [
                  SizedBox(height: MediaQuery.of(context).size.height * 0.01),
                  ImageSelected(
                    imageData: _imageData,
                    onCutImage: (cut, data) {
                      _isCut = cut;
                      widget.updateImageData!(data);
                      _imageData = null;
                      _imageData = data;
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
                          widget.onOkButton();
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
              );
            }
            return const SizedBox.shrink();
          })),
    );
  }
}
