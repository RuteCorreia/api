import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/save_local_controller.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/presentation/pages/controllers/permission.dart';
import 'package:flytec/features/auth/data/models/user_payload_model.dart';
import 'package:flytec/features/cultura/data/models/cultura_model.dart';
import 'package:flytec/features/cultura/domain/usecases/get_culturas_usecase.dart';
import 'package:flytec/features/executor/data/models/excutores_model.dart';
import 'package:flytec/features/executor/domain/repositories/executor_repository.dart';
import 'package:flytec/features/executor/domain/usecases/authentication_usecase.dart';
import 'package:flytec/features/piloto/data/models/excutores_model.dart';
import 'package:flytec/features/piloto/domain/usecases/get_piloto_usecase.dart';
import 'package:flytec/features/weather/data/models/weather_model.dart';
import 'package:flytec/features/weather/domain/repositories/weather_repository.dart';
import 'package:flytec/features/weather/domain/usecases/get_current_weather_usecase.dart';
import 'package:go_router/go_router.dart';
import 'package:jwt_decoder/jwt_decoder.dart';
import 'package:location/location.dart' as lct;
import 'package:modal_progress_hud_nsn/modal_progress_hud_nsn.dart';

import '../../../aplications/data/datasource/clientes_datasource.dart';
import '../../service/auth_service.dart';
import '../blocs/authentication/authentication_bloc.dart';
import '../widgets/custom_auth_logo.dart';
import '../widgets/custom_login_button.dart';
import '../widgets/custom_recover_password_button.dart';
import '../widgets/custom_text_login.dart';

class LoginPage extends StatefulWidget {
  const LoginPage({super.key});

