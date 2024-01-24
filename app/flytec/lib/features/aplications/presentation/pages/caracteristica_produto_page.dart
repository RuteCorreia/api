import 'dart:io';
import 'dart:typed_data';

import 'package:flutter/material.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/core/widgets/custom_text.dart';
import 'package:flytec/features/aplications/presentation/pages/add_contratante_page.dart';
import 'package:flytec/features/aplications/presentation/pages/upload_fotos.dart';
import 'package:go_router/go_router.dart';
import 'package:modal_bottom_sheet/modal_bottom_sheet.dart';

import '../../../../core/widgets/combo_box.dart';
import '../../../auth/presentation/widgets/custom_login_button.dart';

class CaracteristicaProdutoPage extends StatefulWidget {
  const CaracteristicaProdutoPage({super.key});

  @override
  State<CaracteristicaProdutoPage> createState() =>
      _CaracteristicaProdutoPageState();
}

enum DosagemUnidade { LH, KG, ML, HA, NENHUM }

class _CaracteristicaProdutoPageState extends State<CaracteristicaProdutoPage> {
  DosagemUnidade _dosagemUnidade = DosagemUnidade.NENHUM;
  String selectedCultura = "";
  String classificacaoToxicologica = "";
  String produtoSelecionado = "";
  String classe = "";
  String tipoFormulacao = "";
  String alvoBiologico = "";
  String dosePorHectar = "";
  String _imageMapsPath = "";
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
              CustomComboBoxExpanded(
                selectedName:
                    selectedCultura.isEmpty ? "Selecione" : selectedCultura,
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
                                          .culturas
                                          .length,
                                      itemBuilder: (ctx, index) {
                                        final cultura =
                                            getIt<GlobalConfigVars>()
                                                .culturas[index];
                                        return Container(
                                          margin:
                                              const EdgeInsets.only(bottom: 2),
                                          decoration: BoxDecoration(
                                            color: Colors.grey.withOpacity(0.1),
                                          ),
                                          child: ListTile(
                                            onTap: () {
                                              setState(() {
                                                selectedCultura = cultura.nome!;
                                              });
                                              context.pop();
                                            },
                                            style: ListTileStyle.drawer,
                                            title: Text("${cultura.nome}"),
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
              const SizedBox(height: 15),
              const CustomText(text: 'Enviar Receituário Agronômico'),
              const SizedBox(height: 15),
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
              if (_imageMapsPath.isNotEmpty)
                Column(crossAxisAlignment: CrossAxisAlignment.start, children: [
                  const SizedBox(height: 15),
                  const CustomText(text: 'Imagem do Receituário Agronômico'),
                  const SizedBox(height: 15),
                  Container(
                      height: 300,
                      width: MediaQuery.of(context).size.width,
                      decoration: BoxDecoration(
                        image: DecorationImage(
                            image: FileImage(File(_imageMapsPath)),
                            fit: BoxFit.fill),
                      ))
                ]),
              const SizedBox(height: 15),
              const CustomText(text: 'Nome do produto'),
              const SizedBox(height: 10),
              CustomComboBoxExpanded(
                selectedName: produtoSelecionado.isEmpty
                    ? "Selecione"
                    : produtoSelecionado,
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
                                          .produtos
                                          .length,
                                      itemBuilder: (ctx, index) {
                                        final produto =
                                            getIt<GlobalConfigVars>()
                                                .produtos[index];
                                        return Container(
                                          margin:
                                              const EdgeInsets.only(bottom: 2),
                                          decoration: BoxDecoration(
                                            color: Colors.grey.withOpacity(0.1),
                                          ),
                                          child: ListTile(
                                            onTap: () {
                                              setState(() {
                                                produtoSelecionado =
                                                    produto.nome!;
                                                classificacaoToxicologica = produto
                                                    .classificacaoToxicologica!;
                                                classe = produto.classe!;
                                                tipoFormulacao =
                                                    produto.tipoDeFormulacao!;
                                              });
                                              context.pop();
                                            },
                                            style: ListTileStyle.drawer,
                                            title: Text("${produto.nome}"),
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
              const SizedBox(height: 20),
              const CustomText(text: 'Classificação Toxicológica'),
              const SizedBox(height: 14),
              CustomComboBoxExpanded(
                selectedName: classificacaoToxicologica.isEmpty
                    ? "Selecione"
                    : classificacaoToxicologica,
                onTap: () {},
              ),
              const SizedBox(height: 10),
              const SizedBox(height: 14),
              const CustomText(text: 'Classe'),
              const SizedBox(height: 10),
              CustomComboBoxExpanded(
                selectedName: classe.isEmpty ? "Selecione" : classe,
                onTap: () {},
              ),
              const SizedBox(height: 12),
              const CustomText(text: 'Tipo de Formulação'),
              const SizedBox(height: 14),
              CustomComboBoxExpanded(
                selectedName:
                    tipoFormulacao.isEmpty ? "Selecione" : tipoFormulacao,
                onTap: () {},
              ),
              const SizedBox(height: 14),
              const SizedBox(height: 14),
              const CustomText(text: 'Alvo biológico'),
              const SizedBox(height: 14),
              CustomComboBoxExpanded(
                selectedName:
                    alvoBiologico.isEmpty ? "Selecione" : alvoBiologico,
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
                                          .alvosBiologicos
                                          .length,
                                      itemBuilder: (ctx, index) {
                                        final alvo = getIt<GlobalConfigVars>()
                                            .alvosBiologicos[index];
                                        return Container(
                                          margin:
                                              const EdgeInsets.only(bottom: 2),
                                          decoration: BoxDecoration(
                                            color: Colors.grey.withOpacity(0.1),
                                          ),
                                          child: ListTile(
                                            onTap: () {
                                              setState(() {
                                                alvoBiologico = alvo.nome!;
                                              });
                                              context.pop();
                                            },
                                            style: ListTileStyle.drawer,
                                            title: Text("${alvo.nome}"),
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
              const SizedBox(height: 20),
              const CustomText(text: 'Dose do produto comercial por hectare'),
              const SizedBox(height: 14),
              const CustomTextField(
                textEditingController: null,
                textInputType: TextInputType.number,
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
