import 'package:flutter/material.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';

extension WidgetExtensions on Widget {
  Future<BitmapDescriptor> toBitmapDescriptor({Size? logicalSize, Size? imageSize, Duration waitToRender = const Duration(milliseconds: 300), TextDirection textDirection = TextDirection.ltr}) async {
    final widget = RepaintBoundary(
      child: MediaQuery(data: const MediaQueryData(), child: Directionality(textDirection: TextDirection.ltr, child: this)),
    );
    final pngBytes = await Util.createImageFromWidget(widget, waitToRender: waitToRender, logicalSize: logicalSize, imageSize: imageSize);
    return BitmapDescriptor.fromBytes(pngBytes);
  }
}
