import 'package:flutter/material.dart';
import 'package:flytec/features/aplications/components/components_exports.dart';
import 'package:flytec/features/manutencao/components/checklist_revisao_page.dart';
import 'package:flytec/features/manutencao/components/manutencao_componentes_page.dart';
import 'package:flytec/features/manutencao/components/widgets/custom_card_with_color.dart';

class ManutencaoComponentesListPage extends StatefulWidget {
  const ManutencaoComponentesListPage({super.key});

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
                            builder: (context) => const CheckListRevisaoPage(),
                          ));
                    },
                    title: 'Ir para o Checklist'),
                const SizedBox(height: 14),
                CustomButton(onClick: () async {}, title: 'Finalizar'),
                const SizedBox(height: 20),
                CustomCardWithColor(
                  title: 'Componente 1',
                  isSelected: true,
                  onTap: () async {
                    Navigator.push(
                        context,
                        MaterialPageRoute(
                            builder: (context) =>
                                const ManutencaoComponentesPage()));
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
                            builder: (context) =>
                                const ManutencaoComponentesPage()));
                  },
                ),
              ],
            )));
  }
}
