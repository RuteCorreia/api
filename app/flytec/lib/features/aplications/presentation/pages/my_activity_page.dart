import 'package:flutter/material.dart';
import 'package:flytec/features/fire_fighting/presentation/pages/steps/add_firefighting_fourth_step.dart';
import 'package:go_router/go_router.dart';
import 'package:intl/intl.dart';

import '../../../auth/presentation/widgets/custom_login_button.dart';

class MyActivityPage extends StatefulWidget {
  const MyActivityPage({super.key});

  @override
  State<MyActivityPage> createState() => _AddFireFightingSecondStepState();
}

class _AddFireFightingSecondStepState extends State<MyActivityPage> {
  late DateTime? dataSelecionada = DateTime.now();
  late TimeOfDay? time = const TimeOfDay(hour: 12, minute: 43);
  late TimeOfDay? horimetro = const TimeOfDay(hour: 15, minute: 43);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
        title: const Text("Minhas Atividades"),
        actions: [
          IconButton(
            icon: const Icon(Icons.search),
            onPressed: () {
              showAdaptiveDialog<String>(
                context: context,
                useSafeArea: true,
                builder: (BuildContext context) => AlertDialog.adaptive(
                  insetPadding: const EdgeInsets.all(15),
                  title: const SizedBox(),
                  content: SizedBox(
                    height: 590,
                    child: SingleChildScrollView(
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          const Text(
                            'Selecione o cliente',
                            style: TextStyle(
                              color: Color(0xFF00B45D),
                              fontSize: 14,
                              fontFamily: 'Inter',
                              fontWeight: FontWeight.w700,
                              height: 0.11,
                            ),
                          ),
                          const SizedBox(height: 15),
                          CustomComboBox(
                            selectedName: "...",
                            onTap: () async {},
                          ),
                          const SizedBox(height: 15),
                          const Text(
                            'Selecione a data',
                            style: TextStyle(
                              color: Color(0xFF00B45D),
                              fontSize: 14,
                              fontFamily: 'Inter',
                              fontWeight: FontWeight.w700,
                              height: 0.11,
                            ),
                          ),
                          const SizedBox(height: 20),
                          CustomComboBox(
                            selectedName: dataSelecionada == null
                                ? "Selecione"
                                : DateFormat('dd/MM/yyyy')
                                    .format(dataSelecionada!),
                            onTap: () async {
                              final data = await showDatePicker(
                                confirmText: "Selecionar data",
                                cancelText: "Cancelar",
                                helpText: "",
                                context: context,
                                initialDate: DateTime.now(),
                                firstDate: DateTime(2023),
                                lastDate: DateTime(2024),
                              );
                              setState(() {
                                dataSelecionada = data;
                              });
                            },
                          ),
                          const SizedBox(height: 20),
                          const Text(
                            'Hectare',
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
                            padding: const EdgeInsets.symmetric(
                                horizontal: 16, vertical: 10),
                            decoration: ShapeDecoration(
                              shape: RoundedRectangleBorder(
                                side: const BorderSide(
                                    width: 1, color: Color(0xFF636363)),
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
                            'Selecione o piloto',
                            style: TextStyle(
                              color: Color(0xFF00B45D),
                              fontSize: 14,
                              fontFamily: 'Inter',
                              fontWeight: FontWeight.w700,
                              height: 0.11,
                            ),
                          ),
                          const SizedBox(height: 15),
                          CustomComboBox(
                            selectedName: "...",
                            onTap: () async {},
                          ),
                          const SizedBox(height: 15),
                          const Text(
                            'Selecione o executor',
                            style: TextStyle(
                              color: Color(0xFF00B45D),
                              fontSize: 14,
                              fontFamily: 'Inter',
                              fontWeight: FontWeight.w700,
                              height: 0.11,
                            ),
                          ),
                          const SizedBox(height: 15),
                          CustomComboBox(
                            selectedName: "...",
                            onTap: () async {},
                          ),
                          const SizedBox(height: 15),
                          const Text(
                            'Selecione a aeronave',
                            style: TextStyle(
                              color: Color(0xFF00B45D),
                              fontSize: 14,
                              fontFamily: 'Inter',
                              fontWeight: FontWeight.w700,
                              height: 0.11,
                            ),
                          ),
                          const SizedBox(height: 15),
                          CustomComboBox(
                            selectedName: "...",
                            onTap: () async {},
                          ),
                          const SizedBox(height: 15),
                          Center(
                            child: CustomButton(
                              title: "FILTRAR",
                              onClick: () {
                                context.pop();
                              },
                            ),
                          ),
                        ],
                      ),
                    ),
                  ),
                  actions: const <Widget>[],
                ),
              );
            },
          ),
          const SizedBox(width: 10),
        ],
      ),
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: SingleChildScrollView(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const SizedBox(height: 16),
              const CustomText(text: 'Prefixo da Aeronave'),
              const SizedBox(height: 14),
              CustomComboBox(
                selectedName: "Selecione",
                onTap: () async {},
              ),
              const SizedBox(height: 20),
              const CustomText(text: 'Data inicial'),
              const SizedBox(height: 14),
              CustomComboBox(
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
                    firstDate: DateTime(2023),
                    lastDate: DateTime(2024),
                  );
                  setState(() {
                    dataSelecionada = data;
                  });
                },
              ),
              const SizedBox(height: 20),
              const CustomText(text: 'Data final'),
              const SizedBox(height: 14),
              CustomComboBox(
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
                    firstDate: DateTime(2023),
                    lastDate: DateTime(2024),
                  );
                  setState(() {
                    dataSelecionada = data;
                  });
                },
              ),
              const SizedBox(height: 40),
              const Text(
                'Hectares voados',
                style: TextStyle(
                  color: Color(0xFF00B45D),
                  fontSize: 14,
                  fontFamily: 'Inter',
                  fontWeight: FontWeight.w700,
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
                      hintText: "-",
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
              const SizedBox(height: 10),
              const Text(
                'Comissão',
                style: TextStyle(
                  color: Color(0xFF00B45D),
                  fontSize: 14,
                  fontFamily: 'Inter',
                  fontWeight: FontWeight.w700,
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
                      hintText: "-",
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
