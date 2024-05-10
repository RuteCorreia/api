import 'package:flytec/core/utils/save_local_controller.dart';
import 'package:flytec/features/aeronave/data/datasources/remote_aeronave_data_source.dart';
import 'package:flytec/features/aeronave/data/repositories/aeronave_repository_impl.dart';
import 'package:flytec/features/aeronave/domain/repositories/aeronave_repository.dart';
import 'package:flytec/features/aeronave/domain/usecases/get_veiculante_usecase.dart';
import 'package:flytec/features/altura_voo/data/datasources/remote_altura_voo_data_source.dart';
import 'package:flytec/features/altura_voo/data/repositories/altura_voo_repository_impl.dart';
import 'package:flytec/features/altura_voo/domain/repositories/altura_voo_repository.dart';
import 'package:flytec/features/altura_voo/domain/usecases/get_altura_voo_usecase.dart';
import 'package:flytec/features/alvo_biologico/data/datasources/remote_alvo_biologico_data_source.dart';
import 'package:flytec/features/alvo_biologico/data/repositories/produto_repository_impl.dart';
import 'package:flytec/features/alvo_biologico/domain/repositories/produto_repository.dart';
import 'package:flytec/features/alvo_biologico/domain/usecases/get_alvo_biologico_usecase.dart';
import 'package:flytec/features/aplications/data/datasource/clientes_datasource.dart';
import 'package:flytec/features/aplications/data/datasource/remote_report_aplications_datasource.dart';
import 'package:flytec/features/aplications/data/repository/report_aplication_repository_impl.dart';
import 'package:flytec/features/aplications/domain/repository/report_aplication_repository.dart';
import 'package:flytec/features/aplications/domain/usecases/send_report_aplication_usecase.dart';
import 'package:flytec/features/aplications/services/clientes_service.dart';
import 'package:flytec/features/auth/data/datasources/remote_company_data_source.dart';
import 'package:flytec/features/auth/data/repositories/company_repository_impl.dart';
import 'package:flytec/features/auth/domain/repositories/company_repository.dart';
import 'package:flytec/features/auth/domain/usecases/authentication_usecase.dart';
import 'package:flytec/features/auth/domain/usecases/obtain_logo_company_usecase.dart';
import 'package:flytec/features/bulas/data/datasource/remote_bula_datasource.dart';
import 'package:flytec/features/bulas/data/repository/bula_repository_impl.dart';
import 'package:flytec/features/bulas/domains/repository/bula_repository.dart';
import 'package:flytec/features/bulas/domains/usecases/get_bula_usecase.dart';
import 'package:flytec/features/cultura/data/datasources/remote_piloto_data_source.dart';
import 'package:flytec/features/cultura/data/repositories/authentication_repository_impl.dart';
import 'package:flytec/features/cultura/domain/repositories/executor_repository.dart';
import 'package:flytec/features/cultura/domain/usecases/get_culturas_usecase.dart';
import 'package:flytec/features/engenheiros/data/datasources/remote_engenheiro_data_source.dart';
import 'package:flytec/features/engenheiros/data/repositories/engenheiro_repository_impl.dart';
import 'package:flytec/features/engenheiros/domain/repositories/engenheiro_repository.dart';
import 'package:flytec/features/engenheiros/domain/usecases/get_engenheiro_usecase.dart';
import 'package:flytec/features/equipamento/data/datasources/remote_equipamento_data_source.dart';
import 'package:flytec/features/equipamento/data/repositories/equipamento_repository_impl.dart';
import 'package:flytec/features/equipamento/domain/repositories/equipamento_repository.dart';
import 'package:flytec/features/equipamento/domain/usecases/get_equipamentos_usecase.dart';
import 'package:flytec/features/executor/data/datasources/remote_authentication_data_source.dart';
import 'package:flytec/features/executor/data/repositories/authentication_repository_impl.dart';
import 'package:flytec/features/executor/domain/repositories/executor_repository.dart';
import 'package:flytec/features/executor/domain/usecases/authentication_usecase.dart';
import 'package:flytec/features/piloto/data/datasources/remote_piloto_data_source.dart';
import 'package:flytec/features/piloto/data/repositories/authentication_repository_impl.dart';
import 'package:flytec/features/piloto/domain/repositories/executor_repository.dart';
import 'package:flytec/features/piloto/domain/usecases/get_piloto_usecase.dart';
import 'package:flytec/features/produto/data/datasources/remote_produtos_data_source.dart';
import 'package:flytec/features/produto/data/repositories/produto_repository_impl.dart';
import 'package:flytec/features/produto/domain/repositories/produto_repository.dart';
import 'package:flytec/features/produto/domain/usecases/get_produtos_usecase.dart';
import 'package:flytec/features/signature/data/datasources/remote_signture_datasource.dart';
import 'package:flytec/features/signature/data/repositories/signature_repository_impl.dart';
import 'package:flytec/features/signature/domain/repositories/signature_repository.dart';
import 'package:flytec/features/signature/domain/usecases/get_signature_usecase.dart';
import 'package:flytec/features/signature/domain/usecases/save_signature_usecase.dart';
import 'package:flytec/features/tipo_produto/data/datasources/remote_tipo_produto_data_source.dart';
import 'package:flytec/features/tipo_produto/data/repositories/equipamento_repository_impl.dart';
import 'package:flytec/features/tipo_produto/domain/repositories/tipoproduto_repository.dart';
import 'package:flytec/features/tipo_produto/domain/usecases/get_tipo_produtos_usecase.dart';
import 'package:flytec/features/veiculante/data/datasources/remote_veiculante_data_source.dart';
import 'package:flytec/features/veiculante/domain/repositories/produto_repository.dart';
import 'package:flytec/features/veiculante/domain/usecases/get_veiculante_usecase.dart';
import 'package:get_it/get_it.dart';
import 'package:http/http.dart' as http;
import 'package:internet_connection_checker/internet_connection_checker.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../../features/auth/data/datasources/remote_authentication_data_source.dart';
import '../../features/auth/data/repositories/authentication_repository_impl.dart';
import '../../features/auth/domain/repositories/authentication_repository.dart';
import '../../features/auth/presentation/blocs/authentication/authentication_bloc.dart';
import '../../features/auth/service/auth_service.dart';
import '../../features/veiculante/data/repositories/produto_repository_impl.dart';
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

  getIt.registerLazySingleton<RemoteCulturaDataSourceImpl>(
    () => RemoteCulturaDataSourceImpl(
      client: getIt(),
      netWorkInfoI: getIt(),
    ),
  );
  getIt.registerLazySingleton<RemoteProdutoDataSourceImpl>(
    () => RemoteProdutoDataSourceImpl(
      client: getIt(),
      netWorkInfoI: getIt(),
    ),
  );
  getIt.registerLazySingleton<RemoteSignatureDataSourceImpl>(
    () => RemoteSignatureDataSourceImpl(
      client: getIt(),
      netWorkInfoI: getIt(),
    ),
  );

  getIt.registerLazySingleton<RemoteAlvoBilogicoDataSourceImpl>(
    () => RemoteAlvoBilogicoDataSourceImpl(
      client: getIt(),
      netWorkInfoI: getIt(),
    ),
  );
  getIt.registerLazySingleton<RemoteVeiculanteDataSourceImpl>(
    () => RemoteVeiculanteDataSourceImpl(
      client: getIt(),
      netWorkInfoI: getIt(),
    ),
  );
  getIt.registerLazySingleton<RemoteEngenheiroDataSourceImpl>(
    () => RemoteEngenheiroDataSourceImpl(
      client: getIt(),
      netWorkInfoI: getIt(),
    ),
  );
  getIt.registerLazySingleton<RemoteAeroNaveDataSourceImpl>(
    () => RemoteAeroNaveDataSourceImpl(
      client: getIt(),
      netWorkInfoI: getIt(),
    ),
  );
  getIt.registerLazySingleton<RemoteEquipamentoDataSourceImpl>(
    () => RemoteEquipamentoDataSourceImpl(
      client: getIt(),
      netWorkInfoI: getIt(),
    ),
  );
  getIt.registerLazySingleton<RemoteBulaDataSourceImpl>(
    () => RemoteBulaDataSourceImpl(
      client: getIt(),
      netWorkInfoI: getIt(),
    ),
  );
  getIt.registerLazySingleton<RemoteTipoProdutoDataSourceImpl>(
    () => RemoteTipoProdutoDataSourceImpl(
      client: getIt(),
      netWorkInfoI: getIt(),
    ),
  );
  getIt.registerLazySingleton<RemoteAlturVooDataSourceImpl>(
    () => RemoteAlturVooDataSourceImpl(
      client: getIt(),
      netWorkInfoI: getIt(),
    ),
  );
  getIt.registerLazySingleton<RemoteReportAplicationsDatasource>(
    () => RemoteReportAplicationsDatasourceImpl(
      client: getIt(),
      netWorkInfoI: getIt(),
    ),
  );
  getIt.registerLazySingleton<RemoteCompanyDataSourceImpl>(
    () => RemoteCompanyDataSourceImpl(
      client: getIt(),
      netWorkInfoI: getIt(),
    ),
  );

  //REPOSITORES
  getIt.registerLazySingleton<ReportAplicationRepository>(() =>
      ReportAplicationRepositoryImpl(
          remoteReportAplicationsDatasource: getIt()));
  getIt.registerLazySingleton<IAuthenticationRepository>(() =>
      AuthenticationRepositoryImpl(
          remoteAuthenticationDataSourceImpl: getIt()));
  getIt.registerLazySingleton<IExecutorRepository>(
      () => ExecutorRepositoryImpl(remoteExecutorDataSourceImpl: getIt()));
  getIt.registerLazySingleton<IPilotoRepository>(
      () => PilotoRepositoryImpl(remotePilotoDataSourceImpl: getIt()));

  getIt.registerLazySingleton<ICulturaRepository>(
    () => CulturaRepositoryImpl(remoteCulturaDataSourceImpl: getIt()),
  );
  getIt.registerLazySingleton<IProdutoRepository>(
    () => ProdutoRepositoryImpl(remoteProdutoDataSourceImpl: getIt()),
  );
  getIt.registerLazySingleton<SignatureRepository>(
    () => SignatureRepositoryImpl(remoteSignatureDataSourceImpl: getIt()),
  );
  getIt.registerLazySingleton<IAlvoBiologicoRepository>(
    () =>
        AlvoBiologicoRepositoryImpl(remoteAlvoBilogicoDataSourceImpl: getIt()),
  );
  getIt.registerLazySingleton<IEngenheiroRepository>(
    () => EngenheiroRepositoryImpl(remoteEngenheiroDataSourceImpl: getIt()),
  );
  getIt.registerLazySingleton<IVeiculanteRepository>(
    () => VeiculanteRepositoryImpl(remoteVeiculanteDataSourceImpl: getIt()),
  );
  getIt.registerLazySingleton<IAeroNaveRepository>(
    () => AeroNaveRepositoryImpl(remoteAeroNaveRepository: getIt()),
  );
  getIt.registerLazySingleton<IEquipamentoRepository>(
    () => EquipamentoRepositoryImpl(datasource: getIt()),
  );
  getIt.registerLazySingleton<ITipoProdutoRepository>(
    () => TipoProdutoRepositoryImpl(datasource: getIt()),
  );
  getIt.registerLazySingleton<IBulaRepository>(
    () => BulaRepositoryImpl(remoteBulaDataSourceImpl: getIt()),
  );
  getIt.registerLazySingleton<IAlturaVooRepository>(
    () => AlturaVooRepositoryImpl(datasource: getIt()),
  );
  getIt.registerLazySingleton<ICompanyRepository>(
    () => CompanyRepositoryImpl(remoteCompanyDataSourceImpl: getIt()),
  );
  // UseCases
  getIt.registerLazySingleton(() => SendReportAplicationUseCase(getIt()));
  getIt.registerLazySingleton<AuthenticateUseCase>(
      () => AuthenticateUseCase(getIt()));
  getIt.registerLazySingleton<GetExecutoresUseCase>(
      () => GetExecutoresUseCase(getIt()));
  getIt.registerLazySingleton<GetPilotosUseCase>(
      () => GetPilotosUseCase(getIt()));
  getIt.registerLazySingleton<GetEngenheiroUseCase>(
      () => GetEngenheiroUseCase(getIt()));
  getIt.registerLazySingleton<GetCulturasUseCase>(
      () => GetCulturasUseCase(iCulturaRepository: getIt()));
  getIt.registerLazySingleton(
      () => SaveLocalDataController(preferences: getIt()));
  getIt.registerLazySingleton(() => GetProdutosUseCase(getIt()));
  getIt.registerLazySingleton(() => GetSignatureUseCase(getIt()));
  getIt.registerLazySingleton(() => SaveSignatureUseCase(getIt()));
  getIt.registerLazySingleton(() => GetAlvoBiologicoUseCase(getIt()));
  getIt.registerLazySingleton(() => GetVeiculanteUseCase(getIt()));
  getIt.registerLazySingleton(() => GetAeroNaveUseCase(getIt()));
  getIt.registerLazySingleton(() => GetEquipamentoUseCase(getIt()));
  getIt.registerLazySingleton(() => GetBulasUseCase(getIt()));
  getIt.registerLazySingleton(() => GetTipoProdutosUseCase(getIt()));
  getIt.registerLazySingleton(() => GetAlturaVooUseCase(getIt()));
  getIt.registerLazySingleton(() => ObtainLogoCompanyUseCase(getIt()));
}
