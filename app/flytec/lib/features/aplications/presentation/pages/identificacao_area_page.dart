import 'dart:io';

import 'package:dropdown_button2/dropdown_button2.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/presentation/pages/controllers/maps_informations_controller.dart';
import 'package:flytec/features/aplications/presentation/pages/croquis_area/croquis_area_page.dart';
import 'package:flytec/features/aplications/presentation/pages/steps/aplication_second_step.dart';

import '../../../auth/presentation/widgets/custom_login_button.dart';
import 'steps/aplication_first_step.dart';

class IdentificacaoAreaTratamento extends StatefulWidget {
  const IdentificacaoAreaTratamento({super.key});

  @override
  State<IdentificacaoAreaTratamento> createState() =>
      _IdentificacaoAreaTratamentoState();
}

class _IdentificacaoAreaTratamentoState
    extends State<IdentificacaoAreaTratamento> {
  final MapsInformationsController _mapsInformationsController =
      MapsInformationsControllerBrazil();

  String _imagePathMap = '';
  void _updateImagePathMap(String path) {
    _imagePathMap = path;
    setState(() {});
  }

  List<String> _statesOfBrazil = [];
  void _obtainStatesOfBrazil() {
    _statesOfBrazil = _mapsInformationsController.getStatesBrazil;
    setState(() {});
  }
  String _uf = 'SP';
  late String _cityOfUf = '';
  final List<String> _citiesNamesUfBrazil = [];

  Future<void> _obtainCitiesOfUfBrazil(String uf) async {
    _citiesNamesUfBrazil.clear();
    List<String> cities =
        _mapsInformationsController.obtainCitiesFromStateBrazil(uf);
    _cityOfUf = cities.first;
    setState(() {});
    _citiesNamesUfBrazil.addAll(cities);
    setState(() {});
  }

  final TextEditingController _citySearchController = TextEditingController();

  @override
  void dispose() {
    _citySearchController.dispose();
    super.dispose();
  }

  @override
  void initState() {
    super.initState();
    _obtainStatesOfBrazil();
    _obtainCitiesOfUfBrazil('SP');
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
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: SingleChildScrollView(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const SizedBox(height: 20),
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
                            onChanged: (regiaoSelecionada) {
                              _uf = regiaoSelecionada!;
                              _obtainCitiesOfUfBrazil(regiaoSelecionada);
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
                                  width: 200,
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
              const SizedBox(height: 15),
              const CustomText(text: 'Localização'),
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
              const Text(
                'Cultura',
                style: TextStyle(
                  color: Color(0xFF00B45D),
                  fontSize: 14,
                  fontFamily: 'Inter',
                  fontWeight: FontWeight.w700,
                  height: 0.11,
                ),
              ),
              const SizedBox(height: 14),
              CustomCombo(
                selectedName: "Selecione",
                onTap: () {},
              ),
              const SizedBox(height: 14),
              const CustomText(text: 'Extensão(ha)'),
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
              _imagePathMap.isNotEmpty
                  ? Column(
                      crossAxisAlignment: CrossAxisAlignment.center,
                      children: [
                          const Align(
                            alignment: Alignment.centerLeft,
                            child: Padding(
                              padding: EdgeInsets.symmetric(vertical: 16.0),
                              child: CustomText(text: 'Imagem Selecionada'),
                            ),
                          ),
                          Container(
                              height: 300,
                              width: MediaQuery.of(context).size.width,
                              decoration: BoxDecoration(
                                image: DecorationImage(
                                    image: FileImage(File(_imagePathMap)),
                                    fit: BoxFit.fill),
                              )),
                          TextButton(
                            onPressed: () {
                              _imagePathMap = '';
                              setState(() {});
                              Navigator.push(
                                  context,
                                  MaterialPageRoute(
                                      builder: (context) => CroquisAreaCliente(
                                            updateImagePathMap:
                                                _updateImagePathMap,
                                          )));
                              setState(() {});
                            },
                            child: const CustomText(
                                text:
                                    'Clique aqui para selecionar outra imagem'),
                          )
                        ])
                  : CroquiButton(
                      onPressed: () {
                        Navigator.push(
                            context,
                            MaterialPageRoute(
                                builder: (context) => CroquisAreaCliente(
                                      updateImagePathMap: _updateImagePathMap,
                                    )));
                      },
                    ),
              const SizedBox(height: 14),
              Center(
                child: CustomButton(
                  title: "OK",
                  onClick: () {
                    if (_imagePathMap.isNotEmpty) {
                      Util.toastSucesso('Identificação Enviada com Sucesso!');
                      _imagePathMap = '';
                      setState(() {});
                    }
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

class CroquiButton extends StatelessWidget {
  const CroquiButton({super.key, required this.onPressed});
  final VoidCallback? onPressed;
  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onPressed,
      child: Container(
        width: 328,
        height: 50,
        padding: const EdgeInsets.all(10),
        decoration: ShapeDecoration(
          color: Colors.white,
          shape: RoundedRectangleBorder(
            side: const BorderSide(width: 2, color: Color(0xFF00B45D)),
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
                  mainAxisAlignment: MainAxisAlignment.start,
                  crossAxisAlignment: CrossAxisAlignment.center,
                  children: [
                    Container(
                      width: 24,
                      height: 24,
                      clipBehavior: Clip.antiAlias,
                      decoration: const BoxDecoration(),
                      child: const Row(
                        mainAxisSize: MainAxisSize.min,
                        mainAxisAlignment: MainAxisAlignment.center,
                        crossAxisAlignment: CrossAxisAlignment.center,
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
                        mainAxisAlignment: MainAxisAlignment.center,
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Container(
                            width: double.infinity,
                            padding: const EdgeInsets.only(left: 8),
                            child: const Row(
                              mainAxisSize: MainAxisSize.min,
                              mainAxisAlignment: MainAxisAlignment.center,
                              crossAxisAlignment: CrossAxisAlignment.center,
                              children: [
                                Expanded(
                                  child: Column(
                                    mainAxisSize: MainAxisSize.min,
                                    mainAxisAlignment: MainAxisAlignment.center,
                                    crossAxisAlignment:
                                        CrossAxisAlignment.center,
                                    children: [
                                      SizedBox(
                                        width: double.infinity,
                                        child: Text(
                                          'Croqui de área',
                                          style: TextStyle(
                                            color: Color.fromARGB(
                                                255, 121, 118, 118),
                                            fontSize: 16,
                                            fontFamily: 'Inter',
                                            fontWeight: FontWeight.w600,
                                            height: 0.09,
                                          ),
                                        ),
                                      ),
                                    ],
                                  ),
                                ),
                              ],
                            ),
                          ),
                        ],
                      ),
                    ),
                  ],
                ),
              ),
            ),
            const SizedBox(width: 16),
            Container(
              width: 50,
              height: 54,
              clipBehavior: Clip.antiAlias,
              decoration: const BoxDecoration(),
              child: Stack(
                  children: [SvgPicture.asset("assets/images/arrow.svg")]),
            ),
          ],
        ),
      ),
    );
  }
}
