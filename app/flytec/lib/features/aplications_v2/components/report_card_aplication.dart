import 'dart:io';

import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/pdf_generator.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/services/create_aplicacao_report_service.dart';
import 'package:flytec/features/aplications_v2/controller/report_aplication_controller.dart';
import 'package:flytec/features/aplications_v2/enums/report_dashboard_state.dart';
import 'package:flytec/features/aplications_v2/pages/menu_aplication_page.dart';
import 'package:flytec/features/home/presentation/widgets/custom_dialog_button.dart';
import 'package:go_router/go_router.dart';
import 'package:path_provider/path_provider.dart';

class ReportCardAplication extends StatelessWidget {
  final ReportAplicationController _reportAplicationController;
  final int _index;
  const ReportCardAplication(
      {super.key,
      required ReportAplicationController reportAplicationController,
      required int index})
      : _reportAplicationController = reportAplicationController,
        _index = index;

  Color get _getColorStateColor {
    if (_reportAplicationController.listaAplicacao![_index].state ==
        ReportDashBoardState.Enviado) {
      return Colors.blue;
    }
    if (_reportAplicationController.listaAplicacao![_index].state ==
        ReportDashBoardState.Pronto) {
      return Colors.green;
    }
    if (_reportAplicationController.listaAplicacao![_index].state ==
        ReportDashBoardState.Incompleto) {
      return const Color(0xFFFF9900);
    }
    if (_reportAplicationController.listaAplicacao![_index].state ==
        ReportDashBoardState.NaoEnviado) {
      return Colors.red;
    }
    return Colors.blue;
  }

  String get _getTitleStateColor {
    if (_reportAplicationController.listaAplicacao![_index].state ==
        ReportDashBoardState.Enviado) {
      return "Relatório enviado";
    }
    if (_reportAplicationController.listaAplicacao![_index].state ==
        ReportDashBoardState.Pronto) {
      return "Relatório pronto para envio";
    }
    if (_reportAplicationController.listaAplicacao![_index].state ==
        ReportDashBoardState.Incompleto) {
      return "Relatório incompleto";
    }
    if (_reportAplicationController.listaAplicacao![_index].state ==
        ReportDashBoardState.NaoEnviado) {
      return "Relatório não enviado";
    }
    return "Sem descrição";
  }

  DateTime get _date {
    int? epoch =
        int.tryParse(_reportAplicationController.listaAplicacao![_index].data!);
    if (epoch != null) {
      return DateTime.fromMillisecondsSinceEpoch(epoch);
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
                  _reportAplicationController.setAplicacaoSelected(
                      _reportAplicationController.listaAplicacao![_index]);
                  getIt<GlobalConfigVars>().selectedExecutor =
                      _reportAplicationController
                          .listaAplicacao![_index].executor!;
                  getIt<GlobalConfigVars>().selectedPilot =
                      _reportAplicationController
                          .listaAplicacao![_index].piloto!;
                  Navigator.push(context, MaterialPageRoute(builder: (context) {
                    return MenuAplicationPage(
                        reportAplicationController:
                            _reportAplicationController);
                  }));
                },
                text: "Editar",
              ),
              const SizedBox(height: 10),
              CustomDialogButton(
                leftIcon: "assets/images/cancel.svg",
                showRightcon: false,
                onClick: () async {
                  PdfGenerator pdfGenerator = CreateAplicacaoReportService(
                      aplicacao:
                          _reportAplicationController.listaAplicacao![_index]);
                  final document = await pdfGenerator.generatePdf();
                  final documentBytes =
                      await pdfGenerator.saveDocument(document: document);
                  final directory = await getApplicationCacheDirectory();
                  File file = File(
                      "${directory.path}/relatorio_${Util.getRandomString(10)}.pdf");
                  await file.writeAsBytes(documentBytes!);
                  // ignore: use_build_context_synchronously
                  context.pop();
                  // ignore: use_build_context_synchronously
                  context.push("/reportPage", extra: file);
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
                    _reportAplicationController
                            .listaAplicacao![_index].contratante?.nome ??
                        "",
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
                              "${_date.hour}:${_date.minute}",
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
