import 'dart:io';

import 'package:flutter/material.dart';

class ImageSelected extends StatelessWidget {
  final String? imageMapsPath;
  const ImageSelected({super.key, this.imageMapsPath});

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 300,
      width: MediaQuery.of(context).size.width,
      decoration: BoxDecoration(
        image: DecorationImage(
            image: FileImage(
              File(imageMapsPath!),
            ),
            fit: BoxFit.fill),
      ),
    );
  }
}
