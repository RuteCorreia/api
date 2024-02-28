import 'package:flutter/material.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/widgets/combo_box.dart';
import 'package:flytec/features/aplications/data/models/relatorio_model.dart';
import 'package:flytec/features/aplications/presentation/widgets/aircraft_select.dart';
import 'package:flytec/features/aplications/presentation/widgets/degree_select.dart';
import 'package:flytec/features/aplications/presentation/widgets/equipment_select.dart';
import 'package:flytec/features/aplications/presentation/widgets/flight_height_select.dart';
import 'package:flytec/features/aplications/presentation/widgets/product_type_select.dart';
import 'package:flytec/features/aplications/presentation/widgets/relative_humidity_select.dart';
import 'package:flytec/features/aplications/presentation/widgets/speed_wind_select.dart';
import 'package:flytec/features/aplications/presentation/widgets/temperature_select.dart';
import 'package:flytec/features/aplications/presentation/widgets/veiculante_select.dart';
import 'package:flytec/features/auth/presentation/widgets/custom_login_button.dart';
import 'package:go_router/go_router.dart';

import '../../../../core/utils/util.dart';
import 'my_activity_page.dart';

class RecomendacoesTecnicasPage extends StatefulWidget {
  const RecomendacoesTecnicasPage({super.key});

  @override
  State<RecomendacoesTecnicasPage> createState() =>
      _RecomendacoesTecnicasPageState();
}

enum Unidade { KG, L, NENHUM }

class _RecomendacoesTecnicasPageState extends State<RecomendacoesTecnicasPage> {
  String _productType = "Selecione";
  Unidade _unidade = Unidade.NENHUM;
  String _degree = "Selecione";
  String _veiculanteType = "Selecione";
  String _humiditySelected = '+55%';
  String _temperatureSelected = "20.0°C";
  String _flightHeight = "Selecione";
  String _speedWind = "Selecione";
  String _selectedEquipment = "";
  String __selectedAaeronave = "";
  final TextEditingController _qtdVeiculante = TextEditingController();
  final TextEditingController _larguraDaFaixa = TextEditingController();
  final TextEditingController _volumeDeAplicacao = TextEditingController();
  @override
  void initState() {
    super.initState();
    _degree = getIt<GlobalConfigVars>()
        .reportList
        .last
        .recomendacoesTecnicas!
        .angulo!;
    _productType = getIt<GlobalConfigVars>()
        .reportList
        .last
        .recomendacoesTecnicas!
        .tipoProduto!;

    _humiditySelected = getIt<GlobalConfigVars>()
        .reportList
        .last
        .recomendacoesTecnicas!
        .umidadeRelativaDoAr!;

    _temperatureSelected = getIt<GlobalConfigVars>()
        .reportList
        .last
        .recomendacoesTecnicas!
        .temperatura!;

    _flightHeight = getIt<GlobalConfigVars>()
        .reportList
        .last
        .recomendacoesTecnicas!
        .alturaDoVoo!;

    __selectedAaeronave = getIt<GlobalConfigVars>()
        .reportList
        .last
        .recomendacoesTecnicas!
        .aeronave!;
    _selectedEquipment = getIt<GlobalConfigVars>()
        .reportList
        .last
        .recomendacoesTecnicas!
        .equipamento!;
    _speedWind = getIt<GlobalConfigVars>()
        .reportList
        .last
        .recomendacoesTecnicas!
        .velocidadeDoVento!;

    _veiculanteType = getIt<GlobalConfigVars>()
        .reportList
        .last
        .recomendacoesTecnicas!
        .veiculante!;
    _qtdVeiculante.text = getIt<GlobalConfigVars>()
        .reportList
        .last
        .recomendacoesTecnicas!
        .qtdVeiculante!;
    _larguraDaFaixa.text = getIt<GlobalConfigVars>()
        .reportList
        .last
        .recomendacoesTecnicas!
        .larguraDaFaixa!;
    _volumeDeAplicacao.text = getIt<GlobalConfigVars>()
        .reportList
        .last
        .recomendacoesTecnicas!
        .volumeDaAplicacao!;

    if (_humiditySelected.isEmpty) {
      _humiditySelected = "+ 55%";
    }
  }

