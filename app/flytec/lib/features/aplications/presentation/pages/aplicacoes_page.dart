import 'package:flutter/material.dart';
import 'package:flytec/features/aplications/presentation/widgets/speed_wind_select.dart';
import 'package:flytec/features/aplications/presentation/widgets/relative_humidity_select.dart';
import 'package:flytec/features/aplications/presentation/widgets/temperature_select.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/presentation/widgets/image_selected.dart';
import 'package:go_router/go_router.dart';

import '../../../auth/presentation/widgets/custom_login_button.dart';
import 'my_activity_page.dart';
import 'relatorio_aplicacao_page.dart';

class AplicacoesPage extends StatefulWidget {
  const AplicacoesPage({super.key});

  @override
  State<AplicacoesPage> createState() => _AplicacoesPageState();
}

class _AplicacoesPageState extends State<AplicacoesPage> {
  String _speedWindInitial = 'Selecione';
  String _speedWindFinal = 'Selecione';
  String _temperatureSelectedInitial = "20.0°C";
  String _temperatureSelectedFinal = "20.0°C";
  String _imageMapsPath = "";

  String _humiditySelectedInitial = 'Selecione';

  String _humiditySelectedFinal = 'Selecione';
  bool isCut = true;
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
        title: const Text(
          "Aplicações",
          textAlign: TextAlign.center,
          style: TextStyle(
            fontSize: 15,
          ),
        ),
      ),
      body: Padding(
        padding: const EdgeInsets.all(8.0),
        child: SingleChildScrollView(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const SizedBox(height: 16),
              const CustomText(text: 'Data da aplicação'),
              const SizedBox(height: 14),
              const SizedBox(
                  width: double.infinity,
                  child: ComboBox(selectedName: "Selecione")),
              const SizedBox(height: 20),
              const CustomText(text: 'Horário de início'),
              const SizedBox(height: 14),
              const SizedBox(
                width: double.infinity,
                child: ComboBox(selectedName: "Selecione"),
              ),
              const SizedBox(height: 14),
              const CustomText(text: 'Horímetro inicial'),
              const SizedBox(height: 14),
              const CustomTextField(),
              const SizedBox(height: 14),
              const CustomText(text: 'Horário de término'),
              const SizedBox(height: 14),
              const SizedBox(
                  width: double.infinity,
                  child: ComboBox(selectedName: "Selecione")),
              const SizedBox(height: 20),
              const CustomText(text: "Horímetro final"),
              const SizedBox(height: 14),
              const CustomTextField(),
              const SizedBox(height: 20),
              InkWell(
                  onTap: () async {
                    _imageMapsPath = await Util.obtainImagePathMaps(context);
                    setState(() {});
                  },
                  child: const UploadButton()),
              _imageMapsPath.isNotEmpty
                  ? Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        const SizedBox(height: 15),
                        const CustomText(
                            text: 'Imagem do Receituário Agronômico'),
                        const SizedBox(height: 15),
                        ImageSelected(
                          imageMapsPath: _imageMapsPath,
                          isCut: isCut,
                          onCutImage: (cut) {
                            isCut = cut;
                            setState(() {});
                          },
                        )
                      ],
                    )
                  : const SizedBox.shrink(),
              const SizedBox(height: 24),
              const Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [],
              ),
              const Center(
                child: Text(
                  'Condições climáticas durante a aplicação',
                  textAlign: TextAlign.center,
                  style: TextStyle(
                    color: Color(0xFF151515),
                    fontSize: 14,
                    fontFamily: 'Inter',
                    fontWeight: FontWeight.w600,
                    height: 0.11,
                  ),
                ),
              ),
              const SizedBox(height: 40),
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Column(
                    children: [
                      const CustomText(text: "INICIAL"),
                      Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          const SizedBox(height: 40),
                          const CustomText(text: "Temperatura (°C)"),
                          const SizedBox(height: 12),
                          InkWell(
                              onTap: () async {
                                await showDialog(
                                    context: context,
                                    builder: (BuildContext context) {
                                      return AlertDialog(
                                          backgroundColor: Colors.grey[100],
                                          content: TemperatureSelect(
                                              onChangedTemperature: (value) {
                                            setState(() {
                                              _temperatureSelectedInitial =
                                                  value;
                                            });
                                          }));
                                    });
                              },
                              child: ComboBox(
                                  selectedName: _temperatureSelectedInitial))
                        ],
                      ),
                    ],
                  ),
                  Column(
                    children: [
                      const CustomText(text: "FINAL"),
                      Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          const SizedBox(height: 40),
                          const CustomText(text: "Temperatura (°C)"),
                          const SizedBox(height: 12),
                          InkWell(
                              onTap: () async {
                                await showDialog(
                                    context: context,
                                    builder: (BuildContext context) {
                                      return AlertDialog(
                                          backgroundColor: Colors.grey[100],
                                          content: TemperatureSelect(
                                              onChangedTemperature: (value) {
                                            setState(() {
                                              _temperatureSelectedFinal = value;
                                            });
                                          }));
                                    });
                              },
                              child: ComboBox(
                                  selectedName: _temperatureSelectedFinal))
                        ],
                      ),
                    ],
                  ),
                ],
              ),
              const SizedBox(height: 30),
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      const CustomText(text: "U.R do ar (%)"),
                      const SizedBox(height: 12),
                      InkWell(
                          onTap: () async {
                            await showDialog(
                                context: context,
                                builder: (BuildContext context) {
                                  return AlertDialog(
                                      backgroundColor: Colors.grey[100],
                                      content: RelativeHumiditySelect(
                                          onChangedHumidity: (value) {
                                        setState(() {
                                          _humiditySelectedInitial = value;
                                        });
                                      }));
                                });
                          },
                          child:
                              ComboBox(selectedName: _humiditySelectedInitial))
                    ],
                  ),
                  Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      const CustomText(text: "U.R do ar (%)"),
                      const SizedBox(height: 12),
                      InkWell(
                          onTap: () async {
                            await showDialog(
                                context: context,
                                builder: (BuildContext context) {
                                  return AlertDialog(
                                      backgroundColor: Colors.grey[100],
                                      content: RelativeHumiditySelect(
                                          onChangedHumidity: (value) {
                                        setState(() {
                                          _humiditySelectedFinal = value;
                                        });
                                      }));
                                });
                          },
                          child: ComboBox(selectedName: _humiditySelectedFinal))
                    ],
                  )
                ],
              ),
              const SizedBox(height: 30),
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      const CustomText(text: "Vento (km/h)"),
                      const SizedBox(height: 12),
                      InkWell(
                          onTap: () async {
                            await showDialog(
                                context: context,
                                builder: (BuildContext context) {
                                  return AlertDialog(
                                      backgroundColor: Colors.grey[100],
                                      content: SpeedWindSelect(
                                          onChangedSpeedWind: (value) {
                                        setState(() {
                                          _speedWindInitial = value;
                                        });
                                      }));
                                });
                          },
                          child: ComboBox(selectedName: _speedWindInitial))
                    ],
                  ),
                  Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      const CustomText(text: "Vento (km/h)"),
                      const SizedBox(height: 12),
                      InkWell(
                          onTap: () async {
                            await showDialog(
                                context: context,
                                builder: (BuildContext context) {
                                  return AlertDialog(
                                      backgroundColor: Colors.grey[100],
                                      content: SpeedWindSelect(
                                          onChangedSpeedWind: (value) {
                                        setState(() {
                                          _speedWindFinal = value;
                                        });
                                      }));
                                });
                          },
                          child: ComboBox(selectedName: _speedWindFinal))
                    ],
                  )
                ],
              ),
              const SizedBox(height: 20),
              Center(
                child: CustomButton(
                  title: "OK",
                  onClick: () {
                    context.pop();
                  },
                ),
              ),
              const SizedBox(height: 20),
            ],
          ),
        ),
      ),
    );
  }
}

class UploadButton extends StatelessWidget {
  const UploadButton({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 60,
      color: const Color(0xFFECEAEA),
      padding: const EdgeInsets.symmetric(horizontal: 20, vertical: 10),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          const Icon(Icons.close),
          const SizedBox(width: 10),
          Container(
            width: 35,
            height: 35,
            decoration: BoxDecoration(
                color: Colors.blue, borderRadius: BorderRadius.circular(10)),
            child: const Icon(
              Icons.photo_camera,
              size: 20,
              color: Colors.white,
            ),
          ),
          const SizedBox(width: 8),
          const Text(
            'Imagens/Print condições\nclimáticas',
            style: TextStyle(
              color: Color(0xFF151515),
              fontSize: 13,
              fontFamily: 'Inter',
              fontWeight: FontWeight.w600,
            ),
          ),
          const Icon(Icons.arrow_forward_ios_sharp)
        ],
      ),
    );
  }
}
