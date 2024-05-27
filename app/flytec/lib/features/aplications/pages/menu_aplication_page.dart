import 'package:flutter/material.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/pages/contrato_prestacao_servico_page.dart';
import 'package:flytec/features/aplications/pages/dados_responsavel_page.dart';
import 'package:flytec/features/aplications/pages/recomendacoes_tecnicas_page.dart';
import 'package:flytec/features/aplications/components/custom_card_button.dart';
import 'package:flytec/features/aplications/controller/report_aplication_controller.dart';
import 'package:flytec/features/aplications/models/aplicacao.dart';
import 'package:flytec/features/aplications/pages/area_tratada_page.dart';
import 'package:flytec/features/aplications/pages/caracteristicas_produto_aplicado_page.dart';
import 'package:flytec/features/aplications/pages/contratante_page.dart';
import 'package:flytec/features/aplications/pages/relatorio_aplicacao_page.dart';
import 'package:flytec/features/aplications/pages/rastreamento/tracking_page.dart';

class MenuAplicationPage extends StatefulWidget {
  final ReportAplicationController? _reportAplicationController;

  const MenuAplicationPage(
      {required ReportAplicationController? reportAplicationController,
      super.key})
      : _reportAplicationController = reportAplicationController;

  @override
  State<MenuAplicationPage> createState() => _MenuAplicationPageState();
}

class _MenuAplicationPageState extends State<MenuAplicationPage> {
  Aplicacao get _aplicacao =>
      widget._reportAplicationController!.aplicacaoSelected!;

  DateTime get _dataAplicacao {
    int? epoch = int.tryParse(_aplicacao.data!);
    return DateTime.fromMillisecondsSinceEpoch(epoch!);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
        title: const Text(
          "Planejamento \nOperacional de Aplicação Aérea",
          textAlign: TextAlign.center,
        ),
        leading: IconButton(
            onPressed: () {
              widget._reportAplicationController?.updateView!();
              Navigator.pop(context);
            },
            icon: const Icon(Icons.arrow_back)),
      ),
      body: SingleChildScrollView(
        child: Padding(
            padding: const EdgeInsets.all(16.0),
            child:
                Column(crossAxisAlignment: CrossAxisAlignment.start, children: [
              const SizedBox(height: 16),
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Text(
                    'N° ${_aplicacao.refDocument}',
                    textAlign: TextAlign.right,
                    style: const TextStyle(
                      color: Color(0xFF00B45D),
                      fontSize: 14,
                      fontFamily: 'Inter',
                      fontWeight: FontWeight.w700,
                      height: 0.11,
                    ),
                  ),
                  Text(
                    'Data ${Util.getTodayDate(date: _dataAplicacao)}',
                    textAlign: TextAlign.right,
                    style: const TextStyle(
                      color: Color(0xFF00B45D),
                      fontSize: 14,
                      fontFamily: 'Inter',
                      fontWeight: FontWeight.w700,
                      height: 0.11,
                    ),
                  )
                ],
              ),
              const SizedBox(height: 25),
              CustomCardButton(
                title: "Identificação do contratante",
                onTap: () {
                  Navigator.push(
                      context,
                      MaterialPageRoute(
                        builder: (context) => ContrantePage(
                            reportAplicationController:
                                widget._reportAplicationController!),
                      ));
                },
              ),
              CustomCardButton(
                title: "Identificação da área a ser tratada",
                onTap: () {
                  Navigator.push(
                      context,
                      MaterialPageRoute(
                        builder: (context) => AreaTratada(
                            reportAplicationController:
                                widget._reportAplicationController!),
                      ));
                },
              ),
              CustomCardButton(
                title: "Caraterísticas do produto a ser aplicado ",
                onTap: () {
                  Navigator.push(
                      context,
                      MaterialPageRoute(
                        builder: (context) =>
                            CaracteristicasProdutoAplicadoPage(
                                reportAplicationController:
                                    widget._reportAplicationController!),
                      ));
                },
              ),
              CustomCardButton(
                title: "Recomendações técnicas para aplicação",
                onTap: () {
                  Navigator.push(
                      context,
                      MaterialPageRoute(
                        builder: (context) => RecomendacoesTecnicasPage(
                            reportAplicationController:
                                widget._reportAplicationController!),
                      ));
                },
              ),
              CustomCardButton(
                title: "Relatório de aplicação",
                onTap: () {
                  Navigator.push(
                      context,
                      MaterialPageRoute(
                        builder: (context) => RelatorioAplicacaoPage(
                            reportAplicationController:
                                widget._reportAplicationController!),
                      ));
                },
              ),
              CustomCardButton(
                title: "Contrato de prestação de serviços",
                onTap: () {
                  Navigator.push(
                      context,
                      MaterialPageRoute(
                        builder: (context) => ContratoPrestacaoServicoPage(
                            reportAplicationController:
                                widget._reportAplicationController!),
                      ));
                },
              ),
              CustomCardButton(
                title: "Dados do responsável",
                onTap: () {
                  Navigator.push(
                      context,
                      MaterialPageRoute(
                        builder: (context) => DadosResponsavelPage(
                            reportAplicationController:
                                widget._reportAplicationController!),
                      ));
                },
              ),
              CustomCardButton(
                title: "Rastreamento",
                onTap: () {
                  Navigator.push(
                      context,
                      MaterialPageRoute(
                        builder: (context) => TrackingPage(
                          reportApplicationController:
                              widget._reportAplicationController!,
                        ),
                      ));
                },
              ),
            ])),
      ),
    );
  }
}
