import 'package:flutter/material.dart';
import 'package:flytec/features/aplications_v2/components/dashboard_counter.dart';
import 'package:flytec/features/aplications_v2/components/report_card_aplication.dart';
import 'package:flytec/features/aplications_v2/controller/aplications_initialization_controller.dart';
import 'package:flytec/features/aplications_v2/controller/report_aplication_controller.dart';
import 'package:flytec/features/aplications_v2/enums/report_dashboard_state.dart';
import 'package:flytec/features/aplications_v2/pages/create_aplication_page.dart';

class AplicationsPage extends StatefulWidget {
  const AplicationsPage({super.key});

  @override
  State<AplicationsPage> createState() => _AplicationsPageState();
}

class _AplicationsPageState extends State<AplicationsPage> {
  late AplicationsInitializationController _aplicationsInitializationController;
  final ReportAplicationController _reportAplicationController =
      ReportAplicationController();

  Future<void> _initializationAplicationsReports() async {
    _aplicationsInitializationController =
        AplicationsInitializationController();
    await _aplicationsInitializationController.initialize();
    _reportAplicationController.setListRelatorioAplicacao(
        _aplicationsInitializationController.reportsAplications);
    setState(() {});
  }

  @override
  void initState() {
    super.initState();
    _initializationAplicationsReports();
  }

  final ScrollController _scrollController = ScrollController();

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
          _reportAplicationController.setListRelatorioAplicacao(
              _aplicationsInitializationController.reportsAplications);
          setState(() {});
          Navigator.push(context, MaterialPageRoute(builder: (context) {
            return CreateAplicationPage(
              reportAplicationController: _reportAplicationController,
            );
          }));
        },
        child: const Icon(Icons.add, color: Colors.white),
      ),
      body: Builder(
        builder: (context) {
          if (_aplicationsInitializationController.reportsAplications == null) {
            return const Center(
              child: CircularProgressIndicator(),
            );
          }
          if (_aplicationsInitializationController
              .reportsAplications!.isEmpty) {
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
                        value: _reportAplicationController
                            .obtainQuantityReportsByState(
                                ReportDashBoardState.Enviado)
                            .toString(),
                        state: ReportDashBoardState.Enviado,
                      ),
                      ReportDashBoardCounter(
                        text: "Pronto",
                        value: _reportAplicationController
                            .obtainQuantityReportsByState(
                                ReportDashBoardState.Pronto)
                            .toString(),
                        state: ReportDashBoardState.Pronto,
                      ),
                      ReportDashBoardCounter(
                        text: "Incompleto",
                        value: _reportAplicationController
                            .obtainQuantityReportsByState(
                                ReportDashBoardState.Incompleto)
                            .toString(),
                        state: ReportDashBoardState.Incompleto,
                      ),
                      ReportDashBoardCounter(
                        text: "Não enviado",
                        value: _reportAplicationController
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
                      itemCount: _aplicationsInitializationController
                          .reportsAplications?.length,
                      reverse: true,
                      itemBuilder: (context, index) {
                        return ReportCardAplication(
                            reportAplicationController:
                                _reportAplicationController,
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
