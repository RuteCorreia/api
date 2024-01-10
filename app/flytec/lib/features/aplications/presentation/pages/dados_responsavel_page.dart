import 'package:dropdown_button2/dropdown_button2.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:flytec/features/aplications/presentation/pages/controllers/maps_informations_controller.dart';
import 'package:flytec/features/auth/presentation/widgets/custom_login_button.dart';
import 'package:go_router/go_router.dart';
import 'package:mask_text_input_formatter/mask_text_input_formatter.dart';

import 'my_activity_page.dart';

class DadosResponsavelPage extends StatefulWidget {
  const DadosResponsavelPage({super.key});

  @override
  State<DadosResponsavelPage> createState() => _DadosResponsavelPageState();
}

class _DadosResponsavelPageState extends State<DadosResponsavelPage> {
  List<String> _statesOfBrazil = [];
  String _uf = 'SP';
  String _cityOfUf = 'Selecione';
  void _obtainStatesOfBrazil() {
    _statesOfBrazil = _mapsInformationsController.getStatesBrazil;
    setState(() {});
  }

  final MapsInformationsController _mapsInformationsController =
      MapsInformationsControllerBrazil();
  List<String> _citiesNamesUfBrazil = ['Selecione'];
  void _obtainCitiesOfUfBrazil(String uf) {
    _citiesNamesUfBrazil.clear();
    _citiesNamesUfBrazil = ['Selecione'];
    _cityOfUf = 'Selecione';
    setState(() {});
    List<String> cities =
        _mapsInformationsController.obtainCitiesFromStateBrazil(uf);

    _citiesNamesUfBrazil.addAll(cities);
    setState(() {});
  }

  @override
  void initState() {
    super.initState();
    _obtainStatesOfBrazil();
  }

  final TextEditingController _citySearchController = TextEditingController();
  void OpenContrato() {
    showAdaptiveDialog<String>(
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
                  child: const Row(
                    mainAxisAlignment: MainAxisAlignment.start,
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      CancelButton(),
                      SizedBox(width: 5),
                      OkButton(),
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

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
        title: const Text(
          "Dados do responsável",
          textAlign: TextAlign.center,
          style: TextStyle(
            fontSize: 15,
          ),
        ),
      ),
      body: Padding(
        padding: const EdgeInsets.all(12),
        child: ListView(
          children: [
            const SizedBox(height: 16),
            const CustomText(text: 'Data'),
            const SizedBox(height: 14),
            const ComboBox(selectedName: "Selecione"),
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
                Column(
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
                          padding: const EdgeInsets.symmetric(horizontal: 12.0),
                          height: 40,
                          decoration: BoxDecoration(
                            border: Border.all(
                                width: 1, color: const Color(0xFF636363)),
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
                )
              ],
            ),
            const SizedBox(height: 20),
            const CustomText(text: 'Nome completo'),
            const SizedBox(height: 14),
            const CustomTextField(
              text: "Digite aqui",
            ),
            const SizedBox(height: 14),
            const CustomText(text: 'CPF'),
            const SizedBox(height: 14),
            const CustomTextField(
              text: "Digite aqui",
            ),
            const SizedBox(height: 14),
            const CustomText(text: 'Telefone'),
            const SizedBox(height: 14),
            CustomTextField(
              text: "Digite aqui",
              inputFormatters: [
                MaskTextInputFormatter(
                  mask: '(##) #####-####',
                  filter: {"#": RegExp(r'[0-9]')},
                )
              ],
            ),
            const SizedBox(height: 14),
            AssignmentButton(
              onClick: () {
                OpenContrato();
              },
            ),
            Center(
              child: CustomButton(
                title: "FINALIZAR",
                onClick: () {
                  context.pop();
                },
              ),
            ),
          ],
        ),
      ),
    );
  }
}

class OkButton extends StatelessWidget {
  const OkButton({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return Expanded(
      child: InkWell(
        onTap: () {
          context.pop();
          context.push("/addsignature");
        },
        child: Container(
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
      ),
    );
  }
}

class CancelButton extends StatelessWidget {
  const CancelButton({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return Expanded(
      child: InkWell(
        onTap: () {
          context.pop();
        },
        child: Container(
          height: 45,
          padding: const EdgeInsets.symmetric(horizontal: 24, vertical: 10),
          decoration: ShapeDecoration(
            shape: RoundedRectangleBorder(
              side: const BorderSide(width: 2, color: Color(0xFF292929)),
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
    );
  }
}

class AssignmentButton extends StatelessWidget {
  const AssignmentButton({super.key, required this.onClick});
  final VoidCallback? onClick;

  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onClick,
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
          mainAxisSize: MainAxisSize.min,
          mainAxisAlignment: MainAxisAlignment.start,
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            Expanded(
              child: Container(
                child: Column(
                  mainAxisSize: MainAxisSize.min,
                  mainAxisAlignment: MainAxisAlignment.center,
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Container(
                      width: double.infinity,
                      padding: const EdgeInsets.only(left: 8),
                      child: Row(
                        mainAxisSize: MainAxisSize.min,
                        mainAxisAlignment: MainAxisAlignment.center,
                        crossAxisAlignment: CrossAxisAlignment.center,
                        children: [
                          Expanded(
                            child: Container(
                              child: const Column(
                                mainAxisSize: MainAxisSize.min,
                                mainAxisAlignment: MainAxisAlignment.center,
                                crossAxisAlignment: CrossAxisAlignment.center,
                                children: [
                                  SizedBox(
                                    width: double.infinity,
                                    child: Text(
                                      'Assinatura do responsável ',
                                      style: TextStyle(
                                        color:
                                            Color.fromARGB(255, 121, 118, 118),
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
              width: 24,
              height: 24,
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

class CustomTextField extends StatelessWidget {
  const CustomTextField(
      {super.key, this.text = "Digite aqui", this.inputFormatters});
  final String text;
  final List<TextInputFormatter>? inputFormatters;
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
        inputFormatters: inputFormatters ?? [],
        decoration: InputDecoration(
            hintText: text,
            border: InputBorder.none,
            hintStyle: const TextStyle(
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
