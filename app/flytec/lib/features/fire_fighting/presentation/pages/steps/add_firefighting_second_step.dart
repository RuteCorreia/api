import 'package:dropdown_button2/dropdown_button2.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flytec/core/extensions/time_of_day_extension.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/components/aircraft_prefix_select.dart';
import 'package:flytec/features/aplications/components/components_exports.dart';
import 'package:flytec/features/aplications/controller/maps_informations_controller.dart';
import 'package:flytec/features/aplications/pages/contratante_page.dart';
import 'package:flytec/features/fire_fighting/controller/firefighting_controller.dart';
import 'package:flytec/features/fire_fighting/models/firefighting.dart';
import 'package:flytec/features/fire_fighting/models/local_firefighting.dart';
import 'package:flytec/features/fire_fighting/models/pista_firefighting.dart';
import 'package:go_router/go_router.dart';
import 'package:intl/intl.dart';
import 'package:location/location.dart' as lct;

class AddFireFightingSecondStep extends StatefulWidget {
  final FirefightingController _firefightingController;
  const AddFireFightingSecondStep(
      {required FirefightingController firefightingController, super.key})
      : _firefightingController = firefightingController;

  @override
  State<AddFireFightingSecondStep> createState() =>
      _AddFireFightingSecondStepState();
}

class _AddFireFightingSecondStepState extends State<AddFireFightingSecondStep> {
  Firefighting? _firefighting;

  DateTime? _dataSelecionada = DateTime.now();
  TimeOfDay? _horarioAcionamento = const TimeOfDay(hour: 12, minute: 43);
  TimeOfDay? _horarioChegadaPista = const TimeOfDay(hour: 12, minute: 43);
  TextEditingController _numeroAviso = TextEditingController();
  TextEditingController _horimetroAcionamento = TextEditingController();
  TextEditingController _horimetroChegadaPista = TextEditingController();
  TextEditingController _codigoICAOPista = TextEditingController();
  TextEditingController _nomePista = TextEditingController();
  TextEditingController _referencia = TextEditingController();
  TextEditingController _latitudeControllerPista = TextEditingController();
  TextEditingController _longitudeControllerPista = TextEditingController();
  TextEditingController _latitudeControllerLocalIncendio =
      TextEditingController();
  TextEditingController _longitudeControllerLocalIncendio =
      TextEditingController();
  final MapsInformationsController _mapsInformationsController =
      MapsInformationsControllerBrazil();
  final TextEditingController _citySearchController = TextEditingController();

  String _airCraftPrexix = "";
  final List<String> _citiesNamesUfBrazil = [];
  String _cityOfUf = '';
  String _uf = 'SP';
  List<String> _statesOfBrazil = [];
  void _obtainStatesOfBrazil() {
    _statesOfBrazil = _mapsInformationsController.getStatesBrazil;
    setState(() {});
  }