  String getUnidadeVolume({Unidade? unidade}) {
    switch (unidade) {
      case Unidade.KG:
        return "KG";
      case Unidade.L:
        return "L";

      default:
        return "Unknown";
    }
  }

  bool verifyFiels() {
    if (_veiculanteType.isEmpty || _veiculanteType == "Selecione") {
      Util.toastAlerta("Selecione o veiculante");
      return false;
    } else if (_larguraDaFaixa.text.isEmpty) {
      Util.toastAlerta("Digite a largura da faixa");
      return false;
    } else if (_volumeDeAplicacao.text.isEmpty) {
      Util.toastAlerta("Digite o volume de aplicação");
      return false;
    } else if (_unidade == Unidade.NENHUM) {
      Util.toastAlerta("Selecione a unidade do volume de aplicação");
      return false;
    } else if (__selectedAaeronave.isEmpty) {
      Util.toastAlerta("Selecione a aeronave");
      return false;
    } else if (_flightHeight.isEmpty || _flightHeight == "Selecione") {
      Util.toastAlerta("Selecione a altura do voo");
      return false;
    } else if (_temperatureSelected.isEmpty) {
      Util.toastAlerta("Selecione a temperatura");
      return false;
    } else if (_humiditySelected.isEmpty || _humiditySelected == "Selecione") {
      Util.toastAlerta("Selecione a umidade relativa do ar");
      return false;
    } else if (_speedWind.isEmpty || _speedWind == "Selecione") {
      Util.toastAlerta("Selecione a velocidade do vento");
      return false;
    } else if (_productType.isEmpty || _productType == "Selecione") {
      Util.toastAlerta("Selecione o tipo de produto");
      return false;
    } else if (_selectedEquipment.isEmpty ||
        _selectedEquipment == "Selecione") {
      Util.toastAlerta("Selecione o equipamento");
      return false;
    } else if (_degree.isEmpty || _degree == "Selecione") {
      Util.toastAlerta("Selecione o ângulo");
      return false;
    } else {
      getIt<GlobalConfigVars>().reportList.last.recomendacoesTecnicas =
          RecomendacoesTecnicas(
        aeronave: __selectedAaeronave,
        veiculante: _veiculanteType,
        alturaDoVoo: _flightHeight,
        angulo: _degree,
        equipamento: _selectedEquipment,
        larguraDaFaixa: _larguraDaFaixa.text,
        qtdVeiculante: _qtdVeiculante.text,
        temperatura: _temperatureSelected,
        tipoProduto: _productType,
        umidadeRelativaDoAr: _humiditySelected,
        velocidadeDoVento: _speedWind,
        volumeDaAplicacao: _volumeDeAplicacao.text,
        unidadeVolume: getUnidadeVolume(unidade: _unidade),
      );
      Util.toastSucesso("Dados inseridos com sucesso");
      context.pop();
      return true;
    }
  }

