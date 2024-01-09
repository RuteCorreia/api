import 'dart:io';
import 'dart:typed_data';

import 'package:flutter/material.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/presentation/widgets/image_selected.dart';
import 'package:go_router/go_router.dart';

import '../../../auth/presentation/widgets/custom_login_button.dart';
import 'my_activity_page.dart';
import 'steps/aplication_first_step.dart';

class CaracteristicaProdutoPage extends StatefulWidget {
  const CaracteristicaProdutoPage({super.key});

  @override
  State<CaracteristicaProdutoPage> createState() =>
      _CaracteristicaProdutoPageState();
}

enum DosagemUnidade { LH, KG, ML, HA, NENHUM }

class _CaracteristicaProdutoPageState extends State<CaracteristicaProdutoPage> {
  DosagemUnidade _dosagemUnidade = DosagemUnidade.NENHUM;
  String _imageMapsPath = "";
  bool _isCut = true;
  Uint8List? _imageData;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
        title: const Text(
          "Caraterística do\nproduto a ser aplicado",
          textAlign: TextAlign.center,
        ),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: SingleChildScrollView(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const SizedBox(height: 16),
              const CustomText(text: 'Cultura'),
              const SizedBox(height: 14),
              Container(
                width: double.infinity,
                height: 50,
                margin: const EdgeInsets.only(bottom: 20),
                padding:
                    const EdgeInsets.symmetric(horizontal: 16, vertical: 10),
                decoration: ShapeDecoration(
                  shape: RoundedRectangleBorder(
                    side: const BorderSide(width: 1, color: Color(0xFF636363)),
                    borderRadius: BorderRadius.circular(10),
                  ),
                ),
                child: const TextField(
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
              const SizedBox(height: 15),
              const CustomText(text: 'Enviar Receituário Agronômico'),
              const SizedBox(height: 15),
              InkWell(
                onTap: () async {
                  _imageMapsPath = '';
                  _isCut = false;
                  setState(() {});
                  _imageMapsPath = await Util.obtainImagePathMaps(context);
                  _imageData = await File(_imageMapsPath).readAsBytes();  
                  _isCut = true;
                  setState(() {});
                },
                child: Container(
                  height: 60,
                  color: const Color(0xFFECEAEA),
                  padding:
                      const EdgeInsets.symmetric(horizontal: 20, vertical: 10),
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
                        'Enviar Receituário\nAgronômico',
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
                ),
              ),
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
                          isCut: _isCut,
                          imageData: _imageData,

                          onCutImage: (cut, path) {
                            _isCut = cut;
                            setState(() {});
                          },
                        )
                      ],
                    )
                  : const SizedBox.shrink(),
              const SizedBox(height: 15),
              const CustomText(text: 'Nome do produto'),
              const SizedBox(height: 10),
              CustomCombo(
                selectedName: "Selecione",
                onTap: () {},
              ),
              const SizedBox(height: 20),
              const CustomText(text: 'Classificação Toxicológica'),
              const SizedBox(height: 14),
              CustomCombo(
                selectedName: "Selecione",
                onTap: () {},
              ),
              const SizedBox(height: 10),
              const SizedBox(height: 14),
              const CustomText(text: 'Classe'),
              const SizedBox(height: 10),
              CustomCombo(
                selectedName: "Selecione",
                onTap: () {},
              ),
              const SizedBox(height: 12),
              const CustomText(text: 'Tipo de Formulação'),
              const SizedBox(height: 14),
              CustomCombo(
                selectedName: "Selecione",
                onTap: () {},
              ),
              const SizedBox(height: 14),
              const SizedBox(height: 14),
              const CustomText(text: 'Alvo biológico'),
              const SizedBox(height: 14),
              CustomCombo(
                selectedName: "Selecione",
                onTap: () {},
              ),
              const SizedBox(height: 20),
              const CustomText(text: 'Dose do produto comercial por hectare'),
              const SizedBox(height: 14),
              Container(
                width: double.infinity,
                height: 50,
                margin: const EdgeInsets.only(bottom: 20),
                padding:
                    const EdgeInsets.symmetric(horizontal: 16, vertical: 10),
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
              ),
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
              const CustomText(text: 'Adjuvante'),
              const SizedBox(height: 14),
              Container(
                width: double.infinity,
                height: 50,
                margin: const EdgeInsets.only(bottom: 20),
                padding:
                    const EdgeInsets.symmetric(horizontal: 16, vertical: 10),
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
              ),
              const CustomText(text: 'Tipo de serviço'),
              const SizedBox(height: 14),
              Container(
                width: double.infinity,
                height: 50,
                margin: const EdgeInsets.only(bottom: 20),
                padding:
                    const EdgeInsets.symmetric(horizontal: 16, vertical: 10),
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
              ),
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
      ),
    );
  }
}
