import 'package:flutter/material.dart';
import 'package:flytec/core/enums/dashboard_state.dart';
import 'package:flytec/core/widgets/dashboard_counter.dart';
import 'package:flytec/features/manutencao/components/new_manutencao_page.dart';
import 'package:flytec/features/manutencao/components/report_manutencao_card_list.dart';
import 'package:flytec/features/manutencao/controller/manutencao_controller.dart';
import 'package:flytec/features/manutencao/models/report_manutencao_model.dart';

class ManutencaoPageList extends StatefulWidget {
  const ManutencaoPageList({super.key});

  @override
  State<ManutencaoPageList> createState() => _ManutencaoPageListState();
}

class _ManutencaoPageListState extends State<ManutencaoPageList> {
  final ScrollController _scrollController = ScrollController();

  final List<ReportManutencaoModel> _reportManutencaoModel = [
    ReportManutencaoModel(
        createdAt: DateTime.now().millisecondsSinceEpoch,
        prefAeronave: 'ART-1234',
        state: DashBoardState.Enviado),
    ReportManutencaoModel(
        createdAt: DateTime.now().millisecondsSinceEpoch,
        prefAeronave: 'ARX-4402',
        state: DashBoardState.Incompleto)
  ];

  final ManutencaoController _manutencaoController = ManutencaoController();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Manutenção'),
        centerTitle: true,
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () async {
          Navigator.push(
              context,
              MaterialPageRoute(
                  builder: (context) =>  NewManutencaoPage(manutencaoController: _manutencaoController,)));
        },
        child: const Icon(Icons.add, color: Colors.white),
      ),
      body: Padding(
        padding: const EdgeInsets.only(left: 10.0, right: 10.0, top: 16.0),
        child: SingleChildScrollView(
          controller: _scrollController,
          child: Column(
            children: [
              const Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                crossAxisAlignment: CrossAxisAlignment.center,
                children: [
                  ReportDashBoardCounter(
                    text: "Enviado",
                    value: '5',
                    state: DashBoardState.Enviado,
                  ),
                  ReportDashBoardCounter(
                    text: "Pronto",
                    value: '10',
                    state: DashBoardState.Pronto,
                  ),
                  ReportDashBoardCounter(
                    text: "Incompleto",
                    value: '2',
                    state: DashBoardState.Incompleto,
                  ),
                  ReportDashBoardCounter(
                    text: "Não enviado",
                    value: '1',
                    state: DashBoardState.NaoEnviado,
                  ),
                ],
              ),
              const SizedBox(height: 20),
              ListView.builder(
                  controller: _scrollController,
                  shrinkWrap: true,
                  itemCount: _reportManutencaoModel.length,
                  reverse: true,
                  itemBuilder: (context, index) {
                    return ReportManutencaoCard(
                      index: index,
                      reportManutencaoModel: _reportManutencaoModel,
                    );
                  })
            ],
          ),
        ),
      ),
    );
  }
}
