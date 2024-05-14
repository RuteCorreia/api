import 'package:flutter/material.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/controller/report_aplication_controller.dart';
import 'package:flytec/features/aplications/models/aplicacao.dart';
import 'package:flytec/features/aplications/models/aplicacoes.dart';
import 'package:flytec/features/aplications/components/components_exports.dart';
import 'package:flytec/features/aplications/pages/create_new_aplicacao_page.dart';

class AplicacoesListPage extends StatefulWidget {
  final int? _idAplicacao;
  final ReportAplicationController _reportAplicationController;
  const AplicacoesListPage(
      {required ReportAplicationController reportAplicationController,
      required int? idAplicacao,
      super.key})
      : _reportAplicationController = reportAplicationController,
        _idAplicacao = idAplicacao;

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
      if (widget._idAplicacao != null && widget._idAplicacao! > 0) {
        _aplicacoes = await widget._reportAplicationController
            .getAplicacoesByRelatorioAplicacao(widget._idAplicacao!);
        setState(() {});
        return;
      }
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
    if (_aplicacoes[index]?.dataAplicacao == null ||
        _aplicacoes[index]!.dataAplicacao!.isEmpty) return DateTime.now();
    final epoch = int.tryParse(_aplicacoes[index]!.dataAplicacao!);

    return DateTime.fromMillisecondsSinceEpoch(epoch!);
  }

  bool get _aplicationIsSizeMax => _aplicacoes.length < 3;

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
          child: _aplicacoes.isEmpty
              ? const Center(child: Text("Não criou nenhum relatório"))
              : SizedBox(
                  child: SingleChildScrollView(
                    child: ListView.builder(
                        shrinkWrap: true,
                        itemCount: _aplicacoes.length,
                        itemBuilder: (context, index) {
                          return CustomCardButton(
                            title:
                                "Aplicação ${Util.getTodayDate(date: _dataByIndexAplicacao(index))} ",
                            onTap: () {
                              Navigator.push(context,
                                  MaterialPageRoute(builder: (context) {
                                return CreateNewAplicacaoPages(
                                  onAplicacao: (newAplicacao) async {
                                    if (widget._idAplicacao != null &&
                                        widget._idAplicacao! > 0) {
                                      _aplicacoes = await widget
                                          ._reportAplicationController
                                          .getAplicacoesByRelatorioAplicacao(
                                              widget._idAplicacao!);
                                      setState(() {});

                                      final updateAplicacao = newAplicacao;
                                      updateAplicacao.id =
                                          _aplicacoes[index]?.id;

                                      await widget._reportAplicationController
                                          .updateElementInTable(
                                              updateAplicacao.id!,
                                              updateAplicacao.toMap(),
                                              'Aplicacoes');

                                      _aplicacoes = await widget
                                          ._reportAplicationController
                                          .getAplicacoesByRelatorioAplicacao(
                                              widget._idAplicacao);
                                      setState(() {});
                                      return;
                                    }
                                  },
                                  aplicacoes: _aplicacoes[index]!,
                                  reportAplicationController:
                                      widget._reportAplicationController,
                                );
                              }));
                            },
                          );
                        }),
                  ),
                ),
        ),
        floatingActionButton: _aplicationIsSizeMax
            ? FloatingActionButton(
                onPressed: () {
                  Navigator.push(context, MaterialPageRoute(builder: (context) {
                    return CreateNewAplicacaoPages(
                      onAplicacao: (newAplicacao) {
                        _newAplicacao(newAplicacao);
                      },
                      reportAplicationController:
                          widget._reportAplicationController,
                    );
                  }));
                },
                child: const Icon(
                  Icons.add,
                  color: Colors.white,
                ),
              )
            : null);
  }
}
