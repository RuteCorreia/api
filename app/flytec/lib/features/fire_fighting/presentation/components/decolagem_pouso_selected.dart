import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flytec/core/extensions/time_of_day_extension.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/core/widgets/combo_box.dart';
import 'package:flytec/features/auth/presentation/widgets/custom_login_button.dart';
import 'package:flytec/features/fire_fighting/models/decolagem_pouso_firefighting.dart';
import 'package:go_router/go_router.dart';

class DecolagemPousoSelected extends StatefulWidget {
  final Function(DecolagemPousoFirefighting element)
      onAddNewElementsInDecolagemPousoList;

  const DecolagemPousoSelected(
      {required this.onAddNewElementsInDecolagemPousoList, super.key});

  @override
  State<DecolagemPousoSelected> createState() => _DecolagemPousoSelectedState();
}

class _DecolagemPousoSelectedState extends State<DecolagemPousoSelected> {
  final TextEditingController _horimetroDecolagem = TextEditingController();

  final TextEditingController _horimetroPouso = TextEditingController();

  TimeOfDay? _horarioDecolagem = const TimeOfDay(hour: 15, minute: 43);

  TimeOfDay? _horarioPouso = const TimeOfDay(hour: 15, minute: 43);

  @override
  Widget build(BuildContext context) {
    return SingleChildScrollView(
      child: Column(
        mainAxisSize: MainAxisSize.min,
        crossAxisAlignment: CrossAxisAlignment.center,
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          const SizedBox(
            width: 244,
            height: 50,
            child: Text(
              'Adicionar Decolagem e Pouso',
              textAlign: TextAlign.center,
              style: TextStyle(
                color: Color.fromARGB(255, 121, 118, 118),
                fontSize: 16,
                fontFamily: 'Inter',
                fontWeight: FontWeight.w500,
              ),
            ),
          ),
          SizedBox(
            height: 300,
            child: Column(
              children: [
                const SizedBox(
                  width: 328,
                  child: Text(
                    'Decolagem',
                    style: TextStyle(
                      color: Color.fromARGB(255, 121, 118, 118),
                      fontSize: 14,
                      fontFamily: 'Inter',
                      fontWeight: FontWeight.w600,
                      height: 0.11,
                    ),
                  ),
                ),
                const SizedBox(height: 30),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        const Text(
                          'Horário',
                          style: TextStyle(
                            color: Color(0xFF00B45D),
                            fontSize: 14,
                            fontFamily: 'Inter',
                            fontWeight: FontWeight.w700,
                            height: 0.11,
                          ),
                        ),
                        const SizedBox(height: 10),
                        SizedBox(
                          width: 110,
                          height: 50,
                          child: CustomComboBox(
                            selectedName: _horarioDecolagem == null
                                ? "Selecione"
                                : _horarioDecolagem!.to24hours(),
                            onTap: () async {
                              final data = await showTimePicker(
                                  confirmText: "Selecionar hora",
                                  cancelText: "Cancelar",
                                  helpText: "",
                                  context: context,
                                  initialTime:
                                      const TimeOfDay(hour: 12, minute: 23));
      
                              _horarioDecolagem = data;
                              setState(() {});
                            },
                          ),
                        ),
                      ],
                    ),
                    Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        const Text(
                          'Horímetro',
                          style: TextStyle(
                            color: Color(0xFF00B45D),
                            fontSize: 14,
                            fontFamily: 'Inter',
                            fontWeight: FontWeight.w700,
                            height: 0.11,
                          ),
                        ),
                        const SizedBox(height: 10),
                        Container(
                          width: 110,
                          height: 50,
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
                            controller: _horimetroDecolagem,
                            inputFormatters: [
                              FilteringTextInputFormatter.digitsOnly,
                              CustomNumberFormatter()
                            ],
                            keyboardType: TextInputType.number,
                            decoration: const InputDecoration(
                                hintText: "Digite aqui",
                                border: InputBorder.none,
                                hintStyle: TextStyle(
                                  color: Color.fromARGB(255, 121, 118, 118),
                                  fontSize: 13,
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
                const SizedBox(height: 40),
                const SizedBox(
                  width: 328,
                  child: Text(
                    'Pouso',
                    style: TextStyle(
                      color: Color.fromARGB(255, 121, 118, 118),
                      fontSize: 14,
                      fontFamily: 'Inter',
                      fontWeight: FontWeight.w600,
                      height: 0.11,
                    ),
                  ),
                ),
                const SizedBox(height: 30),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        const Text(
                          'Horário',
                          style: TextStyle(
                            color: Color(0xFF00B45D),
                            fontSize: 14,
                            fontFamily: 'Inter',
                            fontWeight: FontWeight.w700,
                            height: 0.11,
                          ),
                        ),
                        const SizedBox(height: 10),
                        SizedBox(
                          width: 110,
                          height: 50,
                          child: CustomComboBox(
                            selectedName: _horarioPouso == null
                                ? "Selecione"
                                : _horarioPouso!.to24hours(),
                            onTap: () async {
                              final data = await showTimePicker(
                                  confirmText: "Selecionar hora",
                                  cancelText: "Cancelar",
                                  helpText: "",
                                  context: context,
                                  initialTime:
                                      const TimeOfDay(hour: 12, minute: 23));
      
                              _horarioPouso = data;
                              setState(() {});
                            },
                          ),
                        ),
                      ],
                    ),
                    Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        const Text(
                          'Horímetro',
                          style: TextStyle(
                            color: Color(0xFF00B45D),
                            fontSize: 14,
                            fontFamily: 'Inter',
                            fontWeight: FontWeight.w700,
                            height: 0.11,
                          ),
                        ),
                        const SizedBox(height: 10),
                        Container(
                          width: 110,
                          height: 50,
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
                            inputFormatters: [
                              FilteringTextInputFormatter.digitsOnly,
                              CustomNumberFormatter()
                            ],
                            controller: _horimetroPouso,
                            keyboardType: TextInputType.number,
                            decoration: const InputDecoration(
                                hintText: "Digite aqui",
                                border: InputBorder.none,
                                hintStyle: TextStyle(
                                  color: Color.fromARGB(255, 121, 118, 118),
                                  fontSize: 13,
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
                Center(
                  child: CustomButton(
                    title: "OK",
                    onClick: () {
                      widget.onAddNewElementsInDecolagemPousoList(
                        DecolagemPousoFirefighting(
                          horarioDecolagem: _horarioDecolagem
                              ?.toDateTime()
                              .millisecondsSinceEpoch,
                          horarioPouso:
                              _horarioPouso?.toDateTime().millisecondsSinceEpoch,
                          horimetroDecolagem: _horimetroDecolagem.text,
                          horimetroPouso: _horimetroPouso.text,
                        ),
                      );
                      context.pop();
                    },
                  ),
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}