  @override
  Widget build(BuildContext context) {
    var data = getIt<GlobalConfigVars>().reportList.last.recomendacoesTecnicas;

    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
        title: const Text(
          "Recomendações técnicas",
          textAlign: TextAlign.center,
        ),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: ListView(
          children: [
            const SizedBox(height: 16),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const CustomText(text: 'Veiculante'),
                    const SizedBox(height: 14),
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
                                      child: VeiculanteSelect(
                                          onChangeVeiculanteType: (value) {
                                        setState(() {
                                          _veiculanteType = value;
                                          data!.veiculante = value;
                                        });
                                      }),
                                    ));
                              });
                        },
                        child: ComboBox(
                            selectedName: _veiculanteType.isEmpty
                                ? "Selecione"
                                : _veiculanteType)),
                  ],
                ),
                Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const CustomText(text: 'Qtde. Veiculante (L)'),
                    const SizedBox(height: 14),
                    Container(
                      width: (MediaQuery.of(context).size.width / 2) - 25,
                      height: 50,
                      padding: const EdgeInsets.symmetric(
                          horizontal: 16, vertical: 10),
                      decoration: ShapeDecoration(
                        shape: RoundedRectangleBorder(
                          side: const BorderSide(
                              width: 1, color: Color(0xFF636363)),
                          borderRadius: BorderRadius.circular(10),
                        ),
                      ),
                      child: TextField(
                        controller: _qtdVeiculante,
                        onChanged: (value) {
                          data!.qtdVeiculante = value;
                        },
                        keyboardType: TextInputType.number,
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
                  ],
                )
              ],
            ),
            const SizedBox(height: 20),
            const CustomText(text: 'Largura da faixa (m)'),
            const SizedBox(height: 10),
            Container(
              width: double.infinity,
              height: 50,
              margin: const EdgeInsets.only(bottom: 20),
              padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 10),
              decoration: ShapeDecoration(
                shape: RoundedRectangleBorder(
                  side: const BorderSide(width: 1, color: Color(0xFF636363)),
                  borderRadius: BorderRadius.circular(10),
                ),
              ),
              child: TextField(
                controller: _larguraDaFaixa,
                onChanged: (value) {
                  data!.larguraDaFaixa = value;
                },
                keyboardType: TextInputType.number,
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
            const SizedBox(height: 17),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const CustomText(text: "Volume de aplicação"),
                    const SizedBox(height: 14),
                    Container(
                      width: (MediaQuery.of(context).size.width / 2) - 40,
                      height: 50,
                      padding: const EdgeInsets.symmetric(
                          horizontal: 16, vertical: 10),
                      decoration: ShapeDecoration(
                        shape: RoundedRectangleBorder(
                          side: const BorderSide(
                              width: 1, color: Color(0xFF636363)),
                          borderRadius: BorderRadius.circular(10),
                        ),
                      ),
                      child: TextField(
                        controller: _volumeDeAplicacao,
                        onChanged: (value) {
                          data!.volumeDaAplicacao = value;
                        },
                        keyboardType: TextInputType.number,
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
                  ],
                ),
                const SizedBox(width: 12),
                Flexible(
                  child: Row(
                    children: [
                      const Text("Kg/ha"),
                      Checkbox(
                          value: _unidade == Unidade.KG,
                          onChanged: (value) {
                            setState(() {
                              _unidade = Unidade.KG;
                            });
                          }),
                    ],
                  ),
                ),
                Flexible(
                  child: Row(
                    children: [
                      const Text("L/ha"),
                      Checkbox(
                          value: _unidade == Unidade.L,
                          onChanged: (value) {
                            setState(() {
                              _unidade = Unidade.L;
                            });
                          }),
                    ],
                  ),
                )
              ],
            ),
            const SizedBox(height: 20),
            const CustomText(text: 'Aeronave'),
            const SizedBox(height: 14),
            CustomComboBoxExpanded(
                selectedName: __selectedAaeronave.isEmpty
                    ? "Selecione"
                    : __selectedAaeronave,
                onTap: () async {
                  Util.closeKeyBoard();
                  await showDialog(
                      context: context,
                      builder: (BuildContext context) {
                        return AlertDialog(
                            backgroundColor: Colors.grey[100],
                            content: SizedBox(
                              width: double.maxFinite,
                              child: AirCraftSelect(onChanged: (value) {
                                setState(() {
                                  __selectedAaeronave = value;
                                  data!.aeronave = value;
                                });
                              }),
                            ));
                      });
                }),
            const SizedBox(height: 10),
            const SizedBox(height: 14),
            const CustomText(text: 'Altura do voo (m)'),
            const SizedBox(height: 10),
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
                              child: FlightHeightSelect(
                                  onChangedFlightHeight: (value) {
                                setState(() {
                                  _flightHeight = value;
                                  data!.alturaDoVoo = value;
                                });
                              }),
                            ));
                      });
                },
                child: ComboBox(
                    selectedName:
                        _flightHeight.isEmpty ? "Selecione" : _flightHeight)),
            const SizedBox(height: 15),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const CustomText(text: 'Temperatura (ºC)'),
                    const SizedBox(height: 14),
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
                                              _temperatureSelected = value;
                                              data!.temperatura = value;
                                            });
                                          }),
                                    ));
                              });
                        },
                        child: ComboBox(selectedName: _temperatureSelected))
                  ],
                ),
                Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const CustomText(text: 'U.R do ar(%)'),
                    const SizedBox(height: 14),
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
                                              _humiditySelected = value;
                                              data!.umidadeRelativaDoAr = value;
                                            });
                                          }),
                                    ));
                              });
                        },
                        child: ComboBox(
                            selectedName: _humiditySelected.isEmpty
                                ? "Selecione"
                                : _humiditySelected))
                  ],
                )
              ],
            ),
            const SizedBox(height: 14),
            const CustomText(text: 'Velocidade do vento'),
            const SizedBox(height: 14),
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
                              child:
                                  SpeedWindSelect(onChangedSpeedWind: (value) {
                                setState(() {
                                  _speedWind = value;
                                  data!.velocidadeDoVento = value;
                                });
                              }),
                            ));
                      });
                },
                child: ComboBox(
                    selectedName:
                        _speedWind.isEmpty ? "Selecione" : _speedWind)),
            const SizedBox(height: 20),
            const CustomText(text: 'Tipo de produto'),
            const SizedBox(height: 14),
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
                              child: ProductTypeSelect(
                                  onChangedProductType: (value) {
                                setState(() {
                                  _productType = value;
                                  data!.tipoProduto = value;
                                });
                              }),
                            ));
                      });
                },
                child: ComboBox(
                    selectedName:
                        _productType.isEmpty ? "Selecione" : _productType)),
            const SizedBox(height: 14),
            const CustomText(text: 'Equipamento'),
            const SizedBox(height: 14),
            CustomComboBoxExpanded(
              selectedName:
                  _selectedEquipment.isEmpty ? "Selecione" : _selectedEquipment,
              onTap: () async {
                await showDialog(
                    context: context,
                    builder: (BuildContext context) {
                      Util.closeKeyBoard();
                      return AlertDialog(
                          backgroundColor: const Color(0xFFF5F5F5),
                          content: SizedBox(
                            width: double.maxFinite,
                            child: EquipmentSelect(onChanged: (value) {
                              setState(() {
                                _selectedEquipment = value;
                                data!.equipamento = value;
                              });
                            }),
                          ));
                    });
              },
            ),
            const SizedBox(height: 14),
            const CustomText(text: 'Ângulo'),
            const SizedBox(height: 14),
            InkWell(
                onTap: () async {
                  await showDialog(
                      context: context,
                      builder: (BuildContext context) {
                        Util.closeKeyBoard();
                        return AlertDialog(
                            backgroundColor: Colors.grey[100],
                            content: SizedBox(
                              width: double.maxFinite,
                              child: DegreeSelect(onChangeDegree: (value) {
                                setState(() {
                                  _degree = value;
                                  data!.angulo = value;
                                });
                              }),
                            ));
                      });
                },
                child: ComboBox(
                    selectedName: _degree.isEmpty ? "Selecione" : _degree)),
            Center(
              child: CustomButton(
                title: "OK",
                onClick: () {
                  verifyFiels();
                },
              ),
            ),
          ],
        ),
      ),
    );
  }
}

class ComboBox extends StatelessWidget {
  const ComboBox({super.key, required this.selectedName});
  final String selectedName;

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 50,
      width: (MediaQuery.of(context).size.width / 2) - 25,
      padding: const EdgeInsets.all(8),
      decoration: ShapeDecoration(
        shape: RoundedRectangleBorder(
          side: const BorderSide(width: 1, color: Color(0xFF636363)),
          borderRadius: BorderRadius.circular(8),
        ),
      ),
      child: Row(
        mainAxisSize: MainAxisSize.min,
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
          SizedBox(
            child: Row(
              mainAxisSize: MainAxisSize.min,
              mainAxisAlignment: MainAxisAlignment.start,
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                Text(
                  selectedName,
                  style: const TextStyle(
                    color: Color.fromARGB(255, 124, 123, 123),
                    fontSize: 16,
                    fontFamily: 'Inter',
                    fontWeight: FontWeight.w500,
                    height: 0.09,
                  ),
                ),
              ],
            ),
          ),
          const Icon(
            Icons.keyboard_arrow_down,
          )
        ],
      ),
    );
  }
}
