import 'dart:io';
import 'dart:typed_data';

import 'package:flutter/material.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/core/widgets/custom_text.dart';
import 'package:flytec/features/aplications/presentation/pages/contrato_page.dart';
import 'package:flytec/features/aplications/presentation/pages/steps/aplication_first_step.dart';
import 'package:flytec/features/aplications/presentation/widgets/image_selected.dart';
import 'package:flytec/features/aplications/presentation/widgets/relative_humidity_select.dart';
import 'package:flytec/features/aplications/presentation/widgets/speed_wind_select.dart';
import 'package:flytec/features/aplications/presentation/widgets/temperature_select.dart';
import 'package:flytec/features/auth/presentation/widgets/custom_login_button.dart';
import 'package:go_router/go_router.dart';
import 'package:intl/intl.dart';

class AplicacoesPage extends StatefulWidget {
  const AplicacoesPage({super.key});

  @override
  State<AplicacoesPage> createState() => _AplicacoesPageState();
}

class _AplicacoesPageState extends State<AplicacoesPage> {
  late DateTime? dataSelecionada = DateTime.now();
  late TimeOfDay? time = const TimeOfDay(hour: 12, minute: 43);
  late TimeOfDay? horimetro = const TimeOfDay(hour: 15, minute: 43);

  TimeOfDay _selectedTime = TimeOfDay.now();
  TimeOfDay _selectedTimeFinal = TimeOfDay.now();

  Future<void> _selectTime(BuildContext context) async {
    final TimeOfDay? picked = await showTimePicker(
      context: context,
      initialTime: _selectedTime,
    );

    if (picked != null && picked != _selectedTime) {
      setState(() {
        _selectedTime = picked;
      });
    }
  }

  Future<void> _selectTimeFinal(BuildContext context) async {
    final TimeOfDay? picked = await showTimePicker(
      context: context,
      initialTime: _selectedTime,
    );

    if (picked != null && picked != _selectedTimeFinal) {
      setState(() {
        _selectedTimeFinal = picked;
      });
    }
  }

  String _speedWindInitial = 'Selecione';
  String _speedWindFinal = 'Selecione';
  String _temperatureSelectedInitial = "20.0°C";
  String _temperatureSelectedFinal = "20.0°C";
  String _imageMapsPath = "";

  String _humiditySelectedInitial = '+ 55%';

  String _humiditySelectedFinal = '+ 55%';
  bool isCut = true;
  Uint8List? _imageData;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
        title: const Text(
          "Aplicações",
          textAlign: TextAlign.center,
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
              CustomCombo(
                selectedName: dataSelecionada == null
                    ? "Selecione"
                    : DateFormat('dd/MM/yyyy').format(dataSelecionada!),
                onTap: () async {
                  final data = await showDatePicker(
                    confirmText: "Selecionar data",
                    cancelText: "Cancelar",
                    helpText: "",
                    context: context,
                    //locale: const Locale("pt"),
                    initialDate: DateTime.now(),
                    firstDate: DateTime(2023),
                    lastDate: DateTime(2024),
                  );
                  setState(() {
                    dataSelecionada = data;
                  });
                },
              ),
              const SizedBox(height: 20),
              const CustomText(text: 'Horário de início'),
              const SizedBox(height: 14),
              CustomCombo(
                  selectedName: "${_selectedTime.hour}:${_selectedTime.minute}",
                  onTap: () {
                    _selectTime(context);
                  }),
              const SizedBox(height: 14),
              const CustomText(text: 'Horímetro inicial'),
              const SizedBox(height: 14),
              const CustomTextField(
                text: "Digite aqui",
                keyboardType: TextInputType.datetime,
              ),
              const SizedBox(height: 14),
              const CustomText(text: 'Horário de término'),
              const SizedBox(height: 14),
              CustomCombo(
                  selectedName:
                      "${_selectedTimeFinal.hour}:${_selectedTimeFinal.minute}",
                  onTap: () {
                    _selectTimeFinal(context);
                  }),
              const SizedBox(height: 20),
              const CustomText(text: "Horímetro final"),
              const SizedBox(height: 14),
              const CustomTextField(
                text: "Digite aqui",
                keyboardType: TextInputType.datetime,
              ),
              const SizedBox(height: 20),
              InkWell(
                  onTap: () async {
                    _imageMapsPath = await Util.obtainImagePathMaps(context);
                    _imageData = await File(_imageMapsPath).readAsBytes();
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
                          imageData: _imageData,
                          onCutImage: (cut, path) {
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
