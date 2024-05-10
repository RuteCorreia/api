import 'dart:io';

import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:flytec/core/extensions/datetime_extension.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/pdf_generator.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/core/enums/dashboard_state.dart';
import 'package:flytec/features/fire_fighting/controller/firefighting_controller.dart';
import 'package:flytec/features/fire_fighting/models/firefighting.dart';
import 'package:flytec/features/fire_fighting/presentation/pages/steps/add_firefighting_second_step.dart';
import 'package:flytec/features/fire_fighting/services/create_fireflighting_report_service.dart';
import 'package:flytec/features/home/presentation/widgets/custom_dialog_button.dart';
import 'package:go_router/go_router.dart';
import 'package:path_provider/path_provider.dart';

class FirefightingReportCard extends StatelessWidget {
  final List<Firefighting> _firefightingList;
  final FirefightingController _firefightingController;
  final int _index;
  const FirefightingReportCard(
      {super.key,
      required List<Firefighting> firefightingList,
      required FirefightingController firefightingController,
      required int index})
      : _firefightingList = firefightingList,
        _firefightingController = firefightingController,
        _index = index;

  Color get _getColorStateColor {
    if (_firefightingList[_index].state == DashBoardState.Enviado) {
      return Colors.blue;
    }
    if (_firefightingList[_index].state == DashBoardState.Pronto) {
      return Colors.green;
    }
    if (_firefightingList[_index].state == DashBoardState.Incompleto) {
      return const Color(0xFFFF9900);
    }
    if (_firefightingList[_index].state == DashBoardState.NaoEnviado) {
      return Colors.red;
    }
    return Colors.blue;
  }

  String get _getTitleStateColor {
    if (_firefightingList[_index].state == DashBoardState.Enviado) {
      return "Relatório enviado";
    }
    if (_firefightingList[_index].state == DashBoardState.Pronto) {
      return "Relatório pronto para envio";
    }
    if (_firefightingList[_index].state == DashBoardState.Incompleto) {
      return "Relatório incompleto";
    }
    if (_firefightingList[_index].state == DashBoardState.NaoEnviado) {
      return "Relatório não enviado";
    }
    return "Sem descrição";
  }

  DateTime get _date {
    if (_firefightingList[_index].data != null) {
      return DateTime.fromMillisecondsSinceEpoch(
          _firefightingList[_index].data!);
    }
    return DateTime.now();
  }

