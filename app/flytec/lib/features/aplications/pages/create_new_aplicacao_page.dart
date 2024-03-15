import 'dart:io';

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flytec/core/extensions/time_of_day_extension.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/components/components_exports.dart';
import 'package:flytec/features/aplications/controller/report_aplication_controller.dart';
import 'package:flytec/features/aplications/models/aplicacao.dart';
import 'package:flytec/features/aplications/models/aplicacoes.dart';
import 'package:flytec/features/aplications/pages/images/upload_foto.dart';
import 'package:intl/intl.dart';

class CreateNewAplicacaoPages extends StatefulWidget {
  final ReportAplicationController _reportAplicationController;
  final void Function(Aplicacoes newAplicacao) _onAplicacao;
  final Aplicacoes? _aplicacoes;
  const CreateNewAplicacaoPages(
      {required ReportAplicationController reportAplicationController,
      required void Function(Aplicacoes newAplicacao) onAplicacao,
      Aplicacoes? aplicacoes,
      super.key})
      : _reportAplicationController = reportAplicationController,
        _onAplicacao = onAplicacao,
        _aplicacoes = aplicacoes;

  @override
  State<CreateNewAplicacaoPages> createState() =>
      _CreateNewAplicacaoPagesState();
}

class _CreateNewAplicacaoPagesState extends State<CreateNewAplicacaoPages> {
  String _speedWindInitial = '0 km/h';
  String _speedWindFinal = '0 km/h';
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
    await _aplicacoesAction();
    Util.toastSucesso("  Dados inseridos com sucesso");

    setState(() {});
    // ignore: use_build_context_synchronously
    Navigator.pop(context);
    return true;
  }

  Aplicacao get _aplicacao =>
      widget._reportAplicationController.aplicacaoSelected!;

  Future<void> _aplicacoesAction() async {
    Aplicacoes aplicacoes = Aplicacoes(
      dataAplicacao: dataSelecionada?.millisecondsSinceEpoch.toString(),
      horaInicio: _selectedTime.to24hours(),
      horaFinal: _selectedTimeFinal.to24hours(),
      horimetroInicial: _horimetroInicial.text,
      horimetroFinal: _horimetroFinal.text,
      temperaturaInicial: _temperatureSelectedInitial,
      temperaturaFinal: _temperatureSelectedFinal,
      umidadeRelativaArInicial: _humiditySelectedInitial,
      umidadeRelativaArFinal: _humiditySelectedFinal,
      ventoInicial: _speedWindInitial,
      ventoFinal: _speedWindFinal,
      imagemCondicaoClimatica: _imageData,
    );
    if (widget._aplicacoes != null) {
      widget._onAplicacao(aplicacoes);
      return;
    }
    int? idRelatorioAplicacao = _aplicacao.relatorioAplicacao?.id;
    if (idRelatorioAplicacao == null) return;
    final aplicacoesToMap = aplicacoes.toMap();
    aplicacoesToMap.addAll({'relatorioAplicacaoId': idRelatorioAplicacao});
    await widget._reportAplicationController
        .createElementInTable(aplicacoesToMap, 'Aplicacoes');
    widget._onAplicacao(aplicacoes);
  }

  @override
  void initState() {
    super.initState();
    if (widget._aplicacoes != null) {
      _speedWindInitial = widget._aplicacoes?.ventoInicial ?? '0 km/h';
      _speedWindFinal = widget._aplicacoes?.ventoFinal ?? '0 km/h';
      _temperatureSelectedInitial =
          widget._aplicacoes?.temperaturaInicial ?? '20.0°C';
      _temperatureSelectedFinal =
          widget._aplicacoes?.temperaturaFinal ?? '20.0°C';
      dataSelecionada = widget._aplicacoes?.dataAplicacao == null
          ? DateTime.now()
          : DateTime.fromMillisecondsSinceEpoch(
              int.tryParse(widget._aplicacoes?.dataAplicacao ?? '') ?? 0);
      _humiditySelectedInitial =
          widget._aplicacoes?.umidadeRelativaArInicial ?? '+ 55%';
      _humiditySelectedFinal =
          widget._aplicacoes?.umidadeRelativaArFinal ?? '+ 55%';
      _horimetroInicial.text = widget._aplicacoes?.horimetroInicial ?? '';
      _horimetroFinal.text = widget._aplicacoes?.horimetroFinal ?? '';
      _selectedTime = TimeOfDay(
          hour: int.tryParse(widget._aplicacoes!.horaInicio!.split(':')[0])!,
          minute: int.tryParse(widget._aplicacoes!.horaInicio!.split(':')[1])!);
      _selectedTimeFinal = TimeOfDay(
          hour: int.tryParse(widget._aplicacoes!.horaFinal!.split(':')[0])!,
          minute: int.tryParse(widget._aplicacoes!.horaFinal!.split(':')[1])!);
      _imageData = widget._aplicacoes?.imagemCondicaoClimatica;
      setState(() {});
    }
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
          leading: IconButton(
            icon: const Icon(Icons.arrow_back),
            onPressed: () async {
              await _aplicacoesAction();
              // ignore: use_build_context_synchronously
              Navigator.pop(context);
            },
          )),
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
                    // ignore: use_build_context_synchronously
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
                  child: Container(
                    height: 60,
                    color: const Color(0xFFECEAEA),
                    padding: const EdgeInsets.symmetric(
                        horizontal: 20, vertical: 10),
                    child: Row(
                      mainAxisAlignment: MainAxisAlignment.spaceBetween,
                      children: [
                        const Icon(Icons.close),
                        const SizedBox(width: 10),
                        Container(
                          width: 35,
                          height: 35,
                          decoration: BoxDecoration(
                              color: Colors.blue,
                              borderRadius: BorderRadius.circular(10)),
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
                  )),
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
                                            scrollToIndex: 0,
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
                                            scrollToIndex: 0,
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
                    await _verifyFields();
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