  Future<void> _obtainCitiesOfUfBrazil(String uf) async {
    _citiesNamesUfBrazil.clear();
    List<String> cities =
        _mapsInformationsController.obtainCitiesFromStateBrazil(uf);
    if (_cityOfUf.isEmpty) {
      _cityOfUf = cities.first;
      setState(() {});
    }
    _citiesNamesUfBrazil.addAll(cities);
    setState(() {});
  }

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) async {
      setState(() {
        _firefighting = widget._firefightingController.firefightingSelected;
        _numeroAviso =
            TextEditingController(text: _firefighting?.numeroAviso?.toString());
        _airCraftPrexix = _firefighting?.prefixoAeronave ?? '';
        _uf = _firefighting!.uf != null && _firefighting!.uf!.isNotEmpty
            ? _firefighting!.uf!
            : 'SP';
        _cityOfUf =
            _firefighting!.cidade != null && _firefighting!.cidade!.isNotEmpty
                ? _firefighting!.cidade!
                : '';
        _dataSelecionada =
            DateTime.fromMillisecondsSinceEpoch(_firefighting!.data!);
        _horarioAcionamento = _firefighting!.horarioAcionamento != null
            ? TimeOfDay.fromDateTime(DateTime.fromMillisecondsSinceEpoch(
                _firefighting!.horarioAcionamento!))
            : const TimeOfDay(hour: 12, minute: 43);
        _horimetroAcionamento =
            TextEditingController(text: _firefighting?.horimetroAcionamento);
        _horarioChegadaPista = _firefighting?.pista?.horarioChegadaPista != null
            ? TimeOfDay.fromDateTime(DateTime.fromMillisecondsSinceEpoch(
                _firefighting!.pista!.horarioChegadaPista!))
            : const TimeOfDay(hour: 12, minute: 43);
        _horimetroChegadaPista = TextEditingController(
            text: _firefighting?.pista?.horimetroChegadaPista);
        _codigoICAOPista =
            TextEditingController(text: _firefighting?.pista?.codigoICAOPista);
        _latitudeControllerPista =
            TextEditingController(text: _firefighting?.pista?.latPista);
        _longitudeControllerPista =
            TextEditingController(text: _firefighting?.pista?.longPista);
        _nomePista =
            TextEditingController(text: _firefighting?.pista?.nomePista);
        _latitudeControllerLocalIncendio =
            TextEditingController(text: _firefighting?.localIncendio?.lat);
        _longitudeControllerLocalIncendio =
            TextEditingController(text: _firefighting?.localIncendio?.long);
        _referencia = TextEditingController(
            text: _firefighting?.localIncendio?.referencia);
      });
      _obtainStatesOfBrazil();
      _obtainCitiesOfUfBrazil(_uf);
      setState(() {});
    });
  }

  Future<void> _actionFirefighting() async {
    _firefighting?.cliente =
        getIt<GlobalConfigVars>().contratanteCombateIncendio?.nome;
    _firefighting?.numeroAviso =
        _numeroAviso.text.isNotEmpty ? int.tryParse(_numeroAviso.text) : 0;
    _firefighting?.prefixoAeronave = _airCraftPrexix;
    _firefighting?.uf = _uf;
    _firefighting?.cidade = _cityOfUf;
    _firefighting?.data = _dataSelecionada?.millisecondsSinceEpoch;
    _firefighting?.horarioAcionamento =
        _horarioAcionamento?.toDateTime().millisecondsSinceEpoch;
    _firefighting?.horimetroAcionamento = _horimetroAcionamento.text;
    setState(() {});
    await _updateFirefighting(_firefighting!.id!, _firefighting!);

    PistaFirefighting? pista;
    pista?.horarioChegadaPista =
        _horarioChegadaPista?.toDateTime().millisecondsSinceEpoch;
    pista?.horimetroChegadaPista = _horimetroChegadaPista.text;
    pista?.codigoICAOPista = _codigoICAOPista.text;
    pista?.nomePista = _nomePista.text;
    pista?.latPista = _latitudeControllerPista.text;
    pista?.longPista = _longitudeControllerPista.text;
    setState(() {});

    await _actionPistaFireghting(pista);

    LocalFirefighting? localIncendio;
    localIncendio?.lat = _latitudeControllerLocalIncendio.text;
    localIncendio?.long = _longitudeControllerLocalIncendio.text;
    localIncendio?.referencia = _referencia.text;
    setState(() {});

    await _actionLocalFirefighting(localIncendio);
  }

  Future<void> _updateFirefighting(int id, Firefighting data) async {
    await widget._firefightingController
        .updateElementInTable(id, data.toMap(), 'Firefighting');

    widget._firefightingController.setFirefightingSelected(data);
    setState(() {});
  }

  Future<void> _actionPistaFireghting(PistaFirefighting? pista) async {
    if (pista == null) return;
    if (pista.id == null || (pista.id != null && pista.id! <= 0)) {
      final idPista = await widget._firefightingController
          .createElementInTable(pista.toJson(), 'PistaFirefighting');
      await widget._firefightingController.updateElementInTable(
          _firefighting!.id!, {'pista_id': idPista}, 'Firefighting');
      widget._firefightingController.setFirefightingSelected(_firefighting!);
      return;
    }
    await widget._firefightingController
        .updateElementInTable(pista.id!, pista.toJson(), 'PistaFirefighting');
    widget._firefightingController.setFirefightingSelected(_firefighting!);
  }

  Future<void> _actionLocalFirefighting(LocalFirefighting? local) async {
    if (local == null) return;
    if (local.id == null || (local.id != null && local.id! <= 0)) {
      final idLocal = await widget._firefightingController
          .createElementInTable(local.toJson(), 'LocalFirefighting');
      await widget._firefightingController.updateElementInTable(
          _firefighting!.id!, {'localIncendio_id': idLocal}, 'Firefighting');
      widget._firefightingController.setFirefightingSelected(_firefighting!);
      return;
    }
    await widget._firefightingController
        .updateElementInTable(local.id!, local.toJson(), 'LocalFirefighting');
    widget._firefightingController.setFirefightingSelected(_firefighting!);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
          title: const Text("Combate a incêndio"),
          leading: IconButton(
            icon: const Icon(Icons.arrow_back),
            onPressed: () async {
              await _actionFirefighting();
              getIt<GlobalConfigVars>().clearGlobalConfigVars();
              // ignore: use_build_context_synchronously
              Navigator.pop(context);
            },
          )),
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: SingleChildScrollView(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const SizedBox(height: 16),
              Row(
                mainAxisAlignment: MainAxisAlignment.end,
                children: [
                  Text(
                    'N° ${_firefighting?.refId ?? ''}',
                    textAlign: TextAlign.right,
                    style: const TextStyle(
                      color: Color(0xFF00B45D),
                      fontSize: 14,
                      fontFamily: 'Inter',
                      fontWeight: FontWeight.w700,
                      height: 0.11,
                    ),
                  )
                ],
              ),
              const SizedBox(height: 14),
              CustomCardButton(
                title: "Identificação do contratante",
                onTap: () {
                  Navigator.push(
                      context,
                      MaterialPageRoute(
                        builder: (context) => const ContrantePage(
                            reportAplicationController: null),
                      ));
                },
              ),
              const SizedBox(height: 14),
              const CustomText(text: 'N° Aviso'),
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
                  keyboardType: TextInputType.number,
                  controller: _numeroAviso,
                  decoration: const InputDecoration(
                      hintText: "Número do aviso",
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
              const CustomText(text: 'Prefixo da Aeronave'),
              const SizedBox(height: 14),
              CustomComboBoxExpanded(
                  selectedName:
                      _airCraftPrexix.isEmpty ? "Selecione" : _airCraftPrexix,
                  onTap: () async {
                    Util.closeKeyBoard();
                    await showDialog(
                        context: context,
                        builder: (BuildContext context) {
                          return AlertDialog(
                              backgroundColor: Colors.grey[100],
                              content: SizedBox(
                                width: double.maxFinite,
                                child: AirCraftPrefixSelect(onChanged: (value) {
                                  setState(() {
                                    _airCraftPrexix = value;
                                  });
                                  Util.closeKeyBoard();
                                }),
                              ));
                        });
                  }),
              const SizedBox(height: 20),
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      const CustomText(text: "UF"),
                      const SizedBox(height: 14),
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
                            const SizedBox(height: 14),
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
              const SizedBox(height: 20),
              const CustomText(text: 'Data'),
              const SizedBox(height: 14),
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
                  setState(() {
                    _dataSelecionada = data;
                  });
                },
              ),
              const SizedBox(height: 20),
              const CustomText(text: 'Horário de Acionamento'),
              const SizedBox(height: 14),
              CustomComboBoxExpanded(
                selectedName: _horarioAcionamento == null
                    ? "Selecione"
                    : _horarioAcionamento!.to24hours(),
                onTap: () async {
                  final data = await showTimePicker(
                      confirmText: "Selecionar hora",
                      cancelText: "Cancelar",
                      helpText: "",
                      context: context,
                      initialTime: const TimeOfDay(hour: 12, minute: 23));
                  setState(() {
                    _horarioAcionamento = data;
                  });
                },
              ),
              const SizedBox(height: 20),
              const CustomText(text: 'Horímetro de Acionamento'),
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
                  keyboardType: TextInputType.number,
                  controller: _horimetroAcionamento,
                  inputFormatters: [
                    FilteringTextInputFormatter.digitsOnly,
                    CustomNumberFormatter()
                  ],
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
              const CustomText(text: 'Horário de chegada na pista'),
              const SizedBox(height: 14),
              CustomComboBoxExpanded(
                selectedName: _horarioChegadaPista == null
                    ? "Selecione"
                    : _horarioChegadaPista!.to24hours(),
                onTap: () async {
                  final data = await showTimePicker(
                      confirmText: "Selecionar hora",
                      cancelText: "Cancelar",
                      helpText: "",
                      context: context,
                      initialTime: const TimeOfDay(hour: 12, minute: 23));
                  setState(() {
                    _horarioChegadaPista = data;
                  });
                },
              ),
              const SizedBox(height: 20),
              const CustomText(text: 'Horímetro de chegada na pista'),
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
                  keyboardType: TextInputType.number,
                  controller: _horimetroChegadaPista,
                  inputFormatters: [
                    FilteringTextInputFormatter.digitsOnly,
                    CustomNumberFormatter()
                  ],
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
              const SizedBox(
                width: 328,
                child: Text(
                  'Pista de operação',
                  style: TextStyle(
                    color: Color(0xFF00B45D),
                    fontSize: 14,
                    fontFamily: 'Inter',
                    fontWeight: FontWeight.bold,
                    height: 0.11,
                  ),
                ),
              ),
              const SizedBox(height: 40),
              const Text(
                'Código ICAO',
                style: TextStyle(
                  color: Color(0xFF00B45D),
                  fontSize: 14,
                  fontFamily: 'Inter',
                  fontWeight: FontWeight.w600,
                  height: 0.11,
                ),
              ),
              const SizedBox(height: 20),
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
                  controller: _codigoICAOPista,
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
              GestureDetector(
                onTap: () async {
                  lct.Location local = lct.Location();
                  local.getLocation().then((lc) async {
                    setState(() {
                      _latitudeControllerPista.text = lc.latitude.toString();
                      _longitudeControllerPista.text = lc.longitude.toString();
                    });
                  });
                },
                child: Container(
                  width: MediaQuery.of(context).size.width,
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
                          controller: _latitudeControllerPista,
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
                          controller: _longitudeControllerPista,
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
              const Text(
                'Nome',
                style: TextStyle(
                  color: Color(0xFF00B45D),
                  fontSize: 14,
                  fontFamily: 'Inter',
                  fontWeight: FontWeight.w600,
                  height: 0.11,
                ),
              ),
              const SizedBox(height: 15),
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
                  controller: _nomePista,
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
              const Text(
                'Local do incêndio ',
                style: TextStyle(
                  color: Color(0xFF00B45D),
                  fontSize: 14,
                  fontFamily: 'Inter',
                  fontWeight: FontWeight.w700,
                  height: 0.11,
                ),
              ),
              const SizedBox(height: 15),
              GestureDetector(
                onTap: () async {
                  lct.Location local = lct.Location();
                  local.getLocation().then((lc) async {
                    setState(() {
                      _latitudeControllerLocalIncendio.text =
                          lc.latitude.toString();
                      _longitudeControllerLocalIncendio.text =
                          lc.longitude.toString();
                    });
                  });
                },
                child: Container(
                  width: MediaQuery.of(context).size.width,
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
                          controller: _latitudeControllerLocalIncendio,
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
                          controller: _longitudeControllerLocalIncendio,
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
              const Text(
                'Referência',
                style: TextStyle(
                  color: Color(0xFF00B45D),
                  fontSize: 14,
                  fontFamily: 'Inter',
                  fontWeight: FontWeight.w700,
                  height: 0.11,
                ),
              ),
              const SizedBox(height: 15),
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
                  controller: _referencia,
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
                  title: "Próximo",
                  onClick: () async {
                    await _actionFirefighting();
                    // ignore: use_build_context_synchronously
                    context.push("/combateIncendioPasso3");
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