  Future<void> _openContextMenu(BuildContext context) async {
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
                  _firefightingController
                      .setFirefightingSelected(_firefightingList[_index]);
                  getIt<GlobalConfigVars>().selectedExecutor =
                      _firefightingList[_index].executor!;
                  getIt<GlobalConfigVars>().selectedPilot =
                      _firefightingList[_index].piloto!;
                  Navigator.push(context, MaterialPageRoute(builder: (context) {
                    return  AddFireFightingSecondStep(firefightingController: _firefightingController, orgaoPrivado: false,);
                  }));
                },
                text: "Editar",
              ),
              const SizedBox(height: 10),
              CustomDialogButton(
                leftIcon: "assets/images/cancel.svg",
                showRightcon: false,
                onClick: () async {
                  PdfGenerator pdfGenerator = CreateFirefightingReportService(
                      _firefightingList[_index]);
                  final document = await pdfGenerator.generatePdf();
                  final documentBytes =
                      await pdfGenerator.saveDocument(document: document);
                  final directory = await getApplicationCacheDirectory();
                  File file = File(
                      "${directory.path}/relatorio_combate_incendio_${Util.getRandomString(10)}.pdf");
                  await file.writeAsBytes(documentBytes!);
                  // ignore: use_build_context_synchronously
                  context.pop();
                  // ignore: use_build_context_synchronously
                  context.push("/reportCombateIncendio", extra: file);
                },
                text: "Gerar Relatório",
              ),
              const SizedBox(height: 10),
              CustomDialogButton(
                leftIcon: "assets/images/cancel.svg",
                showRightcon: false,
                onClick: () async {
                  context.pop();
                },
                text: "Cancelar",
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
  Widget build(BuildContext context) {
    return InkWell(
      onTap: () async {
        await _openContextMenu(context);
      },
      child: Container(
        width: 328,
        height: 150,
        margin: const EdgeInsets.symmetric(vertical: 10),
        padding: const EdgeInsets.all(16),
        clipBehavior: Clip.antiAlias,
        decoration: ShapeDecoration(
          color: const Color(0xFFECEAEA),
          shape: RoundedRectangleBorder(
            side: BorderSide(width: 2, color: _getColorStateColor),
            borderRadius: BorderRadius.circular(8),
          ),
        ),
        child: Row(
          mainAxisSize: MainAxisSize.min,
          mainAxisAlignment: MainAxisAlignment.start,
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            Expanded(
              child: Column(
                mainAxisSize: MainAxisSize.min,
                mainAxisAlignment: MainAxisAlignment.center,
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(
                    _firefightingList[_index].cliente ?? "",
                    style: const TextStyle(
                      color: Color.fromARGB(255, 121, 118, 118),
                      fontSize: 14,
                      fontFamily: 'Inter',
                      fontWeight: FontWeight.w600,
                    ),
                  ),
                  const SizedBox(height: 15),
                  Container(
                    padding:
                        const EdgeInsets.symmetric(horizontal: 8, vertical: 2),
                    decoration: ShapeDecoration(
                      shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(8)),
                    ),
                    child: Row(
                      mainAxisSize: MainAxisSize.min,
                      mainAxisAlignment: MainAxisAlignment.start,
                      crossAxisAlignment: CrossAxisAlignment.center,
                      children: [
                        Container(
                          width: 12,
                          height: 12,
                          decoration: ShapeDecoration(
                            color: _getColorStateColor,
                            shape: RoundedRectangleBorder(
                                borderRadius: BorderRadius.circular(4)),
                          ),
                        ),
                        const SizedBox(width: 10),
                        Text(
                          _getTitleStateColor,
                          style: TextStyle(
                            color: _getColorStateColor,
                            fontSize: 14,
                            fontFamily: 'Inter',
                            fontWeight: FontWeight.w700,
                            height: 0.11,
                          ),
                        ),
                      ],
                    ),
                  ),
                  const SizedBox(height: 8),
                  Row(
                    mainAxisSize: MainAxisSize.min,
                    mainAxisAlignment: MainAxisAlignment.start,
                    crossAxisAlignment: CrossAxisAlignment.center,
                    children: [
                      Container(
                        padding: const EdgeInsets.all(8),
                        decoration: ShapeDecoration(
                          shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(8)),
                        ),
                        child: Row(
                          mainAxisSize: MainAxisSize.min,
                          mainAxisAlignment: MainAxisAlignment.start,
                          crossAxisAlignment: CrossAxisAlignment.center,
                          children: [
                            Container(
                              width: 16,
                              height: 16,
                              clipBehavior: Clip.antiAlias,
                              decoration: const BoxDecoration(),
                              child: Row(
                                mainAxisSize: MainAxisSize.min,
                                mainAxisAlignment: MainAxisAlignment.center,
                                crossAxisAlignment: CrossAxisAlignment.center,
                                children: [
                                  SizedBox(
                                    width: 16,
                                    height: 16,
                                    child: Stack(children: [
                                      SvgPicture.asset(
                                          "assets/images/calendar.svg")
                                    ]),
                                  ),
                                ],
                              ),
                            ),
                            const SizedBox(width: 10),
                            Text(
                              Util.getTodayDate(date: _date),
                              style: const TextStyle(
                                color: Color.fromARGB(255, 121, 118, 118),
                                fontSize: 12,
                                fontFamily: 'Inter',
                                fontWeight: FontWeight.w600,
                                height: 0.12,
                              ),
                            ),
                          ],
                        ),
                      ),
                      const SizedBox(width: 16),
                      Container(
                        padding: const EdgeInsets.all(8),
                        decoration: ShapeDecoration(
                          shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(8)),
                        ),
                        child: Row(
                          mainAxisSize: MainAxisSize.min,
                          mainAxisAlignment: MainAxisAlignment.start,
                          crossAxisAlignment: CrossAxisAlignment.center,
                          children: [
                            Container(
                              width: 16,
                              height: 16,
                              clipBehavior: Clip.antiAlias,
                              decoration: const BoxDecoration(),
                              child: Row(
                                mainAxisSize: MainAxisSize.min,
                                mainAxisAlignment: MainAxisAlignment.center,
                                crossAxisAlignment: CrossAxisAlignment.center,
                                children: [
                                  SizedBox(
                                    width: 16,
                                    height: 16,
                                    child: Stack(children: [
                                      SvgPicture.asset(
                                          "assets/images/timer.svg")
                                    ]),
                                  ),
                                ],
                              ),
                            ),
                            const SizedBox(width: 10),
                            Text(
                              _date.to24hours(),
                              style: const TextStyle(
                                color: Color.fromARGB(255, 121, 118, 118),
                                fontSize: 12,
                                fontFamily: 'Inter',
                                fontWeight: FontWeight.w600,
                                height: 0.12,
                              ),
                            ),
                          ],
                        ),
                      ),
                    ],
                  ),
                ],
              ),
            ),
            const SizedBox(width: 16),
            Container(
              width: 24,
              height: 24,
              clipBehavior: Clip.antiAlias,
              decoration: const BoxDecoration(),
              child: Stack(
                children: [SvgPicture.asset("assets/images/arrow.svg")],
              ),
            ),
          ],
        ),
      ),
    );
  }
}
