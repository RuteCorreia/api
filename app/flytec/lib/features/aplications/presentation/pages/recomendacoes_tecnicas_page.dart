import 'package:flutter/material.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/widgets/combo_box.dart';
import 'package:flytec/core/widgets/custom_text.dart';
import 'package:flytec/features/aplications/presentation/widgets/degree_select.dart';
import 'package:flytec/features/aplications/presentation/widgets/flight_height_select.dart';
import 'package:flytec/features/aplications/presentation/widgets/product_type_select.dart';
import 'package:flytec/features/aplications/presentation/widgets/relative_humidity_select.dart';
import 'package:flytec/features/aplications/presentation/widgets/speed_wind_select.dart';
import 'package:flytec/features/aplications/presentation/widgets/temperature_select.dart';
import 'package:flytec/features/aplications/presentation/widgets/veiculante_select.dart';
import 'package:flytec/features/auth/presentation/widgets/custom_login_button.dart';
import 'package:go_router/go_router.dart';
import 'package:modal_bottom_sheet/modal_bottom_sheet.dart';

class RecomendacoesTecnicas extends StatefulWidget {
  const RecomendacoesTecnicas({super.key});

  @override
  State<RecomendacoesTecnicas> createState() => _RecomendacoesTecnicasState();
}

enum Unidade { KG, L, NENHUM }

class _RecomendacoesTecnicasState extends State<RecomendacoesTecnicas> {
  String _productType = "Selecione";
  Unidade _unidade = Unidade.NENHUM;

  String veiculanteSelecionado = "";
  String aeronaveSelecionada = "";
  String alturaDoVooSelecionado = "";
  String equipamentoSelecionado = "";
  String tipoProdutoSelecionado = "";
  String _degree = "Selecione";
  String _veiculanteType = "Selecione";
  String _humiditySelected = '+ 55%';
  String _temperatureSelected = "20.0°C";
  String _flightHeight = "Selecione";
  String _speedWind = "Selecione";

