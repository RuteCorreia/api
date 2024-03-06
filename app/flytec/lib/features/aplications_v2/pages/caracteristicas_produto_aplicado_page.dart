import 'dart:io';
import 'dart:typed_data';

import 'package:flutter/material.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications_v2/controller/report_aplication_controller.dart';
import 'package:flytec/features/aplications_v2/components/components_exports.dart';
import 'package:flytec/features/aplications_v2/models/aplicacao.dart';
import 'package:flytec/features/aplications_v2/models/caracteristicas_produto_aplicado.dart';
import 'package:flytec/features/aplications_v2/pages/images/upload_foto.dart';
import 'package:modal_bottom_sheet/modal_bottom_sheet.dart';
import 'package:sizer/sizer.dart';

class CaracteristicasProdutoAplicadoPage extends StatefulWidget {
  final ReportAplicationController _reportAplicationController;
  const CaracteristicasProdutoAplicadoPage(
      {required ReportAplicationController reportAplicationController,
      super.key})
      : _reportAplicationController = reportAplicationController;

  @override
  State<CaracteristicasProdutoAplicadoPage> createState() =>
      _CaracteristicasProdutoAplicadoPageState();
}

class _CaracteristicasProdutoAplicadoPageState
    extends State<CaracteristicasProdutoAplicadoPage> {
  CaracteristicasProdutoAplicado? _caracteristicasProdutoAplicado;

  Aplicacao get _aplicacao =>
      widget._reportAplicationController.aplicacaoSelected!;
  Uint8List? _receituarioAgronomico;
  String? _nomeProduto = "";
  String? _classificacaoToxicologica = "";
  String? _classe = "";
  String? _tipoFormulacao = "";
  String? _alvoBiologico = "";
  TextEditingController? _doseProdutoComercialHectare;
  String? _unidadeDoseProdutoComercialHectare = "";
  TextEditingController? _adjuvante;
  TextEditingController? _tipoServico;

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      setState(() {});
      if (_aplicacao.caracteristicasProdutoAplicado?.tipoServico != null) {
        _caracteristicasProdutoAplicado =
            _aplicacao.caracteristicasProdutoAplicado;
        _nomeProduto = _caracteristicasProdutoAplicado!.nomeProduto;
        _classificacaoToxicologica = _aplicacao
            .caracteristicasProdutoAplicado!.classificacaoToxicologica;
        _classe = _caracteristicasProdutoAplicado!.classe;
        _tipoFormulacao = _caracteristicasProdutoAplicado!.tipoFormulacao;
        _alvoBiologico = _caracteristicasProdutoAplicado!.alvoBiologico;
        _doseProdutoComercialHectare = TextEditingController(
            text: _caracteristicasProdutoAplicado!.doseProdutoHectare);
        _unidadeDoseProdutoComercialHectare = _aplicacao
            .caracteristicasProdutoAplicado!.unidadeDoseProdutoHectare;
        _adjuvante = TextEditingController(
            text: _caracteristicasProdutoAplicado!.adjuvante);
        _tipoServico = TextEditingController(
            text: _caracteristicasProdutoAplicado!.tipoServico);
        setState(() {});
      }
    });
  }

  Future<bool> _verifiyFields() async {
    if (getIt<GlobalConfigVars>().selectedCultura.isEmpty) {
      Util.toastAlerta("Selecione a cultura");
      return false;
    } else if (_nomeProduto!.isEmpty) {
      Util.toastAlerta("Selecione a produto");
      return false;
    } else if (_classificacaoToxicologica!.isEmpty) {
      Util.toastAlerta("Selecione a classificação toxicológica");
      return false;
    } else if (_classe!.isEmpty) {
      Util.toastAlerta("Selecione a classe");
      return false;
    } else if (_tipoFormulacao!.isEmpty) {
      Util.toastAlerta("Selecione o tipo de formulação");
      return false;
    } else if (_doseProdutoComercialHectare!.text.isEmpty) {
      Util.toastAlerta("Digite a dose do produto comercial por hectare");
      return false;
    } else if (_unidadeDoseProdutoComercialHectare!.isEmpty) {
      Util.toastAlerta("Selecione a unidade da dosagem ");
      return false;
    } else if (_tipoServico!.text.isEmpty) {
      Util.toastAlerta("Digite o tipo de serviço");
      return false;
    } else if (_receituarioAgronomico!.isEmpty) {
      Util.toastAlerta("Insira a imagem do receituário agronômico");
      return false;
    } else {
      _caracteristicasProdutoAplicado = CaracteristicasProdutoAplicado(
          nomeProduto: _nomeProduto,
          classificacaoToxicologica: _classificacaoToxicologica,
          classe: _classe,
          tipoFormulacao: _tipoFormulacao,
          alvoBiologico: _alvoBiologico,
          doseProdutoHectare: _doseProdutoComercialHectare!.text,
          unidadeDoseProdutoHectare: _unidadeDoseProdutoComercialHectare!,
          adjuvante: _adjuvante!.text,
          tipoServico: _tipoServico!.text,
          receiturarioAgronomico: _receituarioAgronomico);
      Util.toastSucesso("Dados inseridos com sucesso");
      Navigator.pop(context);
      return true;
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
        title: const Text(
          "Características do\nproduto a ser aplicado",
          textAlign: TextAlign.center,
        ),
      ),
      body: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(16.0),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const CustomText(text: "Cultura"),
              const SizedBox(height: 10),
              CustomComboBoxExpanded(
                selectedName: getIt<GlobalConfigVars>().selectedCultura.isEmpty
                    ? "Selecione"
                    : getIt<GlobalConfigVars>().selectedCultura,
                onTap: () async {
                  Util.closeKeyBoard();
                  await showDialog(
                      context: context,
                      builder: (BuildContext context) {
                        return AlertDialog(
                            backgroundColor: const Color(0xFFF5F5F5),
                            content: CultureSelect(onChanged: (value) {
                              getIt<GlobalConfigVars>().selectedCultura = value;
                              setState(() {});
                              Util.closeKeyBoard();
                            }));
                      });
                },
              ),
              const SizedBox(height: 10),
              const CustomText(text: 'Enviar Receituário Agronômico'),
              const SizedBox(height: 10),
              InkWell(
                onTap: () async {
                  final imageMapsPath = await Util.obtainImagePathMaps(context);
                  _receituarioAgronomico =
                      await File(imageMapsPath).readAsBytes();
                  setState(() {});
                  Navigator.push(
                      // ignore: use_build_context_synchronously
                      context,
                      MaterialPageRoute(
                          builder: (context) => UploadFotos(
                                onOkButton: () {
                                  Navigator.pop(context);
                                },
                                updateImageData: (data) {
                                  _receituarioAgronomico = data;
                                  setState(() {});
                                },
                                imageData: _receituarioAgronomico,
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
              if (_receituarioAgronomico != null)
                Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const SizedBox(height: 15),
                    const CustomText(text: 'Imagem do Receituário Agronômico'),
                    const SizedBox(height: 15),
                    Container(
                        height: 300,
                        width: MediaQuery.of(context).size.width,
                        decoration: BoxDecoration(
                          image: DecorationImage(
                              image: MemoryImage(_receituarioAgronomico!),
                              fit: BoxFit.fill),
                        )),
                  ],
                ),
              const SizedBox(height: 10),
              const CustomText(text: 'Nome do produto'),
              const SizedBox(height: 10),
              CustomComboBoxExpanded(
                selectedName:
                    _nomeProduto!.isEmpty ? "Selecione" : _nomeProduto!,
                onTap: () async {
                  Util.closeKeyBoard();
                  await showDialog(
                      context: context,
                      builder: (BuildContext context) {
                        return AlertDialog(
                            backgroundColor: const Color(0xFFF5F5F5),
                            content: SizedBox(
                              width: double.maxFinite,
                              child: ProductNameSelect(
                                  onChangedProductName: (name, produto) {
                                setState(() {
                                  if (produto == null || name.isEmpty) return;
                                  _nomeProduto = name;
                                  _classificacaoToxicologica = produto
                                      .classificacaoToxicologica
                                      .toString();
                                  _classe = produto.classe.toString();
                                  _tipoFormulacao =
                                      produto.tipoFormulacao.toString();
                                  setState(() {});
                                });
                                Util.closeKeyBoard();
                              }),
                            ));
                      });
                },
              ),
              const SizedBox(height: 10),
              const CustomText(text: 'Classificação Toxicológica'),
              const SizedBox(height: 10),
              CustomComboBoxExpanded(
                selectedName: _classificacaoToxicologica!.isEmpty
                    ? "Selecione"
                    : _classificacaoToxicologica!,
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
                                _classificacaoToxicologica = value;
                              });
                            }));
                      });
                },
              ),
              const SizedBox(height: 10),
              const CustomText(text: 'Classe'),
              const SizedBox(height: 10),
              CustomComboBoxExpanded(
                selectedName: _classe!.isEmpty ? "Selecione" : _classe!,
                onTap: () async {
                  Util.closeKeyBoard();
                  await showDialog(
                      context: context,
                      builder: (BuildContext context) {
                        return AlertDialog(
                            backgroundColor: const Color(0xFFF5F5F5),
                            content: SizedBox(
                              width: double.maxFinite,
                              child: ClasseSelect(onChangedClasse: (value) {
                                setState(() {
                                  _classe = value;
                                });
                                Util.closeKeyBoard();
                              }),
                            ));
                      });
                },
              ),
              const SizedBox(height: 10),
              const CustomText(text: 'Tipo de Formulação'),
              const SizedBox(height: 10),
              CustomComboBoxExpanded(
                selectedName:
                    _tipoFormulacao!.isEmpty ? "Selecione" : _tipoFormulacao!,
                onTap: () async {
                  Util.closeKeyBoard();
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
                                  _tipoFormulacao = value;
                                });
                                Util.closeKeyBoard();
                              }),
                            ));
                      });
                },
              ),
              const SizedBox(height: 10),
              const CustomText(text: 'Alvo biológico'),
              const SizedBox(height: 10),
              CustomComboBoxExpanded(
                selectedName:
                    _alvoBiologico!.isEmpty ? "Selecione" : _alvoBiologico!,
                onTap: () async {
                  Util.closeKeyBoard();
                  await showDialog(
                      context: context,
                      builder: (BuildContext context) {
                        return AlertDialog(
                            backgroundColor: const Color(0xFFF5F5F5),
                            content: SizedBox(
                              width: double.maxFinite,
                              child: BiologicTargetSelect(onChanged: (value) {
                                setState(() {
                                  _alvoBiologico = value;
                                });
                                Util.closeKeyBoard();
                              }),
                            ));
                      });
                },
              ),
              const SizedBox(height: 10),
              const CustomText(text: "Dose do produto comercial por hectare"),
              const SizedBox(height: 10),
              CustomTextField(
                textEditingController: _doseProdutoComercialHectare,
                textInputType: TextInputType.number,
                onChanged: (value) {},
              ),
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Row(
                    children: [
                      const Text("ml/ha"),
                      Checkbox(
                          materialTapTargetSize:
                              MaterialTapTargetSize.shrinkWrap,
                          value: _unidadeDoseProdutoComercialHectare == "ml/ha",
                          onChanged: (value) {
                            setState(() {
                              _unidadeDoseProdutoComercialHectare = "ml/ha";
                            });
                          }),
                    ],
                  ),
                  Flexible(
                    child: Row(
                      children: [
                        const Text("L/ha"),
                        Checkbox(
                            materialTapTargetSize:
                                MaterialTapTargetSize.shrinkWrap,
                            value:
                                _unidadeDoseProdutoComercialHectare == "L/ha",
                            onChanged: (value) {
                              setState(() {
                                _unidadeDoseProdutoComercialHectare = "L/ha";
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
                            value:
                                _unidadeDoseProdutoComercialHectare == "g/ha",
                            onChanged: (value) {
                              setState(() {
                                _unidadeDoseProdutoComercialHectare = "g/ha";
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
                            value:
                                _unidadeDoseProdutoComercialHectare == "Kg/ha",
                            onChanged: (value) {
                              setState(() {
                                _unidadeDoseProdutoComercialHectare = "Kg/ha";
                              });
                            }),
                      ],
                    ),
                  )
                ],
              ),
              const SizedBox(height: 5),
              const CustomText(text: "Ajuvante"),
              const SizedBox(height: 10),
              CustomTextField(
                textEditingController: _adjuvante,
                onChanged: (value) {},
              ),
              const SizedBox(height: 5),
              const CustomText(text: "Tipo de Serviço"),
              const SizedBox(height: 10),
              CustomTextField(
                textEditingController: _tipoServico,
                onChanged: (value) {},
              ),
              Center(
                child: CustomButton(
                  title: "OK",
                  onClick: () async {
                    await _verifiyFields();
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
