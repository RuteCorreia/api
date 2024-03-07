import 'dart:io';

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/presentation/pages/aplicacoes_page.dart';
import 'package:flytec/features/aplications_v2/components/components_exports.dart';
import 'package:flytec/features/aplications_v2/pages/images/upload_foto.dart';
import 'package:intl/intl.dart';

class CreateNewAplicacaoPages extends StatefulWidget {
  const CreateNewAplicacaoPages({super.key});

  @override
  State<CreateNewAplicacaoPages> createState() =>
      _CreateNewAplicacaoPagesState();
}

class _CreateNewAplicacaoPagesState extends State<CreateNewAplicacaoPages> {
  String _speedWindInitial = '0 km/h';
  String _speedWindFinal = '20 km/h';
  String _temperatureSelectedInitial = "20.0°C";
  String _temperatureSelectedFinal = "20.0°C";
  late DateTime? dataSelecionada = DateTime.now();
  late TimeOfDay? time = const TimeOfDay(hour: 12, minute: 43);
  late TimeOfDay? horimetro = const TimeOfDay(hour: 15, minute: 43);
  final TextEditingController _horimetroInicial = TextEditingController();
  final TextEditingController _horimetroFinal = TextEditingController();
  String _humiditySelectedInitial = '+ 55%';
  String _humiditySelectedFinal = '+ 55%';
  bool isCut = true;
  Uint8List? _imageData;
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

  Future<bool> _verifyFields() async {
    Util.toastSucesso("  Dados inseridos com sucesso");

    setState(() {});
    Navigator.pop(context);
    return true;
  }

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
                  final dataS = await showDatePicker(
                    confirmText: "Selecionar data",
                    cancelText: "Cancelar",
                    helpText: "",
                    context: context,
                    //locale: const Locale("pt"),
                    initialDate: DateTime.now(),
                    firstDate: DateTime(2024),
                    lastDate: DateTime(2028),
                  );
                  Util.closeKeyBoard();

