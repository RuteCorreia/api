import 'dart:typed_data';

import 'package:dropdown_button2/dropdown_button2.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications_v2/controller/maps_informations_controller.dart';
import 'package:flytec/features/aplications_v2/controller/report_aplication_controller.dart';
import 'package:flytec/features/aplications_v2/components/components_exports.dart';
import 'package:flytec/features/aplications_v2/models/aplicacao.dart';
import 'package:flytec/features/aplications_v2/models/dados_responsavel.dart';
import 'package:go_router/go_router.dart';
import 'package:intl/intl.dart';
import 'package:mask_text_input_formatter/mask_text_input_formatter.dart';

class DadosResponsavelPage extends StatefulWidget {
  final ReportAplicationController _reportAplicationController;
  const DadosResponsavelPage(
      {required ReportAplicationController reportAplicationController,
      super.key})
      : _reportAplicationController = reportAplicationController;

  @override
  State<DadosResponsavelPage> createState() => _DadosResponsavelPageState();
}

class _DadosResponsavelPageState extends State<DadosResponsavelPage> {
  late DateTime? _dataSelecionada = DateTime.now();
  Aplicacao get _aplicacao =>
      widget._reportAplicationController.aplicacaoSelected!;
  DadosResponsavel? _dadosResponsavel;

  List<String> _statesOfBrazil = [];

  void _obtainStatesOfBrazil() {
    _statesOfBrazil = _mapsInformationsController.getStatesBrazil;
    setState(() {});
  }

  final MapsInformationsController _mapsInformationsController =
      MapsInformationsControllerBrazil();
  String _uf = 'SP';
  late String _cityOfUf = '';
  final List<String> _citiesNamesUfBrazil = [];
  final TextEditingController _citySearchController = TextEditingController();
  final TextEditingController _nome = TextEditingController();
  final TextEditingController _documento = TextEditingController();
  final TextEditingController _telefone = TextEditingController();

