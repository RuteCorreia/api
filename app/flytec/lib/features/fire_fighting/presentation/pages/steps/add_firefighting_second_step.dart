import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:flytec/core/extensions/time_of_day_extension.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/components/aircraft_prefix_select.dart';
import 'package:flytec/features/aplications/components/components_exports.dart';
import 'package:flytec/features/aplications/pages/contratante_page.dart';
import 'package:go_router/go_router.dart';
import 'package:intl/intl.dart';


class AddFireFightingSecondStep extends StatefulWidget {
  const AddFireFightingSecondStep({super.key});

  @override
  State<AddFireFightingSecondStep> createState() =>
      _AddFireFightingSecondStepState();
}

class _AddFireFightingSecondStepState extends State<AddFireFightingSecondStep> {
  late DateTime? dataSelecionada = DateTime.now();
  late TimeOfDay? time = const TimeOfDay(hour: 12, minute: 43);
  late TimeOfDay? horimetro = const TimeOfDay(hour: 15, minute: 43);
  String airCraftPrexix = "";
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Combate a incêndio"),
      ),
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
                      airCraftPrexix.isEmpty ? "Selecione" : airCraftPrexix,
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
                                    airCraftPrexix = value;
                                  });
                                  Util.closeKeyBoard();
                                }),
                              ));
                        });
                  }),
              const SizedBox(height: 20),
              const CustomText(text: 'Data'),
              const SizedBox(height: 14),
              CustomComboBoxExpanded(
                selectedName: dataSelecionada == null
                    ? "Selecione"
                    : DateFormat('dd/MM/yyyy').format(dataSelecionada!),
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
                    dataSelecionada = data;
                  });
                },
              ),
              const SizedBox(height: 20),
              const CustomText(text: 'Horário inicial'),
              const SizedBox(height: 14),
              CustomComboBoxExpanded(
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
              const CustomText(text: 'Horímetro inicial'),
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

              const SizedBox(height: 20),
              const SizedBox(
                width: 328,
                child: Text(
                  'Pista de operação',
                  style: TextStyle(
                    color: Color.fromARGB(255, 121, 118, 118),
                    fontSize: 14,
                    fontFamily: 'Inter',
                    fontWeight: FontWeight.w600,
                    height: 0.11,
                  ),
                ),
              ),
              const SizedBox(height: 40),
              const Text(
                'Código ICAO / Coordenadas',
                style: TextStyle(
                  color: Color.fromARGB(255, 121, 118, 118),
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
              const Text(
                'Nome',
                style: TextStyle(
                  color: Color.fromARGB(255, 121, 118, 118),
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
                'Local do incêndio / Coordenadas ',
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
