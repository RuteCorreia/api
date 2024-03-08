import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications_v2/components/components_exports.dart';
import 'package:flytec/features/aplications_v2/controller/report_aplication_controller.dart';
import 'package:flytec/features/aplications_v2/models/aplicacao.dart';
import 'package:flytec/features/aplications_v2/models/relatorio_aplicacao.dart';
import 'package:flytec/features/aplications_v2/pages/aplicacoes_list_page.dart';
import 'package:location/location.dart' as lct;

class RelatorioAplicacaoPage extends StatefulWidget {
  final ReportAplicationController _reportAplicationController;
  const RelatorioAplicacaoPage(
      {required ReportAplicationController reportAplicationController,
      super.key})
      : _reportAplicationController = reportAplicationController;

  @override
  State<RelatorioAplicacaoPage> createState() => _RelatorioAplicacaoPageState();
}

class _RelatorioAplicacaoPageState extends State<RelatorioAplicacaoPage> {
  RelatorioAplicacao? _relatorioAplicacao;

  Aplicacao get _aplicacao =>
      widget._reportAplicationController.aplicacaoSelected!;

  String _volumeUnidade = "";
  String _dosagemUnidade = "";
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
  String _produtoSelecionado = "";
  String _selectedPista = "";

  Future<bool> _verifyFields() async {
    if (_dosagemUnidade.isEmpty) {
      Util.toastAlerta("Selecione a unidade da dosagem");
      return false;
    } else if (_volumeUnidade.isEmpty) {
      Util.toastAlerta("Selecione a unidade do volume de aplicação");
      return false;
    }
    await _relatorioAplicacaoAction();
    setState(() {});
    Util.toastSucesso("Dados salvo com sucesso");
    return true;
  }

  Future<void> _relatorioAplicacaoAction() async {
    _relatorioAplicacao = RelatorioAplicacao(
        cultura: getIt<GlobalConfigVars>().selectedCultura,
        produtoAplicado: _produtoSelecionado,
        dosagem: _dosagemController.text,
        unidadeDosagem: _dosagemUnidade,
        volumeAplicacao: _volumeAplicado.text,
        unidadeVolumeAplicacao: _volumeUnidade,
        totalAreaAplicada: _totalAreaAplicada.text,
        localizacaoPistaCodigoICAO: _selectedPista,
        lat: _latitudeController.text,
        long: _longitudeController.text,
        densidade: _densidadeController.text,
        observacoes: _observations.join("\n"),
        relatorioDGPS: _selectedLog);
    if (_aplicacao.relatorioAplicacao?.id == null) {
      int? idRelatorioAplicacao = await widget._reportAplicationController
          .createElementInTable(
              _relatorioAplicacao!.toMap(), 'RelatorioAplicacao');
      await widget._reportAplicationController.updateElementInTable(
          _aplicacao.id!,
          {'relatorioAplicacao_id': idRelatorioAplicacao},
          'Aplicacao');
      await _updateRelatorioAplicacao(idRelatorioAplicacao!);
      return;
    }
    int? idRelatorioAplicacao = _aplicacao.relatorioAplicacao?.id;
    await widget._reportAplicationController.updateElementInTable(
        idRelatorioAplicacao!,
        _relatorioAplicacao!.toMap(),
        'RelatorioAplicacao');
    await _updateRelatorioAplicacao(idRelatorioAplicacao);
  }

