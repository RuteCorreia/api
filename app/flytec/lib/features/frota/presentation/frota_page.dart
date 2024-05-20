import 'package:flutter/material.dart';
import 'package:flytec/core/enums/dashboard_state.dart';
import 'package:flytec/core/widgets/dashboard_counter.dart';
import 'package:flytec/features/frota/models/report_frota_model.dart';
import 'package:flytec/features/frota/presentation/components/report_frota_card.dart';

class FrotaPage extends StatefulWidget {
  const FrotaPage({super.key});

  @override
  State<FrotaPage> createState() => _FrotaPageState();
}

class _FrotaPageState extends State<FrotaPage> {
  final ScrollController _scrollController = ScrollController();
  final List<ReportFrotaModel> _reportFrotaModel = [
    ReportFrotaModel(
        createdAt: DateTime.now().millisecondsSinceEpoch,
        placaVeiculo: 'ORT-1234',
        state: DashBoardState.Enviado),
    ReportFrotaModel(
        createdAt: DateTime.now().millisecondsSinceEpoch,
        placaVeiculo: 'NQL-2502',
        state: DashBoardState.Incompleto)
  ];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Controle de Frota'),
        centerTitle: true,
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () async {},
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
                  itemCount: _reportFrotaModel.length,
                  reverse: true,
                  itemBuilder: (context, index) {
                    return ReportFrotaCard(
                      index: index,
                      reportFrotaModel: _reportFrotaModel,
                    );
                  })
            ],
          ),
        ),
      ),
    );
  }
}
