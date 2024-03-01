import 'package:flutter/material.dart';

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
        title: const Text(
          "Relatório de Aplicação",
          textAlign: TextAlign.center,
        ),
      ),
    );
  }
}
