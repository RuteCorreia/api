import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:flytec/features/aplications/controller/report_aplication_controller.dart';
import 'package:flytec/features/home/controller/weather_controller.dart';
import 'package:flytec/features/home/models/weather.dart';
import 'package:flytec/features/home/presentation/widgets/assinatura_select.dart';
import 'package:flytec/features/home/presentation/widgets/custom_button_drawer.dart';
import 'package:flytec/features/home/presentation/widgets/custom_drawer_button.dart';
import 'package:go_router/go_router.dart';

import '../widgets/custom_action_button.dart';
import '../widgets/custom_activity_button.dart';
import '../widgets/custom_dialog_button.dart';
import '../widgets/welcome_text.dart';

class HomePaga extends StatefulWidget {
  const HomePaga({super.key});

  @override
  State<HomePaga> createState() => _HomePagaState();
}

class _HomePagaState extends State<HomePaga> {
  final GlobalKey<ScaffoldState> _scaffoldKey = GlobalKey<ScaffoldState>();

  void _openDrawer() {
    _scaffoldKey.currentState?.openDrawer();
  }

  final WeatherController _weatherController = WeatherController();
  Weather? _weatherCurrent;
  bool _loadingObtainWeatherCurrent = true;

  Future<void> obtainWeatherCurrent() async {
    _weatherCurrent = null;
    String country = await _weatherController.getCurrentCountry();

    if (country.isEmpty) {
      country = await _weatherController.getCurrentCountry();
    }

    _weatherCurrent =
        await _weatherController.getCurrentWeatherByCountry(country);
    _loadingObtainWeatherCurrent = false;
    setState(() {});
  }

  ReportAplicationController? _reportAplicationController;

  void _updateView() {
    setState(() {});
  }

