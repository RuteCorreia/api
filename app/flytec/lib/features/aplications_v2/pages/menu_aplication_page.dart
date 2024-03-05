import 'package:flutter/material.dart';
import 'package:flytec/features/aplications_v2/controller/report_aplication_controller.dart';
import 'package:flytec/features/aplications_v2/models/aplicacao.dart';

class MenuAplicationPage extends StatefulWidget {
  final Aplicacao _aplicacao;
  final ReportAplicationController _reportAplicationController;

  const MenuAplicationPage(
      {required Aplicacao aplicacao,
      required ReportAplicationController reportAplicationController,
      super.key})
      : _aplicacao = aplicacao,
        _reportAplicationController = reportAplicationController;

  @override
  State<MenuAplicationPage> createState() => _MenuAplicationPageState();
}

class _MenuAplicationPageState extends State<MenuAplicationPage> {
  @override
  Widget build(BuildContext context) {
    return const Scaffold(
      
    );
  }
}
