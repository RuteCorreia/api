import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/core/usecase/usecase.dart';
import 'package:flytec/features/weather/data/repositories/weather_reposity_impl.dart';
import 'package:flytec/features/weather/domain/entities/weather_entitie.dart';

import '../repositories/weather_repository.dart';

class GetCurrentWeather extends UseCase<WeatherEntityEntity, WeatherParams> {
  final WeatherRepositoryImpl? weatherRepositoryImpl;
  GetCurrentWeather({required this.weatherRepositoryImpl});

  @override
  Future<Either<Failure, WeatherEntityEntity>> call(
      WeatherParams? params) async {
    final response = await weatherRepositoryImpl!.getCurrentWeather(
        params: WeatherParams(lat: params!.lat, long: params.long));
    return response;
  }
}
