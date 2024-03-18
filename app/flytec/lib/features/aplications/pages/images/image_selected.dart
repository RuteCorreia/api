import 'dart:typed_data';
import 'package:crop_your_image/crop_your_image.dart';
import 'package:flutter/material.dart';
import 'package:flytec/features/aplications/components/components_exports.dart';

class ImageSelected extends StatefulWidget {
  final bool isCut;
  final Uint8List? imageData;
  final Function(bool cut, Uint8List data) onCutImage;
  const ImageSelected(
      {super.key,
      this.isCut = true,
      required this.imageData,
      required this.onCutImage});

  @override
  State<ImageSelected> createState() => _ImageSelectedState();
}

class _ImageSelectedState extends State<ImageSelected> {
  final _cropController = CropController();
  bool _isLoading = false;
  @override
  Widget build(BuildContext context) {
    return Builder(builder: (context) {
      if (widget.isCut) {
        return Stack(children: [
          if (_isLoading) const Center(child: CircularProgressIndicator()),
          Column(
            children: [
              SizedBox(
                height: MediaQuery.of(context).size.height * 0.45,
                width: MediaQuery.of(context).size.width,
                child: Crop(
                    image: widget.imageData!,
                    controller: _cropController,
                    progressIndicator:
                        const Center(child: CircularProgressIndicator()),
                    onStatusChanged: (value) {
                      _isLoading = value == CropStatus.loading;
                      setState(() {});
                    },
                    onCropped: (image) async {
                      widget.onCutImage(false, image);
                      setState(() {});
                    }),
              ),
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceAround,
                crossAxisAlignment: CrossAxisAlignment.center,
                children: [
                  IconButton(
                    icon: const Icon(Icons.close),
                    onPressed: () {
                      _cropController.rect =
                          const Rect.fromLTRB(0.1, 0.1, 0.9, 0.9);
                      _cropController.aspectRatio = 1.0;
                    },
                  ),
                  TextButton(
                    onPressed: () async {
                      _cropController.crop();
                    },
                    child: const CustomText(text: 'Pronto'),
                  ),
                ],
              )
            ],
          ),
        ]);
      }
      return Container(
          height: 300,
          width: MediaQuery.of(context).size.width,
          decoration: BoxDecoration(
            image: DecorationImage(
                repeat: ImageRepeat.repeat,
                image: MemoryImage(widget.imageData!),
                fit: BoxFit.fill),
          ));
    });
  }
}
