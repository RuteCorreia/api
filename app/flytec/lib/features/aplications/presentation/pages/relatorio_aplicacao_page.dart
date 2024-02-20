import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/data/models/relatorio_model.dart';
import 'package:flytec/features/aplications/presentation/widgets/logs_select.dart';
import 'package:flytec/features/aplications/presentation/widgets/pista_select.dart';
import 'package:flytec/features/auth/presentation/widgets/custom_login_button.dart';
import 'package:go_router/go_router.dart';
import 'package:location/location.dart' as lct;

import '../../../../core/widgets/combo_box.dart';
import '../widgets/culture_select.dart';
import '../widgets/product_name_select.dart';
import 'my_activity_page.dart';

class RelatorioAplicacaoPage extends StatefulWidget {
  const RelatorioAplicacaoPage({super.key});

  @override
  State<RelatorioAplicacaoPage> createState() => _RelatorioAplicacaoPageState();
}

enum VolumeUnidade { LH, KG, NENHUM }

enum DosagemUnidade { ML, L, G, KG, NENHUM }

class _RelatorioAplicacaoPageState extends State<RelatorioAplicacaoPage> {
  VolumeUnidade _volumeUnidade = VolumeUnidade.NENHUM;
  DosagemUnidade _dosagemUnidade = DosagemUnidade.NENHUM;
  final TextEditingController _latitudeController = TextEditingController();
  final TextEditingController _longitudeController = TextEditingController();
  final TextEditingController _dosagemController = TextEditingController();
  final TextEditingController _volumeAplicado = TextEditingController();
  final TextEditingController _totalAreaAplicada = TextEditingController();

  final TextEditingController _densidadeController = TextEditingController();

  final List<String> _observations = [];
  final List<int> _observationsIndex = [];
  final TextEditingController _observationTextField = TextEditingController();
  String _selectedLog = "Selecione";

  String produtoSelecionado = "";
  String selectedPista = "";
  String getDosagemUnidade({DosagemUnidade? dosagemUnidade}) {
    switch (dosagemUnidade) {
      case DosagemUnidade.L:
        return "L/ha";
      case DosagemUnidade.KG:
        return "Kg/ha";
      case DosagemUnidade.ML:
        return "ml/ha";
      case DosagemUnidade.G:
        return "g/ha";
      default:
        return "Unknown";
    }
  }

  bool verifyFields() {
    // if (getIt<GlobalConfigVars>().selectedCultura.isEmpty) {
    //   Util.toastAlerta("Selecione a cultura");
    //   return false;
    // } else if (produtoSelecionado.isEmpty) {
    //   Util.toastAlerta("Selecione o produto aplicado");
    //   return false;
    // } else if (_dosagemController.text.isEmpty) {
    //   Util.toastAlerta("Digite a dosagem");
    //   return false;
    if (_dosagemUnidade == DosagemUnidade.NENHUM) {
      Util.toastAlerta("Selecione a unidade da dosagem");
      return false;
    }
    // else if (_volumeAplicado.text.isEmpty) {
    //   Util.toastAlerta("Digite o volume aplicado");
    //   return false;
    else if (_volumeUnidade == VolumeUnidade.NENHUM) {
      Util.toastAlerta("Selecione a unidade do volume de aplicação");
      return false;
    }
    // } else if (_totalAreaAplicada.text.isEmpty) {
    //   Util.toastAlerta("Digite o total da área aplicada");
    //   return false;
    // } else if (selectedPista.isEmpty) {
    //   Util.toastAlerta("Selecione a pista");
    //   return false;
    // } else if (_latitudeController.text.isEmpty) {
    //   Util.toastAlerta("Digite a latitude");
    //   return false;
    // } else if (_longitudeController.text.isEmpty) {
    //   Util.toastAlerta("Digite a longitude");
    //   return false;
    // } else if (_densidadeController.text.isEmpty) {
    //   Util.toastAlerta("Digite a densidade");
    //   return false;
    // } else {
      getIt<GlobalConfigVars>().reportList.last.relatorioDeAplicacao =
          RelatorioDeAplicacao(
        cultura: getIt<GlobalConfigVars>().selectedCultura,
        dosagem: _dosagemController.text,
        latitudeSul: _latitudeController.text,
        densidade: _densidadeController.text,
        log: _selectedLog,
        longitudeOeste: _longitudeController.text,
        localizacaoPista: selectedPista,
        observacoes: _observationTextField.text,
        totalAreaAplicada: _totalAreaAplicada.text,
        produtoAplicado: produtoSelecionado,
        volumeDeAplicacao: _volumeAplicado.text,
        unidadeVolume: _volumeUnidade == VolumeUnidade.KG ? "Kg/ha" : "L/ha",
        unidadeDosagem: getDosagemUnidade(dosagemUnidade: _dosagemUnidade),
      );
      setState(() {});
      Util.toastSucesso("Dados salvo com sucesso");
      return true;
    //}
  }

