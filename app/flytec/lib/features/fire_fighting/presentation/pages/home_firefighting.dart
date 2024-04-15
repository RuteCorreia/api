import 'dart:io';

import 'package:flutter/material.dart';
import 'package:flytec/core/utils/pdf_generator.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/core/widgets/combo_box.dart';
import 'package:flytec/features/aplications/components/pilot_select.dart';
import 'package:flytec/features/fire_fighting/services/create_fireflighting_report_service.dart';
import 'package:go_router/go_router.dart';
import 'package:intl/intl.dart';
import 'package:path_provider/path_provider.dart';

import '../../../auth/presentation/widgets/custom_login_button.dart';
import '../../../home/presentation/widgets/custom_dialog_button.dart';
import '../widgets/dashboard_counter.dart';

enum DashBoardState { Enviado, Pronto, Incompleto, NaoEnviado }

class HomeFireFighting extends StatefulWidget {
  const HomeFireFighting({super.key});

  @override
  State<HomeFireFighting> createState() => _HomeFireFightingState();
}

class _HomeFireFightingState extends State<HomeFireFighting> {
  late DateTime? dataSelecionada = DateTime.now();
  late TimeOfDay? time = const TimeOfDay(hour: 12, minute: 43);
  late TimeOfDay? horimetro = const TimeOfDay(hour: 15, minute: 43);
  String selectedPilot = "";
  void openContextMenu() {
    showAdaptiveDialog<String>(
      context: context,
      useSafeArea: true,
      builder: (BuildContext context) => AlertDialog.adaptive(
        insetPadding: const EdgeInsets.all(32),
        content: SizedBox(
          height: 235,
          child: SingleChildScrollView(
            child: Column(
              children: [
                const SizedBox(height: 10),
                const Text(
                  'Escolha uma ação',
                  textAlign: TextAlign.center,
                  style: TextStyle(
                    color: Color.fromARGB(255, 121, 118, 118),
                    fontSize: 16,
                    fontFamily: 'Inter',
                    fontWeight: FontWeight.w500,
                    height: 0.09,
                  ),
                ),
                const SizedBox(height: 20),
                CustomDialogButton(
                  leftIcon: "assets/images/sendicon.svg",
                  text: "Enviar",
                  showRightcon: false,
                  onClick: () {},
                ),
                const SizedBox(height: 10),
                CustomDialogButton(
                  leftIcon: "assets/images/edit.svg",
                  showRightcon: false,
                  onClick: () {
                    context.pop();
                    context.push("/combateincendio", extra: "dd");
                  },
                  text: "Editar",
                ),
                const SizedBox(height: 10),
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
        title: const Text("Combate a Incêndio"),
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
                    height: 450,
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
                          CustomComboBoxExpanded(
                            selectedName: "...",
                            onTap: () async {},
                          ),
                          const SizedBox(height: 15),
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
                          CustomComboBoxExpanded(
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
                                //locale: const Locale("pt-BR"),
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
                          CustomComboBoxExpanded(
                            selectedName: selectedPilot.isEmpty
                                ? "Selecione"
                                : selectedPilot,
                            onTap: () async {
                              Util.closeKeyBoard();
await showDialog(
                                  context: context,
                                  builder: (BuildContext context) {
                                    return AlertDialog(
                                        backgroundColor:
                                            const Color(0xFFF5F5F5),
                                        content: SizedBox(
                                          width: double.maxFinite,
                                          child:
                                              PilotSelect(onChanged: (value) {
                                            setState(() {
                                              selectedPilot = value!;
                                            });
                                            Util.closeKeyBoard();
                                          }),
                                        ));
                                  });
                            },
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
                          CustomComboBoxExpanded(
                            selectedName: "...",
                            onTap: () async {},
                          ),
                          const SizedBox(height: 15),
                          Center(
                            child: CustomButton(
                              title: "FILTRAR",
                              onClick: () {
                                context.push("/myactivity");
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
      body: const Padding(
        padding: EdgeInsets.all(16.0),
        child: SingleChildScrollView(
          child: Column(
            children: [
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  CustomDashBoardCounter(
                    text: "Enviado",
                    value: "12",
                    state: DashBoardState.Enviado,
                  ),
                  CustomDashBoardCounter(
                    text: "Pronto",
                    value: "5",
                    state: DashBoardState.Pronto,
                  ),
                  CustomDashBoardCounter(
                    text: "Incompleto",
                    value: "3",
                    state: DashBoardState.Incompleto,
                  ),
                  CustomDashBoardCounter(
                    text: "Não enviado",
                    value: "2",
                    state: DashBoardState.NaoEnviado,
                  ),
                ],
              ),
              SizedBox(height: 80),
              Align(
                alignment: Alignment.center,
                child: Text(
                  "Nenhum relatório foi gerado",
                  style: TextStyle(
                    fontSize: 16,
                    fontWeight: FontWeight.w500,
                  ),
                ),
              )
              /*   const SizedBox(height: 20),
              DashBoardReport(
                title: "Fazenda Santa Maria",
                date: "15/10/2023",
                hour: "15:30",
                state: DashBoardState.Pronto,
                onClick: () {
                  openContextMenu();
                },
              ),
              DashBoardReport(
                title: "Fazenda Santa Maria",
                date: "15/10/2023",
                hour: "15:30",
                onClick: () {
                  showAdaptiveDialog<String>(
                    context: context,
                    useSafeArea: true,
                    builder: (BuildContext context) => AlertDialog.adaptive(
                      insetPadding: const EdgeInsets.all(32),
                      content: SizedBox(
                        height: 230,
                        child: SingleChildScrollView(
                          child: Column(
                            children: [
                              const SizedBox(height: 10),
                              const Text(
                                'Escolha uma ação',
                                textAlign: TextAlign.center,
                                style: TextStyle(
                                  color: Color.fromARGB(255, 121, 118, 118),
                                  fontSize: 16,
                                  fontFamily: 'Inter',
                                  fontWeight: FontWeight.w500,
                                  height: 0.09,
                                ),
                              ),
                              const SizedBox(height: 20),
                              CustomDialogButton(
                                leftIcon: "assets/images/sendicon.svg",
                                text: "Enviar",
                                showRightcon: false,
                                onClick: () {},
                              ),
                              const SizedBox(height: 10),
                              CustomDialogButton(
                                leftIcon: "assets/images/edit.svg",
                                showRightcon: false,
                                onClick: () {
                                  context.pop();
                                  context.push("/combateincendio", extra: "dd");
                                },
                                text: "Editar",
                              ),
                              const SizedBox(height: 10),
                            ],
                          ),
                        ),
                      ),
                      actions: const <Widget>[],
                    ),
                  );
                },
                state: DashBoardState.NaoEnviado,
              ),
              DashBoardReport(
                title: "Fazenda Santa Maria",
                date: "15/10/2023",
                hour: "15:30",
                onClick: () {
                  showAdaptiveDialog<String>(
                    context: context,
                    useSafeArea: true,
                    builder: (BuildContext context) => AlertDialog.adaptive(
                      insetPadding: const EdgeInsets.all(32),
                      content: SizedBox(
                        height: 130,
                        child: SingleChildScrollView(
                          child: Column(
                            children: [
                              const SizedBox(height: 10),
                              const Text(
                                'Escolha uma ação',
                                textAlign: TextAlign.center,
                                style: TextStyle(
                                  color: Color.fromARGB(255, 121, 118, 118),
                                  fontSize: 16,
                                  fontFamily: 'Inter',
                                  fontWeight: FontWeight.w500,
                                  height: 0.09,
                                ),
                              ),
                              const SizedBox(height: 20),
                              CustomDialogButton(
                                leftIcon: "assets/images/edit.svg",
                                showRightcon: false,
                                onClick: () {
                                  context.pop();
                                  context.push("/combateincendio", extra: "dd");
                                },
                                text: "Editar",
                              ),
                              const SizedBox(height: 10),
                            ],
                          ),
                        ),
                      ),
                      actions: const <Widget>[],
                    ),
                  );
                },
                state: DashBoardState.Incompleto,
              ),
              DashBoardReport(
                title: "Fazenda Santa Maria",
                date: "15/10/2023",
                hour: "15:30",
                onClick: () {
                  showAdaptiveDialog<String>(
                    context: context,
                    useSafeArea: true,
                    builder: (BuildContext context) => AlertDialog.adaptive(
                      insetPadding: const EdgeInsets.all(32),
                      content: SizedBox(
                        height: 130,
                        child: SingleChildScrollView(
                          child: Column(
                            children: [
                              const SizedBox(height: 10),
                              const Text(
                                'Escolha uma ação',
                                textAlign: TextAlign.center,
                                style: TextStyle(
                                  color: Color.fromARGB(255, 121, 118, 118),
                                  fontSize: 16,
                                  fontFamily: 'Inter',
                                  fontWeight: FontWeight.w500,
                                  height: 0.09,
                                ),
                              ),
                              const SizedBox(height: 20),
                              CustomDialogButton(
                                leftIcon: "assets/images/sendicon.svg",
                                text: "Visualizar",
                                showRightcon: false,
                                onClick: () {},
                              ),
                              const SizedBox(height: 10),
                            ],
                          ),
                        ),
                      ),
                      actions: const <Widget>[],
                    ),
                  );
                },
                state: DashBoardState.Enviado,
              )
            */
            ],
          ),
        ),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () async {
          //context.push("/combateIncendioPasso1");
          PdfGenerator pdfGenerator = CreateFireflightingReportService();
          final document = await pdfGenerator.generatePdf();
          final documentBytes =
              await pdfGenerator.saveDocument(document: document);
          final directory = await getApplicationCacheDirectory();
          File file = File(
              "${directory.path}/relatorio_incendio_${Util.getRandomString(10)}.pdf");
          await file.writeAsBytes(documentBytes!);
          // ignore: use_build_context_synchronously
          context.push("/reportPage", extra: file);

        },
        child: const Icon(
          Icons.add,
          color: Colors.white,
        ),
      ),
    );
  }
}