  Future<void> _updateRelatorioAplicacao(int id) async {
    final element = await widget._reportAplicationController
        .getElementById(id, 'RelatorioAplicacao');

    final relatorioAplicacao = RelatorioAplicacao.fromJson(element);
    _relatorioAplicacao = relatorioAplicacao;
    setState(() {});
    _aplicacao.relatorioAplicacao = relatorioAplicacao;
    widget._reportAplicationController.setAplicacaoSelected(_aplicacao);
    setState(() {});
  }

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      if (_aplicacao.relatorioAplicacao?.id != null) {
        _relatorioAplicacao = _aplicacao.relatorioAplicacao!;
        _produtoSelecionado = _relatorioAplicacao!.produtoAplicado!;
        _dosagemController.text = _relatorioAplicacao!.dosagem!;
        _dosagemUnidade = _relatorioAplicacao!.unidadeDosagem!;
        _volumeAplicado.text = _relatorioAplicacao!.volumeAplicacao!;
        _volumeUnidade = _relatorioAplicacao!.unidadeVolumeAplicacao!;
        _totalAreaAplicada.text = _relatorioAplicacao!.totalAreaAplicada!;
        _selectedPista = _relatorioAplicacao!.localizacaoPistaCodigoICAO!;
        _latitudeController.text = _relatorioAplicacao!.lat!;
        _longitudeController.text = _relatorioAplicacao!.long!;
        _densidadeController.text = _relatorioAplicacao!.densidade!;
        _observations.add(_relatorioAplicacao!.observacoes!);
        setState(() {});
      }
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
          centerTitle: true,
          title: const Text(
            "Relatório de aplicação",
            textAlign: TextAlign.center,
          ),
          leading: IconButton(
            icon: const Icon(Icons.arrow_back),
            onPressed: () async {
              await _relatorioAplicacaoAction();
              // ignore: use_build_context_synchronously
              Navigator.pop(context);
            },
          )),
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: ListView(
          children: [
            const CustomText(text: 'Cultura'),
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
                          content: SizedBox(
                            width: double.maxFinite,
                            child: CultureSelect(onChanged: (value) {
                              getIt<GlobalConfigVars>().selectedCultura = value;
                              setState(() {});
                              Util.closeKeyBoard();
                            }),
                          ));
                    });
              },
            ),
            const SizedBox(height: 10),
            const CustomText(text: 'Produto aplicado'),
            const SizedBox(height: 10),
            CustomComboBoxExpanded(
              selectedName: _produtoSelecionado.isEmpty
                  ? "Selecione"
                  : _produtoSelecionado,
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
                                onChangedProductName: (value, produto) {
                              setState(() {
                                _produtoSelecionado = value;
                              });
                              Util.closeKeyBoard();
                            }),
                          ));
                    });
              },
            ),
            const SizedBox(height: 10),
            const CustomText(text: 'Dosagem'),
            const SizedBox(height: 10),
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
                onChanged: (value) {},
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
                          value: _dosagemUnidade == "ml/ha",
                          onChanged: (value) {
                            setState(() {
                              _dosagemUnidade = "ml/ha";
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
                          value: _dosagemUnidade == "L/ha",
                          onChanged: (value) {
                            setState(() {
                              _dosagemUnidade = "L/ha";
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
                          value: _dosagemUnidade == "g/ha",
                          onChanged: (value) {
                            setState(() {
                              _dosagemUnidade = "g/ha";
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
                          value: _dosagemUnidade == "Kg/ha",
                          onChanged: (value) {
                            setState(() {
                              _dosagemUnidade = "Kg/ha";
                            });
                          }),
                    ],
                  ),
                )
              ],
            ),
            const SizedBox(height: 10),
            const CustomText(text: 'Volume de aplicação'),
            const SizedBox(height: 10),
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
                onChanged: (value) {},
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
                        value: _volumeUnidade == "L/ha",
                        onChanged: (value) {
                          setState(() {
                            _volumeUnidade = "L/ha";
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
                          value: _volumeUnidade == "Kg/ha",
                          onChanged: (value) {
                            setState(() {
                              _volumeUnidade = "Kg/ha";
                            });
                          }),
                    ],
                  ),
                )
              ],
            ),
            const SizedBox(height: 10),
            const CustomText(text: 'Total da área aplicada (ha)'),
            const SizedBox(height: 10),
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
                onChanged: (value) {},
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
            const SizedBox(height: 10),
            const CustomText(text: 'Localização da pista/Código ICAO'),
            const SizedBox(height: 10),
            CustomComboBoxExpanded(
              selectedName:
                  _selectedPista.isEmpty ? "Selecione" : _selectedPista,
              onTap: () async {
                Util.closeKeyBoard();
                await showDialog(
                    context: context,
                    builder: (BuildContext context) {
                      return AlertDialog(
                          backgroundColor: const Color(0xFFF5F5F5),
                          content: SizedBox(
                            width: double.maxFinite,
                            child: PistaSelect(onChangedClasse: (value) {
                              setState(() {
                                _selectedPista = value;
                              });
                              Util.closeKeyBoard();
                            }),
                          ));
                    });
              },
            ),
            const SizedBox(height: 10),
            GestureDetector(
              onTap: () async {
                lct.Location local = lct.Location();
                local.getLocation().then((lc) async {
                  setState(() {
                    _latitudeController.text = lc.latitude.toString();
                    _longitudeController.text = lc.longitude.toString();
                  });
                });
              },
              child: Container(
                width: 140,
                height: 45,
                padding:
                    const EdgeInsets.symmetric(horizontal: 24, vertical: 8),
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
            ),
            const SizedBox(height: 10),
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
                        onChanged: (value) {},
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
                        onChanged: (value) {},
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
            const SizedBox(height: 10),
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
                keyboardType: const TextInputType.numberWithOptions(
                    decimal: true, signed: true),
                onChanged: (value) {
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
            const SizedBox(height: 10),
            const CustomText(text: "Alterações do planejamento / Observações "),
            const SizedBox(height: 10),
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
                onChanged: (value) {},
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
            const SizedBox(height: 10),
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
                  onTap: () async {
                    await _verifyFields();
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
            const SizedBox(height: 10),
            const CustomText(
              text: "Relatório do DGPS (Log’s)",
            ),
            const SizedBox(height: 10),
            CustomCombo(
              selectedName: _selectedLog,
              onTap: () async {
                Util.closeKeyBoard();
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
                                getIt<GlobalConfigVars>()
                                    .reportList
                                    .last
                                    .relatorioDeAplicacao
                                    ?.log = value;
                                getIt<GlobalConfigVars>().dgs = value;
                              });
                              Util.closeKeyBoard();
                            }),
                          ));
                    });
              },
            ),
            Center(
              child: CustomButton(
                title: "APLICAÇÕES",
                onClick: () {
                  Navigator.push(context, MaterialPageRoute(builder: (context) {
                    return AplicacoesListPage(
                        reportAplicationController:
                            widget._reportAplicationController);
                  }));
                },
              ),
            ),
          ],
        ),
      ),
    );
  }
}
