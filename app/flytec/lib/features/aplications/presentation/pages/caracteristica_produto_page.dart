import 'dart:io';
import 'dart:typed_data';

import 'package:flutter/material.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/widgets/custom_text.dart';
import 'package:flytec/features/aplications/data/models/relatorio_model.dart';
import 'package:flytec/features/aplications/presentation/pages/add_contratante_page.dart';
import 'package:flytec/features/aplications/presentation/pages/croquis_area/croquis_area_page.dart';
import 'package:flytec/features/aplications/presentation/widgets/biologic_target_select.dart';
import 'package:flytec/features/aplications/presentation/widgets/classe_select.dart';
import 'package:flytec/features/aplications/presentation/widgets/culture_select.dart';
import 'package:flytec/features/aplications/presentation/widgets/product_name_select.dart';
import 'package:flytec/features/aplications/presentation/widgets/toxicological_classification_select.dart';
import 'package:go_router/go_router.dart';
import 'package:image_picker/image_picker.dart';
import 'package:modal_bottom_sheet/modal_bottom_sheet.dart';
import 'package:sizer/sizer.dart';

import '../../../../core/utils/util.dart';
import '../../../../core/widgets/combo_box.dart';
import '../../../auth/presentation/widgets/custom_login_button.dart';
import '../widgets/formulation_type_select.dart';

class CaracteristicaProdutoPage extends StatefulWidget {
  const CaracteristicaProdutoPage({super.key});

  @override
  State<CaracteristicaProdutoPage> createState() =>
      _CaracteristicaProdutoPageState();
}

enum DosagemUnidade { LH, KG, ML, HA, NENHUM }

class _CaracteristicaProdutoPageState extends State<CaracteristicaProdutoPage> {
  DosagemUnidade _dosagemUnidade = DosagemUnidade.NENHUM;
  final TextEditingController _adjuvanteController = TextEditingController();
  final TextEditingController _tipoServicoController = TextEditingController();
  final TextEditingController dosePorHectarController = TextEditingController();

  final ImagePicker picker = ImagePicker();
  String classificacaoToxicologica = "";
  String produtoSelecionado = "";
  String classe = "";
  String tipoFormulacao = "";
  String alvoBiologico = "";
  String dosePorHectar = "";
  String _imageMapsPath = "";
  bool _isCut = true;
  Uint8List? _imageData;
  @override
  void initState() {
    super.initState();
    if (getIt<GlobalConfigVars>().identificacaoAreaModel != null) {
      getIt<GlobalConfigVars>().selectedCultura =
          getIt<GlobalConfigVars>().identificacaoAreaModel!.cultura!;
    }

    var data = getIt<GlobalConfigVars>().reportList.last.carateristicaProduto;

    produtoSelecionado = data!.nomeProduto!;

    classificacaoToxicologica = data.classificacaoToxicologica!;
    classe = data.classe!;
    alvoBiologico = data.alvoBiologico!;
    tipoFormulacao = data.tipoFormulacao!;
    dosePorHectarController.text = data.dosePorHectare!;
    _adjuvanteController.text = data.adjuvante!;
    _tipoServicoController.text = data.tipoServico!;
    switch (data.unidadeHectare) {
      case "ML":
        _dosagemUnidade = DosagemUnidade.ML;
        break;
      case "KG":
        _dosagemUnidade = DosagemUnidade.KG;
        break;
      case "LH":
        _dosagemUnidade = DosagemUnidade.LH;
        break;
      case "HA":
        _dosagemUnidade = DosagemUnidade.HA;
        break;
      default:
        _dosagemUnidade = DosagemUnidade.NENHUM;
    }
  }

  String getDosagemUnidade({DosagemUnidade? unidade}) {
    switch (unidade) {
      case DosagemUnidade.HA:
        return "HA";
      case DosagemUnidade.KG:
        return "KG";
      case DosagemUnidade.LH:
        return "LH";
      case DosagemUnidade.ML:
        return "ML";

      default:
        return "unknown";
    }
  }

