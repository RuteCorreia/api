import 'dart:io';
import 'dart:typed_data';

import 'package:flutter/material.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/data/models/aplicacao_model.dart';
import 'package:flytec/features/aplications/data/models/relatorio_model.dart';
import 'package:flytec/features/aplications/presentation/pages/steps/aplication_first_step.dart';
import 'package:flytec/features/aplications/presentation/pages/upload_fotos.dart';
import 'package:flytec/features/aplications/presentation/widgets/relative_humidity_select.dart';
import 'package:flytec/features/aplications/presentation/widgets/speed_wind_select.dart';
import 'package:flytec/features/aplications/presentation/widgets/temperature_select.dart';
import 'package:go_router/go_router.dart';
import 'package:image_picker/image_picker.dart';
import 'package:intl/intl.dart';

import '../../../auth/presentation/widgets/custom_login_button.dart';
import 'my_activity_page.dart';
import 'relatorio_aplicacao_page.dart';

extension TimeOfDayConverter on TimeOfDay {
  String to24hours() {
    final hour = this.hour.toString().padLeft(2, "0");
    final min = minute.toString().padLeft(2, "0");
    return "$hour:$min";
  }
}

class AplicacoesPage extends StatefulWidget {
  const AplicacoesPage({super.key});

  @override
  State<AplicacoesPage> createState() => _AplicacoesPageState();
}

class _AplicacoesPageState extends State<AplicacoesPage> {
  final ImagePicker picker = ImagePicker();
  String _speedWindInitial = 'Selecione';
  String _speedWindFinal = 'Selecione';
  String _temperatureSelectedInitial = "20.0°C";
  String _temperatureSelectedFinal = "20.0°C";
  String _imageMapsPath = "";

  String _humiditySelectedInitial = '+ 55%';
  bool isCut = true;
  Uint8List? _imageData;

  String _humiditySelectedFinal = '+ 55%';
  late DateTime? dataSelecionada = DateTime.now();
  late TimeOfDay? time = const TimeOfDay(hour: 12, minute: 43);
  late TimeOfDay? horimetro = const TimeOfDay(hour: 15, minute: 43);
  final TextEditingController _horimetroInicial = TextEditingController();
  final TextEditingController _horimetroFinal = TextEditingController();

  TimeOfDay _selectedTime = TimeOfDay.now();
  TimeOfDay _selectedTimeFinal = TimeOfDay.now();

  bool verifyFields() {
    if (_horimetroInicial.text.isEmpty) {
      Util.toastAlerta("Digite o horímetro inicial");
      return false;
    } else if (_horimetroFinal.text.isEmpty) {
      Util.toastAlerta("Digite o horímetro final");
      return false;
    } else if (_speedWindInitial.isEmpty) {
      Util.toastAlerta("Selecione a velocidade do vento inicial");
      return false;
    } else if (_humiditySelectedFinal.isEmpty) {
      Util.toastAlerta("Selecione a velocidade do vento final");
      return false;
    } else {
      Util.toastSucesso("  Dados inseridos com sucesso");
      getIt<GlobalConfigVars>()
          .reportList
          .last
          .relatorioDeAplicacao!
          .aplicacoes = Aplicacoes(
        dataDaAplicacao: DateFormat('dd/MM/yyyy').format(dataSelecionada!),
        horarioDeInicio: _selectedTime.to24hours(),
        horarioDeTermino: _selectedTimeFinal.to24hours(),
        temperaturaFinal: _temperatureSelectedFinal,
        temperaturaIncial: _temperatureSelectedFinal,
        ventoFinal: _speedWindFinal,
        ventoInicial: _speedWindInitial,
        umidadeRelativaInicial: _humiditySelectedInitial,
        umidadeRelativaFinal: _humiditySelectedFinal,
        horimetroInicial: _horimetroInicial.text,
        horimetroFinal: _horimetroFinal.text,
      );

      context.pop();
      return true;
    }
  }

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

