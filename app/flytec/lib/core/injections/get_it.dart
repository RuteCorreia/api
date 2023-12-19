import 'package:flytec/core/utils/save_local_controller.dart';
import 'package:flytec/features/aplications/data/datasource/clientes_datasource.dart';
import 'package:flytec/features/aplications/services/clientes_service.dart';
import 'package:flytec/features/auth/domain/usecases/authentication_usecase.dart';
import 'package:flytec/features/cultura/data/datasources/remote_piloto_data_source.dart';
import 'package:flytec/features/cultura/data/repositories/authentication_repository_impl.dart';
import 'package:flytec/features/cultura/domain/repositories/executor_repository.dart';
import 'package:flytec/features/cultura/domain/usecases/get_culturas_usecase.dart';
import 'package:flytec/features/executor/data/datasources/remote_authentication_data_source.dart';
import 'package:flytec/features/executor/data/repositories/authentication_repository_impl.dart';
import 'package:flytec/features/executor/domain/repositories/executor_repository.dart';
import 'package:flytec/features/executor/domain/usecases/authentication_usecase.dart';
import 'package:flytec/features/piloto/data/datasources/remote_piloto_data_source.dart';
import 'package:flytec/features/piloto/data/repositories/authentication_repository_impl.dart';
import 'package:flytec/features/piloto/domain/repositories/executor_repository.dart';
import 'package:flytec/features/piloto/domain/usecases/get_piloto_usecase.dart';
import 'package:flytec/features/weather/data/dasources/weather_datasource.dart';
import 'package:flytec/features/weather/data/repositories/weather_reposity_impl.dart';
import 'package:flytec/features/weather/domain/usecases/get_current_weather_usecase.dart';
import 'package:get_it/get_it.dart';
import 'package:http/http.dart' as http;
import 'package:internet_connection_checker/internet_connection_checker.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../../features/auth/data/datasources/remote_authentication_data_source.dart';
import '../../features/auth/data/repositories/authentication_repository_impl.dart';
import '../../features/auth/domain/repositories/authentication_repository.dart';
import '../../features/auth/presentation/blocs/authentication/authentication_bloc.dart';
import '../../features/auth/service/auth_service.dart';
import '../network/network_info.dart';
import '../utils/global_config_vars.dart';

final getIt = GetIt.instance;

void setup() async {
  getIt.registerLazySingleton<ClienteDataSourceImpl>(
    () => ClienteDataSourceImpl(),
  );
  getIt.registerLazySingleton(() => GlobalConfigVars());

  //CACHES SERVICES
  getIt.registerLazySingleton(() => ClienteService());
  getIt.registerLazySingleton(() => AuthService());

  getIt.registerLazySingleton(() => AuthenticationBloc());

  getIt.registerLazySingleton<NetWorkInfoImpl>(
      () => NetWorkInfoImpl(connectionChecker: getIt()));

  //*EXTERNAL
  getIt.registerLazySingleton(() => http.Client());
  final sharedPreferences = await SharedPreferences.getInstance();
  getIt.registerSingletonAsync(() async => sharedPreferences);
  getIt.registerLazySingleton(() => InternetConnectionChecker());
  //DATSOURCES
  getIt.registerLazySingleton<RemoteAuthenticationDataSourceImpl>(
    () => RemoteAuthenticationDataSourceImpl(
      client: getIt(),
      netWorkInfoI: getIt(),
    ),
  );

  getIt.registerLazySingleton<RemoteExecutorDataSourceImpl>(
    () => RemoteExecutorDataSourceImpl(
      client: getIt(),
      netWorkInfoI: getIt(),
    ),
  );
  getIt.registerLazySingleton<RemotePilotoDataSourceImpl>(
    () => RemotePilotoDataSourceImpl(
      client: getIt(),
      netWorkInfoI: getIt(),
    ),
  );
  getIt.registerLazySingleton<WeatherDataSourceImpl>(
    () => WeatherDataSourceImpl(
      client: getIt(),
      netWorkInfoI: getIt(),
    ),
  );
  getIt.registerLazySingleton<RemoteCulturaDataSourceImpl>(
    () => RemoteCulturaDataSourceImpl(
      client: getIt(),
      netWorkInfoI: getIt(),
    ),
  );
  //REPOSITORES
  getIt.registerLazySingleton<IAuthenticationRepository>(() =>
      AuthenticationRepositoryImpl(
          remoteAuthenticationDataSourceImpl: getIt()));
  getIt.registerLazySingleton<IExecutorRepository>(
      () => ExecutorRepositoryImpl(remoteExecutorDataSourceImpl: getIt()));
  getIt.registerLazySingleton<IPilotoRepository>(
      () => PilotoRepositoryImpl(remotePilotoDataSourceImpl: getIt()));
  getIt.registerLazySingleton<WeatherRepositoryImpl>(
    () => WeatherRepositoryImpl(weatherDataSourceImpl: getIt()),
  );
  getIt.registerLazySingleton<ICulturaRepository>(
    () => CulturaRepositoryImpl(remoteCulturaDataSourceImpl: getIt()),
  );
  // UseCases
  getIt.registerLazySingleton<AuthenticateUseCase>(
      () => AuthenticateUseCase(getIt()));
  getIt.registerLazySingleton<GetExecutoresUseCase>(
      () => GetExecutoresUseCase(getIt()));
  getIt.registerLazySingleton<GetPilotosUseCase>(
      () => GetPilotosUseCase(getIt()));
  getIt.registerLazySingleton<GetCurrentWeather>(
      () => GetCurrentWeather(weatherRepositoryImpl: getIt()));
  getIt.registerLazySingleton<GetCulturasUseCase>(
      () => GetCulturasUseCase(iCulturaRepository: getIt()));
  getIt.registerLazySingleton(
      () => SaveLocalDataController(preferences: getIt()));
}
