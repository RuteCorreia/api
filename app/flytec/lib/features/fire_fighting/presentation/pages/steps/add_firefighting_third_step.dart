// ignore_for_file: use_build_context_synchronously

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flytec/core/extensions/time_of_day_extension.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/core/widgets/combo_box.dart';
import 'package:flytec/features/aplications/components/custom_text.dart';
import 'package:flytec/features/fire_fighting/controller/firefighting_controller.dart';
import 'package:flytec/features/fire_fighting/models/decolagem_pouso_firefighting.dart';
import 'package:flytec/features/fire_fighting/models/firefighting.dart';
import 'package:flytec/features/fire_fighting/presentation/components/decolagem_pouso_selected.dart';
import 'package:flytec/features/fire_fighting/presentation/pages/contrato_prestacao_servico.dart';

import '../../../../auth/presentation/widgets/custom_login_button.dart';

class AddFireFightingThirdStep extends StatefulWidget {
  final FirefightingController? _firefightingController;
  const AddFireFightingThirdStep(
      {required FirefightingController? firefightingController, super.key})
      : _firefightingController = firefightingController;

  @override
  State<AddFireFightingThirdStep> createState() =>
      _AddFireFightingSecondStepState();
}

class _AddFireFightingSecondStepState extends State<AddFireFightingThirdStep> {
  Firefighting? _firefighting;

  TimeOfDay? _horarioFinalOperacao = const TimeOfDay(hour: 12, minute: 43);
  late TimeOfDay? _horarioCorte = const TimeOfDay(hour: 12, minute: 43);
  TextEditingController _horimetroFinalOperacao = TextEditingController();
  TextEditingController _horimetroCorte = TextEditingController();
  TextEditingController _capacidadeCarga = TextEditingController();
  TextEditingController _totalAguaUtilizada = TextEditingController();
  TextEditingController _observacoes = TextEditingController();
  List<DecolagemPousoFirefighting?> _decolagemPousoList = [];

  void _onAddNewElementsInDecolagemPousoList(
      DecolagemPousoFirefighting element) {
    _decolagemPousoList.add(element);
    setState(() {});
  }

  Future<void> _actionFirefighting() async {
    _firefighting?.observacao = _observacoes.text;
    _firefighting?.horarioFinalOperacao =
        _horarioFinalOperacao?.toDateTime().millisecondsSinceEpoch;
    _firefighting?.horimetroFinalOperacao = _horimetroFinalOperacao.text;
    _firefighting?.horarioCorte =
        _horarioCorte?.toDateTime().millisecondsSinceEpoch;
    _firefighting?.horimetroCorte = _horimetroCorte.text;
    _firefighting?.capacidadeCargaAeronave = _capacidadeCarga.text;
    _firefighting?.totalAguaUtilizadaOperacao = _totalAguaUtilizada.text;
    setState(() {});
    await _updateFirefighting(_firefighting!.id!, _firefighting!);
    if (_decolagemPousoList.isNotEmpty) {
      _firefighting?.decolagemPousoFirefightingList = _decolagemPousoList;
      for (DecolagemPousoFirefighting? decolagemPouso in _decolagemPousoList) {
        await _updateDecolagemPousoFirefighting(decolagemPouso);
      }
    }
  }

  Future<void> _updateDecolagemPousoFirefighting(
      DecolagemPousoFirefighting? decolagemPouso) async {
    if (decolagemPouso == null) return;
    if (decolagemPouso.id == null ||
        (decolagemPouso.id != null && decolagemPouso.id! <= 0)) {
      final decolagemPousoToJson = decolagemPouso.toJson();
      decolagemPousoToJson.addAll({'firefightingId': _firefighting?.id});
      await widget._firefightingController!.createElementInTable(
          decolagemPousoToJson, 'DecolagemPousoFirefighting');

      widget._firefightingController?.setFirefightingSelected(_firefighting!);
      return;
    }
    await widget._firefightingController?.updateElementInTable(
        decolagemPouso.id!,
        decolagemPouso.toJson(),
        'DecolagemPousoFirefighting');
    widget._firefightingController?.setFirefightingSelected(_firefighting!);
  }

