import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:flytec/features/auth/presentation/widgets/custom_login_button.dart';
import 'package:go_router/go_router.dart';
import 'package:location/location.dart' as lct;

import 'my_activity_page.dart';

class RelatorioAplicacaoPage extends StatefulWidget {
  const RelatorioAplicacaoPage({super.key});

  @override
  State<RelatorioAplicacaoPage> createState() => _RelatorioAplicacaoPageState();
}

enum VolumeUnidade { LH, KG, NENHUM }

enum DosagemUnidade { LH, KG, ML, HA, NENHUM }

class _RelatorioAplicacaoPageState extends State<RelatorioAplicacaoPage> {
  VolumeUnidade _volumeUnidade = VolumeUnidade.NENHUM;
  DosagemUnidade _dosagemUnidade = DosagemUnidade.NENHUM;
  final TextEditingController _latitudeController = TextEditingController();
  final TextEditingController _longitudeController = TextEditingController();
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
        title: const Text(
          "Relatório de aplicação",
          textAlign: TextAlign.center,
        ),
      ),
      body: Padding(
        padding: const EdgeInsets.all(12),
        child: ListView(
          children: [
            const SizedBox(height: 16),
            const CustomText(text: 'Cultura'),
            const SizedBox(height: 14),
            const ComboBox(selectedName: "-"),
            const SizedBox(height: 20),
            const CustomText(text: 'Produto aplicado'),
            const SizedBox(height: 14),
            const ComboBox(selectedName: "-"),
            const SizedBox(height: 10),
            const CustomText(text: 'Dosagem'),
            const SizedBox(height: 14),
            const CustomTextField(),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Flexible(
                  child: Row(
                    children: [
                      const Text("ml/ha"),
                      Checkbox(
                          materialTapTargetSize:
                              MaterialTapTargetSize.shrinkWrap,
                          value: _dosagemUnidade == DosagemUnidade.ML,
                          onChanged: (value) {
                            setState(() {
                              _dosagemUnidade = DosagemUnidade.ML;
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
                          materialTapTargetSize:
                              MaterialTapTargetSize.shrinkWrap,
                          value: _dosagemUnidade == DosagemUnidade.LH,
                          onChanged: (value) {
                            setState(() {
                              _dosagemUnidade = DosagemUnidade.LH;
                            });
                          }),
                    ],
                  ),
                ),
                Flexible(
                  child: Row(
                    children: [
                      const Text("g/ha"),
                      Checkbox(
                          materialTapTargetSize:
                              MaterialTapTargetSize.shrinkWrap,
                          value: _dosagemUnidade == DosagemUnidade.HA,
                          onChanged: (value) {
                            setState(() {
                              _dosagemUnidade = DosagemUnidade.HA;
                            });
                          }),
                    ],
                  ),
                ),
                Flexible(
                  child: Row(
                    children: [
                      const Text("Kg/ha"),
                      Checkbox(
                          materialTapTargetSize:
                              MaterialTapTargetSize.shrinkWrap,
                          value: _dosagemUnidade == DosagemUnidade.KG,
                          onChanged: (value) {
                            setState(() {
                              _dosagemUnidade = DosagemUnidade.KG;
                            });
                          }),
                    ],
                  ),
                )
              ],
            ),
            const SizedBox(height: 20),
            const CustomText(text: 'Volume de aplicação'),
            const SizedBox(height: 14),
            const CustomTextField(),
            Row(
              mainAxisAlignment: MainAxisAlignment.start,
              children: [
                Row(
                  children: [
                    const Text("L/ha"),
                    Checkbox(
                        materialTapTargetSize: MaterialTapTargetSize.shrinkWrap,
                        value: _volumeUnidade == VolumeUnidade.LH,
                        onChanged: (value) {
                          setState(() {
                            _volumeUnidade = VolumeUnidade.LH;
                          });
                        }),
                  ],
                ),
                const SizedBox(width: 20),
                Flexible(
                  child: Row(
                    children: [
                      const Text("Kg/ha"),
                      Checkbox(
                          materialTapTargetSize:
                              MaterialTapTargetSize.shrinkWrap,
                          value: _volumeUnidade == VolumeUnidade.KG,
                          onChanged: (value) {
                            setState(() {
                              _volumeUnidade = VolumeUnidade.KG;
                            });
                          }),
                    ],
                  ),
                )
              ],
            ),
            const SizedBox(height: 14),
            const CustomText(text: 'Total da área aplicada (ha)'),
            const SizedBox(height: 14),
            const CustomTextField(),
            const SizedBox(height: 14),
            const CustomText(text: 'Localização da pista/Código ICAO'),
            const SizedBox(height: 14),
            const ComboBox(selectedName: "Selecione"),
            const SizedBox(height: 20),
            CustomGpsButton(onClick: () async {
              lct.Location local = lct.Location();
              local.getLocation().then((lc) async {
                setState(() {
                  _latitudeController.text = lc.latitude.toString();
                  _longitudeController.text = lc.longitude.toString();
                });
              });
            }),
            const SizedBox(height: 20),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const CustomText(text: "Latitude - SUL"),
                    const SizedBox(height: 12),
                    Container(
                      height: 50,
                      width: (MediaQuery.of(context).size.width / 2) - 30,
                      margin: const EdgeInsets.only(bottom: 20),
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
                        controller: _latitudeController,
                        textInputAction: TextInputAction.done,
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
                Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const CustomText(text: "Longitude - OESTE"),
                    const SizedBox(height: 12),
                    Container(
                      height: 50,
                      width: (MediaQuery.of(context).size.width / 2) - 30,
                      margin: const EdgeInsets.only(bottom: 20),
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
                        controller: _longitudeController,
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
            const CustomText(text: "Alterações do planejamento / Observações "),
            const SizedBox(height: 14),
            Container(
              width: double.infinity,
              height: 100,
              margin: const EdgeInsets.only(bottom: 20),
              padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 10),
              decoration: ShapeDecoration(
                shape: RoundedRectangleBorder(
                  side: const BorderSide(width: 1, color: Color(0xFF636363)),
                  borderRadius: BorderRadius.circular(10),
                ),
              ),
              child: const TextField(
                minLines: 4,
                maxLines: 4,
                decoration: InputDecoration(
                    hintText: "-",
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
            const SizedBox(height: 12),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceAround,
              children: [
                Container(
                  height: 40,
                  padding: const EdgeInsets.only(
                      top: 8, left: 20, right: 24, bottom: 8),
                  decoration: ShapeDecoration(
                    shape: RoundedRectangleBorder(
                      side:
                          const BorderSide(width: 2, color: Color(0xFFC21B43)),
                      borderRadius: BorderRadius.circular(10),
                    ),
                  ),
                  child: Row(
                    mainAxisSize: MainAxisSize.min,
                    mainAxisAlignment: MainAxisAlignment.center,
                    crossAxisAlignment: CrossAxisAlignment.center,
                    children: [
                      Container(
                        width: 24,
                        height: 24,
                        clipBehavior: Clip.antiAlias,
                        decoration: const BoxDecoration(),
                        child: Stack(children: [
                          SvgPicture.asset(
                            "assets/images/error_icon.svg",
                          )
                        ]),
                      ),
                      const SizedBox(width: 8),
                      const Text(
                        'LIMPAR',
                        style: TextStyle(
                          color: Color(0xFF636363),
                          fontSize: 14,
                          fontFamily: 'Inter',
                          fontWeight: FontWeight.w700,
                          height: 0.11,
                        ),
                      ),
                    ],
                  ),
                ),
                Container(
                  height: 40,
                  padding: const EdgeInsets.only(
                      top: 8, left: 20, right: 24, bottom: 8),
                  decoration: ShapeDecoration(
                    color: Colors.green,
                    shape: RoundedRectangleBorder(
                      side: const BorderSide(
                        width: 2,
                        color: Colors.green,
                      ),
                      borderRadius: BorderRadius.circular(10),
                    ),
                  ),
                  child: Row(
                    mainAxisSize: MainAxisSize.min,
                    mainAxisAlignment: MainAxisAlignment.center,
                    crossAxisAlignment: CrossAxisAlignment.center,
                    children: [
                      Container(
                        width: 24,
                        height: 24,
                        clipBehavior: Clip.antiAlias,
                        decoration: const BoxDecoration(),
                        child: Stack(children: [
                          SvgPicture.asset(
                            "assets/images/checkbox.svg",
                          )
                        ]),
                      ),
                      const SizedBox(width: 8),
                      const Text(
                        'SALVAR',
                        style: TextStyle(
                          color: Colors.white,
                          fontSize: 14,
                          fontFamily: 'Inter',
                          fontWeight: FontWeight.w700,
                          height: 0.11,
                        ),
                      ),
                    ],
                  ),
                )
              ],
            ),
            const SizedBox(height: 20),
            const CustomText(
              text: "Relatório do DGPS (Log’s)",
            ),
            const SizedBox(height: 13),
            const ComboBox(selectedName: "Selecionar Log"),
            Center(
              child: CustomButton(
                title: "APLICAÇÕES",
                onClick: () {
                  context.push("/aplicacoes");
                },
              ),
            ),
          ],
        ),
      ),
    );
  }
}

class CustomGpsButton extends StatelessWidget {
  const CustomGpsButton({super.key, required this.onClick});
  final VoidCallback? onClick;
  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: onClick,
      child: Container(
        width: 140,
        height: 45,
        padding: const EdgeInsets.symmetric(horizontal: 24, vertical: 8),
        decoration: ShapeDecoration(
          color: const Color(0xFF00B45D),
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(10),
          ),
        ),
        child: const Row(
          mainAxisSize: MainAxisSize.min,
          mainAxisAlignment: MainAxisAlignment.center,
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            Icon(
              Icons.public,
              color: Colors.white,
            ),
            SizedBox(width: 10),
            Text(
              "Buscar pelo GPS",
              style: TextStyle(
                color: Colors.white,
                fontSize: 14,
                fontFamily: 'Inter',
                fontWeight: FontWeight.w700,
                height: 0.11,
              ),
            ),
          ],
        ),
      ),
    );
  }
}

class CustomTextField extends StatelessWidget {
  const CustomTextField({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return Container(
      width: double.infinity,
      height: 50,
      margin: const EdgeInsets.only(bottom: 10),
      padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 10),
      decoration: ShapeDecoration(
        shape: RoundedRectangleBorder(
          side: const BorderSide(width: 1, color: Color(0xFF636363)),
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
