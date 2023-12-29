import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:flytec/features/home/models/weather.dart';
import 'package:geolocator/geolocator.dart';
import 'package:geocoding/geocoding.dart';

class WeatherController {
  final String _baseURLApi = 'http://api.weatherapi.com/v1';
  final String _tokenWeatherApi = 'b8f51c06dc7c4a2f817120627232912';

  Future<String> getCurrentCountry() async {
    try {
      final hasPermission = await Geolocator.checkPermission();
      if (hasPermission == LocationPermission.deniedForever) throw Exception();
      if (hasPermission == LocationPermission.denied) {
        await Geolocator.requestPermission();
      }
      Position position = await Geolocator.getCurrentPosition(
          desiredAccuracy: LocationAccuracy.high);
      List<Placemark> places =
          await placemarkFromCoordinates(position.latitude, position.longitude);
      if (places.isNotEmpty) {
        return places.first.administrativeArea!;
      }
      return '';
    } catch (e) {
      return '';
    }
  }

  Future<Weather?>? getCurrentWeatherByCountry(String country) async {
    if (country.isEmpty) return null;
    try {
      final httpResponse = await http.get(Uri.parse(
          '$_baseURLApi/current.json?key=$_tokenWeatherApi&q=$country&aqi=no'));
      if (httpResponse.statusCode == 200) {
        return Weather.fromJson(jsonDecode(httpResponse.body));
      }
      return null;
    } catch (e) {
      return null;
    }
  }
}
