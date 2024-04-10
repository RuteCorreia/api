import 'package:dropdown_button2/dropdown_button2.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:flytec/core/extensions/time_of_day_extension.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/components/aircraft_prefix_select.dart';
import 'package:flytec/features/aplications/components/components_exports.dart';
import 'package:flytec/features/aplications/controller/maps_informations_controller.dart';
import 'package:flytec/features/aplications/pages/contratante_page.dart';
import 'package:go_router/go_router.dart';
import 'package:intl/intl.dart';
import 'package:location/location.dart' as lct;

class AddFireFightingSecondStep extends StatefulWidget {
  const AddFireFightingSecondStep({super.key});

  @override
  State<AddFireFightingSecondStep> createState() =>
      _AddFireFightingSecondStepState();
}

class _AddFireFightingSecondStepState extends State<AddFireFightingSecondStep> {
  late DateTime? _dataSelecionada = DateTime.now();
  late TimeOfDay? _time = const TimeOfDay(hour: 12, minute: 43);
  late TimeOfDay? _horarioChegadaPista = const TimeOfDay(hour: 12, minute: 43);

  final TextEditingController _latitudeControllerPista =
      TextEditingController();
  final TextEditingController _longitudeControllerPista =
      TextEditingController();
  final TextEditingController _latitudeControllerLocalIncendio =
      TextEditingController();
  final TextEditingController _longitudeControllerLocalIncendio =
      TextEditingController();
  final MapsInformationsController _mapsInformationsController =
      MapsInformationsControllerBrazil();
  final TextEditingController _citySearchController = TextEditingController();

  String _airCraftPrexix = "";
  final List<String> _citiesNamesUfBrazil = [];
  late String _cityOfUf = '';
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
    _cityOfUf = cities.first;
    setState(() {});
    _citiesNamesUfBrazil.addAll(cities);
    setState(() {});
  }

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) async {
      _obtainStatesOfBrazil();
      _obtainCitiesOfUfBrazil('SP');
      setState(() {});
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
          title: const Text("Combate a incêndio"),
          leading: IconButton(
            icon: const Icon(Icons.arrow_back),
            onPressed: () async {
              getIt<GlobalConfigVars>().contratanteCombateIncendio = null;
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
              const Row(
                mainAxisAlignment: MainAxisAlignment.end,
                children: [
                  Text(
                    'N° 1758',
                    textAlign: TextAlign.right,
                    style: TextStyle(
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
                child: const TextField(
                  keyboardType: TextInputType.number,
                  decoration: InputDecoration(
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
                selectedName: _time == null ? "Selecione" : _time!.to24hours(),
                onTap: () async {
                  final data = await showTimePicker(
                      confirmText: "Selecionar hora",
                      cancelText: "Cancelar",
                      helpText: "",
                      context: context,
                      initialTime: const TimeOfDay(hour: 12, minute: 23));
                  setState(() {
                    _time = data;
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
                  title: "Próximo",
                  onClick: () {
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

class CustomText extends StatelessWidget {
  const CustomText({super.key, required this.text});
  final String text;
  @override
  Widget build(BuildContext context) {
    return Text(
      text,
      style: const TextStyle(
        color: Color(0xFF00B45D),
        fontSize: 14,
        fontFamily: 'Inter',
        fontWeight: FontWeight.w700,
        height: 0.11,
      ),
    );
  }
}

class CustomComboBoxExpanded extends StatelessWidget {
  const CustomComboBoxExpanded(
      {super.key, required this.selectedName, required this.onTap});
  final String selectedName;
  final VoidCallback? onTap;
  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onTap,
      child: Container(
        width: 328,
        height: 50,
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
                      color: Color(0xFF636363),
                      fontSize: 14,
                      fontFamily: 'Inter',
                      fontWeight: FontWeight.w600,
                      height: 0.11,
                    ),
                  ),
                ],
              ),
            ),
            Container(
              width: 16,
              height: 16,
              clipBehavior: Clip.antiAlias,
              decoration: const BoxDecoration(),
              child: Stack(children: [
                SvgPicture.asset(
                  "assets/images/arrow.svg",
                )
              ]),
            ),
          ],
        ),
      ),
    );
  }
}
