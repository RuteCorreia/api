import 'package:flutter/material.dart';
import 'package:flytec/features/aplications/components/components_exports.dart';
import 'package:flytec/features/manutencao/components/checklist_revisao_page.dart';
import 'package:flytec/features/manutencao/components/manutencao_componentes_page.dart';
import 'package:flytec/features/manutencao/components/new_manutencao_page.dart';
import 'package:flytec/features/manutencao/components/widgets/custom_card_with_color.dart';
import 'package:flytec/features/manutencao/controller/manutencao_controller.dart';

class ManutencaoComponentesListPage extends StatefulWidget {
  final ManutencaoController _manutencaoController;
  const ManutencaoComponentesListPage(
      {required ManutencaoController manutencaoController, super.key})
      : _manutencaoController = manutencaoController;

  @override
  State<ManutencaoComponentesListPage> createState() =>
      _ManutencaoComponentesListPageState();
}

class _ManutencaoComponentesListPageState
    extends State<ManutencaoComponentesListPage> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: const Text(
            'Manutenção de\nComponente / Aeronave',
            textAlign: TextAlign.center,
          ),
          centerTitle: true,
        ),
        body: Padding(
            padding: const EdgeInsets.all(16.0),
            child: ListView(
              children: [
                const Text(
                  'Aeronave PTX-123',
                  style: TextStyle(fontSize: 14, fontWeight: FontWeight.w500),
                ),
                CustomButton(
                    onClick: () async {
                      Navigator.pushReplacement(
                          context,
                          MaterialPageRoute(
                            builder: (context) =>  CheckListRevisaoPage(manutencaoController: widget._manutencaoController),
                          ));
                    },
                    title: 'Ir para o Checklist'),
                const SizedBox(height: 14),
                CustomButton(
                    onClick: () async {
                      Navigator.pushReplacement(
                          context,
                          MaterialPageRoute(
                              builder: (context) => NewManutencaoPage(
                                    manutencaoController:
                                        widget._manutencaoController,
                                  )));
                    },
                    title: 'Finalizar'),
                const SizedBox(height: 20),
                CustomCardWithColor(
                  title: 'Componente 1',
                  isSelected: true,
                  onTap: () async {
                    Navigator.push(
                        context,
                        MaterialPageRoute(
                            builder: (context) => ManutencaoComponentesPage(
                                  manutencaoController:
                                      widget._manutencaoController,
                                )));
                  },
                ),
                const SizedBox(height: 14),
                CustomCardWithColor(
                  title: 'Componente 2',
                  isSelected: true,
                  onTap: () async {
                    Navigator.push(
                        context,
                        MaterialPageRoute(
                            builder: (context) => ManutencaoComponentesPage(
                                  manutencaoController:
                                      widget._manutencaoController,
                                )));
                  },
                ),
              ],
            )));
  }
}
