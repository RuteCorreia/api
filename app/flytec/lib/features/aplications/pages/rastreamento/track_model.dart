import 'dart:convert';

import 'package:background_locator_2/location_dto.dart';
import 'package:flutter/foundation.dart';
import 'dart:math' as math;

class TrackModel {
  List<LocationDto>? _pontos = [];
  List<LocationDto>? get pontos => _pontos;
  TrackModel();

  TrackModel.fromJson(String json) {
    Map<String, dynamic>? ultimo;
    try {
      if (json.length > 1) {
        //tentar decodificar aquivo todo
        final ajson = jsonDecode('[${json.substring(0, json.length - 1)}]');

        _pontos = ajson.map<LocationDto>((e) {
          if (e.keys.contains("time")) {
            var difPrecision = 15 - "${e["time"]}".length;
            if (difPrecision > 0) {
              e["time"] = e["time"] * math.pow(10, difPrecision);
            }
          }

          if (!e.keys.contains("heading")) {
            e.addAll({"heading": 0.0});
          }
          if (!e.keys.contains("speed")) {
            e.addAll({"speed": 0.0});
          }
          if (!e.keys.contains("speed_accuracy")) {
            e.addAll({"speed_accuracy": 0.0});
          }
          if (!e.keys.contains("accuracy")) {
            e.addAll({"accuracy": 0.0});
          }
          ultimo = e;
          LocationDto loc = LocationDto.fromJson(e);
          return loc;
        }).toList();
      } else {
        _pontos = [];
      }
    } catch (e) {
      // decodificar um a um para tentar recuperar a atividade
      if (kDebugMode) {
        print("$e : $ultimo");
      }
      _pontos = [];
      json = json.replaceAll(',,', ',');
      var strpontos = json.substring(1, json.length - 1).split("},{");
      for (int i = 0; i < strpontos.length; i++) {
        String line = "";
        try {
          line = strpontos[i];
          if (line[0] != '{') {
            line = "{$line";
          }

          if (line[line.length - 1] != '}') {
            line = "$line}";
          }
          Map map = jsonDecode(line);
          if (map['provider'] == null) map['provider'] = '';
          if (map['is_mocked'] == null) map['is_mocked'] = 'false';
          LocationDto loc = LocationDto.fromJson(map);
          _pontos!.add(loc);
        } catch (e) {
          if (kDebugMode) {
            print("Localização corrompida: linha>$i >$line< $e");
          }
        }
      }
    }
  }
}
