import 'dart:io';
import 'dart:ui';
import 'package:crop_image/crop_image.dart';
import 'package:flutter/material.dart';

class ImageSelected extends StatefulWidget {
  final String? imageMapsPath;
  final bool isCut;
  final Function(bool) onCutImage;
  const ImageSelected(
      {super.key,
      this.imageMapsPath,
      this.isCut = true,
      required this.onCutImage});

  @override
  State<ImageSelected> createState() => _ImageSelectedState();
}

class _ImageSelectedState extends State<ImageSelected> {
  final _cropController = CropController(
    aspectRatio: 1,
    defaultCrop: const Rect.fromLTRB(0.1, 0.1, 0.9, 0.9),
  );

  Future<void> _rotateImageToLeft() async => _cropController.rotateLeft();

  Future<void> _rotateImageToRight() async => _cropController.rotateRight();

  late Image imageCropped;

  Future<void> _finishedCrop() async {
    imageCropped = await _cropController.croppedImage();
    setState(() {});
    await _updateImageCropped();
    widget.onCutImage(false);
  }

  Future<void> _updateImageCropped() async {
    final bitmap = await _cropController.croppedBitmap();
    final data = await bitmap.toByteData(format: ImageByteFormat.png);
    final bytes = data!.buffer.asUint64List();
    final file = File(widget.imageMapsPath!);
    file.deleteSync();
    await file.writeAsBytes(bytes, flush: false);
  }

  @override
  Widget build(BuildContext context) {
    return Builder(builder: (context) {
      if (widget.isCut) {
        return ListView(
          shrinkWrap: true,
          physics: const NeverScrollableScrollPhysics(),
          controller: ScrollController(),
          children: [
            SizedBox(
                height: 300,
                width: MediaQuery.of(context).size.width,
                child: CropImage(
                  controller: _cropController,
                  image: Image.file(File(widget.imageMapsPath!)),
                  paddingSize: 25.0,
                  alwaysMove: true,
                )),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceAround,
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                IconButton(
                  icon: const Icon(Icons.close),
                  onPressed: () {
                    _cropController.rotation = CropRotation.up;
                    _cropController.crop =
                        const Rect.fromLTRB(0.1, 0.1, 0.9, 0.9);
                    _cropController.aspectRatio = 1.0;
                  },
                ),
                IconButton(
                  icon: const Icon(Icons.rotate_90_degrees_ccw_outlined),
                  onPressed: _rotateImageToLeft,
                ),
                IconButton(
                  icon: const Icon(Icons.rotate_90_degrees_cw_outlined),
                  onPressed: _rotateImageToRight,
                ),
                TextButton(
                  onPressed: _finishedCrop,
                  child: const Text('Pronto'),
                ),
              ],
            )
          ],
        );
      }
      return Container(
          height: 300,
          width: MediaQuery.of(context).size.width,
          decoration: BoxDecoration(
            image: DecorationImage(image: imageCropped.image, fit: BoxFit.fill),
          ));
    });
  }
}
