import 'dart:developer';

import 'package:flutter/services.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/pdf_generator.dart';
import 'package:flytec/features/aplications/data/models/relatorio_model.dart';
import 'package:intl/intl.dart';
import 'package:pdf/pdf.dart';
import 'package:pdf/widgets.dart' as pw;

class ReportAplicationsGenerate implements PdfGenerator {
  final RelatorioModel relatorioModel;
  ReportAplicationsGenerate({required this.relatorioModel});

  @override
  Future<dynamic> generatePdf({parameters}) async {
    log(relatorioModel.contratoServico!.toJson().toString());
    final pdf = pw.Document();
    final newRoman = pw.Font.times();
    final newRomanBold = pw.Font.timesBold();
    final dateTimeNow = DateTime.now();
    final logoImage = (await rootBundle.load('assets/images/logo-light.png'))
        .buffer
        .asUint8List();
    final mapaImage =
        (await rootBundle.load('assets/images/mapa.png')).buffer.asUint8List();
    pdf.addPage(
      pw.Page(
        pageFormat: PdfPageFormat.a4,
        margin: const pw.EdgeInsets.all(10),
        build: (context) {
          return pw.Container(
            decoration: pw.BoxDecoration(
                border: pw.Border.all(color: PdfColors.black, width: 1.5)),
            child: pw.Column(children: [
              pw.Row(
                  mainAxisAlignment: pw.MainAxisAlignment.spaceBetween,
                  children: [
                    pw.Container(
                        width: 300,
                        height: 80,
                        padding: const pw.EdgeInsets.only(left: 5, top: 5),
                        child: pw.Column(
                          crossAxisAlignment: pw.CrossAxisAlignment.start,
                          children: [
                            pw.Row(children: [
                              pw.Container(
                                child: pw.Image(pw.MemoryImage(logoImage),
                                    fit: pw.BoxFit.contain,
                                    width: 140,
                                    height: 140),
                              ),
                              pw.SizedBox(width: 10),
                              pw.Column(
                                  crossAxisAlignment:
                                      pw.CrossAxisAlignment.start,
                                  children: [
                                    pw.Text('IMAGEM AVIAÇÃO AGRÍCOLA LTDA.',
                                        style: pw.TextStyle(
                                            fontSize: 14,
                                            fontWeight: pw.FontWeight.normal)),
                                    pw.Text(
                                        'MA SP - 000000-0 - CNPJ 00.000.000/0000-00 - Inscr. Est. 000.000.000.000',
                                        maxLines: 1,
                                        style: pw.TextStyle(
                                            fontSize: 8,
                                            fontWeight: pw.FontWeight.normal)),
                                    pw.SizedBox(height: 10),
                                    pw.Text('FONE: (XX) XXXX-XXXX',
                                        style: pw.TextStyle(
                                            fontSize: 14,
                                            fontWeight: pw.FontWeight.bold)),
                                  ])
                            ]),
                            pw.SizedBox(height: 5),
                            pw.Text(
                                'RUA PARANÁ, 000 - CENTRO - CEP 00000-000 - MONÇÕES - EST.SÃO PAULO',
                                style: pw.TextStyle(
                                    fontSize: 8,
                                    fontWeight: pw.FontWeight.normal)),
                          ],
                        )),
                    pw.Column(children: [
                      pw.Align(
                          child: pw.Padding(
                              child: pw.Text(
                                  '                           CDA N° 4046',
                                  style:
                                      const pw.TextStyle(color: PdfColors.red)),
                              padding: const pw.EdgeInsets.symmetric(
                                  horizontal: 10, vertical: 10)),
                          alignment: pw.Alignment.centerRight),
                      pw.Align(
                          alignment: pw.Alignment.centerRight,
                          child: pw.SizedBox(
                            width: 250,
                            child: pw.Column(
                                mainAxisAlignment: pw.MainAxisAlignment.center,
                                children: [
                                  pw.Text('Receituário Agronômico',
                                      style: pw.TextStyle(
                                          fontSize: 14,
                                          font: newRomanBold,
                                          fontWeight: pw.FontWeight.bold)),
                                  pw.SizedBox(height: 2.0),
                                  pw.Container(
                                      height: 28,
                                      padding:
                                          const pw.EdgeInsets.only(bottom: 5),
                                      decoration: const pw.BoxDecoration(
                                        border: pw.Border(
                                          top: pw.BorderSide(
                                              width: 1.5,
                                              color: PdfColors.black),
                                          left: pw.BorderSide(
                                              width: 1.5,
                                              color: PdfColors.black),
                                        ),
                                      ),
                                      child: pw.Row(
                                          mainAxisAlignment:
                                              pw.MainAxisAlignment.center,
                                          crossAxisAlignment:
                                              pw.CrossAxisAlignment.end,
                                          children: [
                                            pw.Text('N° '),
                                            pw.Text('0234 ',
                                                style: const pw.TextStyle(
                                                  color: PdfColors.red,
                                                )),
                                            pw.Text(' Data '),
                                            pw.Text(DateFormat('dd/MM/yyyy')
                                                .format(dateTimeNow)
                                                .toString()),
                                          ])),
                                ]),
                          )),
                    ]),
                  ]),
              pw.Divider(height: 1, thickness: 1.5),
              pw.Column(children: [
                pw.Text('Planejamento Operacional de Aplicação Aérea',
                    style: pw.TextStyle(
                        fontSize: 16,
                        font: newRomanBold,
                        color: PdfColors.green800,
                        fontWeight: pw.FontWeight.normal)),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Text('Identificação do Contratante',
                    style: pw.TextStyle(
                        fontSize: 14,
                        font: newRomanBold,
                        fontWeight: pw.FontWeight.bold)),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Row(
                    mainAxisAlignment: pw.MainAxisAlignment.start,
                    crossAxisAlignment: pw.CrossAxisAlignment.start,
                    children: [
                      pw.SizedBox(
                        width: 250,
                        child: pw.Padding(
                          padding: const pw.EdgeInsets.only(left: 10, top: 2),
                          child: pw.Text(
                              'Nome ${relatorioModel.cliente!.nome} ',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman)),
                        ),
                      ),
                      pw.Container(
                          height: 30,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              top: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                              left: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Column(
                              mainAxisAlignment:
                                  pw.MainAxisAlignment.spaceEvenly,
                              crossAxisAlignment: pw.CrossAxisAlignment.start,
                              children: [
                                pw.Padding(
                                  padding: const pw.EdgeInsets.only(left: 10),
                                  child: pw.Text(
                                      'CNPJ/CPF  ${relatorioModel.cliente!.cpf}',
                                      style: pw.TextStyle(
                                          fontSize: 12, font: newRoman)),
                                ),
                                pw.Container(
                                  width: 325,
                                  height: 1,
                                  color: PdfColors.black,
                                ),
                                pw.Padding(
                                  padding: const pw.EdgeInsets.only(left: 10),
                                  child: pw.Text('I.E/R.G. ',
                                      style: pw.TextStyle(
                                          fontSize: 12, font: newRoman)),
                                ),
                              ])),
                    ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Row(
                    mainAxisAlignment: pw.MainAxisAlignment.start,
                    crossAxisAlignment: pw.CrossAxisAlignment.start,
                    children: [
                      pw.SizedBox(
                        width: 250,
                        child: pw.Padding(
                          padding: const pw.EdgeInsets.only(left: 10, top: 2),
                          child: pw.Text(
                              'Endereço: ${relatorioModel.cliente!.uf}',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman)),
                        ),
                      ),
                      pw.Container(
                          height: 25,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              top: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                              left: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Padding(
                            padding: const pw.EdgeInsets.only(left: 10, top: 2),
                            child: pw.Text(
                                'Município/UF ${relatorioModel.cliente!.endereco} ',
                                style:
                                    pw.TextStyle(fontSize: 12, font: newRoman)),
                          )),
                    ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Text('Identificação da Área a ser Tratada',
                    style: pw.TextStyle(
                        fontSize: 14,
                        font: newRomanBold,
                        fontWeight: pw.FontWeight.bold)),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Row(
                    mainAxisAlignment: pw.MainAxisAlignment.start,
                    crossAxisAlignment: pw.CrossAxisAlignment.start,
                    children: [
                      pw.SizedBox(
                        width: 250,
                        child: pw.Padding(
                          padding: const pw.EdgeInsets.only(left: 10, top: 2),
                          child: pw.Text(
                              'Localização: ${relatorioModel.areaTratada!.localizacao}',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman)),
                        ),
                      ),
                      pw.Container(
                          height: 28,
                          width: 150,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              top: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                              left: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Padding(
                            padding: const pw.EdgeInsets.only(left: 10, top: 2),
                            child: pw.Text(
                                'Cultura: ${relatorioModel.areaTratada!.cultura}',
                                style:
                                    pw.TextStyle(fontSize: 12, font: newRoman)),
                          )),
                      pw.Container(
                          height: 28,
                          width: 100,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              top: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                              left: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Column(
                              mainAxisAlignment: pw.MainAxisAlignment.start,
                              crossAxisAlignment: pw.CrossAxisAlignment.start,
                              children: [
                                pw.Padding(
                                  padding: const pw.EdgeInsets.only(
                                      left: 10, top: 2),
                                  child: pw.Text(
                                      'Extensão (Ha) ${relatorioModel.areaTratada!.extensao}',
                                      style: pw.TextStyle(
                                          fontSize: 11, font: newRoman)),
                                ),
                              ])),
                    ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Text('Características do Produto a ser Aplicado',
                    style: pw.TextStyle(
                        fontSize: 14,
                        font: newRomanBold,
                        fontWeight: pw.FontWeight.bold)),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Row(
                    crossAxisAlignment: pw.CrossAxisAlignment.center,
                    children: [
                      pw.Container(
                          height: 20,
                          width: 115,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          alignment: pw.Alignment.centerLeft,
                          child: pw.Text('    Nome',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          height: 20,
                          width: 80,
                          alignment: pw.Alignment.center,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text('Classe Tox',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          height: 20,
                          width: 120,
                          alignment: pw.Alignment.center,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text('Tipo de Serviço',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          height: 20,
                          width: 100,
                          alignment: pw.Alignment.center,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text('Formulação',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          height: 20,
                          width: 80,
                          alignment: pw.Alignment.center,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text('Quant/Ha',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          height: 20,
                          width: 80,
                          alignment: pw.Alignment.center,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text('Adjuvante',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                    ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Row(
                    crossAxisAlignment: pw.CrossAxisAlignment.center,
                    children: [
                      pw.Container(
                        child: pw.Text(
                            " ${relatorioModel.carateristicaProduto!.nomeProduto}",
                            style: pw.TextStyle(fontSize: 11, font: newRoman)),
                        height: 25,
                        width: 115,
                        decoration: const pw.BoxDecoration(
                          border: pw.Border(
                            right: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                          ),
                        ),
                      ),
                      pw.Container(
                        height: 25,
                        width: 80,
                        child: pw.Text(
                            " ${relatorioModel.carateristicaProduto!.classificacaoToxicologica}",
                            style: pw.TextStyle(fontSize: 11, font: newRoman)),
                        decoration: const pw.BoxDecoration(
                          border: pw.Border(
                            top: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                            right: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                          ),
                        ),
                      ),
                      pw.Container(
                        height: 25,
                        width: 120,
                        child: pw.Text(
                            " ${relatorioModel.carateristicaProduto!.tipoServico}",
                            style: pw.TextStyle(fontSize: 11, font: newRoman)),
                        decoration: const pw.BoxDecoration(
                          border: pw.Border(
                            top: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                            right: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                          ),
                        ),
                      ),
                      pw.Container(
                        height: 25,
                        width: 100,
                        child: pw.Text(
                            " ${relatorioModel.carateristicaProduto!.tipoFormulacao}",
                            style: pw.TextStyle(fontSize: 11, font: newRoman)),
                        decoration: const pw.BoxDecoration(
                          border: pw.Border(
                            top: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                            right: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                          ),
                        ),
                      ),
                      pw.Container(
                        height: 25,
                        width: 80,
                        child: pw.Text(
                            " ${relatorioModel.carateristicaProduto!.dosePorHectare} ${relatorioModel.carateristicaProduto!.unidadeHectare}",
                            style: pw.TextStyle(fontSize: 11, font: newRoman)),
                        decoration: const pw.BoxDecoration(
                          border: pw.Border(
                            top: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                            right: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                          ),
                        ),
                      ),
                      pw.Container(
                        height: 25,
                        width: 80,
                        child: pw.Text(
                            " ${relatorioModel.carateristicaProduto!.adjuvante}",
                            style: pw.TextStyle(fontSize: 11, font: newRoman)),
                        decoration: const pw.BoxDecoration(
                          border: pw.Border(
                            top: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                          ),
                        ),
                      ),
                    ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Text('Recomendações Técnicas para a Aplicação',
                    style: pw.TextStyle(
                        fontSize: 14,
                        font: newRomanBold,
                        fontWeight: pw.FontWeight.bold)),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Row(
                    crossAxisAlignment: pw.CrossAxisAlignment.start,
                    children: [
                      pw.Container(
                          width: 137,
                          height: 28,
                          padding: const pw.EdgeInsets.only(left: 5, top: 2),
                          alignment: pw.Alignment.topLeft,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text(
                              'Veiculante: ${relatorioModel.recomendacoesTecnicas!.veiculante}',
                              textAlign: pw.TextAlign.left,
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          width: 137,
                          height: 28,
                          padding: const pw.EdgeInsets.only(top: 2),
                          alignment: pw.Alignment.topCenter,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text(
                              'Vazão ${relatorioModel.recomendacoesTecnicas!.volumeDaAplicacao} ${relatorioModel.recomendacoesTecnicas!.unidadeVolume}',
                              textAlign: pw.TextAlign.left,
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          width: 137,
                          height: 28,
                          padding: const pw.EdgeInsets.only(top: 2),
                          alignment: pw.Alignment.topCenter,
                          child: pw.Text(
                              'Largura Faixa  ${relatorioModel.recomendacoesTecnicas!.larguraDaFaixa}',
                              textAlign: pw.TextAlign.left,
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                    ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Row(
                    crossAxisAlignment: pw.CrossAxisAlignment.start,
                    children: [
                      pw.Container(
                          width: 115,
                          height: 28,
                          padding: const pw.EdgeInsets.only(left: 5, top: 2),
                          alignment: pw.Alignment.topLeft,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text(
                              'Aeronave  ${relatorioModel.recomendacoesTecnicas!.aeronave}',
                              textAlign: pw.TextAlign.left,
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          width: 115,
                          height: 28,
                          padding: const pw.EdgeInsets.only(top: 2),
                          alignment: pw.Alignment.topCenter,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text(
                              'Altura Vôo  ${relatorioModel.recomendacoesTecnicas!.alturaDoVoo}',
                              textAlign: pw.TextAlign.left,
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          width: 115,
                          height: 28,
                          padding: const pw.EdgeInsets.only(top: 2),
                          alignment: pw.Alignment.topCenter,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text(
                              'Temp. C°  ${relatorioModel.recomendacoesTecnicas!.temperatura}',
                              textAlign: pw.TextAlign.left,
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          width: 115,
                          height: 28,
                          padding: const pw.EdgeInsets.only(top: 2),
                          alignment: pw.Alignment.topCenter,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text(
                              'U.R% do Ar  ${relatorioModel.recomendacoesTecnicas!.umidadeRelativaDoAr}',
                              textAlign: pw.TextAlign.left,
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          width: 115,
                          height: 28,
                          padding: const pw.EdgeInsets.only(left: 5, top: 2),
                          alignment: pw.Alignment.topLeft,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text(
                              'Veloc. Vento  ${relatorioModel.recomendacoesTecnicas!.velocidadeDoVento}',
                              textAlign: pw.TextAlign.left,
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                    ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Container(
                    height: 25,
                    padding: const pw.EdgeInsets.only(left: 5, top: 2),
                    alignment: pw.Alignment.topLeft,
                    child: pw.Text(
                        'Regulagem Equip. Aplicação ${relatorioModel.recomendacoesTecnicas?.equipamento} ${relatorioModel.recomendacoesTecnicas?.angulo}',
                        textAlign: pw.TextAlign.left,
                        style: pw.TextStyle(fontSize: 12, font: newRoman))),
                pw.Divider(height: 1, thickness: 1.5),
                pw.SizedBox(
                    height: 50,
                    child: pw.Row(
                      crossAxisAlignment: pw.CrossAxisAlignment.center,
                      mainAxisAlignment: pw.MainAxisAlignment.spaceBetween,
                      children: [
                        pw.Container(
                            width: 300,
                            padding:
                                const pw.EdgeInsets.only(left: 5, bottom: 5),
                            alignment: pw.Alignment.bottomCenter,
                            child: pw.Column(
                              mainAxisAlignment: pw.MainAxisAlignment.center,
                              crossAxisAlignment: pw.CrossAxisAlignment.start,
                              children: [
                                pw.Text(" ${relatorioModel.executor}"),
                                pw.Divider(height: 0.5, thickness: 1.0),
                                pw.Text('Executor',
                                    textAlign: pw.TextAlign.left,
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                                pw.Text('CFTA',
                                    textAlign: pw.TextAlign.left,
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                              ],
                            )),
                        pw.Container(
                            padding:
                                const pw.EdgeInsets.only(right: 5, bottom: 5),
                            width: 180,
                            child: pw.Text(
                                ' ${relatorioModel.areaTratada!.cidade} ${relatorioModel.areaTratada!.uf},  ${relatorioModel.dadosDoResponsavel!.data} ',
                                textAlign: pw.TextAlign.right,
                                maxLines: 1,
                                style: pw.TextStyle(
                                    fontSize: 12, font: newRoman))),

                        /* pw.SizedBox(
                            width: 80,
                            child: pw.Text('de',
                                textAlign: pw.TextAlign.left,
                                style: pw.TextStyle(
                                    fontSize: 12, font: newRoman))), */
                      ],
                    )),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Text('Relatório de Aplicação',
                    style: pw.TextStyle(
                        fontSize: 16,
                        font: newRomanBold,
                        color: PdfColors.green800,
                        fontWeight: pw.FontWeight.normal)),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Row(
                    crossAxisAlignment: pw.CrossAxisAlignment.center,
                    children: [
                      pw.Container(
                          height: 25,
                          width: 75,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          alignment: pw.Alignment.center,
                          child: pw.Text(
                              'Cultura ${relatorioModel.areaTratada!.cultura}',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          height: 25,
                          width: 120,
                          alignment: pw.Alignment.center,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text('Produto Aplicado',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          height: 25,
                          width: 100,
                          alignment: pw.Alignment.center,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text('Dosagem',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          height: 25,
                          width: 80,
                          alignment: pw.Alignment.center,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text('Volume',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          height: 25,
                          width: 80,
                          alignment: pw.Alignment.center,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text('Densidade',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          height: 25,
                          width: 120,
                          alignment: pw.Alignment.center,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text('Área Total Aplicada',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                    ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Row(
                    crossAxisAlignment: pw.CrossAxisAlignment.center,
                    children: [
                      pw.Container(
                        height: 25,
                        width: 75,
                        alignment: pw.Alignment.center,
                        child: pw.Text(
                            " ${relatorioModel.relatorioDeAplicacao!.cultura}"),
                        decoration: const pw.BoxDecoration(
                          border: pw.Border(
                            right: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                          ),
                        ),
                      ),
                      pw.Container(
                        height: 25,
                        width: 120,
                        alignment: pw.Alignment.center,
                        child: pw.Text(
                            " ${relatorioModel.relatorioDeAplicacao!.produtoAplicado}"),
                        decoration: const pw.BoxDecoration(
                          border: pw.Border(
                            top: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                            right: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                          ),
                        ),
                      ),
                      pw.Container(
                        height: 25,
                        width: 100,
                        alignment: pw.Alignment.center,
                        child: pw.Text(
                            " ${relatorioModel.relatorioDeAplicacao!.dosagem} ${relatorioModel.relatorioDeAplicacao!.unidadeDosagem}"),
                        decoration: const pw.BoxDecoration(
                          border: pw.Border(
                            top: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                            right: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                          ),
                        ),
                      ),
                      pw.Container(
                        height: 25,
                        width: 80,
                        alignment: pw.Alignment.center,
                        child: pw.Text(
                            " ${relatorioModel.relatorioDeAplicacao!.volumeDeAplicacao} ${relatorioModel.relatorioDeAplicacao!.unidadeVolume}"),
                        decoration: const pw.BoxDecoration(
                          border: pw.Border(
                            top: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                            right: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                          ),
                        ),
                      ),
                      pw.Container(
                        height: 25,
                        width: 80,
                        alignment: pw.Alignment.center,
                        child: pw.Text(
                            " ${relatorioModel.relatorioDeAplicacao!.densidade}",
                            style: const pw.TextStyle(
                              fontSize: 9,
                            )),
                        decoration: const pw.BoxDecoration(
                          border: pw.Border(
                            top: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                            right: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                          ),
                        ),
                      ),
                      pw.Container(
                        height: 25,
                        width: 120,
                        alignment: pw.Alignment.center,
                        child: pw.Text(
                            " ${relatorioModel.relatorioDeAplicacao!.totalAreaAplicada}"),
                        decoration: const pw.BoxDecoration(
                          border: pw.Border(
                            top: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                          ),
                        ),
                      ),
                    ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Row(
                    crossAxisAlignment: pw.CrossAxisAlignment.center,
                    children: [
                      pw.Container(
                          height: 35,
                          width: 100,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          alignment: pw.Alignment.center,
                          child: pw.Text('Data Aplicação',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Column(children: [
                            pw.Container(
                                height: 17,
                                width: 170,
                                alignment: pw.Alignment.center,
                                child: pw.Text('Horário',
                                    style: pw.TextStyle(
                                        fontSize: 12, font: newRoman))),
                            pw.Row(children: [
                              pw.Container(
                                  height: 17,
                                  width: 85,
                                  decoration: const pw.BoxDecoration(
                                    border: pw.Border(
                                      top: pw.BorderSide(
                                          width: 1.5, color: PdfColors.black),
                                      right: pw.BorderSide(
                                          width: 1.5, color: PdfColors.black),
                                    ),
                                  ),
                                  alignment: pw.Alignment.center,
                                  child: pw.Text('Início',
                                      style: pw.TextStyle(
                                          fontSize: 12, font: newRoman))),
                              pw.Container(
                                  height: 17,
                                  width: 85,
                                  decoration: const pw.BoxDecoration(
                                    border: pw.Border(
                                      top: pw.BorderSide(
                                          width: 1.5, color: PdfColors.black),
                                    ),
                                  ),
                                  alignment: pw.Alignment.center,
                                  child: pw.Text('Término',
                                      style: pw.TextStyle(
                                          fontSize: 12, font: newRoman))),
                            ])
                          ])),
                      pw.Column(children: [
                        pw.Container(
                            height: 16,
                            alignment: pw.Alignment.center,
                            child: pw.Text(
                                'Condições climáticas durante a aplicação',
                                style: pw.TextStyle(
                                    fontSize: 12, font: newRoman))),
                        pw.Row(children: [
                          pw.Container(
                              height: 17.5,
                              width: 85,
                              decoration: const pw.BoxDecoration(
                                border: pw.Border(
                                  top: pw.BorderSide(
                                      width: 1.5, color: PdfColors.black),
                                  right: pw.BorderSide(
                                      width: 1.5, color: PdfColors.black),
                                ),
                              ),
                              alignment: pw.Alignment.center,
                              child: pw.Text('Temp. (° C)',
                                  style: pw.TextStyle(
                                      fontSize: 12, font: newRoman))),
                          pw.Container(
                              height: 17.5,
                              width: 95,
                              decoration: const pw.BoxDecoration(
                                border: pw.Border(
                                  top: pw.BorderSide(
                                      width: 1.5, color: PdfColors.black),
                                  right: pw.BorderSide(
                                      width: 1.5, color: PdfColors.black),
                                ),
                              ),
                              alignment: pw.Alignment.center,
                              child: pw.Text('U. R do Ar (%)',
                                  style: pw.TextStyle(
                                      fontSize: 12, font: newRoman))),
                          pw.Container(
                              height: 17.5,
                              width: 125,
                              decoration: const pw.BoxDecoration(
                                border: pw.Border(
                                  top: pw.BorderSide(
                                      width: 1.5, color: PdfColors.black),
                                ),
                              ),
                              alignment: pw.Alignment.center,
                              child: pw.Text('Vento (Km/h ou m/s)',
                                  style: pw.TextStyle(
                                      fontSize: 12, font: newRoman))),
                        ])
                      ]),
                    ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Column(children: [
                  pw.Row(children: [
                    pw.Container(
                      height: 15,
                      width: 100,
                      child: pw.Text(
                          " ${relatorioModel.relatorioDeAplicacao!.aplicacoes!.dataDaAplicacao}"),
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 85,
                      child: pw.Text(
                          " ${relatorioModel.relatorioDeAplicacao!.aplicacoes!.horarioDeInicio}"),
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 85,
                      child: pw.Text(
                          " ${relatorioModel.relatorioDeAplicacao!.aplicacoes!.horarioDeTermino}"),
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 85,
                      child: pw.Text(
                          " ${relatorioModel.relatorioDeAplicacao!.aplicacoes!.temperaturaIncial}"),
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 95,
                      child: pw.Text(
                          " ${relatorioModel.relatorioDeAplicacao!.aplicacoes!.umidadeRelativaInicial}"),
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 125,
                      child: pw.Text(
                          " ${relatorioModel.relatorioDeAplicacao!.aplicacoes!.ventoInicial}"),
                    ),
                  ]),
                  pw.Divider(height: 1, thickness: 1.5),
                  pw.Row(children: [
                    pw.Container(
                      height: 15,
                      width: 100,
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 85,
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 85,
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 85,
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 95,
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 125,
                    ),
                  ]),
                  pw.Divider(height: 1, thickness: 1.5),
                  pw.Row(children: [
                    pw.Container(
                      height: 15,
                      width: 100,
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 85,
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 85,
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 85,
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 95,
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 125,
                    ),
                  ]),
                ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Row(children: [
                  pw.Container(
                    height: 25,
                    width: 270,
                    decoration: const pw.BoxDecoration(
                      border: pw.Border(
                        right:
                            pw.BorderSide(width: 1.5, color: PdfColors.black),
                      ),
                    ),
                    alignment: pw.Alignment.bottomLeft,
                    padding: const pw.EdgeInsets.only(left: 2, bottom: 2),
                    child: pw.Text(
                        'Localização da Pista:  ${relatorioModel.relatorioDeAplicacao!.localizacaoPista}',
                        style: pw.TextStyle(fontSize: 12, font: newRoman)),
                  ),
                  pw.Container(
                    height: 25,
                    width: 70,
                    decoration: const pw.BoxDecoration(
                      border: pw.Border(
                        right:
                            pw.BorderSide(width: 1.5, color: PdfColors.black),
                      ),
                    ),
                    alignment: pw.Alignment.center,
                    child: pw.Text('Horimetro:',
                        style: pw.TextStyle(fontSize: 12, font: newRoman)),
                  ),
                  pw.Container(
                    height: 25,
                    width: 120,
                    decoration: const pw.BoxDecoration(
                      border: pw.Border(
                        right:
                            pw.BorderSide(width: 1.5, color: PdfColors.black),
                      ),
                    ),
                    padding: const pw.EdgeInsets.only(left: 2, top: 2),
                    alignment: pw.Alignment.topLeft,
                    child: pw.Text(
                        'Inicial: ${relatorioModel.relatorioDeAplicacao!.aplicacoes!.horimetroInicial}',
                        style: pw.TextStyle(fontSize: 12, font: newRoman)),
                  ),
                  pw.Container(
                    height: 25,
                    width: 120,
                    padding: const pw.EdgeInsets.only(left: 2, top: 2),
                    alignment: pw.Alignment.topLeft,
                    child: pw.Text(
                        'Final:  ${relatorioModel.relatorioDeAplicacao!.aplicacoes!.horimetroFinal}',
                        style: pw.TextStyle(fontSize: 12, font: newRoman)),
                  ),
                ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Row(children: [
                  pw.Container(
                    height: 28,
                    width: 340,
                    decoration: const pw.BoxDecoration(
                      border: pw.Border(
                        right:
                            pw.BorderSide(width: 1.5, color: PdfColors.black),
                      ),
                    ),
                    alignment: pw.Alignment.centerLeft,
                    padding: const pw.EdgeInsets.only(left: 2),
                    child: pw.Text(
                        'Alterações do Planejamento: ${relatorioModel.relatorioDeAplicacao!.observacoes}',
                        style: pw.TextStyle(fontSize: 12, font: newRoman)),
                  ),
                  pw.Container(
                    height: 28,
                    width: 240,
                    padding: const pw.EdgeInsets.only(left: 2),
                    alignment: pw.Alignment.centerLeft,
                    child: pw.Text(
                        'Emitiu relatório do DGPS: ${getIt<GlobalConfigVars>().dgs}',
                        style: pw.TextStyle(fontSize: 12, font: newRoman)),
                  ),
                ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Text('Contrato de Prestação de Serviços',
                    style: pw.TextStyle(
                        fontSize: 16,
                        font: newRomanBold,
                        color: PdfColors.green800,
                        fontWeight: pw.FontWeight.normal)),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Container(
                    margin: const pw.EdgeInsets.symmetric(
                        horizontal: 4.0, vertical: 4.0),
                    height: 40,
                    decoration: const pw.BoxDecoration(
                      border: pw.Border(
                        right:
                            pw.BorderSide(width: 1.5, color: PdfColors.black),
                        bottom:
                            pw.BorderSide(width: 1.5, color: PdfColors.black),
                        top: pw.BorderSide(width: 1.5, color: PdfColors.black),
                        left: pw.BorderSide(width: 1.5, color: PdfColors.black),
                      ),
                    ),
                    child: pw.Column(children: [
                      pw.Container(
                        height: 15,
                        child: pw.Text('Pagamento em Moeda',
                            style: pw.TextStyle(fontSize: 12, font: newRoman)),
                      ),
                      pw.Divider(height: 1, thickness: 1.5),
                      pw.Padding(
                          padding: const pw.EdgeInsets.symmetric(horizontal: 5),
                          child: pw.Row(
                              mainAxisAlignment:
                                  pw.MainAxisAlignment.spaceBetween,
                              children: [
                                pw.Container(
                                  height: 20,
                                  alignment: pw.Alignment.bottomCenter,
                                  child: pw.Text(
                                      'Preço Ha: ${relatorioModel.contratoServico!.preco!}',
                                      style: pw.TextStyle(
                                          fontSize: 12, font: newRoman)),
                                ),
                                pw.Container(
                                  height: 20,
                                  alignment: pw.Alignment.bottomCenter,
                                  child: pw.Text(
                                      'Valor Total: ${relatorioModel.contratoServico!.valorTotal!}',
                                      style: pw.TextStyle(
                                          fontSize: 12, font: newRoman)),
                                ),
                                pw.Row(children: [
                                  pw.Container(
                                    height: 20,
                                    alignment: pw.Alignment.bottomCenter,
                                    child: pw.Text('Vencimento:',
                                        style: pw.TextStyle(
                                            fontSize: 12, font: newRoman)),
                                  ),
                                  pw.Container(
                                    height: 20,
                                    alignment: pw.Alignment.bottomCenter,
                                    child: pw.Text(
                                        '${relatorioModel.contratoServico!.vencimento}',
                                        style: pw.TextStyle(
                                            fontWeight: pw.FontWeight.bold,
                                            fontSize: 12,
                                            font: newRoman)),
                                  ),
                                ])
                              ]))
                    ])),
                pw.Container(
                  height: 15,
                  margin:
                      const pw.EdgeInsets.only(left: 4.0, bottom: 4, right: 4),
                  alignment: pw.Alignment.center,
                  child: pw.Text(
                      '         O contratante declara estar plenamente de acordo com os serviços executados, área, valor e forma de pagamento expressa neste contrato, tendo o mesmo valor como comprovante de entrega dos serviços efetudos',
                      style: pw.TextStyle(fontSize: 11, font: newRoman)),
                ),
                pw.Container(
                  height: 15,
                  margin: const pw.EdgeInsets.only(left: 4.0, top: 4, right: 4),
                  alignment: pw.Alignment.center,
                  child: pw.Text(
                      '         E, por estarem de acordo com todas as cláusulas, itens e demais condições estabelecidas neste contrato, inclusive as constantes no verso, as partes firmam o presente, tendo valor como testemunhas as assinaturas do Piloto e Eng. Agrônomo.',
                      style: pw.TextStyle(fontSize: 11, font: newRoman)),
                ),
                pw.Container(
                  height: 15,
                  margin:
                      const pw.EdgeInsets.only(left: 4.0, right: 4, top: 12),
                  alignment: pw.Alignment.centerRight,
                  child: pw.Text(
                      '${relatorioModel.dadosDoResponsavel!.cidade} ${relatorioModel.dadosDoResponsavel!.uf}, ${relatorioModel.dadosDoResponsavel!.data!.split("/")[0]} de ${relatorioModel.dadosDoResponsavel!.data!.split("/")[1]} de ${relatorioModel.dadosDoResponsavel!.data!.split("/")[2]}',
                      style: pw.TextStyle(fontSize: 11, font: newRoman)),
                ),
                pw.SizedBox(height: 12),
                pw.Padding(
                    padding: const pw.EdgeInsets.symmetric(horizontal: 5),
                    child: pw.Row(
                        mainAxisAlignment: pw.MainAxisAlignment.spaceBetween,
                        crossAxisAlignment: pw.CrossAxisAlignment.start,
                        children: [
                          pw.Column(
                              crossAxisAlignment: pw.CrossAxisAlignment.start,
                              children: [
                                pw.Text(
                                    '${relatorioModel.dadosDoResponsavel!.nomeCompleto}',
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                                pw.Text('Contratante',
                                    textAlign: pw.TextAlign.left,
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                                pw.Text(
                                    'DOCUMENTO ${relatorioModel.dadosDoResponsavel!.cpf}',
                                    textAlign: pw.TextAlign.left,
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                              ]),
                          pw.Column(
                              crossAxisAlignment: pw.CrossAxisAlignment.start,
                              children: [
                                pw.Text('_______________________________',
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                                pw.Text('Eng. Agro',
                                    textAlign: pw.TextAlign.left,
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                              ]),
                          pw.Column(
                              crossAxisAlignment: pw.CrossAxisAlignment.start,
                              children: [
                                pw.Text('_______________________________',
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                                pw.Text('Piloto',
                                    textAlign: pw.TextAlign.left,
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                                pw.Text('COD. ANAC',
                                    textAlign: pw.TextAlign.left,
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                              ]),
                          pw.Column(
                              crossAxisAlignment: pw.CrossAxisAlignment.start,
                              children: [
                                pw.Text('_______________________________',
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                                pw.Text('Executor',
                                    textAlign: pw.TextAlign.left,
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                                pw.Text('CFTA',
                                    textAlign: pw.TextAlign.left,
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                              ])
                        ])),
              ]),
            ]),
          );
        },
      ),
    );
    pdf.addPage(pw.Page(
        pageFormat: PdfPageFormat.a4,
        margin: const pw.EdgeInsets.all(10),
        build: (context) {
          return pw.Container(
              decoration: pw.BoxDecoration(
                  border: pw.Border.all(color: PdfColors.black, width: 1.5)),
              child: pw.Column(children: [
                pw.Text('Foto do mapa da área',
                    style: pw.TextStyle(
                        fontSize: 16,
                        font: newRomanBold,
                        color: PdfColors.green800,
                        fontWeight: pw.FontWeight.normal)),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Container(
                  alignment: pw.Alignment.center,
                  decoration: pw.BoxDecoration(
                      border:
                          pw.Border.all(color: PdfColors.black, width: 1.5)),
                  child: pw.Image(pw.MemoryImage(mapaImage),
                      fit: pw.BoxFit.contain),
                ),
              ]));
        }));
    pdf.addPage(pw.Page(
        pageFormat: PdfPageFormat.a4,
        margin: const pw.EdgeInsets.all(10),
        build: (context) {
          return pw.Container(
              decoration: pw.BoxDecoration(
                  border: pw.Border.all(color: PdfColors.black, width: 1.5)),
              child: pw.Column(children: [
                pw.Text('Receituário Agronômico',
                    style: pw.TextStyle(
                        fontSize: 16,
                        font: newRomanBold,
                        color: PdfColors.green800,
                        fontWeight: pw.FontWeight.normal)),
                pw.Divider(height: 1, thickness: 1.5),
              ]));
        }));

    return pdf;
  }

  @override
  Future<Uint8List?> saveDocument({dynamic document}) async {
    return await document.save();
  }
}
 
/* 
import 'dart:typed_data';

import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/pdf_generator.dart';
import 'package:flytec/features/aplications/data/models/relatorio_model.dart';
import 'package:intl/intl.dart';
import 'package:pdf/pdf.dart';
import 'package:pdf/widgets.dart' as pw;

class ReportAplicationsGenerate implements PdfGenerator {
  final RelatorioModel relatorioModel;
  ReportAplicationsGenerate({required this.relatorioModel});
  @override
  Future<dynamic> generatePdf({parameters}) async {
    final pdf = pw.Document();
    final newRoman = pw.Font.times();
    final newRomanBold = pw.Font.timesBold();
    final dateTimeNow = DateTime.now();
    pdf.addPage(
      pw.Page(
        pageFormat: PdfPageFormat.a4,
        margin: const pw.EdgeInsets.all(10),
        build: (context) {
          return pw.Container(
            decoration: pw.BoxDecoration(
                border: pw.Border.all(color: PdfColors.black, width: 1.5)),
            child: pw.Column(children: [
              pw.Align(
                  child: pw.Padding(
                      child: pw.Text('CDA N°                    '),
                      padding: const pw.EdgeInsets.symmetric(
                          horizontal: 10, vertical: 10)),
                  alignment: pw.Alignment.centerRight),
              pw.Align(
                  alignment: pw.Alignment.centerRight,
                  child: pw.SizedBox(
                    width: 250,
                    child: pw.Column(
                        mainAxisAlignment: pw.MainAxisAlignment.center,
                        children: [
                          pw.Text('Receituário Agronômico',
                              style: pw.TextStyle(
                                  fontSize: 14,
                                  font: newRomanBold,
                                  fontWeight: pw.FontWeight.bold)),
                          pw.SizedBox(height: 2.0),
                          pw.Container(
                              height: 28,
                              padding: const pw.EdgeInsets.only(bottom: 5),
                              decoration: const pw.BoxDecoration(
                                border: pw.Border(
                                  top: pw.BorderSide(
                                      width: 1.5, color: PdfColors.black),
                                  left: pw.BorderSide(
                                      width: 1.5, color: PdfColors.black),
                                ),
                              ),
                              child: pw.Row(
                                  mainAxisAlignment:
                                      pw.MainAxisAlignment.center,
                                  crossAxisAlignment: pw.CrossAxisAlignment.end,
                                  children: [
                                    pw.Text('N° '),
                                    pw.Text('0234'),
                                    pw.Text(' Data '),
                                    pw.Text(DateFormat('dd/MM/yyyy')
                                        .format(dateTimeNow)
                                        .toString()),
                                  ])),
                        ]),
                  )),
              pw.Divider(height: 1, thickness: 1.5),
              pw.Column(children: [
                pw.Text('Planejamento Operacional de Aplicação Aérea',
                    style: pw.TextStyle(
                        fontSize: 16,
                        font: newRomanBold,
                        color: PdfColors.green800,
                        fontWeight: pw.FontWeight.normal)),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Text('Identificação do Contratante',
                    style: pw.TextStyle(
                        fontSize: 14,
                        font: newRomanBold,
                        fontWeight: pw.FontWeight.bold)),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Row(
                    mainAxisAlignment: pw.MainAxisAlignment.start,
                    crossAxisAlignment: pw.CrossAxisAlignment.start,
                    children: [
                      pw.SizedBox(
                        width: 250,
                        child: pw.Padding(
                          padding: const pw.EdgeInsets.only(left: 10, top: 2),
                          child: pw.Text(
                              'Nome ${relatorioModel.cliente!.nome} ',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman)),
                        ),
                      ),
                      pw.Container(
                          height: 30,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              top: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                              left: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Column(
                              mainAxisAlignment:
                                  pw.MainAxisAlignment.spaceEvenly,
                              crossAxisAlignment: pw.CrossAxisAlignment.start,
                              children: [
                                pw.Row(children: [
                                  pw.Padding(
                                    padding: const pw.EdgeInsets.only(left: 10),
                                    child: pw.Text('CNPJ/CPF ',
                                        style: pw.TextStyle(
                                            fontSize: 12, font: newRoman)),
                                  ),
                                  pw.Padding(
                                    padding: const pw.EdgeInsets.only(left: 10),
                                    child: pw.Text(
                                        '${relatorioModel.cliente!.cpf}',
                                        style: pw.TextStyle(
                                            fontWeight: pw.FontWeight.bold,
                                            fontSize: 12,
                                            font: newRomanBold)),
                                  ),
                                ]),
                                pw.Container(
                                  width: 325,
                                  height: 1,
                                  color: PdfColors.black,
                                ),
                                pw.Padding(
                                  padding: const pw.EdgeInsets.only(left: 10),
                                  child: pw.Text('I.E/R.G. ',
                                      style: pw.TextStyle(
                                          fontSize: 12, font: newRoman)),
                                ),
                              ])),
                    ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Row(
                    mainAxisAlignment: pw.MainAxisAlignment.start,
                    crossAxisAlignment: pw.CrossAxisAlignment.start,
                    children: [
                      pw.Row(children: [
                        pw.SizedBox(
                          width: 60,
                          child: pw.Padding(
                            padding: const pw.EdgeInsets.only(left: 10, top: 2),
                            child: pw.Text('Endereço:',
                                style:
                                    pw.TextStyle(fontSize: 12, font: newRoman)),
                          ),
                        ),
                        pw.SizedBox(
                          width: 50,
                          child: pw.Padding(
                            padding: const pw.EdgeInsets.only(left: 10, top: 2),
                            child: pw.Text(' ${relatorioModel.cliente!.uf}',
                                style: pw.TextStyle(
                                    fontSize: 12,
                                    fontWeight: pw.FontWeight.bold,
                                    font: newRomanBold)),
                          ),
                        ),
                      ]),
                      pw.Container(
                          height: 25,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              top: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                              left: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Padding(
                            padding: const pw.EdgeInsets.only(left: 10, top: 2),
                            child: pw.Text('Município/UF ',
                                style:
                                    pw.TextStyle(fontSize: 12, font: newRoman)),
                          )),
                    ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Text('Identificação da Área a ser Tratada',
                    style: pw.TextStyle(
                        fontSize: 14,
                        font: newRomanBold,
                        fontWeight: pw.FontWeight.bold)),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Row(
                    mainAxisAlignment: pw.MainAxisAlignment.start,
                    crossAxisAlignment: pw.CrossAxisAlignment.start,
                    children: [
                      pw.SizedBox(
                          width: 250,
                          child: pw.Row(children: [
                            pw.Padding(
                              padding:
                                  const pw.EdgeInsets.only(left: 10, top: 2),
                              child: pw.Text('Localização:',
                                  style: pw.TextStyle(
                                      fontSize: 12, font: newRoman)),
                            ),
                            pw.Padding(
                              padding:
                                  const pw.EdgeInsets.only(left: 10, top: 2),
                              child: pw.Text(
                                  ' ${relatorioModel.areaTratada!.localizacao}',
                                  style: pw.TextStyle(
                                      fontWeight: pw.FontWeight.bold,
                                      fontSize: 12,
                                      font: newRomanBold)),
                            ),
                          ])),
                      pw.Container(
                          height: 28,
                          width: 150,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              top: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                              left: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Padding(
                              padding:
                                  const pw.EdgeInsets.only(left: 10, top: 2),
                              child: pw.Row(children: [
                                pw.Text('Cultura:',
                                    style: pw.TextStyle(
                                        fontSize: 12, font: newRoman)),
                                pw.Text(
                                    ' ${relatorioModel.areaTratada!.cultura}',
                                    style: pw.TextStyle(
                                        fontSize: 12,
                                        fontWeight: pw.FontWeight.bold,
                                        font: newRomanBold)),
                              ]))),
                      pw.Container(
                          height: 28,
                          width: 100,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              top: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                              left: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Column(
                              mainAxisAlignment: pw.MainAxisAlignment.start,
                              crossAxisAlignment: pw.CrossAxisAlignment.start,
                              children: [
                                pw.Padding(
                                  padding: const pw.EdgeInsets.only(
                                      left: 10, top: 2),
                                  child: pw.Row(children: [
                                    pw.Text('Extensão (Ha) ',
                                        style: pw.TextStyle(
                                            fontSize: 12, font: newRoman)),
                                    pw.Text(
                                        '${relatorioModel.areaTratada!.extensao}',
                                        style: pw.TextStyle(
                                            fontSize: 12,
                                            font: newRomanBold,
                                            fontWeight: pw.FontWeight.bold))
                                  ]),
                                ),
                              ])),
                    ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Text('Características do Produto a ser Aplicado',
                    style: pw.TextStyle(
                        fontSize: 14,
                        font: newRomanBold,
                        fontWeight: pw.FontWeight.bold)),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Row(
                    crossAxisAlignment: pw.CrossAxisAlignment.center,
                    children: [
                      pw.Container(
                          height: 20,
                          width: 115,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          alignment: pw.Alignment.centerLeft,
                          child: pw.Text('    Nome',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          height: 20,
                          width: 80,
                          alignment: pw.Alignment.center,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text('Classe Tox',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          height: 20,
                          width: 120,
                          alignment: pw.Alignment.center,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text('Tipo de Serviço',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          height: 20,
                          width: 100,
                          alignment: pw.Alignment.center,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text('Formulação',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          height: 20,
                          width: 80,
                          alignment: pw.Alignment.center,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text('Quant/Ha',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          height: 20,
                          width: 80,
                          alignment: pw.Alignment.center,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text('Adjuvante',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                    ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Row(
                    crossAxisAlignment: pw.CrossAxisAlignment.center,
                    children: [
                      pw.Container(
                        child: pw.Text(
                            " ${relatorioModel.carateristicaProduto!.nomeProduto}",
                            style: pw.TextStyle(
                                fontWeight: pw.FontWeight.bold,
                                fontSize: 12,
                                font: newRomanBold)),
                        height: 25,
                        width: 115,
                        decoration: const pw.BoxDecoration(
                          border: pw.Border(
                            right: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                          ),
                        ),
                      ),
                      pw.Container(
                        height: 25,
                        width: 80,
                        child: pw.Text(
                            " ${relatorioModel.carateristicaProduto!.classe}",
                            style: pw.TextStyle(
                                fontWeight: pw.FontWeight.bold,
                                fontSize: 12,
                                font: newRomanBold)),
                        decoration: const pw.BoxDecoration(
                          border: pw.Border(
                            top: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                            right: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                          ),
                        ),
                      ),
                      pw.Container(
                        height: 25,
                        width: 120,
                        child: pw.Text(
                            " ${relatorioModel.carateristicaProduto!.tipoServico}",
                            style: pw.TextStyle(
                                fontWeight: pw.FontWeight.bold,
                                fontSize: 12,
                                font: newRomanBold)),
                        decoration: const pw.BoxDecoration(
                          border: pw.Border(
                            top: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                            right: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                          ),
                        ),
                      ),
                      pw.Container(
                        height: 25,
                        width: 100,
                        child: pw.Text(
                            " ${relatorioModel.carateristicaProduto!.tipoFormulacao}",
                            style: pw.TextStyle(
                                fontWeight: pw.FontWeight.bold,
                                fontSize: 12,
                                font: newRomanBold)),
                        decoration: const pw.BoxDecoration(
                          border: pw.Border(
                            top: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                            right: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                          ),
                        ),
                      ),
                      pw.Container(
                        height: 25,
                        width: 80,
                        child: pw.Text(
                            " ${relatorioModel.carateristicaProduto!.unidadeHectare}",
                            style: pw.TextStyle(
                                fontWeight: pw.FontWeight.bold,
                                fontSize: 12,
                                font: newRomanBold)),
                        decoration: const pw.BoxDecoration(
                          border: pw.Border(
                            top: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                            right: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                          ),
                        ),
                      ),
                      pw.Container(
                        height: 25,
                        width: 80,
                        child: pw.Text(
                            " ${relatorioModel.carateristicaProduto!.adjuvante}",
                            style: pw.TextStyle(
                                fontWeight: pw.FontWeight.bold,
                                fontSize: 12,
                                font: newRomanBold)),
                        decoration: const pw.BoxDecoration(
                          border: pw.Border(
                            top: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                          ),
                        ),
                      ),
                    ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Text('Recomendações Técnicas para a Aplicação',
                    style: pw.TextStyle(
                        fontSize: 14,
                        font: newRomanBold,
                        fontWeight: pw.FontWeight.bold)),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Row(
                    crossAxisAlignment: pw.CrossAxisAlignment.start,
                    children: [
                      pw.Container(
                          width: 137,
                          height: 28,
                          padding: const pw.EdgeInsets.only(left: 5, top: 2),
                          alignment: pw.Alignment.topLeft,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text(
                              'Veiculante: ${relatorioModel.recomendacoesTecnicas!.veiculante}',
                              textAlign: pw.TextAlign.left,
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          width: 137,
                          height: 28,
                          padding: const pw.EdgeInsets.only(top: 2),
                          alignment: pw.Alignment.topCenter,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text(
                              'Quant. Veic.  ${relatorioModel.recomendacoesTecnicas!.qtdVeiculante}',
                              textAlign: pw.TextAlign.left,
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          width: 137,
                          height: 28,
                          padding: const pw.EdgeInsets.only(top: 2),
                          alignment: pw.Alignment.topCenter,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text(
                              'Vazão L/Ha: ${relatorioModel.recomendacoesTecnicas!.volumeDaAplicacao}',
                              textAlign: pw.TextAlign.left,
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          width: 137,
                          height: 28,
                          padding: const pw.EdgeInsets.only(top: 2),
                          alignment: pw.Alignment.topCenter,
                          child: pw.Text(
                              'Largura Faixa  ${relatorioModel.recomendacoesTecnicas!.larguraDaFaixa}',
                              textAlign: pw.TextAlign.left,
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                    ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Row(
                    crossAxisAlignment: pw.CrossAxisAlignment.start,
                    children: [
                      pw.Container(
                          width: 115,
                          height: 28,
                          padding: const pw.EdgeInsets.only(left: 5, top: 2),
                          alignment: pw.Alignment.topLeft,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Row(children: [
                            pw.Text('Aeronave ',
                                textAlign: pw.TextAlign.left,
                                style:
                                    pw.TextStyle(fontSize: 12, font: newRoman)),
                            pw.Text(
                                ' ${relatorioModel.recomendacoesTecnicas!.aeronave}',
                                textAlign: pw.TextAlign.left,
                                style: pw.TextStyle(
                                  fontSize: 12,
                                  fontWeight: pw.FontWeight.bold,
                                  font: newRoman,
                                ))
                          ])),
                      pw.Container(
                          width: 115,
                          height: 28,
                          padding: const pw.EdgeInsets.only(top: 2),
                          alignment: pw.Alignment.topCenter,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Row(children: [
                            pw.Text(
                                'Altura Vôo  ${relatorioModel.recomendacoesTecnicas!.alturaDoVoo}',
                                textAlign: pw.TextAlign.left,
                                style:
                                    pw.TextStyle(fontSize: 12, font: newRoman)),
                            pw.Text(
                                ' ${relatorioModel.recomendacoesTecnicas!.alturaDoVoo}',
                                textAlign: pw.TextAlign.left,
                                style:
                                    pw.TextStyle(fontSize: 12, font: newRoman))
                          ])),
                      pw.Container(
                          width: 115,
                          height: 28,
                          padding: const pw.EdgeInsets.only(top: 2),
                          alignment: pw.Alignment.topCenter,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text(
                              'Temp. C°  ${relatorioModel.recomendacoesTecnicas!.temperatura}',
                              textAlign: pw.TextAlign.left,
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          width: 115,
                          height: 28,
                          padding: const pw.EdgeInsets.only(top: 2),
                          alignment: pw.Alignment.topCenter,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text(
                              'U.R% do Ar  ${relatorioModel.recomendacoesTecnicas!.umidadeRelativaDoAr}',
                              textAlign: pw.TextAlign.left,
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          width: 115,
                          height: 28,
                          padding: const pw.EdgeInsets.only(left: 5, top: 2),
                          alignment: pw.Alignment.topLeft,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text(
                              'Veloc. Vento  ${relatorioModel.recomendacoesTecnicas!.velocidadeDoVento}',
                              textAlign: pw.TextAlign.left,
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                    ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Row(
                    crossAxisAlignment: pw.CrossAxisAlignment.start,
                    children: [
                      pw.Container(
                          width: 345,
                          height: 25,
                          padding: const pw.EdgeInsets.only(left: 5, top: 2),
                          alignment: pw.Alignment.topLeft,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text('Regulagem Equip. Aplicação',
                              textAlign: pw.TextAlign.left,
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          width: 200,
                          height: 25,
                          padding: const pw.EdgeInsets.only(left: 5, top: 2),
                          alignment: pw.Alignment.topLeft,
                          child: pw.Text('Outros',
                              textAlign: pw.TextAlign.left,
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                    ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.SizedBox(
                    height: 50,
                    child: pw.Row(
                      crossAxisAlignment: pw.CrossAxisAlignment.center,
                      mainAxisAlignment: pw.MainAxisAlignment.spaceBetween,
                      children: [
                        pw.Container(
                            width: 320,
                            padding:
                                const pw.EdgeInsets.only(left: 5, bottom: 5),
                            alignment: pw.Alignment.bottomCenter,
                            child: pw.Column(
                              mainAxisAlignment: pw.MainAxisAlignment.end,
                              crossAxisAlignment: pw.CrossAxisAlignment.start,
                              children: [
                                pw.Text(" ${relatorioModel.executor}"),
                                pw.Text(
                                    '_____________________________________________________________',
                                    textAlign: pw.TextAlign.left,
                                    style: pw.TextStyle(
                                        fontSize: 11, font: newRoman)),
                                pw.Text('Executor',
                                    textAlign: pw.TextAlign.left,
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                                pw.Text('CFTA',
                                    textAlign: pw.TextAlign.left,
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                              ],
                            )),
                        pw.SizedBox(
                            width: 80,
                            child: pw.Text('de',
                                textAlign: pw.TextAlign.left,
                                style: pw.TextStyle(
                                    fontSize: 12, font: newRoman))),
                        pw.SizedBox(
                            width: 80,
                            child: pw.Text('de',
                                textAlign: pw.TextAlign.left,
                                style: pw.TextStyle(
                                    fontSize: 12, font: newRoman))),
                      ],
                    )),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Text('Relatório de Aplicação',
                    style: pw.TextStyle(
                        fontSize: 16,
                        font: newRomanBold,
                        color: PdfColors.green800,
                        fontWeight: pw.FontWeight.normal)),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Row(
                    crossAxisAlignment: pw.CrossAxisAlignment.center,
                    children: [
                      pw.Container(
                          height: 25,
                          width: 75,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          alignment: pw.Alignment.center,
                          child: pw.Text('Cultura',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          height: 25,
                          width: 120,
                          alignment: pw.Alignment.center,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text('Produto Aplicado',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          height: 25,
                          width: 100,
                          alignment: pw.Alignment.center,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text('Dosagem L/HA',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          height: 25,
                          width: 80,
                          alignment: pw.Alignment.center,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text('Vazão L/HA',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          height: 25,
                          width: 80,
                          alignment: pw.Alignment.center,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text('Densidade',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          height: 25,
                          width: 120,
                          alignment: pw.Alignment.center,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text('Área Total Aplicada',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                    ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Row(
                    crossAxisAlignment: pw.CrossAxisAlignment.center,
                    children: [
                      pw.Container(
                        height: 25,
                        width: 75,
                        child: pw.Text(
                            " ${relatorioModel.relatorioDeAplicacao!.cultura}",
                            style: pw.TextStyle(
                                fontWeight: pw.FontWeight.bold,
                                fontSize: 12,
                                font: newRomanBold)),
                        decoration: const pw.BoxDecoration(
                          border: pw.Border(
                            right: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                          ),
                        ),
                      ),
                      pw.Container(
                        height: 25,
                        width: 120,
                        child: pw.Text(
                            " ${relatorioModel.relatorioDeAplicacao!.produtoAplicado}",
                            style: pw.TextStyle(
                                fontWeight: pw.FontWeight.bold,
                                fontSize: 12,
                                font: newRomanBold)),
                        decoration: const pw.BoxDecoration(
                          border: pw.Border(
                            top: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                            right: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                          ),
                        ),
                      ),
                      pw.Container(
                        height: 25,
                        width: 100,
                        child: pw.Text(
                            " ${relatorioModel.relatorioDeAplicacao!.dosagem}",
                            style: pw.TextStyle(
                                fontWeight: pw.FontWeight.bold,
                                fontSize: 12,
                                font: newRomanBold)),
                        decoration: const pw.BoxDecoration(
                          border: pw.Border(
                            top: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                            right: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                          ),
                        ),
                      ),
                      pw.Container(
                        height: 25,
                        width: 80,
                        child: pw.Text(
                            " ${relatorioModel.relatorioDeAplicacao!.unidadeDosagem}",
                            style: pw.TextStyle(
                                fontWeight: pw.FontWeight.bold,
                                fontSize: 12,
                                font: newRomanBold)),
                        decoration: const pw.BoxDecoration(
                          border: pw.Border(
                            top: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                            right: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                          ),
                        ),
                      ),
                      pw.Container(
                        height: 25,
                        width: 80,
                        decoration: const pw.BoxDecoration(
                          border: pw.Border(
                            top: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                            right: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                          ),
                        ),
                      ),
                      pw.Container(
                        height: 25,
                        width: 120,
                        child: pw.Text(
                            " ${relatorioModel.relatorioDeAplicacao!.totalAreaAplicada}",
                            style: pw.TextStyle(
                                fontWeight: pw.FontWeight.bold,
                                fontSize: 12,
                                font: newRomanBold)),
                        decoration: const pw.BoxDecoration(
                          border: pw.Border(
                            top: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                          ),
                        ),
                      ),
                    ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Row(
                    crossAxisAlignment: pw.CrossAxisAlignment.center,
                    children: [
                      pw.Container(
                          height: 35,
                          width: 100,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          alignment: pw.Alignment.center,
                          child: pw.Text('Data Aplicação',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Column(children: [
                            pw.Container(
                                height: 17,
                                width: 170,
                                alignment: pw.Alignment.center,
                                child: pw.Text('Horário',
                                    style: pw.TextStyle(
                                        fontSize: 12, font: newRoman))),
                            pw.Row(children: [
                              pw.Container(
                                  height: 17,
                                  width: 85,
                                  decoration: const pw.BoxDecoration(
                                    border: pw.Border(
                                      top: pw.BorderSide(
                                          width: 1.5, color: PdfColors.black),
                                      right: pw.BorderSide(
                                          width: 1.5, color: PdfColors.black),
                                    ),
                                  ),
                                  alignment: pw.Alignment.center,
                                  child: pw.Text('Início',
                                      style: pw.TextStyle(
                                          fontSize: 12, font: newRoman))),
                              pw.Container(
                                  height: 17,
                                  width: 85,
                                  decoration: const pw.BoxDecoration(
                                    border: pw.Border(
                                      top: pw.BorderSide(
                                          width: 1.5, color: PdfColors.black),
                                    ),
                                  ),
                                  alignment: pw.Alignment.center,
                                  child: pw.Text('Término',
                                      style: pw.TextStyle(
                                          fontSize: 12, font: newRoman))),
                            ])
                          ])),
                      pw.Column(children: [
                        pw.Container(
                            height: 16,
                            alignment: pw.Alignment.center,
                            child: pw.Text(
                                'Condições climáticas durante a aplicação',
                                style: pw.TextStyle(
                                    fontSize: 12, font: newRoman))),
                        pw.Row(children: [
                          pw.Container(
                              height: 17.5,
                              width: 85,
                              decoration: const pw.BoxDecoration(
                                border: pw.Border(
                                  top: pw.BorderSide(
                                      width: 1.5, color: PdfColors.black),
                                  right: pw.BorderSide(
                                      width: 1.5, color: PdfColors.black),
                                ),
                              ),
                              alignment: pw.Alignment.center,
                              child: pw.Text('Temp. (° C)',
                                  style: pw.TextStyle(
                                      fontSize: 12, font: newRoman))),
                          pw.Container(
                              height: 17.5,
                              width: 95,
                              decoration: const pw.BoxDecoration(
                                border: pw.Border(
                                  top: pw.BorderSide(
                                      width: 1.5, color: PdfColors.black),
                                  right: pw.BorderSide(
                                      width: 1.5, color: PdfColors.black),
                                ),
                              ),
                              alignment: pw.Alignment.center,
                              child: pw.Text('U. R do Ar (%)',
                                  style: pw.TextStyle(
                                      fontSize: 12, font: newRoman))),
                          pw.Container(
                              height: 17.5,
                              width: 125,
                              decoration: const pw.BoxDecoration(
                                border: pw.Border(
                                  top: pw.BorderSide(
                                      width: 1.5, color: PdfColors.black),
                                ),
                              ),
                              alignment: pw.Alignment.center,
                              child: pw.Text('Vento (Km/h ou m/s)',
                                  style: pw.TextStyle(
                                      fontSize: 12, font: newRoman))),
                        ])
                      ]),
                    ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Column(children: [
                  pw.Row(children: [
                    pw.Container(
                      height: 15,
                      width: 100,
                      child: pw.Text(
                          " ${relatorioModel.relatorioDeAplicacao!.aplicacoes!.dataDaAplicacao}",
                          style: pw.TextStyle(
                              fontWeight: pw.FontWeight.bold,
                              fontSize: 12,
                              font: newRomanBold)),
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 85,
                      child: pw.Text(
                          " ${relatorioModel.relatorioDeAplicacao!.aplicacoes!.horarioDeInicio}",
                          style: pw.TextStyle(
                              fontWeight: pw.FontWeight.bold,
                              fontSize: 12,
                              font: newRomanBold)),
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 85,
                      child: pw.Text(
                          " ${relatorioModel.relatorioDeAplicacao!.aplicacoes!.horarioDeTermino}",
                          style: pw.TextStyle(
                              fontWeight: pw.FontWeight.bold,
                              fontSize: 12,
                              font: newRomanBold)),
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 85,
                      child: pw.Text(
                          " ${relatorioModel.relatorioDeAplicacao!.aplicacoes!.temperaturaIncial}",
                          style: pw.TextStyle(
                              fontWeight: pw.FontWeight.bold,
                              fontSize: 12,
                              font: newRomanBold)),
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 95,
                      child: pw.Text(
                          " ${relatorioModel.relatorioDeAplicacao!.aplicacoes!.umidadeRelativaInicial}",
                          style: pw.TextStyle(
                              fontWeight: pw.FontWeight.bold,
                              fontSize: 12,
                              font: newRomanBold)),
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 125,
                      child: pw.Text(
                          " ${relatorioModel.relatorioDeAplicacao!.aplicacoes!.ventoInicial}",
                          style: pw.TextStyle(
                              fontWeight: pw.FontWeight.bold,
                              fontSize: 12,
                              font: newRomanBold)),
                    ),
                  ]),
                  pw.Divider(height: 1, thickness: 1.5),
                  pw.Row(children: [
                    pw.Container(
                      height: 15,
                      width: 100,
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 85,
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 85,
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 85,
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 95,
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 125,
                    ),
                  ]),
                  pw.Divider(height: 1, thickness: 1.5),
                  pw.Row(children: [
                    pw.Container(
                      height: 15,
                      width: 100,
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 85,
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 85,
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 85,
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 95,
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 125,
                    ),
                  ]),
                ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Row(children: [
                  pw.Container(
                    height: 25,
                    width: 270,
                    decoration: const pw.BoxDecoration(
                      border: pw.Border(
                        right:
                            pw.BorderSide(width: 1.5, color: PdfColors.black),
                      ),
                    ),
                    alignment: pw.Alignment.bottomLeft,
                    padding: const pw.EdgeInsets.only(left: 2, bottom: 2),
                    child: pw.Row(children: [
                      pw.Text('Localização da Pista: ',
                          style: pw.TextStyle(fontSize: 12, font: newRoman)),
                      pw.Text(
                          '${relatorioModel.relatorioDeAplicacao!.localizacaoPista}',
                          style: pw.TextStyle(
                              fontSize: 12,
                              fontWeight: pw.FontWeight.bold,
                              font: newRomanBold))
                    ]),
                  ),
                  pw.Container(
                    height: 25,
                    width: 70,
                    decoration: const pw.BoxDecoration(
                      border: pw.Border(
                        right:
                            pw.BorderSide(width: 1.5, color: PdfColors.black),
                      ),
                    ),
                    alignment: pw.Alignment.center,
                    child: pw.Text('Horimetro:',
                        style: pw.TextStyle(fontSize: 12, font: newRoman)),
                  ),
                  pw.Container(
                    height: 25,
                    width: 120,
                    decoration: const pw.BoxDecoration(
                      border: pw.Border(
                        right:
                            pw.BorderSide(width: 1.5, color: PdfColors.black),
                      ),
                    ),
                    padding: const pw.EdgeInsets.only(left: 2, top: 2),
                    alignment: pw.Alignment.topLeft,
                    child: pw.Row(children: [
                      pw.Text('Inicial: ',
                          style: pw.TextStyle(fontSize: 12, font: newRoman)),
                      pw.Text(
                          '${relatorioModel.relatorioDeAplicacao!.aplicacoes!.horimetroInicial}',
                          style: pw.TextStyle(
                              fontSize: 12,
                              fontWeight: pw.FontWeight.bold,
                              font: newRomanBold))
                    ]),
                  ),
                  pw.Container(
                    height: 25,
                    width: 120,
                    padding: const pw.EdgeInsets.only(left: 2, top: 2),
                    alignment: pw.Alignment.topLeft,
                    child: pw.Row(children: [
                      pw.Text('Final:',
                          style: pw.TextStyle(fontSize: 12, font: newRoman)),
                      pw.Text(
                        ' ${relatorioModel.relatorioDeAplicacao!.aplicacoes!.horimetroFinal}',
                        style: pw.TextStyle(
                          fontSize: 12,
                          fontWeight: pw.FontWeight.bold,
                          font: newRomanBold,
                        ),
                      )
                    ]),
                  ),
                ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Row(children: [
                  pw.Container(
                    height: 28,
                    width: 340,
                    decoration: const pw.BoxDecoration(
                      border: pw.Border(
                        right:
                            pw.BorderSide(width: 1.5, color: PdfColors.black),
                      ),
                    ),
                    alignment: pw.Alignment.centerLeft,
                    padding: const pw.EdgeInsets.only(left: 2),
                    child: pw.Text('Alterações do Planejamento:',
                        style: pw.TextStyle(fontSize: 12, font: newRoman)),
                  ),
                  pw.Container(
                    height: 28,
                    width: 240,
                    padding: const pw.EdgeInsets.only(left: 2),
                    alignment: pw.Alignment.centerLeft,
                    child: pw.Text(
                        'Emitiu relatório do DGPS: ${getIt<GlobalConfigVars>().dgs}',
                        style: pw.TextStyle(fontSize: 12, font: newRoman)),
                  ),
                ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Text('Contrato de Prestação de Serviços',
                    style: pw.TextStyle(
                        fontSize: 16,
                        font: newRomanBold,
                        color: PdfColors.green800,
                        fontWeight: pw.FontWeight.normal)),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Container(
                    margin: const pw.EdgeInsets.symmetric(
                        horizontal: 4.0, vertical: 4.0),
                    height: 40,
                    decoration: const pw.BoxDecoration(
                      border: pw.Border(
                        right:
                            pw.BorderSide(width: 1.5, color: PdfColors.black),
                        bottom:
                            pw.BorderSide(width: 1.5, color: PdfColors.black),
                        top: pw.BorderSide(width: 1.5, color: PdfColors.black),
                        left: pw.BorderSide(width: 1.5, color: PdfColors.black),
                      ),
                    ),
                    child: pw.Column(children: [
                      pw.Container(
                        height: 15,
                        child: pw.Text('Pagamento em Moeda',
                            style: pw.TextStyle(fontSize: 12, font: newRoman)),
                      ),
                      pw.Divider(height: 1, thickness: 1.5),
                      pw.Padding(
                          padding: const pw.EdgeInsets.symmetric(horizontal: 5),
                          child: pw.Row(
                              mainAxisAlignment:
                                  pw.MainAxisAlignment.spaceBetween,
                              children: [
                                pw.Container(
                                  height: 20,
                                  alignment: pw.Alignment.bottomCenter,
                                  child: pw.Row(children: [
                                    pw.Text('Preço Ha:',
                                        style: pw.TextStyle(
                                            fontSize: 12, font: newRoman)),
                                    pw.Text(
                                        ' ${relatorioModel.contratoServico!.preco!}',
                                        style: pw.TextStyle(
                                            fontSize: 12,
                                            fontWeight: pw.FontWeight.bold,
                                            font: newRomanBold))
                                  ]),
                                ),
                                pw.Container(
                                  height: 20,
                                  alignment: pw.Alignment.bottomCenter,
                                  child: pw.Row(children: [
                                    pw.Text('Valor Total: ',
                                        style: pw.TextStyle(
                                            fontSize: 12, font: newRoman)),
                                    pw.Text(
                                        ' ${relatorioModel.contratoServico!.valorTotal!}',
                                        style: pw.TextStyle(
                                            fontWeight: pw.FontWeight.bold,
                                            fontSize: 12,
                                            font: newRomanBold))
                                  ]),
                                ),
                                pw.Row(children: [
                                  pw.Container(
                                    height: 20,
                                    alignment: pw.Alignment.bottomCenter,
                                    child: pw.Text('Vencimento:',
                                        style: pw.TextStyle(
                                            fontSize: 12, font: newRoman)),
                                  ),
                                  pw.Container(
                                    height: 20,
                                    alignment: pw.Alignment.bottomCenter,
                                    child: pw.Text(
                                        '${relatorioModel.contratoServico!.vencimento}',
                                        style: pw.TextStyle(
                                            fontWeight: pw.FontWeight.bold,
                                            fontSize: 12,
                                            font: newRomanBold)),
                                  ),
                                ])
                              ]))
                    ])),
                pw.Container(
                  height: 15,
                  margin:
                      const pw.EdgeInsets.only(left: 4.0, bottom: 4, right: 4),
                  alignment: pw.Alignment.center,
                  child: pw.Text(
                      '         O contratante declara estar plenamente de acordo com os serviços executados, área, valor e forma de pagamento expressa neste contrato, tendo o mesmo valor como comprovante de entrega dos serviços efetudos',
                      style: pw.TextStyle(fontSize: 11, font: newRoman)),
                ),
                pw.Container(
                  height: 15,
                  margin: const pw.EdgeInsets.only(left: 4.0, top: 4, right: 4),
                  alignment: pw.Alignment.center,
                  child: pw.Text(
                      '         E, por estarem de acordo com todas as cláusulas, itens e demais condições estabelecidas neste contrato, inclusive as constantes no verso, as partes firmam o presente, tendo valor como testemunhas as assinaturas do Piloto e Eng. Agrônomo.',
                      style: pw.TextStyle(fontSize: 11, font: newRoman)),
                ),
                pw.Container(
                  height: 15,
                  margin:
                      const pw.EdgeInsets.only(left: 4.0, right: 4, top: 12),
                  alignment: pw.Alignment.centerRight,
                  child: pw.Text(
                      '${relatorioModel.dadosDoResponsavel!.cidade} ${relatorioModel.dadosDoResponsavel!.uf}, ${relatorioModel.dadosDoResponsavel!.data}  de ______________ de ______',
                      style: pw.TextStyle(fontSize: 11, font: newRoman)),
                ),
                pw.SizedBox(height: 12),
                pw.Padding(
                    padding: const pw.EdgeInsets.symmetric(horizontal: 5),
                    child: pw.Row(
                        mainAxisAlignment: pw.MainAxisAlignment.spaceBetween,
                        crossAxisAlignment: pw.CrossAxisAlignment.start,
                        children: [
                          pw.Column(
                              crossAxisAlignment: pw.CrossAxisAlignment.start,
                              children: [
                                pw.Text('_______________________________',
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                                pw.Text('Contratante',
                                    textAlign: pw.TextAlign.left,
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                                pw.Text(
                                    'RG ${relatorioModel.dadosDoResponsavel!.cpf}',
                                    textAlign: pw.TextAlign.left,
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                              ]),
                          pw.Column(
                              crossAxisAlignment: pw.CrossAxisAlignment.start,
                              children: [
                                pw.Text('_______________________________',
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                                pw.Text('Eng. Agro',
                                    textAlign: pw.TextAlign.left,
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                              ]),
                          pw.Column(
                              crossAxisAlignment: pw.CrossAxisAlignment.start,
                              children: [
                                pw.Text('_______________________________',
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                                pw.Text('Piloto',
                                    textAlign: pw.TextAlign.left,
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                                pw.Text('COD. ANAC',
                                    textAlign: pw.TextAlign.left,
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                              ]),
                          pw.Column(
                              crossAxisAlignment: pw.CrossAxisAlignment.start,
                              children: [
                                pw.Text('_______________________________',
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                                pw.Text('Executor',
                                    textAlign: pw.TextAlign.left,
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                                pw.Text('CFTA',
                                    textAlign: pw.TextAlign.left,
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                              ])
                        ])),
              ]),
            ]),
          );
        },
      ),
    );

    return pdf;
  }

  @override
  Future<Uint8List?> saveDocument({dynamic document}) async {
    return await document.save();
  }
}
 */
