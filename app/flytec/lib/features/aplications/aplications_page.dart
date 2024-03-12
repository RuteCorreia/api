import 'package:flutter/material.dart';
import 'package:flytec/features/aplications/components/dashboard_counter.dart';
import 'package:flytec/features/aplications/components/report_card_aplication.dart';
import 'package:flytec/features/aplications/controller/report_aplication_controller.dart';
import 'package:flytec/features/aplications/enums/report_dashboard_state.dart';
import 'package:flytec/features/aplications/pages/create_aplication_page.dart';

class AplicationsPage extends StatefulWidget {
  final ReportAplicationController _reportAplicationController;
  const AplicationsPage(
      {super.key,
      required ReportAplicationController reportAplicationController})
      : _reportAplicationController = reportAplicationController;

  @override
  State<AplicationsPage> createState() => _AplicationsPageState();
}

class _AplicationsPageState extends State<AplicationsPage> {
  final ScrollController _scrollController = ScrollController();

  void _updateView() {
    setState(() {});
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
        title: const Text(
          "Aplicações",
          textAlign: TextAlign.center,
        ),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () async {
          widget._reportAplicationController.setListRelatorioAplicacao(
              widget._reportAplicationController.listaAplicacao);
          setState(() {});
          Navigator.push(context, MaterialPageRoute(builder: (context) {
            return CreateAplicationPage(
              reportAplicationController: widget._reportAplicationController,
              updateView: _updateView,
            );
          }));
        },
        child: const Icon(Icons.add, color: Colors.white),
      ),
      body: Builder(
        builder: (context) {
          if (widget._reportAplicationController.listaAplicacao == null) {
            return const Center(
              child: CircularProgressIndicator(),
            );
          }
          if (widget._reportAplicationController.listaAplicacao!.isEmpty) {
            return const Align(
              alignment: Alignment.center,
              child: Text(
                "Nenhum relatório foi gerado",
                style: TextStyle(
                  fontSize: 16,
                  fontWeight: FontWeight.w500,
                ),
              ),
            );
          }
          return Padding(
            padding: const EdgeInsets.only(left: 10.0, right: 10.0, top: 16.0),
            child: SingleChildScrollView(
              controller: _scrollController,
              child: Column(
                children: [
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    crossAxisAlignment: CrossAxisAlignment.center,
                    children: [
                      ReportDashBoardCounter(
                        text: "Enviado",
                        value: widget._reportAplicationController
                            .obtainQuantityReportsByState(
                                ReportDashBoardState.Enviado)
                            .toString(),
                        state: ReportDashBoardState.Enviado,
                      ),
                      ReportDashBoardCounter(
                        text: "Pronto",
                        value: widget._reportAplicationController
                            .obtainQuantityReportsByState(
                                ReportDashBoardState.Pronto)
                            .toString(),
                        state: ReportDashBoardState.Pronto,
                      ),
                      ReportDashBoardCounter(
                        text: "Incompleto",
                        value: widget._reportAplicationController
                            .obtainQuantityReportsByState(
                                ReportDashBoardState.Incompleto)
                            .toString(),
                        state: ReportDashBoardState.Incompleto,
                      ),
                      ReportDashBoardCounter(
                        text: "Não enviado",
                        value: widget._reportAplicationController
                            .obtainQuantityReportsByState(
                                ReportDashBoardState.NaoEnviado)
                            .toString(),
                        state: ReportDashBoardState.NaoEnviado,
                      ),
                    ],
                  ),
                  const SizedBox(height: 20),
                  ListView.builder(
                      controller: _scrollController,
                      shrinkWrap: true,
                      itemCount: widget
                          ._reportAplicationController.listaAplicacao!.length,
                      reverse: true,
                      itemBuilder: (context, index) {
                        return ReportCardAplication(
                            reportAplicationController:
                                widget._reportAplicationController,
                            index: index);
                      })
                ],
              ),
            ),
          );
        },
      ),
    );
  }
}
