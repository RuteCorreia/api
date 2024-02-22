import 'dart:io';

import 'package:flutter/material.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/pdf_generator.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/core/widgets/combo_box.dart';
import 'package:flytec/features/aplications/presentation/widgets/aircraft_select.dart';
import 'package:flytec/features/aplications/presentation/widgets/dashboard_report.dart';
import 'package:flytec/features/aplications/presentation/widgets/pilot_select.dart';
import 'package:flytec/features/aplications/services/aplication_cache_service.dart';
import 'package:flytec/features/aplications/services/report_aplications_generate_service.dart';
import 'package:go_router/go_router.dart';
import 'package:intl/intl.dart';
import 'package:path_provider/path_provider.dart';

import '../../../auth/presentation/widgets/custom_login_button.dart';
import '../../../home/presentation/widgets/custom_dialog_button.dart';
import '../widgets/dashboard_counter.dart';

enum DashBoardState { Enviado, Pronto, Incompleto, NaoEnviado }

class HomeAplicationPage extends StatefulWidget {
  const HomeAplicationPage({super.key});

  @override
  State<HomeAplicationPage> createState() => _HomeAplicationPageState();
}

class _HomeAplicationPageState extends State<HomeAplicationPage> {
  late DateTime? dataSelecionada = DateTime.now();
  late TimeOfDay? time = const TimeOfDay(hour: 12, minute: 43);
  late TimeOfDay? horimetro = const TimeOfDay(hour: 15, minute: 43);
  String __selectedAaeronave = "";
  late PdfGenerator _pdfGenerator;

  String selectedPilot = "";

  Future<void> _openContextMenu({int? reportListIndex}) async {
    await showAdaptiveDialog<String>(
      context: context,
      useSafeArea: true,
      builder: (BuildContext context) => AlertDialog.adaptive(
        insetPadding: const EdgeInsets.all(32),
        content: SingleChildScrollView(
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
                },
                text: "Editar",
              ),
              const SizedBox(height: 10),
              CustomDialogButton(
                leftIcon: "assets/images/cancel.svg",
                showRightcon: false,
                onClick: () async {
                  _pdfGenerator = ReportAplicationsGenerate(
                      relatorioModel: getIt<ReportCacheService>()
                          .reportList[reportListIndex!]);
                  final document = await _pdfGenerator.generatePdf();
                  final documentBytes =
                      await _pdfGenerator.saveDocument(document: document);
                  final directory = await getApplicationCacheDirectory();
                  File file = File(
                      "${directory.path}/relatorio_${Util.getRandomString(10)}.pdf");
                  await file.writeAsBytes(documentBytes!);
                  context.pop();
                  context.push("/reportPage", extra: file);
                },
                text: "Gerar Relatório",
              ),
              const SizedBox(height: 10),
            ],
          ),
        ),
        actions: const <Widget>[],
      ),
    );
  }

  @override
  void initState() {
    super.initState();
    getIt<ReportCacheService>().reportList.clear();
    for (var element in getIt<GlobalConfigVars>().reportList) {
      getIt<ReportCacheService>().reportList.add(element);
    }
  }

  int _obtainQuantityReportsByState(DashBoardState state) {
    return getIt<GlobalConfigVars>().reportList.where((element) {
      element.dashBoardState ??= DashBoardState.Incompleto;
      return element.dashBoardState == state;
    }).length;
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
        title: const Text("Aplicações"),
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
                    height: 490,
                    child: StatefulBuilder(builder: (context, updateState) {
                      return SingleChildScrollView(
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
                                  initialDate: DateTime.now(),
                                  firstDate: DateTime(2024),
                                  lastDate: DateTime(2028),
                                );
                                Util.closeKeyBoard();

                                updateState(() {
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
                                              updateState(() {
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
                                selectedName: __selectedAaeronave.isEmpty
                                    ? "Selecione"
                                    : __selectedAaeronave,
                                onTap: () async {
                                  await showDialog(
                                      context: context,
                                      builder: (BuildContext context) {
                                        return AlertDialog(
                                            backgroundColor: Colors.grey[100],
                                            content: SizedBox(
                                              width: double.maxFinite,
                                              child: AirCraftSelect(
                                                  onChanged: (value) {
                                                updateState(() {
                                                  __selectedAaeronave = value;
                                                });
                                                Util.closeKeyBoard();
                                              }),
                                            ));
                                      });
                                }),
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
                      );
                    }),
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
        padding: const EdgeInsets.all(16.0),
        child: SingleChildScrollView(
          child: Column(
            children: [
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  CustomDashBoardCounter(
                    text: "Enviado",
                    value: _obtainQuantityReportsByState(DashBoardState.Enviado)
                        .toString(),
                    state: DashBoardState.Enviado,
                  ),
                  CustomDashBoardCounter(
                    text: "Pronto",
                    value: _obtainQuantityReportsByState(DashBoardState.Pronto)
                        .toString(),
                    state: DashBoardState.Pronto,
                  ),
                  CustomDashBoardCounter(
                    text: "Incompleto",
                    value:
                        _obtainQuantityReportsByState(DashBoardState.Incompleto)
                            .toString(),
                    state: DashBoardState.Incompleto,
                  ),
                  CustomDashBoardCounter(
                    text: "Não enviado",
                    value:
                        _obtainQuantityReportsByState(DashBoardState.NaoEnviado)
                            .toString(),
                    state: DashBoardState.NaoEnviado,
                  ),
                ],
              ),
              const SizedBox(height: 80),
              getIt<GlobalConfigVars>().reportList.isEmpty
                  ? const Align(
                      alignment: Alignment.center,
                      child: Text(
                        "Nenhum relatório foi gerado",
                        style: TextStyle(
                          fontSize: 16,
                          fontWeight: FontWeight.w500,
                        ),
                      ),
                    )
                  : const SizedBox(),
              SizedBox(
                height: 400,
                child: ListView.builder(
                    itemCount: getIt<ReportCacheService>().reportList.length,
                    itemBuilder: (context, index) {
                      return DashBoardReport(
                        title: getIt<ReportCacheService>()
                            .reportList[index]
                            .cliente!
                            .nome,
                        date: getIt<ReportCacheService>()
                            .reportList[index]
                            .dadosDoResponsavel!
                            .data,
                        hour: "${DateTime.now().hour}:${DateTime.now().minute}",
                        state: getIt<ReportCacheService>()
                            .reportList[index]
                            .dashBoardState!,
                        onClick: () async {
                          await _openContextMenu(reportListIndex: index);
                        },
                      );
                    }),
              )
            ],
          ),
        ),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () {
          context.push("/aplicationstep1");
        },
        child: const Icon(
          Icons.add,
          color: Colors.white,
        ),
      ),
    );
  }
}
