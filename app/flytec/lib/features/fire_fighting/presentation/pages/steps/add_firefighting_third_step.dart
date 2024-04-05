import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:flytec/core/extensions/time_of_day_extension.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/core/widgets/combo_box.dart';
import 'package:go_router/go_router.dart';

import '../../../../auth/presentation/widgets/custom_login_button.dart';

class AddFireFightingThirdStep extends StatefulWidget {
  const AddFireFightingThirdStep({super.key});

  @override
  State<AddFireFightingThirdStep> createState() =>
      _AddFireFightingSecondStepState();
}

class _AddFireFightingSecondStepState extends State<AddFireFightingThirdStep> {
  late DateTime? dataSelecionada = DateTime.now();
  TimeOfDay? time = const TimeOfDay(hour: 12, minute: 43);
  TimeOfDay? horimetro = const TimeOfDay(hour: 15, minute: 43);
  TimeOfDay? horimetro2 = const TimeOfDay(hour: 15, minute: 43);
  late TimeOfDay? _horarioCorte = const TimeOfDay(hour: 12, minute: 43);
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Combate a Incêndio"),
      ),
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
                  mainAxisAlignment: MainAxisAlignment.start,
                  crossAxisAlignment: CrossAxisAlignment.center,
                  children: [
                    const Expanded(
                      child: Column(
                        mainAxisSize: MainAxisSize.min,
                        mainAxisAlignment: MainAxisAlignment.center,
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          SizedBox(
                            width: double.infinity,
                            child: Row(
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
                                          'Adicionar Decolagem / Pouso',
                                          style: TextStyle(
                                            color: Color.fromARGB(
                                                255, 121, 118, 118),
                                            fontSize: 14,
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
                    const SizedBox(width: 1),
                    InkWell(
                      onTap: () {
                        showAdaptiveDialog<String>(
                          context: context,
                          useSafeArea: true,
                          builder: (BuildContext context) =>
                              StatefulBuilder(builder: (context, update) {
                            return AlertDialog.adaptive(
                              insetPadding: const EdgeInsets.all(15),
                              title: const SizedBox(
                                width: 244,
                                height: 30,
                                child: Text(
                                  'Adicionar Decolagem e Pouso',
                                  textAlign: TextAlign.center,
                                  style: TextStyle(
                                    color: Color.fromARGB(255, 121, 118, 118),
                                    fontSize: 16,
                                    fontFamily: 'Inter',
                                    fontWeight: FontWeight.w500,
                                    height: 0.09,
                                  ),
                                ),
                              ),
                              content: SizedBox(
                                height: 340,
                                child: Column(
                                  children: [
                                    const SizedBox(
                                      width: 328,
                                      child: Text(
                                        'Decolagem',
                                        style: TextStyle(
                                          color: Color.fromARGB(
                                              255, 121, 118, 118),
                                          fontSize: 14,
                                          fontFamily: 'Inter',
                                          fontWeight: FontWeight.w600,
                                          height: 0.11,
                                        ),
                                      ),
                                    ),
                                    const SizedBox(height: 30),
                                    Row(
                                      mainAxisAlignment:
                                          MainAxisAlignment.spaceBetween,
                                      children: [
                                        Column(
                                          crossAxisAlignment:
                                              CrossAxisAlignment.start,
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
                                                selectedName: horimetro == null
                                                    ? "Selecione"
                                                    : horimetro!.to24hours(),
                                                onTap: () async {
                                                  final data =
                                                      await showTimePicker(
                                                          confirmText:
                                                              "Selecionar hora",
                                                          cancelText:
                                                              "Cancelar",
                                                          helpText: "",
                                                          context: context,
                                                          initialTime:
                                                              const TimeOfDay(
                                                                  hour: 12,
                                                                  minute: 23));
                                                  update(() {
                                                    horimetro = data;
                                                  });
                                                },
                                              ),
                                            ),
                                          ],
                                        ),
                                        Column(
                                          crossAxisAlignment:
                                              CrossAxisAlignment.start,
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
                                              padding:
                                                  const EdgeInsets.symmetric(
                                                      horizontal: 16,
                                                      vertical: 10),
                                              decoration: ShapeDecoration(
                                                shape: RoundedRectangleBorder(
                                                  side: const BorderSide(
                                                      width: 1,
                                                      color: Color(0xFF636363)),
                                                  borderRadius:
                                                      BorderRadius.circular(10),
                                                ),
                                              ),
                                              child: TextField(
                                                inputFormatters: [
                                                  FilteringTextInputFormatter
                                                      .digitsOnly,
                                                  CustomNumberFormatter()
                                                ],
                                                keyboardType:
                                                    TextInputType.number,
                                                decoration:
                                                    const InputDecoration(
                                                        hintText: "Digite aqui",
                                                        border:
                                                            InputBorder.none,
                                                        hintStyle: TextStyle(
                                                          color: Color.fromARGB(
                                                              255,
                                                              121,
                                                              118,
                                                              118),
                                                          fontSize: 13,
                                                          fontFamily: 'Inter',
                                                          fontWeight:
                                                              FontWeight.w500,
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
                                          color: Color.fromARGB(
                                              255, 121, 118, 118),
                                          fontSize: 14,
                                          fontFamily: 'Inter',
                                          fontWeight: FontWeight.w600,
                                          height: 0.11,
                                        ),
                                      ),
                                    ),
                                    const SizedBox(height: 30),
                                    Row(
                                      mainAxisAlignment:
                                          MainAxisAlignment.spaceBetween,
                                      children: [
                                        Column(
                                          crossAxisAlignment:
                                              CrossAxisAlignment.start,
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
                                                selectedName: horimetro2 == null
                                                    ? "Selecione"
                                                    : horimetro2!.to24hours(),
                                                onTap: () async {
                                                  final data =
                                                      await showTimePicker(
                                                          confirmText:
                                                              "Selecionar hora",
                                                          cancelText:
                                                              "Cancelar",
                                                          helpText: "",
                                                          context: context,
                                                          initialTime:
                                                              const TimeOfDay(
                                                                  hour: 12,
                                                                  minute: 23));
                                                  update(() {
                                                    horimetro2 = data;
                                                  });
                                                },
                                              ),
                                            ),
                                          ],
                                        ),
                                        Column(
                                          crossAxisAlignment:
                                              CrossAxisAlignment.start,
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
                                              padding:
                                                  const EdgeInsets.symmetric(
                                                      horizontal: 16,
                                                      vertical: 10),
                                              decoration: ShapeDecoration(
                                                shape: RoundedRectangleBorder(
                                                  side: const BorderSide(
                                                      width: 1,
                                                      color: Color(0xFF636363)),
                                                  borderRadius:
                                                      BorderRadius.circular(10),
                                                ),
                                              ),
                                              child: TextField(
                                                inputFormatters: [
                                                  FilteringTextInputFormatter
                                                      .digitsOnly,
                                                  CustomNumberFormatter()
                                                ],
                                                keyboardType:
                                                    TextInputType.number,
                                                decoration:
                                                    const InputDecoration(
                                                        hintText: "Digite aqui",
                                                        border:
                                                            InputBorder.none,
                                                        hintStyle: TextStyle(
                                                          color: Color.fromARGB(
                                                              255,
                                                              121,
                                                              118,
                                                              118),
                                                          fontSize: 13,
                                                          fontFamily: 'Inter',
                                                          fontWeight:
                                                              FontWeight.w500,
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
                                          context.pop();
                                        },
                                      ),
                                    ),
                                  ],
                                ),
                              ),
                              actions: const <Widget>[],
                            );
                          }),
                        );
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
                selectedName: time == null ? "Selecione" : time!.to24hours(),
                onTap: () async {
                  final data = await showTimePicker(
                      confirmText: "Selecionar hora",
                      cancelText: "Cancelar",
                      helpText: "",
                      context: context,
                      initialTime: const TimeOfDay(hour: 12, minute: 23));
                  setState(() {
                    time = data;
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
              const CustomText(text: 'Horário de CORTE'),
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
              const CustomText(text: 'Horímetro de CORTE'),
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
              const SizedBox(height: 20),
              const CustomText(text: 'Capacidade de carga da aeronave'),
              const SizedBox(height: 14),
              CustomComboBox(
                selectedName: "Selecione",
                onTap: () async {
                  /*    final data = await showTimePicker(
                      confirmText: "Selecionar hora",
                      cancelText: "Cancelar",
                      helpText: "",
                      context: context,
                      initialTime: const TimeOfDay(hour: 12, minute: 23));
                  setState(() {
                    horimetro = data;
                  }); */
                },
              ),
              const SizedBox(height: 20),
              const CustomText(text: 'Total de água utilizada na operação'),
              const SizedBox(height: 14),
              CustomComboBox(
                selectedName: "Selecione",
                onTap: () async {},
              ),
              const SizedBox(height: 20),
              Center(
                child: CustomButton(
                  title: "Próximo",
                  onClick: () {
                    context.push("/combateIncendioPasso4");
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

class CustomComboBox extends StatelessWidget {
  const CustomComboBox(
      {super.key, required this.selectedName, required this.onTap});
  final String selectedName;
  final VoidCallback? onTap;
  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onTap,
      child: Container(
        width: 328,
        height: 40,
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
