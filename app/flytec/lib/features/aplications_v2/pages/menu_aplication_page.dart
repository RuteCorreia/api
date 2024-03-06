import 'package:flutter/material.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications_v2/components/custom_card_button.dart';
import 'package:flytec/features/aplications_v2/controller/report_aplication_controller.dart';
import 'package:flytec/features/aplications_v2/models/aplicacao.dart';
import 'package:flytec/features/aplications_v2/pages/contratante_page.dart';

class MenuAplicationPage extends StatefulWidget {
  final ReportAplicationController _reportAplicationController;

  const MenuAplicationPage(
      {
      required ReportAplicationController reportAplicationController,
      super.key})
      : 
        _reportAplicationController = reportAplicationController;

  @override
  State<MenuAplicationPage> createState() => _MenuAplicationPageState();
}

class _MenuAplicationPageState extends State<MenuAplicationPage> {
  Aplicacao get _aplicacao => widget._reportAplicationController.aplicacaoSelected!;

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
          "Planejamento Operacional \nde Aplicação Aérea",
          textAlign: TextAlign.center,
        ),
      ),
      body: Padding(
          padding: const EdgeInsets.all(16.0),
          child:
              Column(crossAxisAlignment: CrossAxisAlignment.start, children: [
            const SizedBox(height: 16),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Text(
                  'N° ${_aplicacao.id}',
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
                              widget._reportAplicationController),
                    ));
              },
            ),
            CustomCardButton(
              title: "Identificação da área a ser tratada",
              onTap: () {},
            ),
            CustomCardButton(
              title: "Caraterísticas do produto a ser aplicado ",
              onTap: () {},
            ),
            CustomCardButton(
              title: "Recomendações técnicas para aplicação",
              onTap: () {},
            ),
            CustomCardButton(
              title: "Relatório de aplicação",
              onTap: () {},
            ),
            CustomCardButton(
              title: "Contrato de prestação de serviços",
              onTap: () {},
            ),
            CustomCardButton(
              title: "Dados do responsável",
              onTap: () {},
            ),
          ])),
    );
  }
}
