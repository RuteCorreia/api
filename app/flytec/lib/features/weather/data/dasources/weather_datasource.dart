import 'package:flytec/core/errors/exception.dart';
import 'package:flytec/features/weather/data/models/weather_model.dart';
import 'package:flytec/features/weather/domain/repositories/weather_repository.dart';
import 'package:http/http.dart' as http;

import '../../../../core/network/network_info.dart';

abstract interface class IWeatherDataSource {
  Future<WeatherModel> getCurrentWeather({required WeatherParams? params});
}

class WeatherDataSourceImpl extends IWeatherDataSource {
  final http.Client client;
  final NetWorkInfoImpl? netWorkInfoI;
  WeatherDataSourceImpl({required this.client, required this.netWorkInfoI});
  @override
  Future<WeatherModel> getCurrentWeather(
      {required WeatherParams? params}) async {
    if (await netWorkInfoI!.isConnected) {
      final response = await client.get(
        Uri.parse(
            "https://api.openweathermap.org/data/2.5/weather?lat=${params!.lat}&lon=${params.long}&appid=7967a0a2dc20316ef20b727b9342ed13&lang=pt_br&units=metric"),
      );

      print("WEATHER DATA: ${response.body}");

      if (response.statusCode == 200) {
        return Future.value(
          weatherModelFromJson(response.body),
        );
      } else {
        throw ServerException(message: "StatusCode: ${response.statusCode}");
      }
    } else {
      throw NetWorkException(message: "Sem conexão a internet");
    }
  }
}
