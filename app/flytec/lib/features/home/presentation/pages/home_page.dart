import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/home/presentation/widgets/custom_button_drawer.dart';
import 'package:go_router/go_router.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../../../../core/injections/get_it.dart';
import '../widgets/custom_action_button.dart';
import '../widgets/custom_activity_button.dart';
import '../widgets/custom_dialog_button.dart';
import '../widgets/custom_drawer_button.dart';
import '../widgets/welcome_text.dart';

class HomePaga extends StatelessWidget {
  HomePaga({super.key});
  final GlobalKey<ScaffoldState> _scaffoldKey = GlobalKey<ScaffoldState>();

  void _openDrawer() {
    _scaffoldKey.currentState!.openDrawer();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      key: _scaffoldKey,
      drawer: Container(
        width: 255,
        height: 800,
        decoration: const ShapeDecoration(
          color: Colors.white,
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.only(
              topRight: Radius.circular(27),
              bottomRight: Radius.circular(27),
            ),
          ),
        ),
        child: ListView(
          children: [
            const SizedBox(height: 70),
            SizedBox(
              width: 200,
              height: 80,
              child: Row(
                mainAxisSize: MainAxisSize.min,
                mainAxisAlignment: MainAxisAlignment.start,
                crossAxisAlignment: CrossAxisAlignment.center,
                children: [
                  Container(
                    width: 49,
                    height: 49,
                    decoration: ShapeDecoration(
                      image: const DecorationImage(
                          image: AssetImage(
                        "assets/images/profile_icon.png",
                      )),
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(27),
                      ),
                    ),
                  ),
                  const SizedBox(width: 8),
                  Container(
                    child: const Column(
                      mainAxisSize: MainAxisSize.min,
                      mainAxisAlignment: MainAxisAlignment.spaceBetween,
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Text(
                          'Rodrigo Freitas',
                          style: TextStyle(
                            color: Color.fromARGB(255, 121, 118, 118),
                            fontSize: 14,
                            fontFamily: 'Inter',
                            fontWeight: FontWeight.w700,
                            height: 0.11,
                          ),
                        ),
                        SizedBox(height: 16),
                        Text(
                          'Editar perfil',
                          style: TextStyle(
                            color: Color.fromARGB(255, 121, 118, 118),
                            fontSize: 12,
                            fontFamily: 'Inter',
                            fontWeight: FontWeight.w400,
                            height: 0.12,
                          ),
                        )
                      ],
                    ),
                  ),
                  const Spacer(),
                  SizedBox(
                    width: 24,
                    height: 24,
                    child: Stack(
                      children: [
                        SvgPicture.asset(
                          "assets/images/arrow_right.svg",
                        )
                      ],
                    ),
                  ),
                ],
              ),
            ),
            const SizedBox(height: 50),
            CustomDrawerButton(
              imageUrl: "assets/images/remix_icon.svg",
              text: "Relatório Operacional",
              onClick: () {},
            ),
            const SizedBox(height: 32),
            CustomDrawerButton(
              imageUrl: "assets/images/map_icon.svg",
              text: "Minhas Atividades",
              onClick: () {},
            ),
            const SizedBox(height: 157),
            CustomDrawerButton(
              imageUrl: "assets/images/settings_icon.svg",
              text: "Configurações",
              onClick: () {},
            ),
            const SizedBox(height: 32),
            CustomDrawerButton(
              imageUrl: "assets/images/logout_icon.svg",
              text: "Sair",
              onClick: () async {
                context.pushReplacement("/login");

                SharedPreferences preferences =
                    await SharedPreferences.getInstance();
                await preferences.clear();
              },
            ),
          ],
        ),
      ),
      body: SingleChildScrollView(
        child: Column(
          children: [
            Container(
              width: 360,
              height: 103,
              padding: const EdgeInsets.only(
                top: 54,
                left: 16,
                right: 32,
                bottom: 32,
              ),
              clipBehavior: Clip.antiAlias,
              decoration: const BoxDecoration(color: Colors.white),
              child: Row(
                mainAxisSize: MainAxisSize.min,
                mainAxisAlignment: MainAxisAlignment.start,
                crossAxisAlignment: CrossAxisAlignment.center,
                children: [
                  CustomMenuButton(
                    onClick: () {
                      _openDrawer();
                    },
                  ),
                  const SizedBox(width: 16),
                  const Expanded(
                    child: SizedBox(
                      child: Text(
                        'Dashboard',
                        textAlign: TextAlign.center,
                        style: TextStyle(
                          color: Color.fromARGB(255, 12, 6, 6),
                          fontSize: 20,
                          fontFamily: 'Inter',
                          fontWeight: FontWeight.w600,
                          height: 0.07,
                        ),
                      ),
                    ),
                  ),
                ],
              ),
            ),
            const SizedBox(height: 26),
            GestureDetector(
              onTap: () async {
                print(getIt<GlobalConfigVars>().alturaVoo);
                print(getIt<GlobalConfigVars>().equipamentos);
                print(getIt<GlobalConfigVars>().tiposProdutos);
                //await getIt<GetAlturaVooUseCase>().call(NoParams());
              },
              child: WelcomeText(
                userName: "${getIt<GlobalConfigVars>().userPayload.name}",
              ),
            ),
            Container(
              width: double.infinity,
              height: 200,
              margin: const EdgeInsets.symmetric(horizontal: 18, vertical: 5),
              padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 16),
              clipBehavior: Clip.antiAlias,
              decoration: ShapeDecoration(
                shape: RoundedRectangleBorder(
                  side: const BorderSide(width: 2, color: Color(0xFF00B45D)),
                  borderRadius: BorderRadius.circular(8),
                ),
              ),
              child: Column(
                mainAxisSize: MainAxisSize.min,
                mainAxisAlignment: MainAxisAlignment.center,
                crossAxisAlignment: CrossAxisAlignment.center,
                children: [
                  SizedBox(
                    width: double.infinity,
                    height: 72,
                    child: Stack(
                      children: [
                        Positioned(
                          left: 0,
                          top: 19,
                          child: Container(
                            width: 29.89,
                            height: 32,
                            clipBehavior: Clip.antiAlias,
                            decoration: const BoxDecoration(),
                            child: Column(
                              mainAxisSize: MainAxisSize.min,
                              mainAxisAlignment: MainAxisAlignment.center,
                              crossAxisAlignment: CrossAxisAlignment.center,
                              children: [
                                SizedBox(
                                  width: 29.89,
                                  height: 32,
                                  child: Stack(children: [
                                    SvgPicture.asset(
                                        "assets/images/sun_foggy_fill.svg")
                                  ]),
                                ),
                              ],
                            ),
                          ),
                        ),
                        Positioned(
                          left: 44.84,
                          top: 0,
                          child: SizedBox(
                            width: 237.27,
                            height: 72,
                            child: Row(
                              mainAxisSize: MainAxisSize.min,
                              mainAxisAlignment: MainAxisAlignment.center,
                              crossAxisAlignment: CrossAxisAlignment.center,
                              children: [
                                Expanded(
                                  child: Container(
                                    child: Column(
                                      mainAxisSize: MainAxisSize.min,
                                      mainAxisAlignment:
                                          MainAxisAlignment.center,
                                      crossAxisAlignment:
                                          CrossAxisAlignment.center,
                                      children: [
                                        SizedBox(
                                          width: double.infinity,
                                          child: Text(
                                            '${getIt<GlobalConfigVars>().weather.weather![0].description}',
                                            style: const TextStyle(
                                              color: Color.fromARGB(
                                                  255, 121, 118, 118),
                                              fontSize: 16,
                                              fontFamily: 'Inter',
                                              fontWeight: FontWeight.w700,
                                              height: 0.09,
                                            ),
                                          ),
                                        ),
                                        const SizedBox(height: 30),
                                        SizedBox(
                                          width: double.infinity,
                                          child: Text.rich(
                                            TextSpan(
                                              children: [
                                                TextSpan(
                                                  text:
                                                      getIt<GlobalConfigVars>()
                                                          .weather
                                                          .main!
                                                          .temp!
                                                          .toStringAsFixed(0),
                                                  style: const TextStyle(
                                                    color: Color(0xFF00B45D),
                                                    fontSize: 32,
                                                    fontFamily: 'Inter',
                                                    fontWeight: FontWeight.w700,
                                                    height: 0.05,
                                                  ),
                                                ),
                                                const TextSpan(
                                                  text: '°C',
                                                  style: TextStyle(
                                                    color: Color(0xFF00B45D),
                                                    fontSize: 20,
                                                    fontFamily: 'Inter',
                                                    fontWeight: FontWeight.w700,
                                                    height: 0.07,
                                                  ),
                                                ),
                                              ],
                                            ),
                                          ),
                                        ),
                                      ],
                                    ),
                                  ),
                                ),
                              ],
                            ),
                          ),
                        ),
                        Positioned(
                          left: 289.58,
                          top: 24,
                          child: Container(
                            width: 22.42,
                            height: 24,
                            clipBehavior: Clip.antiAlias,
                            decoration: const BoxDecoration(),
                          ),
                        ),
                      ],
                    ),
                  ),
                  const SizedBox(height: 16),
                  SizedBox(
                    width: double.infinity,
                    height: 72,
                    child: Stack(
                      children: [
                        Positioned(
                          left: 0,
                          top: 19,
                          child: Container(
                            width: 29.89,
                            height: 32,
                            clipBehavior: Clip.antiAlias,
                            decoration: const BoxDecoration(),
                            child: Column(
                              mainAxisSize: MainAxisSize.min,
                              mainAxisAlignment: MainAxisAlignment.center,
                              crossAxisAlignment: CrossAxisAlignment.center,
                              children: [
                                SizedBox(
                                  width: 29.89,
                                  height: 32,
                                  child: Stack(children: [
                                    SvgPicture.asset(
                                        "assets/images/home_icon.svg")
                                  ]),
                                ),
                              ],
                            ),
                          ),
                        ),
                        Positioned(
                          left: 44.84,
                          top: 0,
                          child: SizedBox(
                            width: 237.27,
                            height: 72,
                            child: Row(
                              mainAxisSize: MainAxisSize.min,
                              mainAxisAlignment: MainAxisAlignment.center,
                              crossAxisAlignment: CrossAxisAlignment.center,
                              children: [
                                Expanded(
                                  child: Container(
                                    child: Column(
                                      mainAxisSize: MainAxisSize.min,
                                      mainAxisAlignment:
                                          MainAxisAlignment.center,
                                      crossAxisAlignment:
                                          CrossAxisAlignment.center,
                                      children: [
                                        const SizedBox(
                                          width: double.infinity,
                                          child: Text(
                                            'Direção e velocidade do vento',
                                            style: TextStyle(
                                              color: Color.fromARGB(
                                                  255, 121, 118, 118),
                                              fontSize: 16,
                                              fontFamily: 'Inter',
                                              fontWeight: FontWeight.w700,
                                              height: 0.09,
                                            ),
                                          ),
                                        ),
                                        const SizedBox(height: 30),
                                        SizedBox(
                                          width: double.infinity,
                                          child: Text.rich(
                                            TextSpan(
                                              children: [
                                                TextSpan(
                                                  text: Util.converterMetrosPorSegundoParaKmPorHora(
                                                          getIt<GlobalConfigVars>()
                                                              .weather
                                                              .wind!
                                                              .speed!)
                                                      .toStringAsFixed(0),
                                                  style: const TextStyle(
                                                    color: Color(0xFF00B45D),
                                                    fontSize: 32,
                                                    fontFamily: 'Inter',
                                                    fontWeight: FontWeight.w700,
                                                    height: 0.05,
                                                  ),
                                                ),
                                                const TextSpan(
                                                  text: ' km/h',
                                                  style: TextStyle(
                                                    color: Color(0xFF00B45D),
                                                    fontSize: 20,
                                                    fontFamily: 'Inter',
                                                    fontWeight: FontWeight.w700,
                                                    height: 0.07,
                                                  ),
                                                ),
                                              ],
                                            ),
                                          ),
                                        ),
                                      ],
                                    ),
                                  ),
                                ),
                              ],
                            ),
                          ),
                        ),
                        Positioned(
                          left: 289.58,
                          top: 24,
                          child: Container(
                            width: 22.42,
                            height: 24,
                            clipBehavior: Clip.antiAlias,
                            decoration: const BoxDecoration(),
                          ),
                        ),
                      ],
                    ),
                  ),
                ],
              ),
            ),
            const SizedBox(height: 20),
            Padding(
              padding: const EdgeInsets.symmetric(horizontal: 16),
              child: Column(
                children: [
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      Expanded(
                        child: ActivityButton(
                          text: "Minhas Atividades",
                          value: "0",
                          onTap: () {},
                        ),
                      ),
                      const SizedBox(width: 8),
                      Expanded(
                        child: ActivityButton(
                          text: "Relatórios",
                          value: "0",
                          onTap: () {},
                        ),
                      ),
                    ],
                  ),
                  const SizedBox(height: 22),
                  const SizedBox(
                    width: 328,
                    height: 25,
                    child: Text(
                      'Ações',
                      style: TextStyle(
                        color: Color.fromARGB(255, 121, 118, 118),
                        fontSize: 20,
                        fontFamily: 'Inter',
                        fontWeight: FontWeight.w700,
                        height: 0.07,
                      ),
                    ),
                  ),
                  CustomActionButton(
                    imageUrl: "assets/images/remix_icon_2.svg",
                    onClick: () {
                      context.push("/myactivity");
                    },
                    text: "Minhas Atividades",
                  ),
                  const SizedBox(height: 10),
                  CustomActionButton(
                    imageUrl: "assets/images/edit_icon.svg",
                    onClick: () {
                      showAdaptiveDialog<String>(
                        context: context,
                        useSafeArea: true,
                        builder: (BuildContext context) => AlertDialog.adaptive(
                          insetPadding: const EdgeInsets.all(32),
                          title: const SizedBox(
                            width: 244,
                            height: 30,
                            child: Text(
                              'Escolha o tipo de relatório',
                              textAlign: TextAlign.center,
                              style: TextStyle(
                                color: Color.fromARGB(255, 121, 118, 118),
                                fontSize: 16,
                                fontFamily: 'Inter',
                                fontWeight: FontWeight.w500,
                                height: 0.09,
                              ),
                            ),
                          ),
                          content: SizedBox(
                            height: 200,
                            child: Column(
                              children: [
                                CustomDialogButton(
                                  leftIcon:
                                      "assets/images/icomoon_free_fire.svg",
                                  text: "Aplicação",
                                  onClick: () {
                                    context.push("/aplications");
                                  },
                                ),
                                const SizedBox(height: 10),
                                CustomDialogButton(
                                  leftIcon: "assets/images/fire.svg",
                                  onClick: () {
                                    context.pop();
                                    context.push("/combateincendio",
                                        extra: "dd");
                                  },
                                  text: "Combate a incêndio",
                                )
                              ],
                            ),
                          ),
                          actions: const <Widget>[],
                        ),
                      );
                    },
                    text: "Relatório Operacional",
                  ),
                ],
              ),
            )
          ],
        ),
      ),
    );
  }
}
