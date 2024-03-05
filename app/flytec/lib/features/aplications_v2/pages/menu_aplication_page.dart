import 'package:flutter/material.dart';
import 'package:flytec/features/aplications_v2/models/aplicacao.dart';

class MenuAplicationPage extends StatefulWidget {
  final Aplicacao _aplicacao;

  const MenuAplicationPage({required Aplicacao aplicacao, super.key})
      : _aplicacao = aplicacao;

  @override
  State<MenuAplicationPage> createState() => _MenuAplicationPageState();
}

class _MenuAplicationPageState extends State<MenuAplicationPage> {
  @override
  Widget build(BuildContext context) {
    return const Placeholder();
  }
}
