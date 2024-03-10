import 'package:flutter/material.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications_v2/controller/report_aplication_controller.dart';
import 'package:flytec/features/aplications_v2/models/aplicacao.dart';
import 'package:flytec/features/aplications_v2/models/aplicacoes.dart';
import 'package:flytec/features/aplications_v2/components/components_exports.dart';
import 'package:flytec/features/aplications_v2/pages/create_new_aplicacao_page.dart';

class AplicacoesListPage extends StatefulWidget {
  final ReportAplicationController _reportAplicationController;
  const AplicacoesListPage(
      {required ReportAplicationController reportAplicationController,
      super.key})
      : _reportAplicationController = reportAplicationController;

  @override
  State<AplicacoesListPage> createState() => _AplicacoesListPageState();
}

class _AplicacoesListPageState extends State<AplicacoesListPage> {
  List<Aplicacoes?> _aplicacoes = [];

  Aplicacao get _aplicacao =>
      widget._reportAplicationController.aplicacaoSelected!;

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) async {
      _aplicacoes = await widget._reportAplicationController
          .getAplicacoesByRelatorioAplicacao(_aplicacao.relatorioAplicacaoId);
      setState(() {});
    });
  }

  void _newAplicacao(Aplicacoes aplicacao) {
    _aplicacoes.add(aplicacao);
    setState(() {});
  }

  DateTime? _dataByIndexAplicacao(int index) {
    if (_aplicacoes[index]?.dataAplicacao == null) return DateTime.now();
    final epoch = int.tryParse(_aplicacoes[index]!.dataAplicacao!);

    return DateTime.fromMillisecondsSinceEpoch(epoch!);
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
      body: Padding(
        padding: const EdgeInsets.all(8.0),
        child: SingleChildScrollView(
          child: _aplicacoes.isEmpty
              ? const Center(child: Text("Não criou nenhum relatório"))
              : SizedBox(
                  child: ListView.builder(
                      shrinkWrap: true,
                      itemCount: _aplicacoes.length,
                      itemBuilder: (context, index) {
                        return CustomCardButton(
                          title:
                              "Aplicação ${Util.getTodayDate(date: _dataByIndexAplicacao(index))} ",
                          onTap: () {},
                        );
                      }),
                ),
        ),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () {
          Navigator.push(context, MaterialPageRoute(builder: (context) {
            return CreateNewAplicacaoPages(
              onAplicacao: (newAplicacao) {
                _newAplicacao(newAplicacao);
              },
              reportAplicationController: widget._reportAplicationController,
            );
          }));
        },
        child: const Icon(
          Icons.add,
          color: Colors.white,
        ),
      ),
    );
  }
}