  @override
  void initState() {
    getIt<GlobalConfigVars>().reportList.last.relatorioDeAplicacao!.aplicacoes =
        Aplicacoes(
            dataDaAplicacao: "",
            horarioDeInicio: "",
            horarioDeTermino: "",
            horimetroFinal: "",
            horimetroInicial: "",
            temperaturaFinal: "",
            temperaturaIncial: "",
            umidadeRelativaFinal: "",
            umidadeRelativaInicial: "",
            ventoFinal: "",
            ventoInicial: "");
    var relatorio = getIt<GlobalConfigVars>()
        .reportList
        .last
        .relatorioDeAplicacao!
        .aplicacoes;
    relatorio!.temperaturaIncial = "20.0°C";
    relatorio.temperaturaFinal = "20.0°C";
    relatorio.umidadeRelativaInicial = '+ 55%';
    relatorio.umidadeRelativaFinal = '+ 55%';
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    var data = getIt<GlobalConfigVars>()
        .reportList
        .last
        .relatorioDeAplicacao!
        .aplicacoes;

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
                    data!.dataDaAplicacao =
                        "${dataSelecionada!.day}/${dataSelecionada!.month}/${dataSelecionada!.year}";
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
                    data!.horarioDeInicio = _selectedTime.to24hours();
                    Util.closeKeyBoard();
                  }),
              const SizedBox(height: 14),
              const CustomText(text: 'Horímetro inicial'),
              const SizedBox(height: 14),
              Container(
                width: (MediaQuery.of(context).size.width / 2) - 25,
                height: 50,
                padding:
                    const EdgeInsets.symmetric(horizontal: 16, vertical: 10),
                decoration: ShapeDecoration(
                  shape: RoundedRectangleBorder(
                    side: const BorderSide(width: 1, color: Color(0xFF636363)),
                    borderRadius: BorderRadius.circular(10),
                  ),
                ),
                child: TextField(
                  controller: _horimetroInicial,
                  onChanged: (value) {
                    data!.horimetroInicial = value;
                  },
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
                    data!.horarioDeTermino = _selectedTimeFinal.to24hours();
                    Util.closeKeyBoard();
                  }),
              const SizedBox(height: 20),
              const CustomText(text: "Horímetro final"),
              const SizedBox(height: 14),
              Container(
                width: (MediaQuery.of(context).size.width / 2) - 25,
                height: 50,
                padding:
                    const EdgeInsets.symmetric(horizontal: 16, vertical: 10),
                decoration: ShapeDecoration(
                  shape: RoundedRectangleBorder(
                    side: const BorderSide(width: 1, color: Color(0xFF636363)),
                    borderRadius: BorderRadius.circular(10),
                  ),
                ),
                child: TextField(
                  controller: _horimetroFinal,
                  onChanged: (value) {
                    data!.horimetroFinal = value;
                  },
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
                    _imageMapsPath = await Util.obtainImagePathMaps(context);
                    _imageData = await File(_imageMapsPath).readAsBytes();

                    // ignore: use_build_context_synchronously
                    Navigator.push(
                        context,
                        MaterialPageRoute(
                            builder: (context) => UploadFotos(
                                  updateImagePathMap: (path) {
                                    _imageMapsPath = path;
                                    setState(() {});
                                  },
                                  imageData: _imageData,
                                  imagePath: _imageMapsPath,
                                  onOkButton: () {
                                    context.pop();
                                  },
                                )));
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
                        Container(
                            height: 300,
                            width: MediaQuery.of(context).size.width,
                            decoration: BoxDecoration(
                              image: DecorationImage(
                                  image: FileImage(File(_imageMapsPath)),
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
                                await showDialog(
                                    context: context,
                                    builder: (BuildContext context) {
                                      return AlertDialog(
                                          backgroundColor: Colors.grey[100],
                                          content: SizedBox(
                                            width: double.maxFinite,
                                            child: TemperatureSelect(
                                                onChangedTemperature: (value) {
                                              setState(() {
                                                _temperatureSelectedInitial =
                                                    value;
                                                data!.temperaturaIncial = value;
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
                                await showDialog(
                                    context: context,
                                    builder: (BuildContext context) {
                                      return AlertDialog(
                                          backgroundColor: Colors.grey[100],
                                          content: SizedBox(
                                            width: double.maxFinite,
                                            child: TemperatureSelect(
                                                onChangedTemperature: (value) {
                                              setState(() {
                                                _temperatureSelectedFinal =
                                                    value;
                                                data!.temperaturaFinal = value;
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
                            await showDialog(
                                context: context,
                                builder: (BuildContext context) {
                                  return AlertDialog(
                                      backgroundColor: Colors.grey[100],
                                      content: SizedBox(
                                        width: double.maxFinite,
                                        child: RelativeHumiditySelect(
                                            onChangedHumidity: (value) {
                                          setState(() {
                                            _humiditySelectedInitial = value;
                                            data!.umidadeRelativaInicial =
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
                            await showDialog(
                                context: context,
                                builder: (BuildContext context) {
                                  return AlertDialog(
                                      backgroundColor: Colors.grey[100],
                                      content: SizedBox(
                                        width: double.maxFinite,
                                        child: RelativeHumiditySelect(
                                            onChangedHumidity: (value) {
                                          setState(() {
                                            _humiditySelectedFinal = value;
                                            data!.umidadeRelativaFinal = value;
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
                            await showDialog(
                                context: context,
                                builder: (BuildContext context) {
                                  return AlertDialog(
                                      backgroundColor: Colors.grey[100],
                                      content: SizedBox(
                                        width: double.maxFinite,
                                        child: SpeedWindSelect(
                                            onChangedSpeedWind: (value) {
                                          setState(() {
                                            _speedWindInitial = value;
                                            data!.ventoInicial = value;
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
                            await showDialog(
                                context: context,
                                builder: (BuildContext context) {
                                  return AlertDialog(
                                      backgroundColor: Colors.grey[100],
                                      content: SizedBox(
                                        width: double.maxFinite,
                                        child: SpeedWindSelect(
                                            onChangedSpeedWind: (value) {
                                          setState(() {
                                            _speedWindFinal = value;
                                            data!.ventoFinal = value;
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
                  onClick: () {
                    final verify = verifyFields();
                    if (!verify) {
                      return;
                    }
                    getIt<GlobalConfigVars>().aplications.add(AplicacaoModel(
                        data:
                            "${dataSelecionada!.day}/${dataSelecionada!.month}/${dataSelecionada!.year}",
                        umidadeFinal: _humiditySelectedFinal,
                        umidadeInicial: _humiditySelectedInitial,
                        ventoFinal: _speedWindFinal,
                        ventoInicial: _speedWindInitial,
                        horarioInicial: _selectedTime.hour.toString(),
                        temperaturaFinal: _temperatureSelectedFinal,
                        temperaturaInicial: _temperatureSelectedInitial));

                    context.pop();
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
