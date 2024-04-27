import 'package:flutter/material.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';

class MarkerMaps {
  final Marker? marker;
  String observation;
  TextEditingController? controller;
  bool isExpanded;

  MarkerMaps({this.marker, this.observation = '', this.controller,this.isExpanded = false});

  void setIsExpanded(bool value) {
    isExpanded = value;
  }
}
