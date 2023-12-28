import 'package:flutter/material.dart';
import 'package:flytec/features/aplications/presentation/widgets/relative_humidity_select.dart';
import 'package:flytec/features/aplications/presentation/widgets/temperature_select.dart';
import 'package:flytec/features/auth/presentation/widgets/custom_login_button.dart';
import 'package:go_router/go_router.dart';

import 'my_activity_page.dart';

class RecomendacoesTecnicas extends StatefulWidget {
  const RecomendacoesTecnicas({super.key});

  @override
  State<RecomendacoesTecnicas> createState() => _RecomendacoesTecnicasState();
}

enum Unidade { KG, L, NENHUM }

class _RecomendacoesTecnicasState extends State<RecomendacoesTecnicas> {
  Unidade _unidade = Unidade.NENHUM;
  String _humiditySelected = 'Selecione';
  String _temperatureSelected = "Selecione";

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
                const Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    CustomText(text: 'Veiculante'),
                    SizedBox(height: 14),
                    ComboBox(selectedName: "Selecione"),
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
            const ComboBox(selectedName: "Selecione"),
            const SizedBox(height: 10),
            const SizedBox(height: 14),
            const CustomText(text: 'Altura do voo (m)'),
            const SizedBox(height: 10),
            const ComboBox(selectedName: "Selecione"),
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
            const ComboBox(selectedName: "Selecione"),
            const SizedBox(height: 20),
            const CustomText(text: 'Tipo de produto'),
            const SizedBox(height: 14),
            const ComboBox(selectedName: "Selecione"),
            const SizedBox(height: 14),
            const CustomText(text: 'Equipamento'),
            const SizedBox(height: 14),
            const ComboBox(selectedName: "Selecione"),
            const SizedBox(height: 14),
            const CustomText(text: 'Ângulo'),
            const SizedBox(height: 14),
            const ComboBox(selectedName: "Selecione"),
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