  Uint8List? _signature;
  void _onUpdateSignature(Uint8List? signature) {
    _signature = signature;
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

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      _obtainStatesOfBrazil();
      _obtainCitiesOfUfBrazil('SP');
      setState(() {});
      if (_aplicacao.dadosResponsavel?.data != null) {
        _dadosResponsavel = _aplicacao.dadosResponsavel;
        int? epoch = int.tryParse(_dadosResponsavel!.data!);
        _dataSelecionada = epoch != null
            ? DateTime.fromMillisecondsSinceEpoch(epoch)
            : DateTime.now();
        _uf = _dadosResponsavel!.uf!;
        _cityOfUf = _dadosResponsavel!.cidade!;
        _nome.text = _dadosResponsavel!.nomeCompleto!;
        _documento.text = _dadosResponsavel!.documento!;
        _telefone.text = _dadosResponsavel!.telefone!;
        _signature = _dadosResponsavel!.assinaturaResponsavel!;
        setState(() {});
      }
    });
  }

  Future<void> _openSignature() async {
    await showAdaptiveDialog<String>(
      context: context,
      useSafeArea: true,
      builder: (BuildContext context) => AlertDialog.adaptive(
        insetPadding: const EdgeInsets.all(20),
        content: SizedBox(
          height: 400,
          child: SingleChildScrollView(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Container(
                  padding: const EdgeInsets.all(10),
                  decoration: BoxDecoration(
                    color: Colors.white,
                    borderRadius: BorderRadius.circular(10),
                  ),
                  child: const Text(
                      '''O contratante declara estar plenamente de acordo com os serviços executados, área, valor e forma de pagamento expressa nesse contrato, tendo o mesmo valor como comprovante de entrega dos serviços prestados.
    E por estarem de acordo com todas as cláusulas, itens e demais condições estabelecidas neste contrato, as partes firmam o presente, tendo valor como testemunha as assinaturas digitais do Piloto e Engenheiro Agrônomo.

                   '''),
                ),
                const SizedBox(height: 10),
                Container(
                  width: 350,
                  height: 45,
                  padding: const EdgeInsets.symmetric(horizontal: 6),
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.start,
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      InkWell(
                        onTap: () {
                          Navigator.pop(context);
                        },
                        child: Container(
                          height: 45,
                          padding: const EdgeInsets.symmetric(
                              horizontal: 24, vertical: 10),
                          decoration: ShapeDecoration(
                            shape: RoundedRectangleBorder(
                              side: const BorderSide(
                                  width: 2, color: Color(0xFF292929)),
                              borderRadius: BorderRadius.circular(8),
                            ),
                          ),
                          child: const Row(
                            mainAxisSize: MainAxisSize.min,
                            mainAxisAlignment: MainAxisAlignment.center,
                            crossAxisAlignment: CrossAxisAlignment.center,
                            children: [
                              Text(
                                'CANCELAR',
                                style: TextStyle(
                                  color: Color(0xFF151515),
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
                      const SizedBox(width: 5),
                      InkWell(
                        onTap: () {
                          Navigator.pop(context);
                          context.push("/addsignature",
                              extra: {"onUpdateSignature": _onUpdateSignature});
                        },
                        child: Container(
                          height: 45,
                          padding: const EdgeInsets.symmetric(
                              horizontal: 24, vertical: 8),
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
                              Text(
                                'OK',
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
                )
              ],
            ),
          ),
        ),
        actions: const <Widget>[],
      ),
    );
  }

  Future<bool> _verifyFields() async {
    await _dadosResponsavelAction();
    Util.toastSucesso("Dados inseridos com sucesso");
    getIt<GlobalConfigVars>().clearGlobalConfigVars();
    Future.delayed(const Duration(seconds: 1), () {
      context.push("/home");
    });

    return true;
  }

  Future<void> _dadosResponsavelAction() async {
    _dadosResponsavel = DadosResponsavel(
        data: _dataSelecionada?.millisecondsSinceEpoch.toString(),
        uf: _uf,
        cidade: _cityOfUf,
        nomeCompleto: _nome.text,
        documento: _documento.text,
        telefone: _telefone.text,
        assinaturaResponsavel: _signature);

    if (_aplicacao.dadosResponsavel?.id == null) {
      int? idDadosResponsavel = await widget._reportAplicationController
          .createElementInTable(_dadosResponsavel!.toMap(), 'DadosResponsavel');
      await widget._reportAplicationController.updateElementInTable(
          _aplicacao.id!,
          {'dadosResponsavel_id': idDadosResponsavel},
          'Aplicacao');
      await _updateDadosResponsavel(idDadosResponsavel!);
      return;
    }
    int? idDadosResponsavel = _aplicacao.dadosResponsavel!.id;
    await widget._reportAplicationController.updateElementInTable(
        idDadosResponsavel!, _dadosResponsavel!.toMap(), 'DadosResponsavel');
    await _updateDadosResponsavel(idDadosResponsavel);
  }

  Future<void> _updateDadosResponsavel(int id) async {
    final element = await widget._reportAplicationController
        .getElementById(id, 'DadosResponsavel');
    final idDadosResponsavel = DadosResponsavel.fromJson(element);
    _dadosResponsavel = idDadosResponsavel;
    setState(() {});
    _aplicacao.dadosResponsavel = idDadosResponsavel;
    widget._reportAplicationController.setAplicacaoSelected(_aplicacao);
    setState(() {});
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
          centerTitle: true,
          title: const Text(
            "Dados do responsável",
            textAlign: TextAlign.center,
          ),
          leading: IconButton(
            icon: const Icon(Icons.arrow_back),
            onPressed: () async {
              await _dadosResponsavelAction();
              // ignore: use_build_context_synchronously
              Navigator.pop(context);
            },
          )),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: ListView(
          children: [
            const CustomText(text: 'Data'),
            const SizedBox(height: 10),
            CustomComboBoxExpanded(
              selectedName: _dataSelecionada == null
                  ? "Selecione"
                  : DateFormat('dd/MM/yyyy').format(_dataSelecionada!),
              onTap: () async {
                final data = await showDatePicker(
                  confirmText: "Selecionar data",
                  cancelText: "Cancelar",
                  helpText: "",
                  context: context,
                  initialDate: DateTime.now(),
                  firstDate: DateTime(2024),
                  lastDate: DateTime(2028),
                );
                Util.closeKeyBoard();

                _dataSelecionada = data;
                setState(() {});
              },
            ),
            const SizedBox(height: 10),
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
                                      width: 1, color: const Color(0xFF636363)),
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
                                      hintStyle: const TextStyle(fontSize: 12),
                                      border: OutlineInputBorder(
                                        borderRadius: BorderRadius.circular(8),
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
            const CustomText(text: "Nome Completo"),
            const SizedBox(height: 10),
            CustomTextField(
              textEditingController: _nome,
              onChanged: (value) {},
            ),
            const CustomText(text: "Documento"),
            const SizedBox(height: 10),
            CustomTextField(
              textEditingController: _documento,
              textInputType: TextInputType.number,
              onChanged: (value) {},
            ),
            const CustomText(text: "Telefone"),
            const SizedBox(height: 10),
            CustomTextField(
              textEditingController: _telefone,
              onChanged: (value) {},
              textInputType: TextInputType.number,
              formater: [
                MaskTextInputFormatter(
                  mask: '(##) #####-####',
                  filter: {"#": RegExp(r'[0-9]')},
                )
              ],
            ),
            const SizedBox(height: 10),
            InkWell(
              onTap: () async {
                await _openSignature();
              },
              child: Container(
                width: 328,
                height: 56,
                padding: const EdgeInsets.all(16),
                clipBehavior: Clip.antiAlias,
                decoration: ShapeDecoration(
                  color: Colors.white,
                  shape: RoundedRectangleBorder(
                    side: const BorderSide(width: 2, color: Color(0xFF00B45D)),
                    borderRadius: BorderRadius.circular(8),
                  ),
                ),
                child: Row(
                  mainAxisAlignment: MainAxisAlignment.start,
                  crossAxisAlignment: CrossAxisAlignment.center,
                  children: [
                    const Text(
                      'Assinatura do responsável ',
                      style: TextStyle(
                        color: Color.fromARGB(255, 121, 118, 118),
                        fontSize: 16,
                        fontFamily: 'Inter',
                        fontWeight: FontWeight.w600,
                      ),
                    ),
                    const SizedBox(width: 16),
                    Container(
                      width: 24,
                      height: 24,
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
            const SizedBox(height: 10),
            if (_signature != null)
              Container(
                  height: 200,
                  width: MediaQuery.of(context).size.width,
                  decoration: BoxDecoration(
                    image: DecorationImage(
                        image: MemoryImage(_signature!), fit: BoxFit.fill),
                  )),
            const SizedBox(height: 10),
            Center(
              child: CustomButton(
                title: "FINALIZAR",
                onClick: () async {
                  await _verifyFields();
                },
              ),
            ),
          ],
        ),
      ),
    );
  }
}
