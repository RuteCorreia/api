import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/failures.dart';

import '../entities/weather_entitie.dart';

abstract class IWeatherRepositoty {
  Future<Either<Failure, WeatherEntityEntity>> getCurrentWeather({
    required WeatherParams? params,
  });
}

final class WeatherParams {
  final double? lat;
  final double? long;
  WeatherParams({required this.lat, required this.long});
}
