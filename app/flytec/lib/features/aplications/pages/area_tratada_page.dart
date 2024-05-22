import 'dart:convert';
import 'dart:io';
import 'dart:typed_data';

import 'package:dropdown_button2/dropdown_button2.dart';
import 'package:flutter/material.dart';
import 'package:flutter_pdfview/flutter_pdfview.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/controller/maps_informations_controller.dart';
import 'package:flytec/features/aplications/controller/report_aplication_controller.dart';
import 'package:flytec/features/aplications/models/aplicacao.dart';
import 'package:flytec/features/aplications/models/identificacao_area_tratada.dart';
import 'package:flytec/features/aplications/components/components_exports.dart';
import 'package:flytec/features/aplications/pages/images/croqui_area/croqui_area.dart';
import 'package:path_provider/path_provider.dart';

class AreaTratada extends StatefulWidget {
  final ReportAplicationController _reportAplicationController;
  const AreaTratada(
      {required ReportAplicationController reportAplicationController,
      super.key})
      : _reportAplicationController = reportAplicationController;

  @override
  State<AreaTratada> createState() => _AreaTratadaState();
}

class _AreaTratadaState extends State<AreaTratada> {
  IdentificacaoAreaTratada? _areaTratada;

  Aplicacao get _aplicacao =>
      widget._reportAplicationController.aplicacaoSelected!;

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) async {
      _obtainStatesOfBrazil();
      _obtainCitiesOfUfBrazil('SP');
      setState(() {});
      if (_aplicacao.identificacaoAreaTratada?.id != null) {
        _areaTratada = _aplicacao.identificacaoAreaTratada!;
        _localizacaoController =
            TextEditingController(text: _areaTratada!.localizacao);
        _extensaoController =
            TextEditingController(text: _areaTratada!.extensao.toString());
        _uf = (_areaTratada!.uf!.isNotEmpty ? _areaTratada?.uf : 'SP')!;
        _cityOfUf = _areaTratada?.cidade ?? '';
        if (_cityOfUf.isEmpty) {
          await _obtainCitiesOfUfBrazil(_uf);
        }
        _isPdf = _areaTratada!.isPdf!;
        _imageData = _areaTratada!.croquiArea;
        setState(() {});
      }
    });
  }

  Future<void> _verifyFields() async {
    if (_localizacaoController.text.isEmpty) {
      Util.toastAlerta("Digite a localizacao");
      return;
    } else if (getIt<GlobalConfigVars>().selectedCultura.isEmpty) {
      Util.toastAlerta("Selecione a cultura");
      return;
    } else if (_extensaoController.text.isEmpty) {
      Util.toastAlerta("Selecione a extensão");
      return;
    } else if (_imageData == null) {
      Util.toastAlerta("Selecione a imagem da área");
      return;
    } else {
      await _areaTratadaAction();
      Util.toastSucesso("Dados inseridos com sucesso!");
      // ignore: use_build_context_synchronously
      Navigator.pop(context);
    }
  }

  Future<void> _areaTratadaAction() async {
    _areaTratada = IdentificacaoAreaTratada(
      localizacao: _localizacaoController.text,
      extensao: _extensaoController.text,
      uf: _uf,
      cidade: _cityOfUf,
      croquiArea: _imageData,
      isPdf: _isPdf,
      cultura: getIt<GlobalConfigVars>().selectedCultura,
    );

    if (_aplicacao.identificacaoAreaTratada?.id == null ||
        _aplicacao.identificacaoAreaTratada!.id! <= 0) {
      int? idAreaTratada = await widget._reportAplicationController
          .createElementInTable(
              _areaTratada!.toMap(), 'IdentificacaoAreaTratada');
      await widget._reportAplicationController.updateElementInTable(
          _aplicacao.id!,
          {'identificacaoAreaTratada_id': idAreaTratada},
          'Aplicacao');
      await _updateIdentificacao(idAreaTratada!);
      return;
    }
    int? idAreaTratada = _aplicacao.identificacaoAreaTratada?.id;
    await widget._reportAplicationController.updateElementInTable(
        idAreaTratada!, _areaTratada!.toMap(), 'IdentificacaoAreaTratada');
    await _updateIdentificacao(idAreaTratada);
  }

  Future<void> _updateIdentificacao(int id) async {
    final element = await widget._reportAplicationController
        .getElementById(id, 'IdentificacaoAreaTratada');
    final identificacaoAreaTratada = IdentificacaoAreaTratada.fromJson(element);
    _areaTratada = identificacaoAreaTratada;
    setState(() {});
    _aplicacao.identificacaoAreaTratada = identificacaoAreaTratada;
    widget._reportAplicationController.setAplicacaoSelected(_aplicacao);
    setState(() {});
  }

  final MapsInformationsController _mapsInformationsController =
      MapsInformationsControllerBrazil();

  final TextEditingController _citySearchController = TextEditingController();
  TextEditingController _localizacaoController = TextEditingController();
  TextEditingController _extensaoController = TextEditingController();
  late String _cityOfUf = '';
  String _uf = 'SP';
  String? _imageData;
  bool _isPdf = false;

  final List<String> _citiesNamesUfBrazil = [];
  List<String> _statesOfBrazil = [];

  void _obtainStatesOfBrazil() {
    _statesOfBrazil = _mapsInformationsController.getStatesBrazil;
    setState(() {});
  }

  Future<void> _obtainCitiesOfUfBrazil(String uf) async {
    _citiesNamesUfBrazil.clear();
    List<String> cities =
        _mapsInformationsController.obtainCitiesFromStateBrazil(uf);
    _cityOfUf = cities.first;
    setState(() {});
    _citiesNamesUfBrazil.addAll(cities);
    setState(() {});
  }

  void _updateImageDate(Uint8List data, File file, bool isPdf) {
    _imageData = isPdf ? file.path : base64Encode(data);
    _isPdf = isPdf;
    setState(() {});
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
          centerTitle: true,
          title: const Text(
            "Identificação da área \na ser tratada",
            textAlign: TextAlign.center,
          ),
          leading: IconButton(
            icon: const Icon(Icons.arrow_back),
            onPressed: () async {
              await _areaTratadaAction();
              // ignore: use_build_context_synchronously
              Navigator.pop(context);
            },
          )),
      body: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(16.0),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      const CustomText(text: "UF"),
                      const SizedBox(height: 10),
                      Container(
                          decoration: BoxDecoration(
                            border: Border.all(
                                width: 1, color: const Color(0xFF636363)),
                            borderRadius: BorderRadius.circular(10.0),
                          ),
                          alignment: Alignment.center,
                          width: 100,
                          height: 40,
                          padding: const EdgeInsets.symmetric(horizontal: 8.0),
                          child: DropdownButton<String>(
                            onChanged: (regiaoSelecionada) async {
                              _uf = regiaoSelecionada!;
                              await _obtainCitiesOfUfBrazil(regiaoSelecionada);
                              setState(() {});
                            },
                            alignment: Alignment.center,
                            disabledHint: const SizedBox.shrink(),
                            underline: const SizedBox.shrink(),
                            value: _uf,
                            icon: const Icon(
                              Icons.keyboard_arrow_down,
                              color: Colors.black,
                            ),
                            items: _statesOfBrazil.map((String regiao) {
                              return DropdownMenuItem(
                                value: regiao,
                                child: Text(
                                  regiao,
                                  style:
                                      const TextStyle(color: Color(0xFF636363)),
                                ),
                              );
                            }).toList(),
                          ))
                    ],
                  ),
                  Builder(
                    builder: (context) {
                      if (_citiesNamesUfBrazil.isNotEmpty &&
                          _cityOfUf.isNotEmpty) {
                        return Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            const CustomText(text: "Cidade"),
                            const SizedBox(height: 10),
                            DropdownButtonHideUnderline(
                              child: DropdownButton2<String>(
                                isExpanded: true,
                                items: _citiesNamesUfBrazil
                                    .map((item) => DropdownMenuItem(
                                          value: item,
                                          child: Text(
                                            item,
                                            style: const TextStyle(
                                              fontSize: 14,
                                            ),
                                          ),
                                        ))
                                    .toList(),
                                value: _cityOfUf,
                                onChanged: (value) {
                                  setState(() {
                                    _cityOfUf = value!;
                                  });
                                },
                                buttonStyleData: ButtonStyleData(
                                  padding: const EdgeInsets.symmetric(
                                      horizontal: 12.0),
                                  height: 40,
                                  decoration: BoxDecoration(
                                    border: Border.all(
                                        width: 1,
                                        color: const Color(0xFF636363)),
                                    borderRadius: BorderRadius.circular(10.0),
                                  ),
                                  width: 180,
                                ),
                                dropdownStyleData: const DropdownStyleData(
                                  maxHeight: 200,
                                  padding: EdgeInsets.all(0),
                                ),
                                menuItemStyleData: const MenuItemStyleData(
                                  height: 40,
                                ),
                                dropdownSearchData: DropdownSearchData(
                                  searchController: _citySearchController,
                                  searchInnerWidgetHeight: 50,
                                  searchInnerWidget: Container(
                                    height: 50,
                                    padding: const EdgeInsets.only(
                                      right: 8,
                                      top: 4.0,
                                      bottom: 4.0,
                                      left: 8,
                                    ),
                                    child: TextFormField(
                                      controller: _citySearchController,
                                      decoration: InputDecoration(
                                        isDense: true,
                                        hintText: 'Digite a cidade',
                                        hintStyle:
                                            const TextStyle(fontSize: 12),
                                        border: OutlineInputBorder(
                                          borderRadius:
                                              BorderRadius.circular(8),
                                        ),
                                      ),
                                    ),
                                  ),
                                  searchMatchFn: (item, searchValue) {
                                    return item.value
                                        .toString()
                                        .toLowerCase()
                                        .contains(searchValue.toLowerCase());
                                  },
                                ),
                                onMenuStateChange: (isOpen) {
                                  if (!isOpen) {
                                    _citySearchController.clear();
                                  }
                                },
                              ),
                            ),
                          ],
                        );
                      }
                      return const SizedBox.shrink();
                    },
                  ),
                ],
              ),
              const SizedBox(height: 10),
              const CustomText(text: "Localização"),
              const SizedBox(height: 10),
              CustomTextField(
                textEditingController: _localizacaoController,
                onChanged: (value) {},
              ),
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
                      useSafeArea: true,
                      builder: (BuildContext context) {
                        return AlertDialog(
                            scrollable: true,
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
              const CustomText(text: "Extensão(ha)"),
              const SizedBox(height: 10),
              CustomTextField(
                textEditingController: _extensaoController,
                onChanged: (value) {},
                textInputType: TextInputType.number,
              ),
              const SizedBox(height: 10),
              _imageData != null
                  ? _isPdf
                      ? Column(
                          crossAxisAlignment: CrossAxisAlignment.center,
                          children: [
                              const Align(
                                alignment: Alignment.centerLeft,
                                child: Padding(
                                  padding: EdgeInsets.only(bottom: 16.0),
                                  child: CustomText(text: 'PDF Selecionado'),
                                ),
                              ),
                              SizedBox(
                                height: 300,
                                width: MediaQuery.of(context).size.width,
                                child: PDFView(
                                  filePath: _imageData,
                                ),
                              ),
                              TextButton(
                                onPressed: () {
                                  _imageData = null;
                                  setState(() {});
                                  Navigator.push(
                                      context,
                                      MaterialPageRoute(
                                          builder: (context) => CroquiArea(
                                                updateImageData:
                                                    (Uint8List? data,
                                                        bool isPdf) async {
                                                  final directory =
                                                      await getApplicationCacheDirectory();
                                                  File file = File(
                                                      "${directory.path}/pdf_${Util.getRandomString(10)}.pdf");
                                                  await file
                                                      .writeAsBytes(data!);
                                                  _updateImageDate(
                                                      data, file, isPdf);
                                                },
                                              )));
                                  setState(() {});
                                },
                                child: const CustomText(
                                    text:
                                        'Clique aqui para selecionar outro PDF'),
                              )
                            ])
                      : Column(
                          crossAxisAlignment: CrossAxisAlignment.center,
                          children: [
                              const Align(
                                alignment: Alignment.centerLeft,
                                child: Padding(
                                  padding: EdgeInsets.only(bottom: 16.0),
                                  child: CustomText(text: 'Imagem Selecionada'),
                                ),
                              ),
                              Container(
                                  height: 300,
                                  width: MediaQuery.of(context).size.width,
                                  decoration: BoxDecoration(
                                    image: DecorationImage(
                                        image: MemoryImage(
                                            base64Decode(_imageData!)),
                                        fit: BoxFit.fill),
                                  )),
                              TextButton(
                                onPressed: () {
                                  _imageData = null;
                                  setState(() {});
                                  Navigator.push(
                                      context,
                                      MaterialPageRoute(
                                          builder: (context) => CroquiArea(
                                                updateImageData:
                                                    (Uint8List? data,
                                                        bool isPdf) async {
                                                  final directory =
                                                      await getApplicationCacheDirectory();
                                                  File file = File(
                                                      "${directory.path}/pdf_${Util.getRandomString(10)}.pdf");
                                                  await file
                                                      .writeAsBytes(data!);
                                                  _updateImageDate(
                                                      data, file, isPdf);
                                                },
                                              )));
                                  setState(() {});
                                },
                                child: const CustomText(
                                    text:
                                        'Clique aqui para selecionar outra imagem'),
                              )
                            ])
                  : InkWell(
                      onTap: () {
                        Navigator.push(context,
                            MaterialPageRoute(builder: (context) {
                          return CroquiArea(
                            updateImageData:
                                (Uint8List? data, bool isPdf) async {
                              final directory =
                                  await getApplicationCacheDirectory();
                              File file = File(
                                  "${directory.path}/pdf_${Util.getRandomString(10)}.pdf");
                              await file.writeAsBytes(data!);
                              _updateImageDate(data, file, isPdf);
                            },
                          );
                        }));
                      },
                      child: Container(
                        width: 328,
                        height: 50,
                        padding: const EdgeInsets.all(10),
                        decoration: ShapeDecoration(
                          color: Colors.white,
                          shape: RoundedRectangleBorder(
                            side: const BorderSide(
                                width: 2, color: Color(0xFF00B45D)),
                            borderRadius: BorderRadius.circular(8),
                          ),
                        ),
                        child: Row(
                          mainAxisSize: MainAxisSize.min,
                          mainAxisAlignment: MainAxisAlignment.start,
                          crossAxisAlignment: CrossAxisAlignment.center,
                          children: [
                            Expanded(
                              child: SizedBox(
                                  height: 24,
                                  child: Row(
                                      mainAxisSize: MainAxisSize.min,
                                      mainAxisAlignment:
                                          MainAxisAlignment.start,
                                      crossAxisAlignment:
                                          CrossAxisAlignment.center,
                                      children: [
                                        Container(
                                          width: 24,
                                          height: 24,
                                          clipBehavior: Clip.antiAlias,
                                          decoration: const BoxDecoration(),
                                          child: const Row(
                                            mainAxisSize: MainAxisSize.min,
                                            mainAxisAlignment:
                                                MainAxisAlignment.center,
                                            crossAxisAlignment:
                                                CrossAxisAlignment.center,
                                            children: [
                                              Icon(
                                                Icons.edit,
                                                color: Colors.green,
                                              )
                                            ],
                                          ),
                                        ),
                                        Expanded(
                                            child: Column(
                                                mainAxisSize: MainAxisSize.min,
                                                mainAxisAlignment:
                                                    MainAxisAlignment.center,
                                                crossAxisAlignment:
                                                    CrossAxisAlignment.start,
                                                children: [
                                              Container(
                                                  width: double.infinity,
                                                  padding:
                                                      const EdgeInsets.only(
                                                          left: 8),
                                                  child: const Row(
                                                      mainAxisSize:
                                                          MainAxisSize.min,
                                                      mainAxisAlignment:
                                                          MainAxisAlignment
                                                              .center,
                                                      crossAxisAlignment:
                                                          CrossAxisAlignment
                                                              .center,
                                                      children: [
                                                        Expanded(
                                                            child: Column(
                                                                mainAxisSize:
                                                                    MainAxisSize
                                                                        .min,
                                                                mainAxisAlignment:
                                                                    MainAxisAlignment
                                                                        .center,
                                                                crossAxisAlignment:
                                                                    CrossAxisAlignment
                                                                        .center,
                                                                children: [
                                                              SizedBox(
                                                                  width: double
                                                                      .infinity,
                                                                  child: Text(
                                                                      'Croqui de área',
                                                                      style:
                                                                          TextStyle(
                                                                        color: Color.fromARGB(
                                                                            255,
                                                                            121,
                                                                            118,
                                                                            118),
                                                                        fontSize:
                                                                            16,
                                                                        fontFamily:
                                                                            'Inter',
                                                                        fontWeight:
                                                                            FontWeight.w600,
                                                                        height:
                                                                            0.09,
                                                                      )))
                                                            ]))
                                                      ]))
                                            ]))
                                      ])),
                            ),
                            const SizedBox(width: 16),
                            Container(
                              width: 50,
                              height: 54,
                              clipBehavior: Clip.antiAlias,
                              decoration: const BoxDecoration(),
                              child: Stack(children: [
                                SvgPicture.asset("assets/images/arrow.svg")
                              ]),
                            ),
                          ],
                        ),
                      ),
                    ),
              Center(
                  child: CustomButton(
                title: "OK",
                onClick: () async {
                  await _verifyFields();
                },
              ))
            ],
          ),
        ),
      ),
    );
  }
}