  @override
  State<LoginPage> createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {
  final TextEditingController _editingControllerEmail = TextEditingController();

  final TextEditingController _editingControllerPassword =
      TextEditingController();

  double currentLatitude = 0;
  double currentLongitude = 0;
  @override
  void initState() {
    super.initState();
    CheckPermissionLocation(
      context,
      () {
        lct.Location local = lct.Location();
        local.getLocation().then((lc) async {
          print("============LATITUDE=========== ${lc.latitude}");
          print("============LONGITUDE=========== ${lc.longitude}");
          setState(() {
            currentLatitude = lc.latitude!;
            currentLongitude = lc.longitude!;
          });
        });
      },
    ).getPermission();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: BlocConsumer<AuthenticationBloc, AuthenticationState>(
        bloc: getIt<AuthenticationBloc>(),
        listener: (context, state) {
          if (state is AuthenticationValidatorState) {
            Util.toastErro(state.message);
          }
          if (state is AuthenticationSuccessState) {
            Util.Token = state.authModel!.token!;
            getIt<GlobalConfigVars>().userPayload = UserPayloadModel.fromJson(
                JwtDecoder.decode(state.authModel!.token!));
            getIt<AuthService>().saveToken(state.authModel!.token!);
            print("AUTHENTICATION SUCCESS ${state.authModel!.token}");

            final response = Future.wait([
              getIt<ClienteDataSourceImpl>().getClients().then((value) {
                getIt<GlobalConfigVars>().setClientes(clientesData: value);
              }),
              getIt<GetExecutoresUseCase>().call(NoParams()).then((value) {
                value.fold((left) {
                  Util.toastErro("Ocorreu um erro ao buscar os executores");
                }, (right) {
                  final executores = right as List<ExecutorModel>;
                  getIt<GlobalConfigVars>()
                      .setExecutores(executoresData: executores);
                });
              }),
              getIt<GetPilotosUseCase>().call(NoParams()).then((value) {
                value.fold((left) {
                  Util.toastErro("Ocorreu um erro ao buscar os pilotos");
                }, (right) {
                  final pilotos = right as List<PilotoModel>;
                  getIt<GlobalConfigVars>().setPilotos(pilotosData: pilotos);
                });
              }),
              getIt<GetCurrentWeather>()
                  .call(WeatherParams(
                lat: currentLatitude,
                long: currentLongitude,
              ))
                  .then((value) {
                value.fold((l) {}, (r) {
                  final weatherData = r as WeatherModel;
                  getIt<GlobalConfigVars>()
                      .setWeatherData(weatherData: weatherData);
                });
              }),
              getIt<GetCulturasUseCase>().call(NoParams()).then((value) {
                value.fold((l) {}, (r) {
                  final culturas = r as List<CulturaModel>;
                  getIt<GlobalConfigVars>().setCulturas(culturaData: culturas);
                });
              })
            ]);

            response.whenComplete(() {
              var preloadData =
                  getIt<SaveLocalDataController>().initializeLocalData();
              getIt<SaveLocalDataController>()
                  .salvarLocalPreloadData(preloadData: preloadData)
                  .then((value) {
                print(
                  "SAVED PRELOAD CACHE  ${value.toString()} ",
                );
              });
              Future.delayed(const Duration(seconds: 1), () {
                context.push("/home");
              });
            });
          }
          if (state is AuthenticationError) {
            Util.toastErro(state.message);
          }
        },
        builder: (context, state) {
          return ModalProgressHUD(
            inAsyncCall: state is AuthenticationLoadingState,
            progressIndicator: const CircularProgressIndicator.adaptive(
              valueColor: AlwaysStoppedAnimation(Colors.green),
            ),
            child: Center(
              child: SingleChildScrollView(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.center,
                  children: [
                    const SizedBox(height: 90),
                    const CustomAuthLogo(),
                    const SizedBox(height: 22),
                    const CustomTextLogin(),
                    const SizedBox(height: 75),
                    Container(
                      width: 300,
                      height: 55,
                      padding: const EdgeInsets.symmetric(horizontal: 16),
                      decoration: ShapeDecoration(
                        shape: RoundedRectangleBorder(
                          side: const BorderSide(
                              width: 2, color: Color(0xFFD8D5D5)),
                          borderRadius: BorderRadius.circular(10),
                        ),
                      ),
                      child: TextField(
                        controller: _editingControllerEmail,
                        autofillHints: const [AutofillHints.email],
                        decoration: const InputDecoration(
                            hintText: "Email",
                            border: InputBorder.none,
                            hintStyle: TextStyle(
                              color: Color.fromARGB(255, 121, 118, 118),
                              fontSize: 16,
                              fontFamily: 'Inter',
                              fontWeight: FontWeight.w500,
                              height: 0.09,
                            )),
                      ),
                    ),
                    const SizedBox(height: 10),
                    Container(
                      width: 300,
                      height: 55,
                      padding: const EdgeInsets.symmetric(horizontal: 16),
                      decoration: ShapeDecoration(
                        shape: RoundedRectangleBorder(
                          side: const BorderSide(
                              width: 2, color: Color(0xFFD8D5D5)),
                          borderRadius: BorderRadius.circular(10),
                        ),
                      ),
                      child: TextField(
                        autofillHints: const [AutofillHints.password],
                        controller: _editingControllerPassword,
                        decoration: const InputDecoration(
                            hintText: "Senha",
                            border: InputBorder.none,
                            hintStyle: TextStyle(
                              color: Color.fromARGB(255, 121, 118, 118),
                              fontSize: 16,
                              fontFamily: 'Inter',
                              fontWeight: FontWeight.w500,
                              height: 0.09,
                            )),
                      ),
                    ),
                    CustomButton(
                      title: "Entrar",
                      onClick: () async {
                        getIt<AuthenticationBloc>().add(
                          LoginEvent(
                            password: _editingControllerPassword.text,
                            username: _editingControllerEmail.text,
                            context: context,
                          ),
                        );
                      },
                    ),
                    const SizedBox(height: 25),
                    RecoverPassWordButton(
                      onClick: () {},
                    )
                  ],
                ),
              ),
            ),
          );
        },
      ),
    );
  }
}