  bool verifiyFields() {
    if (getIt<GlobalConfigVars>().selectedCultura.isEmpty) {
      Util.toastAlerta("Selecione a cultura");
      return false;
    } else if (produtoSelecionado.isEmpty) {
      Util.toastAlerta("Selecione a produto");
      return false;
    } else if (classificacaoToxicologica.isEmpty) {
      Util.toastAlerta("Selecione a classificação toxicológica");
      return false;
    } else if (classe.isEmpty) {
      Util.toastAlerta("Selecione a classe");
      return false;
    } else if (tipoFormulacao.isEmpty) {
      Util.toastAlerta("Selecione o tipo de formulação");
      return false;
    } else if (alvoBiologico.isEmpty) {
      Util.toastAlerta("Selecione o alvo biológico");
      return false;
    } else if (dosePorHectarController.text.isEmpty) {
      Util.toastAlerta("Digite a dose do produto comercial por hectare");
      return false;
    } else if (_dosagemUnidade == DosagemUnidade.NENHUM) {
      Util.toastAlerta("Selecione a unidade da dosagem ");
      return false;
    } else if (_adjuvanteController.text.isEmpty) {
      Util.toastAlerta("Digite o Adjuvante");
      return false;
    } else if (_tipoServicoController.text.isEmpty) {
      Util.toastAlerta("Digite o tipo de serviço");
      return false;
    } else {
      getIt<GlobalConfigVars>().reportList.last.carateristicaProduto =
          CarateristicaProduto(
              adjuvante: _adjuvanteController.text,
              alvoBiologico: alvoBiologico,
              classe: classe,
              classificacaoToxicologica: classificacaoToxicologica,
              cultura: getIt<GlobalConfigVars>().selectedCultura,
              dosePorHectare: dosePorHectarController.text,
              nomeProduto: produtoSelecionado,
              tipoFormulacao: tipoFormulacao,
              tipoServico: _tipoServicoController.text,
              unidadeHectare: getDosagemUnidade(unidade: _dosagemUnidade));
      Util.toastSucesso("Dados inseridos com sucesso");
      context.pop();
      return true;
    }
  }