  Future<void> _updateFirefighting(int id, Firefighting data) async {
    await widget._firefightingController!
        .updateElementInTable(id, data.toMap(), 'Firefighting');
    widget._firefightingController?.setFirefightingSelected(data);
    setState(() {});
  }

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      setState(() {
        _firefighting = widget._firefightingController?.firefightingSelected;
        _decolagemPousoList =
            _firefighting?.decolagemPousoFirefightingList ?? [];
        _observacoes = TextEditingController(text: _firefighting?.observacao);
        _horarioFinalOperacao = _firefighting!.horarioFinalOperacao != null
            ? TimeOfDay.fromDateTime(DateTime.fromMillisecondsSinceEpoch(
                _firefighting!.horarioFinalOperacao!))
            : const TimeOfDay(hour: 12, minute: 43);
        _horimetroFinalOperacao =
            TextEditingController(text: _firefighting?.horimetroFinalOperacao);
        _horarioCorte = _firefighting!.horarioCorte != null
            ? TimeOfDay.fromDateTime(DateTime.fromMillisecondsSinceEpoch(
                _firefighting!.horarioCorte!))
            : const TimeOfDay(hour: 12, minute: 43);
        _horimetroCorte =
            TextEditingController(text: _firefighting?.horimetroCorte);
        _capacidadeCarga =
            TextEditingController(text: _firefighting?.capacidadeCargaAeronave);
        _totalAguaUtilizada = TextEditingController(
            text: _firefighting?.totalAguaUtilizadaOperacao);
      });
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
          title: const Text("Combate a Incêndio"),
          leading: IconButton(
            icon: const Icon(Icons.arrow_back),
            onPressed: () async {
              await _actionFirefighting();
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
              Container(
                width: 328,
                height: 64,
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
                  mainAxisAlignment: MainAxisAlignment.spaceAround,
                  crossAxisAlignment: CrossAxisAlignment.center,
                  children: [
                    const Text(
                      'Adicionar Decolagem / Pouso',
                      style: TextStyle(
                        color: Color.fromARGB(255, 121, 118, 118),
                        fontSize: 14,
                        fontFamily: 'Inter',
                        fontWeight: FontWeight.w600,
                      ),
                    ),
                    const SizedBox(width: 1),
                    InkWell(
                      onTap: () {
                        showDialog(
                            context: context,
                            useSafeArea: true,
                            builder: (BuildContext context) => AlertDialog(
                                content: DecolagemPousoSelected(
                                    onAddNewElementsInDecolagemPousoList:
                                        (element) =>
                                            _onAddNewElementsInDecolagemPousoList(
                                                element))));
                      },
                      child: Container(
                        width: 30,
                        height: 30,
                        padding: const EdgeInsets.all(1),
                        clipBehavior: Clip.antiAlias,
                        decoration: ShapeDecoration(
                          color: const Color(0xFF00B45D),
                          shape: RoundedRectangleBorder(
                            borderRadius: BorderRadius.circular(3.49),
                          ),
                        ),
                        child: const Column(
                          mainAxisSize: MainAxisSize.min,
                          mainAxisAlignment: MainAxisAlignment.center,
                          crossAxisAlignment: CrossAxisAlignment.center,
                          children: [
                            Icon(
                              Icons.add,
                              color: Colors.white,
                            )
                          ],
                        ),
                      ),
                    ),
                  ],
                ),
              ),
              if (_decolagemPousoList.isNotEmpty) ...[
                const SizedBox(height: 10),
                const Text(
                  'Pousos e Decolagens',
                  style: TextStyle(
                    color: Color.fromARGB(255, 121, 118, 118),
                    fontSize: 14,
                    fontFamily: 'Inter',
                    fontWeight: FontWeight.w600,
                  ),
                ),
                ListView.builder(
                    itemBuilder: (context, index) => Padding(
                          padding: const EdgeInsets.symmetric(vertical: 2.0),
                          child: Container(
                            width: 328,
                            height: 90,
                            clipBehavior: Clip.antiAlias,
                            padding: const EdgeInsets.all(8.0),
                            decoration: ShapeDecoration(
                              color: Colors.white,
                              shape: RoundedRectangleBorder(
                                side: const BorderSide(
                                    width: 2, color: Color(0xFF00B45D)),
                                borderRadius: BorderRadius.circular(8),
                              ),
                            ),
                            child: Row(
                              mainAxisAlignment: MainAxisAlignment.spaceBetween,
                              crossAxisAlignment: CrossAxisAlignment.center,
                              children: [
                                Column(
                                  crossAxisAlignment: CrossAxisAlignment.start,
                                  children: [
                                    Text(
                                      'Item ${index + 1}',
                                      style: const TextStyle(
                                        color: Color(0xFF00B45D),
                                        fontSize: 10,
                                        fontFamily: 'Inter',
                                        fontWeight: FontWeight.w600,
                                      ),
                                    ),
                                    Text(
                                      '- Horímetro Decolagem: ${_decolagemPousoList[index]!.horimetroDecolagem!.isEmpty ? '0.0' : _decolagemPousoList[index]!.horimetroDecolagem}',
                                      style: const TextStyle(
                                        color:
                                            Color.fromARGB(255, 121, 118, 118),
                                        fontSize: 10,
                                        fontFamily: 'Inter',
                                        fontWeight: FontWeight.w600,
                                      ),
                                    ),
                                    Text(
                                      '- Horário Decolagem: ${TimeOfDay.fromDateTime(DateTime.fromMillisecondsSinceEpoch(_decolagemPousoList[index]!.horarioDecolagem!)).to24hours()}',
                                      style: const TextStyle(
                                        color:
                                            Color.fromARGB(255, 121, 118, 118),
                                        fontSize: 10,
                                        fontFamily: 'Inter',
                                        fontWeight: FontWeight.w600,
                                      ),
                                    ),
                                    Text(
                                      '- Horímetro Pouso: ${_decolagemPousoList[index]!.horimetroPouso!.isEmpty ? '0.0' : _decolagemPousoList[index]!.horimetroPouso}',
                                      style: const TextStyle(
                                        color:
                                            Color.fromARGB(255, 121, 118, 118),
                                        fontSize: 10,
                                        fontFamily: 'Inter',
                                        fontWeight: FontWeight.w600,
                                      ),
                                    ),
                                    Text(
                                      '- Horário Pouso: ${TimeOfDay.fromDateTime(DateTime.fromMillisecondsSinceEpoch(_decolagemPousoList[index]!.horarioPouso!)).to24hours()}',
                                      style: const TextStyle(
                                        color:
                                            Color.fromARGB(255, 121, 118, 118),
                                        fontSize: 10,
                                        fontFamily: 'Inter',
                                        fontWeight: FontWeight.w600,
                                      ),
                                    )
                                  ],
                                ),
                                InkWell(
                                    onTap: () {
                                      _decolagemPousoList.removeAt(index);
                                      setState(() {});
                                    },
                                    child: Container(
                                      width: 20,
                                      height: 20,
                                      clipBehavior: Clip.antiAlias,
                                      decoration: ShapeDecoration(
                                        color: const Color(0xFF00B45D),
                                        shape: RoundedRectangleBorder(
                                          borderRadius:
                                              BorderRadius.circular(3.49),
                                        ),
                                      ),
                                      child: const Icon(
                                        Icons.close,
                                        size: 20,
                                        color: Colors.white,
                                      ),
                                    )),
                              ],
                            ),
                          ),
                        ),
                    itemCount: _decolagemPousoList.length,
                    shrinkWrap: true),
              ],
              const SizedBox(height: 20),
              const CustomText(text: "Observações "),
              const SizedBox(height: 10),
              Container(
                width: double.infinity,
                height: 100,
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
                  maxLength: 30,
                  maxLines: 3,
                  controller: _observacoes,
                  onChanged: (value) {},
                  textInputAction: TextInputAction.done,
                  decoration: const InputDecoration(
                      hintText: "-",
                      border: InputBorder.none,
                      counterText: "",
                      hintStyle: TextStyle(
                        color: Color.fromARGB(255, 121, 118, 118),
                        fontSize: 16,
                        fontFamily: 'Inter',
                        fontWeight: FontWeight.w500,
                      )),
                ),
              ),
              const CustomText(text: 'Horário final da operação'),
              const SizedBox(height: 14),
              CustomComboBox(
                selectedName: _horarioFinalOperacao == null
                    ? "Selecione"
                    : _horarioFinalOperacao!.to24hours(),
                onTap: () async {
                  final data = await showTimePicker(
                      confirmText: "Selecionar hora",
                      cancelText: "Cancelar",
                      helpText: "",
                      context: context,
                      initialTime: const TimeOfDay(hour: 12, minute: 23));
                  setState(() {
                    _horarioFinalOperacao = data;
                  });
                },
              ),
              const SizedBox(height: 20),
              const CustomText(text: 'Horímetro final da operação'),
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
                  controller: _horimetroFinalOperacao,
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
                      )),
                ),
              ),
              const CustomText(text: 'Horário de corte'),
              const SizedBox(height: 14),
              CustomComboBoxExpanded(
                selectedName: _horarioCorte == null
                    ? "Selecione"
                    : _horarioCorte!.to24hours(),
                onTap: () async {
                  final data = await showTimePicker(
                      confirmText: "Selecionar hora",
                      cancelText: "Cancelar",
                      helpText: "",
                      context: context,
                      initialTime: const TimeOfDay(hour: 12, minute: 23));
                  setState(() {
                    _horarioCorte = data;
                  });
                },
              ),
              const SizedBox(height: 20),
              const CustomText(text: 'Horímetro de corte'),
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
                  inputFormatters: [
                    FilteringTextInputFormatter.digitsOnly,
                    CustomNumberFormatter()
                  ],
                  keyboardType: TextInputType.number,
                  controller: _horimetroCorte,
                  decoration: const InputDecoration(
                      hintText: "Digite aqui",
                      border: InputBorder.none,
                      hintStyle: TextStyle(
                        color: Color.fromARGB(255, 121, 118, 118),
                        fontSize: 16,
                        fontFamily: 'Inter',
                        fontWeight: FontWeight.w500,
                      )),
                ),
              ),
              const CustomText(text: 'Capacidade de carga da aeronave'),
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
                  controller: _capacidadeCarga,
                  keyboardType:
                      const TextInputType.numberWithOptions(decimal: false),
                  inputFormatters: <TextInputFormatter>[
                    FilteringTextInputFormatter.digitsOnly,
                    CustomNumberFormatterTho()
                  ],
                  decoration: const InputDecoration(
                      hintText: "Digite aqui",
                      border: InputBorder.none,
                      hintStyle: TextStyle(
                        color: Color.fromARGB(255, 121, 118, 118),
                        fontSize: 16,
                        fontFamily: 'Inter',
                        fontWeight: FontWeight.w500,
                      )),
                ),
              ),
              const CustomText(text: 'Total de água utilizada na operação'),
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
                  controller: _totalAguaUtilizada,
                  keyboardType:
                      const TextInputType.numberWithOptions(decimal: false),
                  inputFormatters: <TextInputFormatter>[
                    FilteringTextInputFormatter.digitsOnly,
                    CustomNumberFormatterTho()
                  ],
                  decoration: const InputDecoration(
                      hintText: "Digite aqui",
                      border: InputBorder.none,
                      hintStyle: TextStyle(
                        color: Color.fromARGB(255, 121, 118, 118),
                        fontSize: 16,
                        fontFamily: 'Inter',
                        fontWeight: FontWeight.w500,
                      )),
                ),
              ),
              const SizedBox(height: 14),
              Center(
                child: CustomButton(
                  title: "Próximo",
                  onClick: () async {
                    await _actionFirefighting();
                    Navigator.push(
                        context,
                        MaterialPageRoute(
                          builder: (context) => ContratoPrestacaoServicoPage(
                              firefightingController:
                                  widget._firefightingController!),
                        ));
                  
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

class CustomNumberFormatterTho extends TextInputFormatter {
  @override
  TextEditingValue formatEditUpdate(
      TextEditingValue oldValue, TextEditingValue newValue) {
    if (newValue.text.isEmpty) {
      return newValue.copyWith(text: '');
    }

    final number = int.tryParse(newValue.text.replaceAll('.', ''));

    if (number != null) {
      final formattedText = number.toString().replaceAllMapped(
            RegExp(r'(\d{1,3})(?=(\d{3})+(?!\d))'),
            (Match match) => '${match[1]}.',
          );

      return newValue.copyWith(
        text: formattedText,
        selection: TextSelection.collapsed(offset: formattedText.length),
      );
    }

    return oldValue;
  }
}