  Future<void> _initializationAplicationsReports() async {
    await _reportAplicationController?.obtainReportsAplications();
    setState(() {});
  }

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) async {
      obtainWeatherCurrent();
      _reportAplicationController =
          ReportAplicationController(updateView: _updateView);
      await _initializationAplicationsReports();
    });
  }

  Uint8List? _signature;
  Future<void> _createSignature(Uint8List? signature) async {
    _signature = signature;
    setState(() {});
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      key: _scaffoldKey,
      drawer: SafeArea(
        child: Drawer(
          backgroundColor: Colors.white,
          shape: const RoundedRectangleBorder(
            borderRadius: BorderRadius.only(
              topRight: Radius.circular(10.0),
              bottomRight: Radius.circular(10.0),
            ),
          ),
          width: MediaQuery.of(context).size.width * 0.7,
          child: Padding(
            padding: const EdgeInsets.all(8.0),
            child: ListView(
              children: <Widget>[
                DrawerHeader(
                    child: Center(
                        child: Image.asset("assets/images/logotipo.png",
                            height: 200))),
                CustomDrawerButton(
                  icon: Icons.edit,
                  text: "Cadastrar Assinatura",
                  onClick: () async {
                    context.pop();
                    if (_signature != null) {
                      // ignore: use_build_context_synchronously
                      await showDialog(
                          context: context,
                          useSafeArea: true,
                          builder: (BuildContext context) {
                            return AlertDialog(
                                scrollable: true,
                                backgroundColor: const Color(0xFFF5F5F5),
                                content: AssinaturaSelect(
                                  updateSignature: _createSignature,
                                  assinatura: _signature,
                                ));
                          });
                      return;
                    }
                    // ignore: use_build_context_synchronously
                    await context.push('/addsignature', extra: {
                      'onUpdateSignature': _createSignature,
                    });
                  },
                ),
              ],
            ),
          ),
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
                          color: Color.fromARGB(255, 121, 118, 118),
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
            const SizedBox(height: 16),
            GestureDetector(
              onTap: () async {},
              child: const Padding(
                padding: EdgeInsets.symmetric(horizontal: 16.0),
                child: WelcomeText(
                  userName: "Rodrigo",
                ),
              ),
            ),
            Builder(
              builder: (context) {
                if (_loadingObtainWeatherCurrent) {
                  return Container(
                    width: double.infinity,
                    height: 200,
                    margin:
                        const EdgeInsets.symmetric(horizontal: 18, vertical: 5),
                    padding:
                        const EdgeInsets.symmetric(horizontal: 8, vertical: 16),
                    clipBehavior: Clip.antiAlias,
                    decoration: ShapeDecoration(
                      shape: RoundedRectangleBorder(
                        side: const BorderSide(
                            width: 2, color: Color(0xFF00B45D)),
                        borderRadius: BorderRadius.circular(8),
                      ),
                    ),
                    child: const Center(child: CircularProgressIndicator()),
                  );
                }
                if (_weatherCurrent == null) {
                  return Container(
                    width: double.infinity,
                    height: 200,
                    margin:
                        const EdgeInsets.symmetric(horizontal: 18, vertical: 5),
                    padding:
                        const EdgeInsets.symmetric(horizontal: 8, vertical: 16),
                    clipBehavior: Clip.antiAlias,
                    decoration: ShapeDecoration(
                      shape: RoundedRectangleBorder(
                        side: const BorderSide(
                            width: 2, color: Color(0xFF00B45D)),
                        borderRadius: BorderRadius.circular(8),
                      ),
                    ),
                    child: InkWell(
                        onTap: () async {
                          _loadingObtainWeatherCurrent = true;
                          setState(() {});
                          await obtainWeatherCurrent();
                        },
                        child: const Column(
                          mainAxisAlignment: MainAxisAlignment.center,
                          children: [
                            Icon(
                              Icons.refresh,
                              size: 50,
                              color: Colors.green,
                            ),
                            SizedBox(height: 16),
                            Text(
                              "Clique para atualizar",
                              style: TextStyle(
                                color: Color.fromARGB(255, 121, 118, 118),
                                fontSize: 16,
                                fontFamily: 'Inter',
                                fontWeight: FontWeight.w700,
                                height: 0.09,
                              ),
                            ),
                          ],
                        )),
                  );
                }
                return Container(
                  width: double.infinity,
                  margin:
                      const EdgeInsets.symmetric(horizontal: 18, vertical: 5),
                  padding:
                      const EdgeInsets.symmetric(horizontal: 8, vertical: 16),
                  clipBehavior: Clip.antiAlias,
                  decoration: ShapeDecoration(
                    shape: RoundedRectangleBorder(
                      side:
                          const BorderSide(width: 2, color: Color(0xFF00B45D)),
                      borderRadius: BorderRadius.circular(8),
                    ),
                  ),
                  child: Column(
                    children: [
                      SizedBox(
                        width: double.infinity,
                        height: 80,
                        child: Row(
                          crossAxisAlignment: CrossAxisAlignment.center,
                          mainAxisAlignment: MainAxisAlignment.start,
                          children: [
                            SvgPicture.asset(
                                "assets/images/sun_foggy_fill.svg"),
                            const SizedBox(width: 16),
                            Column(
                              crossAxisAlignment: CrossAxisAlignment.start,
                              mainAxisAlignment: MainAxisAlignment.center,
                              children: [
                                Text(
                                  _weatherCurrent!
                                      .condition.translationConditionWeather,
                                  style: const TextStyle(
                                    color: Color.fromARGB(255, 121, 118, 118),
                                    fontSize: 16,
                                    fontFamily: 'Inter',
                                    fontWeight: FontWeight.w700,
                                  ),
                                ),
                                Text.rich(
                                  TextSpan(
                                    children: [
                                      TextSpan(
                                        text:
                                            '${_weatherCurrent!.tempC.toInt()}°',
                                        style: const TextStyle(
                                          color: Color(0xFF00B45D),
                                          fontSize: 32,
                                          fontFamily: 'Inter',
                                          fontWeight: FontWeight.w700,
                                        ),
                                      ),
                                      const TextSpan(
                                        text: 'c',
                                        style: TextStyle(
                                          color: Color(0xFF00B45D),
                                          fontSize: 20,
                                          fontFamily: 'Inter',
                                          fontWeight: FontWeight.w700,
                                        ),
                                      ),
                                    ],
                                  ),
                                ),
                              ],
                            ),
                          ],
                        ),
                      ),
                      const SizedBox(height: 10),
                      SizedBox(
                        width: double.infinity,
                        height: 100,
                        child: Row(
                          crossAxisAlignment: CrossAxisAlignment.center,
                          mainAxisAlignment: MainAxisAlignment.start,
                          children: [
                            SvgPicture.asset("assets/images/home_icon.svg"),
                            const SizedBox(width: 16),
                            Column(
                              crossAxisAlignment: CrossAxisAlignment.start,
                              mainAxisAlignment: MainAxisAlignment.center,
                              children: [
                                SizedBox(
                                  width:
                                      MediaQuery.of(context).size.width * 0.65,
                                  child: const Text(
                                    'Direção e velocidade do vento',
                                    maxLines: 2,
                                    style: TextStyle(
                                      color: Color.fromARGB(255, 121, 118, 118),
                                      fontSize: 16,
                                      fontFamily: 'Inter',
                                      fontWeight: FontWeight.w700,
                                    ),
                                  ),
                                ),
                                Text.rich(
                                  TextSpan(
                                    children: [
                                      TextSpan(
                                        text:
                                            _weatherCurrent!.windKph.toString(),
                                        style: const TextStyle(
                                          color: Color(0xFF00B45D),
                                          fontSize: 32,
                                          fontFamily: 'Inter',
                                          fontWeight: FontWeight.w700,
                                        ),
                                      ),
                                      const TextSpan(
                                        text: ' km/h',
                                        style: TextStyle(
                                          color: Color(0xFF00B45D),
                                          fontSize: 20,
                                          fontFamily: 'Inter',
                                          fontWeight: FontWeight.w700,
                                        ),
                                      ),
                                    ],
                                  ),
                                ),
                              ],
                            ),
                          ],
                        ),
                      ),
                    ],
                  ),
                );
              },
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
                      Builder(builder: (context) {
                        if (_reportAplicationController == null) {
                          return const SizedBox();
                        }
                        return Expanded(
                            child: ActivityButton(
                                text: "Aplicações",
                                value: _reportAplicationController!
                                    .listaAplicacao?.length
                                    .toString(),
                                onTap: () {}));
                      }),
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
                                    _reportAplicationController
                                        ?.setUpdateUpdateView(_updateView);
                                    context.push("/aplications", extra: {
                                      'reportAplicationController':
                                          _reportAplicationController
                                    });
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
            ),
            const SizedBox(height: 20),
          ],
        ),
      ),
    );
  }
}