  @override
  Widget build(BuildContext context) {
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
                          await showDialog(
                              context: context,
                              builder: (BuildContext context) {
                                return AlertDialog(
                                    backgroundColor: Colors.grey[100],
                                    content: VeiculanteSelect(
                                        onChangeVeiculanteType: (value) {
                                      setState(() {
                                        _veiculanteType = value;
                                      });
                                    }));
                              });
                        },
                        child: ComboBox(selectedName: _veiculanteType)),
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
                      child: const TextField(
                        decoration: InputDecoration(
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
            const ComboBox(selectedName: "Selecione"),
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
                      child: const TextField(
                        keyboardType: TextInputType.number,
                        decoration: InputDecoration(
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
            CustomComboBox(
              selectedName: aeronaveSelecionada.isEmpty
                  ? "Selecione"
                  : aeronaveSelecionada,
              onTap: () {
                showMaterialModalBottomSheet(
                  context: context,
                  builder: (context) => SingleChildScrollView(
                    controller: ModalScrollController.of(context),
                    child: Container(
                      height: 400,
                      color: Colors.white,
                      child: Padding(
                        padding: const EdgeInsets.all(0.0),
                        child: SingleChildScrollView(
                          child: Column(
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [
                              SizedBox(
                                height: 500,
                                child: ListView.builder(
                                    padding: EdgeInsets.zero,
                                    itemCount: getIt<GlobalConfigVars>()
                                        .aeronaves
                                        .length,
                                    itemBuilder: (ctx, index) {
                                      final aeronave = getIt<GlobalConfigVars>()
                                          .aeronaves[index];
                                      return Container(
                                        margin:
                                            const EdgeInsets.only(bottom: 2),
                                        decoration: BoxDecoration(
                                          color: Colors.grey.withOpacity(0.1),
                                        ),
                                        child: ListTile(
                                          onTap: () {
                                            setState(() {
                                              aeronaveSelecionada =
                                                  aeronave.prefixo!;
                                            });
                                            context.pop();
                                          },
                                          style: ListTileStyle.drawer,
                                          title: Text("${aeronave.prefixo}"),
                                        ),
                                      );
                                    }),
                              )
                            ],
                          ),
                        ),
                      ),
                    ),
                  ),
                );
              },
            ),
            const SizedBox(height: 10),
            const SizedBox(height: 14),
            const CustomText(text: 'Altura do voo (m)'),
            const SizedBox(height: 10),
            InkWell(
                onTap: () async {
                  await showDialog(
                      context: context,
                      builder: (BuildContext context) {
                        return AlertDialog(
                            backgroundColor: Colors.grey[100],
                            content: FlightHeightSelect(
                                onChangedFlightHeight: (value) {
                              setState(() {
                                _flightHeight = value;
                              });
                            }));
                      });
                },
                child: ComboBox(selectedName: _flightHeight)),
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
                          await showDialog(
                              context: context,
                              builder: (BuildContext context) {
                                return AlertDialog(
                                    backgroundColor: Colors.grey[100],
                                    content: TemperatureSelect(
                                        onChangedTemperature: (value) {
                                      setState(() {
                                        _temperatureSelected = value;
                                      });
                                    }));
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
                          await showDialog(
                              context: context,
                              builder: (BuildContext context) {
                                return AlertDialog(
                                    backgroundColor: Colors.grey[100],
                                    content: RelativeHumiditySelect(
                                        onChangedHumidity: (value) {
                                      setState(() {
                                        _humiditySelected = value;
                                      });
                                    }));
                              });
                        },
                        child: ComboBox(selectedName: _humiditySelected))
                  ],
                )
              ],
            ),
            const SizedBox(height: 14),
            const CustomText(text: 'Velocidade do vento'),
            const SizedBox(height: 14),
            InkWell(
                onTap: () async {
                  await showDialog(
                      context: context,
                      builder: (BuildContext context) {
                        return AlertDialog(
                            backgroundColor: Colors.grey[100],
                            content:
                                SpeedWindSelect(onChangedSpeedWind: (value) {
                              setState(() {
                                _speedWind = value;
                              });
                            }));
                      });
                },
                child: ComboBox(selectedName: _speedWind)),
            const SizedBox(height: 20),
            const CustomText(text: 'Tipo de produto'),
            const SizedBox(height: 14),
            InkWell(
                onTap: () async {
                  await showDialog(
                      context: context,
                      builder: (BuildContext context) {
                        return AlertDialog(
                            backgroundColor: Colors.grey[100],
                            content: ProductTypeSelect(
                                onChangedProductType: (value) {
                              setState(() {
                                _productType = value;
                              });
                            }));
                      });
                },
                child: ComboBox(selectedName: _productType)),
            const SizedBox(height: 14),
            const CustomText(text: 'Equipamento'),
            const SizedBox(height: 14),
            CustomComboBoxExpanded(
              selectedName: equipamentoSelecionado.isEmpty
                  ? "Selecione"
                  : equipamentoSelecionado,
              onTap: () {
                showMaterialModalBottomSheet(
                  context: context,
                  builder: (context) => SingleChildScrollView(
                    controller: ModalScrollController.of(context),
                    child: Container(
                      height: 400,
                      color: Colors.white,
                      child: Padding(
                        padding: const EdgeInsets.all(0.0),
                        child: SingleChildScrollView(
                          child: Column(
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [
                              SizedBox(
                                height: 500,
                                child: ListView.builder(
                                    padding: EdgeInsets.zero,
                                    itemCount: getIt<GlobalConfigVars>()
                                        .equipamentos
                                        .length,
                                    itemBuilder: (ctx, index) {
                                      final equipamento =
                                          getIt<GlobalConfigVars>()
                                              .equipamentos[index];
                                      return Container(
                                        margin:
                                            const EdgeInsets.only(bottom: 2),
                                        decoration: BoxDecoration(
                                          color: Colors.grey.withOpacity(0.1),
                                        ),
                                        child: ListTile(
                                          onTap: () {
                                            setState(() {
                                              equipamentoSelecionado =
                                                  equipamento.nome!;
                                            });
                                            context.pop();
                                          },
                                          style: ListTileStyle.drawer,
                                          title: Text("${equipamento.nome}"),
                                        ),
                                      );
                                    }),
                              )
                            ],
                          ),
                        ),
                      ),
                    ),
                  ),
                );
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
                        return AlertDialog(
                            backgroundColor: Colors.grey[100],
                            content: DegreeSelect(onChangeDegree: (value) {
                              setState(() {
                                _degree = value;
                              });
                            }));
                      });
                },
                child: ComboBox(selectedName: _degree)),
            Center(
              child: CustomButton(
                title: "OK",
                onClick: () {
                  context.pop();
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
