import 'package:flutter/material.dart';
import 'package:flytec/features/aplications_v2/controller/report_aplication_controller.dart';

class ReportAplicationPage extends StatefulWidget {
  const ReportAplicationPage({super.key});

  @override
  State<ReportAplicationPage> createState() => _ReportAplicationPageState();
}

class _ReportAplicationPageState extends State<ReportAplicationPage> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
        title: Text(
          "Relatório de Aplicação - ${ReportAplicationController.idRelatorioAplicacoes}",
          textAlign: TextAlign.center,
        ),
      ),
    );
  }
}