  @override
  Widget build(BuildContext context) {
    var data = getIt<GlobalConfigVars>().reportList.last.carateristicaProduto;

    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
        title: const Text(
          "Caraterísticas do\nproduto a ser aplicado",
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
                selectedName: getIt<GlobalConfigVars>().selectedCultura.isEmpty
                    ? "Selecione"
                    : getIt<GlobalConfigVars>().selectedCultura,
                onTap: () async {
                  await showDialog(
                      context: context,
                      builder: (BuildContext context) {
                        return AlertDialog(
                            backgroundColor: const Color(0xFFF5F5F5),
                            content: SizedBox(
                              width: double.maxFinite,
                              child: CultureSelect(onChanged: (value) {
                                setState(() {
                                  getIt<GlobalConfigVars>().selectedCultura =
                                      value;
                                });
                                Util.closeKeyBoard();
                              }),
                            ));
                      });
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
                          builder: (context) => UplodadFotos(
                                updateImagePathMap: (path) {
                                  _imageMapsPath = path;
                                  setState(() {});
                                },
                                imageData: _imageData,
                                imagePath: _imageMapsPath,
                              )));
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
                onTap: () async {
                  await showDialog(
                      context: context,
                      builder: (BuildContext context) {
                        return AlertDialog(
                            backgroundColor: const Color(0xFFF5F5F5),
                            content: SizedBox(
                              width: double.maxFinite,
                              child: ProductNameSelect(
                                  onChangedProductName: (value, produto) {
                                setState(() {
                                  produtoSelecionado = value;
                                  data!.nomeProduto = value;

                                  if (produto == null) {
                                    return;
                                  }
                                  classificacaoToxicologica =
                                      produto.classificacaoToxicologica!;
                                  classe = produto.classe!;
                                  tipoFormulacao = produto.tipoDeFormulacao!;
                                  data.nomeProduto = value;
                                  data.classe = produto.classe;
                                  data.tipoFormulacao =
                                      produto.tipoDeFormulacao;
                                  data.classificacaoToxicologica =
                                      produto.classificacaoToxicologica;
                                });
                                Util.closeKeyBoard();
                              }),
                            ));
                      });
                },
              ),
              const SizedBox(height: 20),
              const CustomText(text: 'Classificação Toxicológica'),
              const SizedBox(height: 14),
              CustomComboBoxExpanded(
                selectedName: classificacaoToxicologica.isEmpty
                    ? "Selecione"
                    : classificacaoToxicologica,
                onTap: () async {
                  await showMaterialModalBottomSheet(
                      context: context,
                      builder: (BuildContext context) {
                        return Container(
                            height: 80.h,
                            padding: const EdgeInsets.symmetric(horizontal: 10),
                            color: const Color(0xFFF5F5F5),
                            child: ToxicologicalClassificationSelect(
                                onSelect: (value) {
                              setState(() {
                                classificacaoToxicologica = value;
                                data!.classificacaoToxicologica = value;
                              });
                            }));
                      });
                },
              ),
              const SizedBox(height: 10),
              const SizedBox(height: 14),
              const CustomText(text: 'Classe'),
              const SizedBox(height: 10),
              CustomComboBoxExpanded(
                selectedName: classe.isEmpty ? "Selecione" : classe,
                onTap: () async {
                  await showDialog(
                      context: context,
                      builder: (BuildContext context) {
                        return AlertDialog(
                            backgroundColor: const Color(0xFFF5F5F5),
                            content: SizedBox(
                              width: double.maxFinite,
                              child: ClasseSelect(onChangedClasse: (value) {
                                setState(() {
                                  classe = value;
                                  data!.classe = value;
                                });
                                Util.closeKeyBoard();
                              }),
                            ));
                      });
                },
              ),
              const SizedBox(height: 12),
              const CustomText(text: 'Tipo de Formulação'),
              const SizedBox(height: 14),
              CustomComboBoxExpanded(
                selectedName:
                    tipoFormulacao.isEmpty ? "Selecione" : tipoFormulacao,
                onTap: () async {
                  await showDialog(
                      context: context,
                      builder: (BuildContext context) {
                        return AlertDialog(
                            backgroundColor: const Color(0xFFF5F5F5),
                            content: SizedBox(
                              width: double.maxFinite,
                              child: FormulationTypeSelect(
                                  onChangedFormulationType: (value) {
                                setState(() {
                                  tipoFormulacao = value;
                                  data!.tipoFormulacao = value;
                                });
                                Util.closeKeyBoard();
                              }),
                            ));
                      });
                },
              ),
              const SizedBox(height: 14),
              const SizedBox(height: 14),
              const CustomText(text: 'Alvo biológico'),
              const SizedBox(height: 14),
              CustomComboBoxExpanded(
                selectedName:
                    alvoBiologico.isEmpty ? "Selecione" : alvoBiologico,
                onTap: () async {
                  await showDialog(
                      context: context,
                      builder: (BuildContext context) {
                        return AlertDialog(
                            backgroundColor: const Color(0xFFF5F5F5),
                            content: SizedBox(
                              width: double.maxFinite,
                              child: BiologicTargetSelect(onChanged: (value) {
                                setState(() {
                                  alvoBiologico = value;
                                  data!.alvoBiologico = value;
                                });
                                Util.closeKeyBoard();
                              }),
                            ));
                      });
                },
              ),
              const SizedBox(height: 20),
              const CustomText(text: 'Dose do produto comercial por hectare'),
              const SizedBox(height: 14),
              CustomTextField(
                textEditingController: dosePorHectarController,
                textInputType: TextInputType.number,
                onChanged: (String value) {
                  data!.dosePorHectare = value;
                },
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
                                data!.unidadeHectare = "ML";
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
                                data!.unidadeHectare = "LH";
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
                                data!.unidadeHectare = "HA";
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
                                data!.unidadeHectare = "KG";
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
                child: TextField(
                  controller: _adjuvanteController,
                  onChanged: (value) {
                    data!.adjuvante = value;
                  },
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
                child: TextField(
                  controller: _tipoServicoController,
                  onChanged: (value) {
                    data!.tipoServico = value;
                  },
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
              Center(
                child: CustomButton(
                  title: "OK",
                  onClick: () {
                    verifiyFields();
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
