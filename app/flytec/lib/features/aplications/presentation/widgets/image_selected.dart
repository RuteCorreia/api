import 'dart:io';
import 'dart:typed_data';
import 'package:crop_your_image/crop_your_image.dart';
import 'package:flutter/material.dart';
import 'package:flytec/features/aplications/presentation/pages/my_activity_page.dart';

class ImageSelected extends StatefulWidget {
  final String? imageMapsPath;
  final bool isCut;
  final Uint8List? imageData;
  final Function(bool cut, String path) onCutImage;
  const ImageSelected(
      {super.key,
      this.imageMapsPath,
      this.isCut = true,
      required this.imageData,
      required this.onCutImage});

  @override
  State<ImageSelected> createState() => _ImageSelectedState();
}

class _ImageSelectedState extends State<ImageSelected> {
  final _cropController = CropController();

  Future<void> _updateImageCropped(Uint8List newData) async {
    final file = File(widget.imageMapsPath!);
    file.deleteSync();
    await file.writeAsBytes(newData, flush: false);
    setState(() {});
  }


  @override
  Widget build(BuildContext context) {
    return Builder(builder: (context) {
      if (widget.isCut) {
        return Column(
          children: [
            SizedBox(
              height: MediaQuery.of(context).size.height * 0.45,
              width: MediaQuery.of(context).size.width,
              child: Crop(
                  image: widget.imageData!,
                  controller: _cropController,
                  onCropped: (image) async {
                    await _updateImageCropped(image);
                    widget.onCutImage(false, widget.imageMapsPath!);
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
        );

      }
      return Container(
          height: 300,
          width: MediaQuery.of(context).size.width,
          decoration: BoxDecoration(
            image: DecorationImage(
                image: FileImage(File(widget.imageMapsPath!)),
                fit: BoxFit.fill),
          ));
    });
  }
}