                  setState(() {
                    dataSelecionada = dataS;
                  });
                },
              ),
              const SizedBox(height: 20),
              const CustomText(text: 'Horário de início'),
              const SizedBox(height: 14),
              CustomCombo(
                  selectedName: _selectedTime.to24hours(),
                  onTap: () {
                    _selectTime(context);

                    Util.closeKeyBoard();
                  }),
              const SizedBox(height: 14),
              const CustomText(text: 'Horímetro inicial'),
              const SizedBox(height: 14),
              Container(
                width: (MediaQuery.of(context).size.width / 2) - 25,
                height: 50,
                padding:
                    const EdgeInsets.symmetric(horizontal: 16, vertical: 0),
                decoration: ShapeDecoration(
                  shape: RoundedRectangleBorder(
                    side: const BorderSide(width: 1, color: Color(0xFF636363)),
                    borderRadius: BorderRadius.circular(10),
                  ),
                ),
                child: TextField(
                  controller: _horimetroInicial,
                  onChanged: (value) {},
                  inputFormatters: [
                    // obrigatório
                    FilteringTextInputFormatter.digitsOnly,
                    CustomNumberFormatter()
                  ],
                  keyboardType: TextInputType.datetime,
                  decoration: const InputDecoration(
                      hintText: "Digite aqui",
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
              const SizedBox(height: 14),
              const CustomText(text: 'Horário de término'),
              const SizedBox(height: 14),
              CustomCombo(
                  selectedName: _selectedTimeFinal.to24hours(),
                  onTap: () {
                    _selectTimeFinal(context);

                    Util.closeKeyBoard();
                  }),
              const SizedBox(height: 20),
              const CustomText(text: "Horímetro final"),
              const SizedBox(height: 14),
              Container(
                width: (MediaQuery.of(context).size.width / 2) - 25,
                height: 50,
                padding:
                    const EdgeInsets.symmetric(horizontal: 16, vertical: 0),
                decoration: ShapeDecoration(
                  shape: RoundedRectangleBorder(
                    side: const BorderSide(width: 1, color: Color(0xFF636363)),
                    borderRadius: BorderRadius.circular(10),
                  ),
                ),
                child: TextField(
                  controller: _horimetroFinal,
                  onChanged: (value) {},
                  inputFormatters: [
                    // obrigatório
                    FilteringTextInputFormatter.digitsOnly,
                    CustomNumberFormatter()
                  ],
                  keyboardType: TextInputType.datetime,
                  decoration: const InputDecoration(
                      hintText: "Digite aqui",
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
              const SizedBox(height: 20),
              InkWell(
                  onTap: () async {
                    final imageMapsPath =
                        await Util.obtainImagePathMaps(context);
                    _imageData = await File(imageMapsPath).readAsBytes();
                    Navigator.push(
                        // ignore: use_build_context_synchronously
                        context,
                        MaterialPageRoute(
                            builder: (context) => UploadFotos(
                                  updateImageData: (data) {
                                    _imageData = data;
                                    setState(() {});
                                  },
                                  imageData: _imageData,
                                  onOkButton: () {
                                    Navigator.pop(context);
                                  },
                                )));
                  },
                  child: const UploadButton()),
              _imageData != null
                  ? Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        const SizedBox(height: 15),
                        const CustomText(
                            text: 'Imagem do Receituário Agronômico'),
                        const SizedBox(height: 15),
                        Container(
                            height: 300,
                            width: MediaQuery.of(context).size.width,
                            decoration: BoxDecoration(
                              image: DecorationImage(
                                  image: MemoryImage(_imageData!),
                                  fit: BoxFit.fill),
                            ))
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
                                Util.closeKeyBoard();
                                await showDialog(
                                    context: context,
                                    builder: (BuildContext context) {
                                      return AlertDialog(
                                          backgroundColor: Colors.grey[100],
                                          content: SizedBox(
                                            width: double.maxFinite,
                                            child: TemperatureSelect(
                                                scrollTheList: true,
                                                scrollToIndex: 19,
                                                onChangedTemperature: (value) {
                                                  setState(() {
                                                    _temperatureSelectedInitial =
                                                        value;
                                                  });
                                                  Util.closeKeyBoard();
                                                }),
                                          ));
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
                                Util.closeKeyBoard();
                                await showDialog(
                                    context: context,
                                    builder: (BuildContext context) {
                                      return AlertDialog(
                                          backgroundColor: Colors.grey[100],
                                          content: SizedBox(
                                            width: double.maxFinite,
                                            child: TemperatureSelect(
                                                scrollTheList: true,
                                                scrollToIndex: 19,
                                                onChangedTemperature: (value) {
                                                  setState(() {
                                                    _temperatureSelectedFinal =
                                                        value;
                                                  });
                                                  Util.closeKeyBoard();
                                                }),
                                          ));
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
                            Util.closeKeyBoard();
                            await showDialog(
                                context: context,
                                builder: (BuildContext context) {
                                  return AlertDialog(
                                      backgroundColor: Colors.grey[100],
                                      content: SizedBox(
                                        width: double.maxFinite,
                                        child: RelativeHumiditySelect(
                                            scrollTheList: true,
                                            scrollToIndex: 54,
                                            onChangedHumidity: (value) {
                                              setState(() {
                                                _humiditySelectedInitial =
                                                    value;
                                              });
                                              Util.closeKeyBoard();
                                            }),
                                      ));
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
                            Util.closeKeyBoard();
                            await showDialog(
                                context: context,
                                builder: (BuildContext context) {
                                  return AlertDialog(
                                      backgroundColor: Colors.grey[100],
                                      content: SizedBox(
                                        width: double.maxFinite,
                                        child: RelativeHumiditySelect(
                                            scrollTheList: true,
                                            scrollToIndex: 54,
                                            onChangedHumidity: (value) {
                                              setState(() {
                                                _humiditySelectedFinal = value;
                                              });
                                              Util.closeKeyBoard();
                                            }),
                                      ));
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
                            Util.closeKeyBoard();
                            await showDialog(
                                context: context,
                                builder: (BuildContext context) {
                                  return AlertDialog(
                                      backgroundColor: Colors.grey[100],
                                      content: SizedBox(
                                        width: double.maxFinite,
                                        child: SpeedWindSelect(
                                            scrollTheList: true,
                                            scrollToIndex: 19,
                                            onChangedSpeedWind: (value) {
                                              setState(() {
                                                _speedWindInitial = value;
                                              });
                                              Util.closeKeyBoard();
                                            }),
                                      ));
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
                            Util.closeKeyBoard();
                            await showDialog(
                                context: context,
                                builder: (BuildContext context) {
                                  return AlertDialog(
                                      backgroundColor: Colors.grey[100],
                                      content: SizedBox(
                                        width: double.maxFinite,
                                        child: SpeedWindSelect(
                                            scrollTheList: true,
                                            scrollToIndex: 19,
                                            onChangedSpeedWind: (value) {
                                              setState(() {
                                                _speedWindFinal = value;
                                              });
                                              Util.closeKeyBoard();
                                            }),
                                      ));
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
                  onClick: () async {
                    final verify = await _verifyFields();
                    if (!verify) {
                      return;
                    }

                    // ignore: use_build_context_synchronously
                    Navigator.pop(context);
                    // ignore: use_build_context_synchronously
                    Navigator.pop(context);
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
