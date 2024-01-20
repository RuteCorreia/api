import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/exception.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/features/weather/data/dasources/weather_datasource.dart';
import 'package:flytec/features/weather/domain/entities/weather_entitie.dart';
import 'package:flytec/features/weather/domain/repositories/weather_repository.dart';

class WeatherRepositoryImpl implements IWeatherRepositoty {
  final WeatherDataSourceImpl weatherDataSourceImpl;
  WeatherRepositoryImpl({required this.weatherDataSourceImpl});
  @override
  Future<Either<Failure, WeatherEntityEntity>> getCurrentWeather(
      {required WeatherParams? params}) async {
    try {
      final response = await weatherDataSourceImpl.getCurrentWeather(
          params: WeatherParams(lat: params!.lat, long: params.long));
      return Right(response);
    } on NetWorkException {
      return Left(NetWorkFailure());
    }
  }
}