  @override
  void initState() {
    super.initState();
    var data = getIt<GlobalConfigVars>().reportList.last.relatorioDeAplicacao;

    produtoSelecionado = data!.produtoAplicado!;
    _dosagemController.text = data.dosagem!;
    _volumeAplicado.text = data.volumeDeAplicacao!;
    _totalAreaAplicada.text = data.totalAreaAplicada!;
    _longitudeController.text = data.longitudeOeste!;
    _latitudeController.text = data.latitudeSul!;
    _observationTextField.text = data.observacoes!;
    selectedPista = data.localizacaoPista!;
    if (getIt<GlobalConfigVars>().selectedCultura.isNotEmpty) {
      data.cultura = getIt<GlobalConfigVars>().selectedCultura;
    }
  }

  @override
  Widget build(BuildContext context) {
    var data = getIt<GlobalConfigVars>().reportList.last.relatorioDeAplicacao;

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
                              getIt<GlobalConfigVars>().selectedCultura = value;
                              data!.cultura = value;
                              setState(() {});
                              Util.closeKeyBoard();
                            }),
                          ));
                    });
              },
            ),
            const SizedBox(height: 20),
            const CustomText(text: 'Produto aplicado'),
            const SizedBox(height: 14),
            CustomComboBoxExpanded(
              selectedName:
                  produtoSelecionado.isEmpty ? "Selecione" : produtoSelecionado,
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
                                data!.produtoAplicado = value;
                              });
                              Util.closeKeyBoard();
                            }),
                          ));
                    });
              },
            ),
            const SizedBox(height: 10),
            const CustomText(text: 'Dosagem'),
            const SizedBox(height: 14),
            Container(
              width: (MediaQuery.of(context).size.width / 2) - 25,
              height: 50,
              padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 10),
              decoration: ShapeDecoration(
                shape: RoundedRectangleBorder(
                  side: const BorderSide(width: 1, color: Color(0xFF636363)),
                  borderRadius: BorderRadius.circular(10),
                ),
              ),
              child: TextField(
                controller: _dosagemController,
                onChanged: (value) {
                  data!.dosagem = value;
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
                              data!.unidadeDosagem = "ml/ha";
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
                          value: _dosagemUnidade == DosagemUnidade.L,
                          onChanged: (value) {
                            setState(() {
                              _dosagemUnidade = DosagemUnidade.L;
                              data!.unidadeDosagem = "L/ha";
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
                          value: _dosagemUnidade == DosagemUnidade.G,
                          onChanged: (value) {
                            setState(() {
                              _dosagemUnidade = DosagemUnidade.G;
                              data!.unidadeDosagem = "g/ha";
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
                              data!.unidadeDosagem = "Kg/ha";
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
            Container(
              width: (MediaQuery.of(context).size.width / 2) - 25,
              height: 50,
              padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 10),
              decoration: ShapeDecoration(
                shape: RoundedRectangleBorder(
                  side: const BorderSide(width: 1, color: Color(0xFF636363)),
                  borderRadius: BorderRadius.circular(10),
                ),
              ),
              child: TextField(
                controller: _volumeAplicado,
                onChanged: (value) {
                  data!.volumeDeAplicacao = value;
                  setState(() {});
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
                            data!.unidadeVolume = "L/ha";
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
                              data!.unidadeVolume = "Kg/ha";
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
            Container(
              width: (MediaQuery.of(context).size.width / 2) - 25,
              height: 50,
              padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 10),
              decoration: ShapeDecoration(
                shape: RoundedRectangleBorder(
                  side: const BorderSide(width: 1, color: Color(0xFF636363)),
                  borderRadius: BorderRadius.circular(10),
                ),
              ),
              child: TextField(
                controller: _totalAreaAplicada,
                onChanged: (value) {
                  data!.totalAreaAplicada = value;
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
            const SizedBox(height: 14),
            const CustomText(text: 'Localização da pista/Código ICAO'),
            const SizedBox(height: 14),
            CustomComboBoxExpanded(
              selectedName: selectedPista.isEmpty ? "Selecione" : selectedPista,
              onTap: () async {
                await showDialog(
                    context: context,
                    builder: (BuildContext context) {
                      return AlertDialog(
                          backgroundColor: const Color(0xFFF5F5F5),
                          content: SizedBox(
                            width: double.maxFinite,
                            child: PistaSelect(onChangedClasse: (value) {
                              setState(() {
                                selectedPista = value;
                                data!.localizacaoPista = value;
                              });
                              Util.closeKeyBoard();
                            }),
                          ));
                    });
              },
            ),
            const SizedBox(height: 20),
            CustomGpsButton(onClick: () async {
              lct.Location local = lct.Location();
              local.getLocation().then((lc) async {
                setState(() {
                  _latitudeController.text = lc.latitude.toString();
                  _longitudeController.text = lc.longitude.toString();
                  data!.latitudeSul = lc.latitude.toString();
                  data.longitudeOeste = lc.longitude.toString();
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
                        onChanged: (value) {
                          data!.latitudeSul = value;
                        },
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
                        onChanged: (value) {
                          data!.longitudeOeste = value;
                          setState(() {});
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
            const CustomText(text: 'Densidade'),
            const SizedBox(height: 14),
            Container(
              width: (MediaQuery.of(context).size.width / 2) - 25,
              height: 50,
              padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 10),
              decoration: ShapeDecoration(
                shape: RoundedRectangleBorder(
                  side: const BorderSide(width: 1, color: Color(0xFF636363)),
                  borderRadius: BorderRadius.circular(10),
                ),
              ),
              child: TextField(
                controller: _densidadeController,
                keyboardType: TextInputType.number,
                inputFormatters: [FilteringTextInputFormatter.digitsOnly],
                onChanged: (value) {
                  data!.densidade = value;
                  setState(() {});
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
              child: TextField(
                minLines: 4,
                maxLines: 4,
                controller: _observationTextField,
                onChanged: (value) {
                  data!.observacoes = value;
                },
                textInputAction: TextInputAction.done,
                onSubmitted: (value) {
                  if (_observationTextField.text.isEmpty) return;
                  try {
                    _observations.add(_observationTextField.text);
                    _observationsIndex.add(_observations.length);
                    _observationTextField.clear();
                    setState(() {});
                    Util.toastSucesso("Observação adicionada com sucesso!");
                  } catch (e) {
                    Util.toastErro("Observação não foi adicionada");
                  }
                },
                decoration: const InputDecoration(
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
                InkWell(
                  onTap: () {
                    _observationTextField.clear();
                    setState(() {});
                  },
                  child: Container(
                    height: 40,
                    padding: const EdgeInsets.only(
                        top: 8, left: 20, right: 24, bottom: 8),
                    decoration: ShapeDecoration(
                      shape: RoundedRectangleBorder(
                        side: const BorderSide(
                            width: 2, color: Color(0xFFC21B43)),
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
                ),
                InkWell(
                  onTap: () {
                    try {
                      verifyFields();
                    } catch (e) {}
                  },
                  child: Container(
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
                  ),
                )
              ],
            ),
            const SizedBox(height: 20),
            const CustomText(
              text: "Relatório do DGPS (Log’s)",
            ),
            const SizedBox(height: 13),
            InkWell(
                onTap: () async {
                  await showDialog(
                      context: context,
                      builder: (BuildContext context) {
                        return AlertDialog(
                            backgroundColor: const Color(0xFFF5F5F5),
                            content: SizedBox(
                              width: double.maxFinite,
                              child: LogsSelect(onChanged: (value) {
                                setState(() {
                                  _selectedLog = value;
                                  getIt<GlobalConfigVars>().dgs = value;
                                });
                                Util.closeKeyBoard();
                              }),
                            ));
                      });
                },
                child: ComboBox(selectedName: _selectedLog)),
            Center(
              child: CustomButton(
                title: "APLICAÇÕES",
                onClick: () {
                  context.push("/minhasaplicacoes");
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
  final TextInputType textInputType;
  const CustomTextField({super.key, this.textInputType = TextInputType.text});

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
      child: TextField(
        keyboardType: textInputType,
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
