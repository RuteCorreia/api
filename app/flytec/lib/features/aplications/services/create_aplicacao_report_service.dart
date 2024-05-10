import 'dart:convert';
import 'package:flutter/services.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/executor/data/models/excutores_model.dart';
import 'package:flytec/features/piloto/data/models/excutores_model.dart';
import 'package:intl/intl.dart';
import 'package:pdf/pdf.dart';
import 'package:pdf/widgets.dart' as pw;
import 'package:flytec/core/utils/pdf_generator.dart';
import 'package:flytec/features/aplications/models/aplicacao.dart';

class CreateAplicacaoReportService implements PdfGenerator {
  final Aplicacao aplicacao;
  CreateAplicacaoReportService({required this.aplicacao});

  @override
  Future generatePdf({parameters}) async {
    final pdf = pw.Document();
    final newRoman = pw.Font.times();
    final newRomanBold = pw.Font.timesBold();
    final epochDataEmissao = int.tryParse(
        aplicacao.caracteristicasProdutoAplicado?.dataEmissao ?? '');
    final dateDataEmissao = epochDataEmissao != null
        ? DateTime.fromMillisecondsSinceEpoch(epochDataEmissao)
        : null;
    final logoImage = (await rootBundle.load('assets/images/logo-light.png'))
        .buffer
        .asUint8List();
    final epochContratante =
        int.tryParse(aplicacao.dadosResponsavel?.data ?? '');
    final dateTimeContratante = epochContratante != null
        ? DateTime?.fromMillisecondsSinceEpoch(epochContratante)
        : null;
    String? dataContratante = dateTimeContratante != null
        ? "${dateTimeContratante.day}/${dateTimeContratante.month}/${dateTimeContratante.year}"
        : '';
    final vencimentoEpoch =
        int.tryParse(aplicacao.contratoPrestacaoServico?.vencimento ?? '');
    final dataVencimentoContrato = vencimentoEpoch != null
        ? DateTime.fromMillisecondsSinceEpoch(vencimentoEpoch)
        : null;
    String? vencimentoContrato =
        "${dataVencimentoContrato?.day ?? ''}/${dataVencimentoContrato?.month ?? ''}/${dataVencimentoContrato?.year ?? ''}";

    final aplicacoes01 = aplicacao.relatorioAplicacao!.aplicacoes!.isNotEmpty &&
            aplicacao.relatorioAplicacao?.aplicacoes?.first != null
        ? aplicacao.relatorioAplicacao?.aplicacoes?.first
        : null;
    final aplicacoes02 = aplicacao.relatorioAplicacao!.aplicacoes!.isNotEmpty &&
            aplicacao.relatorioAplicacao!.aplicacoes!.length > 1 &&
            aplicacao.relatorioAplicacao?.aplicacoes?[1] != null
        ? aplicacao.relatorioAplicacao?.aplicacoes![1]
        : null;
    final aplicacoes03 = aplicacao.relatorioAplicacao!.aplicacoes!.isNotEmpty &&
            aplicacao.relatorioAplicacao!.aplicacoes!.length > 2 &&
            aplicacao.relatorioAplicacao?.aplicacoes?[2] != null
        ? aplicacao.relatorioAplicacao?.aplicacoes![2]
        : null;

    int? epochAplicacao = int.tryParse(aplicacao.data!);
    final dataAplicacao = DateTime.fromMillisecondsSinceEpoch(epochAplicacao!);
    final pilotoSelected = getIt<GlobalConfigVars>().pilotos.firstWhere(
        (piloto) => aplicacao.piloto == piloto.nomePiloto,
        orElse: () => const PilotoModel(nomePiloto: '', cdac: 'COD.ANAC'));
    final executorSelected = getIt<GlobalConfigVars>().executores.firstWhere(
        (executor) => aplicacao.executor == executor.nome,
        orElse: () => const ExecutorModel(cfta: 'CFTA'));
    final engenheiroSelected =
        getIt<GlobalConfigVars>().engenheiros.firstOrNull;
    final empresa = getIt<GlobalConfigVars>().userPayload.empresa;

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
                                child: pw.Image(
                                    pw.MemoryImage(
                                        empresa?.logoEmpresa != null &&
                                                empresa!.logoEmpresa!.isNotEmpty
                                            ? base64Decode(empresa.logoEmpresa!)
                                            : logoImage),
                                    fit: pw.BoxFit.contain,
                                    width: 140,
                                    height: 140),
                              ),
                              pw.SizedBox(width: 10),
                              pw.Column(
                                  crossAxisAlignment:
                                      pw.CrossAxisAlignment.start,
                                  children: [
                                    pw.Text(
                                        empresa?.nomeEmpresa ??
                                            'IMAGEM AVIAÇÃO AGRÍCOLA LTDA.',
                                        style: pw.TextStyle(
                                            fontSize: 14,
                                            fontWeight: pw.FontWeight.normal)),
                                    pw.Text(
                                        'MA SP - ${empresa?.numeroEmpresa ?? '000000-0'} - CNPJ 00.000.000/0000-00 - Inscr. Est. ${empresa?.inscricaoEstadualEmpresa ?? '000.000.000.000'}',
                                        maxLines: 1,
                                        style: pw.TextStyle(
                                            fontSize: 8,
                                            fontWeight: pw.FontWeight.normal)),
                                    pw.SizedBox(height: 10),
                                    pw.Text(
                                        'FONE: ${empresa?.telefoneEmpresa ?? '(XX) XXXX-XXXX'}',
                                        style: pw.TextStyle(
                                            fontSize: 14,
                                            fontWeight: pw.FontWeight.bold)),
                                  ])
                            ]),
                            pw.SizedBox(height: 5),
                            pw.Text(
                                '${empresa?.enderecoEmpresa ?? 'RUA PARANÁ, 000 - CENTRO'} - CEP ${empresa?.cepEmpresa ?? '00000-000'} - ${empresa?.cidadeEmpresa ?? 'MONÇÕES'} - EST.${empresa?.estadoEmpresa ?? 'SÃO PAULO'}',
                                style: pw.TextStyle(
                                    fontSize: 8,
                                    fontWeight: pw.FontWeight.normal)),
                          ],
                        )),
                    pw.Column(children: [
                      pw.Padding(
                        padding: const pw.EdgeInsets.only(top: 2),
                        child: pw.Align(
                            child: pw.Text(
                                '                           CDA N° ${empresa?.nrCDAEmpresa ?? '4046'}',
                                style: const pw.TextStyle(
                                    color: PdfColors.red, fontSize: 10)),
                            alignment: pw.Alignment.centerRight),
                      ),
                      pw.Align(
                          child: pw.Text(
                              '                                       N° RELATÓRIO: ${aplicacao.refDocument}',
                              style: const pw.TextStyle(
                                  color: PdfColors.black, fontSize: 10)),
                          alignment: pw.Alignment.centerRight),
                      pw.Align(
                          child: pw.Text(
                              '                                  DATA: ${Util.getTodayDate(date: dataAplicacao)}',
                              style: const pw.TextStyle(
                                  color: PdfColors.black, fontSize: 10)),
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
                                            pw.Text(
                                                '${aplicacao.caracteristicasProdutoAplicado?.numeroReceituarioAgronomico?.toString().padLeft(4, '0') ?? ''} ',
                                                style: const pw.TextStyle(
                                                  color: PdfColors.red,
                                                )),
                                            pw.Text(' Data '),
                                            pw.Text(dateDataEmissao != null
                                                ? DateFormat('dd/MM/yyyy')
                                                    .format(dateDataEmissao)
                                                    .toString()
                                                : ''),
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
                              'Nome: ${aplicacao.contratante?.nome ?? ''} ',
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
                                      'CNPJ/CPF:  ${aplicacao.contratante?.cnpj ?? ''}',
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
                                  child: pw.Text(
                                      'I.E/R.G: ${aplicacao.contratante!.inscricaoEstadual!.isNotEmpty ? aplicacao.contratante?.inscricaoEstadual ?? '' : aplicacao.contratante?.rg ?? ''}',
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
                      pw.Container(
                          height: 30,
                          width: 250,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              top: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                              left: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Padding(
                            padding: const pw.EdgeInsets.only(left: 5, top: 2),
                            child: pw.Text(
                                'Endereço: ${aplicacao.contratante?.endereco ?? ''}',
                                maxLines: 2,
                                style:
                                    pw.TextStyle(fontSize: 12, font: newRoman)),
                          )),
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
                          child: pw.Padding(
                            padding: const pw.EdgeInsets.only(left: 10, top: 2),
                            child: pw.Text(
                                'Município/UF: ${aplicacao.contratante?.cidade ?? ''}, ${aplicacao.contratante?.uf ?? ''} ',
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
                              'Localização: ${aplicacao.identificacaoAreaTratada?.localizacao ?? ''}',
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
                                'Cultura: ${aplicacao.identificacaoAreaTratada?.cultura ?? ''}',
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
                                      'Extensão ${aplicacao.identificacaoAreaTratada?.extensao ?? ''}ha',
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
                          width: 55,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          alignment: pw.Alignment.center,
                          child: pw.Text('Nome',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          height: 20,
                          width: 90,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          alignment: pw.Alignment.center,
                          child: pw.Text('Alvo Biológico',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          height: 20,
                          width: 60,
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
                          width: 110,
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
                          child: pw.Text('Quant/ha',
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
                            " ${aplicacao.caracteristicasProdutoAplicado?.nomeProduto ?? ''}",
                            textAlign: pw.TextAlign.center,
                            style: pw.TextStyle(fontSize: 11, font: newRoman)),
                        height: 25,
                        width: 55,
                        alignment: pw.Alignment.center,
                        decoration: const pw.BoxDecoration(
                          border: pw.Border(
                            right: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                          ),
                        ),
                      ),
                      pw.Container(
                          height: 25,
                          width: 90,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          alignment: pw.Alignment.center,
                          child: pw.Text(
                              ' ${aplicacao.caracteristicasProdutoAplicado?.alvoBiologico ?? ''}',
                              textAlign: pw.TextAlign.center,
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                        height: 25,
                        width: 60,
                        alignment: pw.Alignment.center,
                        child: pw.Text(
                            " ${aplicacao.caracteristicasProdutoAplicado?.classificacaoToxicologica ?? ''}",
                            textAlign: pw.TextAlign.center,
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
                        width: 110,
                        alignment: pw.Alignment.center,
                        child: pw.Text(
                            " ${aplicacao.caracteristicasProdutoAplicado?.tipoServico ?? ''}",
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
                        height: 23,
                        width: 100,
                        alignment: pw.Alignment.center,
                        child: pw.Text(
                            " ${aplicacao.caracteristicasProdutoAplicado?.tipoFormulacao ?? ''}",
                            textAlign: pw.TextAlign.center,
                            style: pw.TextStyle(fontSize: 11, font: newRoman)),
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
                        alignment: pw.Alignment.center,
                        child: pw.Text(
                            " ${aplicacao.caracteristicasProdutoAplicado?.doseProdutoHectare ?? ''} ${aplicacao.caracteristicasProdutoAplicado?.unidadeDoseProdutoHectare ?? ''}",
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
                        alignment: pw.Alignment.center,
                        child: pw.Text(
                            " ${aplicacao.caracteristicasProdutoAplicado?.adjuvante ?? ''}",
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
                          height: 18,
                          padding: const pw.EdgeInsets.only(left: 5, top: 2),
                          alignment: pw.Alignment.topLeft,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text(
                              'Veiculante: ${aplicacao.recomendacoesTecnicas?.veiculante ?? ''}',
                              textAlign: pw.TextAlign.left,
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          width: 137,
                          height: 18,
                          padding: const pw.EdgeInsets.only(top: 2),
                          alignment: pw.Alignment.topCenter,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text(
                              'Volume ${aplicacao.recomendacoesTecnicas?.volumeAplicacao ?? ''} ${aplicacao.recomendacoesTecnicas?.unidadevolumeAplicacao ?? ''}',
                              textAlign: pw.TextAlign.left,
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          width: 137,
                          height: 18,
                          padding: const pw.EdgeInsets.only(top: 2),
                          alignment: pw.Alignment.topCenter,
                          child: pw.Text(
                              'Largura Faixa  ${aplicacao.recomendacoesTecnicas?.larguraFaixa ?? ''}',
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
                          height: 20,
                          padding: const pw.EdgeInsets.only(left: 5, top: 2),
                          alignment: pw.Alignment.topLeft,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text(
                              'Aeronave  ${aplicacao.recomendacoesTecnicas?.aeronave ?? ''}',
                              textAlign: pw.TextAlign.left,
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          width: 115,
                          height: 20,
                          padding: const pw.EdgeInsets.only(top: 2),
                          alignment: pw.Alignment.topCenter,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text(
                              'Altura Vôo  ${aplicacao.recomendacoesTecnicas?.alturaVoo ?? ''} m',
                              textAlign: pw.TextAlign.left,
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          width: 115,
                          height: 20,
                          padding: const pw.EdgeInsets.only(top: 2),
                          alignment: pw.Alignment.topCenter,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text(
                              'Temp.  <${aplicacao.recomendacoesTecnicas?.temperatura ?? ''}',
                              textAlign: pw.TextAlign.left,
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          width: 115,
                          height: 20,
                          padding: const pw.EdgeInsets.only(top: 2),
                          alignment: pw.Alignment.topCenter,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text(
                              'U.R do Ar  >${aplicacao.recomendacoesTecnicas?.umidadeRelativaAr?.replaceAll('+', '') ?? ''}',
                              textAlign: pw.TextAlign.left,
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          width: 115,
                          height: 20,
                          padding: const pw.EdgeInsets.only(left: 5, top: 2),
                          alignment: pw.Alignment.topLeft,
                          decoration: const pw.BoxDecoration(
                            border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black),
                            ),
                          ),
                          child: pw.Text(
                              'Veloc. Vento  ${aplicacao.recomendacoesTecnicas?.velocidadeVento ?? ''}',
                              textAlign: pw.TextAlign.left,
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                    ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Container(
                    height: 20,
                    padding: const pw.EdgeInsets.only(left: 5, top: 2),
                    alignment: pw.Alignment.topLeft,
                    child: pw.Text(
                        'Regulagem Equip. Aplicação ${aplicacao.recomendacoesTecnicas?.equipamento ?? ''} ${aplicacao.recomendacoesTecnicas?.angulo ?? ''}',
                        textAlign: pw.TextAlign.left,
                        style: pw.TextStyle(fontSize: 12, font: newRoman))),
                pw.Divider(height: 1, thickness: 1.5),
                pw.SizedBox(
                    height: 38,
                    child: pw.Row(
                      crossAxisAlignment: pw.CrossAxisAlignment.center,
                      mainAxisAlignment: pw.MainAxisAlignment.spaceBetween,
                      children: [
                        pw.Container(
                            width: 300,
                            padding: const pw.EdgeInsets.only(left: 5, top: 5),
                            alignment: pw.Alignment.bottomCenter,
                            child: pw.Column(
                              mainAxisAlignment: pw.MainAxisAlignment.center,
                              crossAxisAlignment: pw.CrossAxisAlignment.start,
                              children: [
                                pw.Text(" ${aplicacao.executor ?? ''}"),
                                pw.Divider(height: 0.5, thickness: 1.0),
                                pw.Text('Executor',
                                    textAlign: pw.TextAlign.left,
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                                pw.Text('CFTA ${executorSelected.cfta ?? ''}',
                                    textAlign: pw.TextAlign.left,
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                              ],
                            )),
                        pw.Container(
                            padding: const pw.EdgeInsets.only(right: 5),
                            width: 180,
                            child: pw.Text(
                                ' ${aplicacao.identificacaoAreaTratada?.cidade ?? ''} ${aplicacao.identificacaoAreaTratada?.uf ?? ''},  ${aplicacao.dadosResponsavel?.data != null ? Util.getTodayDate(date: DateTime.fromMillisecondsSinceEpoch(int.tryParse(aplicacao.dadosResponsavel?.data ?? '') ?? DateTime.now().millisecondsSinceEpoch)) : ''} ',
                                textAlign: pw.TextAlign.right,
                                maxLines: 1,
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
                            " ${aplicacao.relatorioAplicacao?.cultura ?? ''}"),
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
                            " ${aplicacao.relatorioAplicacao?.produtoAplicado ?? ''}"),
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
                            " ${aplicacao.relatorioAplicacao?.dosagem ?? ''} ${aplicacao.relatorioAplicacao?.unidadeDosagem ?? ''}"),
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
                            " ${aplicacao.relatorioAplicacao?.volumeAplicacao ?? ''} ${aplicacao.relatorioAplicacao?.unidadeVolumeAplicacao ?? ''}"),
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
                          aplicacao.relatorioAplicacao?.densidade ?? '',
                        ),
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
                            " ${aplicacao.relatorioAplicacao?.totalAreaAplicada ?? ''}ha"),
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
                                width: 80,
                                alignment: pw.Alignment.center,
                                child: pw.Text('Horário',
                                    style: pw.TextStyle(
                                        fontSize: 12, font: newRoman))),
                            pw.Row(children: [
                              pw.Container(
                                  height: 17,
                                  width: 60,
                                  decoration: const pw.BoxDecoration(
                                    border: pw.Border(
                                      top: pw.BorderSide(
                                          width: 1.5, color: PdfColors.black),
                                      right: pw.BorderSide(
                                          width: 1.5, color: PdfColors.black),
                                    ),
                                  ),
                                  alignment: pw.Alignment.center,
                                  child: pw.Text('Inicial',
                                      style: pw.TextStyle(
                                          fontSize: 12, font: newRoman))),
                              pw.Container(
                                  height: 17,
                                  width: 60,
                                  decoration: const pw.BoxDecoration(
                                    border: pw.Border(
                                      top: pw.BorderSide(
                                          width: 1.5, color: PdfColors.black),
                                    ),
                                  ),
                                  alignment: pw.Alignment.center,
                                  child: pw.Text('Final',
                                      style: pw.TextStyle(
                                          fontSize: 12, font: newRoman))),
                            ])
                          ])),
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
                                width: 120,
                                alignment: pw.Alignment.center,
                                child: pw.Text('Temperatura °C',
                                    style: pw.TextStyle(
                                        fontSize: 12, font: newRoman))),
                            pw.Row(children: [
                              pw.Container(
                                  height: 17,
                                  width: 60,
                                  decoration: const pw.BoxDecoration(
                                    border: pw.Border(
                                      top: pw.BorderSide(
                                          width: 1.5, color: PdfColors.black),
                                      right: pw.BorderSide(
                                          width: 1.5, color: PdfColors.black),
                                    ),
                                  ),
                                  alignment: pw.Alignment.center,
                                  child: pw.Text('Inicial',
                                      style: pw.TextStyle(
                                          fontSize: 12, font: newRoman))),
                              pw.Container(
                                  height: 17,
                                  width: 60,
                                  decoration: const pw.BoxDecoration(
                                    border: pw.Border(
                                      top: pw.BorderSide(
                                          width: 1.5, color: PdfColors.black),
                                    ),
                                  ),
                                  alignment: pw.Alignment.center,
                                  child: pw.Text('Final',
                                      style: pw.TextStyle(
                                          fontSize: 12, font: newRoman))),
                            ])
                          ])),
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
                                width: 100,
                                alignment: pw.Alignment.center,
                                child: pw.Text('U.R. (%)',
                                    style: pw.TextStyle(
                                        fontSize: 12, font: newRoman))),
                            pw.Row(children: [
                              pw.Container(
                                  height: 17,
                                  width: 50,
                                  decoration: const pw.BoxDecoration(
                                    border: pw.Border(
                                      top: pw.BorderSide(
                                          width: 1.5, color: PdfColors.black),
                                      right: pw.BorderSide(
                                          width: 1.5, color: PdfColors.black),
                                    ),
                                  ),
                                  alignment: pw.Alignment.center,
                                  child: pw.Text('Inicial',
                                      style: pw.TextStyle(
                                          fontSize: 12, font: newRoman))),
                              pw.Container(
                                  height: 17,
                                  width: 50,
                                  decoration: const pw.BoxDecoration(
                                    border: pw.Border(
                                      top: pw.BorderSide(
                                          width: 1.5, color: PdfColors.black),
                                    ),
                                  ),
                                  alignment: pw.Alignment.center,
                                  child: pw.Text('Final',
                                      style: pw.TextStyle(
                                          fontSize: 12, font: newRoman))),
                            ])
                          ])),
                      pw.Container(
                          child: pw.Column(children: [
                        pw.Container(
                            height: 17,
                            width: 135,
                            alignment: pw.Alignment.center,
                            child: pw.Text('Velocidade Vento (Km/h)',
                                style: pw.TextStyle(
                                    fontSize: 12, font: newRoman))),
                        pw.Row(children: [
                          pw.Container(
                              height: 17,
                              width: 67.5,
                              decoration: const pw.BoxDecoration(
                                border: pw.Border(
                                  top: pw.BorderSide(
                                      width: 1.5, color: PdfColors.black),
                                  right: pw.BorderSide(
                                      width: 1.5, color: PdfColors.black),
                                ),
                              ),
                              alignment: pw.Alignment.center,
                              child: pw.Text('Inicial',
                                  style: pw.TextStyle(
                                      fontSize: 12, font: newRoman))),
                          pw.Container(
                              height: 17,
                              width: 67.5,
                              decoration: const pw.BoxDecoration(
                                border: pw.Border(
                                  top: pw.BorderSide(
                                      width: 1.5, color: PdfColors.black),
                                ),
                              ),
                              alignment: pw.Alignment.center,
                              child: pw.Text('Final',
                                  style: pw.TextStyle(
                                      fontSize: 12, font: newRoman))),
                        ])
                      ])),
                    ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Column(children: [
                  pw.Row(children: [
                    pw.Container(
                      height: 15,
                      width: 100,
                      alignment: pw.Alignment.center,
                      child: pw.Text(
                          " ${int.tryParse(aplicacoes01 != null ? aplicacoes01.dataAplicacao! : '') != null ? Util.getTodayDate(date: DateTime.fromMillisecondsSinceEpoch(int.tryParse(aplicacoes01!.dataAplicacao!)!)) : ""}"),
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                        height: 15,
                        width: 60,
                        decoration: const pw.BoxDecoration(
                          border: pw.Border(
                            top: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                            right: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                          ),
                        ),
                        alignment: pw.Alignment.center,
                        child: pw.Text(
                          aplicacoes01 != null ? aplicacoes01.horaInicio! : '',
                        )),
                    pw.Container(
                        height: 15,
                        width: 60,
                        decoration: const pw.BoxDecoration(
                          border: pw.Border(
                            top: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                            right: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                          ),
                        ),
                        alignment: pw.Alignment.center,
                        child: pw.Text(
                          aplicacoes01 != null ? aplicacoes01.horaFinal! : '',
                        )),
                    pw.Container(
                      height: 15,
                      width: 60,
                      alignment: pw.Alignment.center,
                      child: pw.Text(
                          " ${aplicacoes01 != null ? aplicacoes01.temperaturaInicial! : ''}"),
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 60,
                      alignment: pw.Alignment.center,
                      child: pw.Text(
                          " ${aplicacoes01 != null ? aplicacoes01.temperaturaFinal : ''}"),
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 50,
                      alignment: pw.Alignment.center,
                      child: pw.Text(
                          " ${aplicacoes01 != null ? aplicacoes01.umidadeRelativaArInicial?.replaceAll('+', '') : ''}"),
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 50,
                      alignment: pw.Alignment.center,
                      child: pw.Text(
                          " ${aplicacoes01 != null ? aplicacoes01.umidadeRelativaArFinal?.replaceAll('+', '') : ''}"),
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 67.5,
                      alignment: pw.Alignment.center,
                      decoration: const pw.BoxDecoration(
                          border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black))),
                      child: pw.Text(
                          " ${aplicacoes01 != null ? aplicacoes01.ventoInicial : ''}"),
                    ),
                    pw.Container(
                      height: 15,
                      width: 67.5,
                      alignment: pw.Alignment.center,
                      child: pw.Text(
                          " ${aplicacoes01 != null ? aplicacoes01.ventoFinal : ''}"),
                    ),
                  ]),
                  pw.Divider(height: 1, thickness: 1.5),
                  pw.Row(children: [
                    pw.Container(
                      height: 15,
                      width: 100,
                      alignment: pw.Alignment.center,
                      child: pw.Text(
                          " ${int.tryParse(aplicacoes02 != null ? aplicacoes02.dataAplicacao! : '') != null ? Util.getTodayDate(date: DateTime.fromMillisecondsSinceEpoch(int.tryParse(aplicacoes02!.dataAplicacao!)!)) : ""}"),
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                        height: 15,
                        width: 60,
                        decoration: const pw.BoxDecoration(
                          border: pw.Border(
                            top: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                            right: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                          ),
                        ),
                        alignment: pw.Alignment.center,
                        child: pw.Text(
                          '${aplicacoes02 != null ? aplicacoes02.horaInicio : ''}',
                        )),
                    pw.Container(
                        height: 15,
                        width: 60,
                        decoration: const pw.BoxDecoration(
                          border: pw.Border(
                            top: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                            right: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                          ),
                        ),
                        alignment: pw.Alignment.center,
                        child: pw.Text(
                          '${aplicacoes02 != null ? aplicacoes02.horaFinal : ''}',
                        )),
                    pw.Container(
                      height: 15,
                      width: 60,
                      alignment: pw.Alignment.center,
                      child: pw.Text(
                          " ${aplicacoes02 != null ? aplicacoes02.temperaturaInicial : ''}"),
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 60,
                      alignment: pw.Alignment.center,
                      child: pw.Text(
                          " ${aplicacoes02 != null ? aplicacoes02.temperaturaFinal : ''}"),
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 50,
                      alignment: pw.Alignment.center,
                      child: pw.Text(
                          " ${aplicacoes02 != null ? aplicacoes02.umidadeRelativaArInicial : ''}"),
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 50,
                      alignment: pw.Alignment.center,
                      child: pw.Text(
                          " ${aplicacoes02 != null ? aplicacoes02.umidadeRelativaArFinal : ''}"),
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 67.5,
                      alignment: pw.Alignment.center,
                      decoration: const pw.BoxDecoration(
                          border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black))),
                      child: pw.Text(
                          " ${aplicacoes02 != null ? aplicacoes02.ventoInicial : ''}"),
                    ),
                    pw.Container(
                      height: 15,
                      width: 67.5,
                      alignment: pw.Alignment.center,
                      child: pw.Text(
                          " ${aplicacoes02 != null ? aplicacoes02.ventoFinal : ''}"),
                    ),
                  ]),
                  pw.Divider(height: 1, thickness: 1.5),
                  pw.Row(children: [
                    pw.Container(
                      height: 15,
                      width: 100,
                      alignment: pw.Alignment.center,
                      child: pw.Text(
                          " ${int.tryParse(aplicacoes03 != null ? aplicacoes03.dataAplicacao! : '') != null ? Util.getTodayDate(date: DateTime.fromMillisecondsSinceEpoch(int.tryParse(aplicacoes03!.dataAplicacao!)!)) : ""}"),
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                        height: 15,
                        width: 60,
                        decoration: const pw.BoxDecoration(
                          border: pw.Border(
                            top: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                            right: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                          ),
                        ),
                        alignment: pw.Alignment.center,
                        child: pw.Text(
                          '${aplicacoes03 != null ? aplicacoes03.horaInicio : ''}',
                        )),
                    pw.Container(
                        height: 15,
                        width: 60,
                        decoration: const pw.BoxDecoration(
                          border: pw.Border(
                            top: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                            right: pw.BorderSide(
                                width: 1.5, color: PdfColors.black),
                          ),
                        ),
                        alignment: pw.Alignment.center,
                        child: pw.Text(
                          '${aplicacoes03 != null ? aplicacoes03.horaFinal : ''}',
                        )),
                    pw.Container(
                      height: 15,
                      width: 60,
                      alignment: pw.Alignment.center,
                      child: pw.Text(
                          " ${aplicacoes03 != null ? aplicacoes03.temperaturaInicial : ''}"),
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 60,
                      alignment: pw.Alignment.center,
                      child: pw.Text(
                          " ${aplicacoes03 != null ? aplicacoes03.temperaturaFinal : ''}"),
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 50,
                      alignment: pw.Alignment.center,
                      child: pw.Text(
                          " ${aplicacoes03 != null ? aplicacoes03.umidadeRelativaArInicial : ''}"),
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 50,
                      alignment: pw.Alignment.center,
                      child: pw.Text(
                          " ${aplicacoes03 != null ? aplicacoes03.umidadeRelativaArFinal : ''}"),
                      decoration: const pw.BoxDecoration(
                        border: pw.Border(
                          right:
                              pw.BorderSide(width: 1.5, color: PdfColors.black),
                        ),
                      ),
                    ),
                    pw.Container(
                      height: 15,
                      width: 67.5,
                      alignment: pw.Alignment.center,
                      decoration: const pw.BoxDecoration(
                          border: pw.Border(
                              right: pw.BorderSide(
                                  width: 1.5, color: PdfColors.black))),
                      child: pw.Text(
                          " ${aplicacoes03 != null ? aplicacoes03.ventoInicial : ''}"),
                    ),
                    pw.Container(
                      height: 15,
                      width: 67.5,
                      alignment: pw.Alignment.center,
                      child: pw.Text(
                          " ${aplicacoes03 != null ? aplicacoes03.ventoFinal : ''}"),
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
                        'PISTA:  ${aplicacao.relatorioAplicacao?.localizacaoPistaCodigoICAO ?? ''} ${double.tryParse(aplicacao.relatorioAplicacao?.lat ?? '')?.toStringAsFixed(4) ?? ''}, ${double.tryParse(aplicacao.relatorioAplicacao?.long ?? '')?.toStringAsFixed(4) ?? ''}',
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
                        'Inicial: ${aplicacoes01 != null ? aplicacoes01.horimetroInicial : ''}',
                        style: pw.TextStyle(fontSize: 12, font: newRoman)),
                  ),
                  pw.Container(
                    height: 25,
                    width: 120,
                    padding: const pw.EdgeInsets.only(left: 2, top: 2),
                    alignment: pw.Alignment.topLeft,
                    child: pw.Text(
                        'Final:  ${aplicacoes03 != null ? aplicacoes03.horimetroFinal : aplicacoes02 != null ? aplicacoes02.horimetroFinal : aplicacoes01 != null ? aplicacoes01.horimetroFinal : ''}',
                        style: pw.TextStyle(fontSize: 12, font: newRoman)),
                  ),
                ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Row(children: [
                  pw.Container(
                    height: 55,
                    width: 200,
                    decoration: const pw.BoxDecoration(
                      border: pw.Border(
                        right:
                            pw.BorderSide(width: 1.5, color: PdfColors.black),
                      ),
                    ),
                    alignment: pw.Alignment.centerLeft,
                    padding: const pw.EdgeInsets.only(left: 2),
                    child: pw.Text(
                        'Observações: ${aplicacao.relatorioAplicacao?.observacoes ?? ''}',
                        style: pw.TextStyle(fontSize: 12, font: newRoman)),
                  ),
                  pw.Container(
                    height: 55,
                    width: 380,
                    padding: const pw.EdgeInsets.only(left: 2),
                    alignment: pw.Alignment.centerLeft,
                    child: pw.Text(
                        'Relatório DGPS (LOG\'s): ${aplicacao.relatorioAplicacao?.relatorioDGPS ?? ''}',
                        style: pw.TextStyle(fontSize: 12, font: newRoman)),
                  )
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
                    margin: const pw.EdgeInsets.symmetric(vertical: 4.0),
                    height: 25,
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
                    child: pw.Column(
                        mainAxisAlignment: pw.MainAxisAlignment.center,
                        crossAxisAlignment: pw.CrossAxisAlignment.center,
                        children: [
                          pw.Padding(
                              padding:
                                  const pw.EdgeInsets.symmetric(horizontal: 5),
                              child: pw.Row(
                                  crossAxisAlignment:
                                      pw.CrossAxisAlignment.center,
                                  mainAxisAlignment:
                                      pw.MainAxisAlignment.spaceBetween,
                                  children: [
                                    pw.Container(
                                        alignment: pw.Alignment.bottomCenter,
                                        child: pw.Text(
                                            'Distância Da Pista: ${aplicacao.contratoPrestacaoServico?.distanciaPista ?? ''} km',
                                            style: pw.TextStyle(
                                                fontSize: 12, font: newRoman))),
                                    pw.Container(
                                      alignment: pw.Alignment.bottomCenter,
                                      child: pw.Text(
                                          'Valor/${aplicacao.contratoPrestacaoServico?.unidadePreco ?? ''}: ${aplicacao.contratoPrestacaoServico?.preco ?? ''}',
                                          style: pw.TextStyle(
                                              fontSize: 12, font: newRoman)),
                                    ),
                                    pw.Container(
                                      alignment: pw.Alignment.bottomCenter,
                                      child: pw.Text(
                                          'Valor Total: ${aplicacao.contratoPrestacaoServico?.valorTotal ?? ''}',
                                          style: pw.TextStyle(
                                              fontSize: 12, font: newRoman)),
                                    ),
                                    pw.Row(children: [
                                      pw.Container(
                                        alignment: pw.Alignment.bottomCenter,
                                        child: pw.Text('Vencimento:',
                                            style: pw.TextStyle(
                                                fontSize: 12, font: newRoman)),
                                      ),
                                      pw.Container(
                                        alignment: pw.Alignment.bottomCenter,
                                        child: pw.Text(vencimentoContrato,
                                            style: pw.TextStyle(
                                                fontWeight: pw.FontWeight.bold,
                                                fontSize: 12,
                                                font: newRoman)),
                                      ),
                                    ])
                                  ])),
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
                  height: 10,
                  margin:
                      const pw.EdgeInsets.only(left: 4.0, right: 4, top: 12),
                  alignment: pw.Alignment.centerRight,
                  child: pw.Text(
                      '${aplicacao.dadosResponsavel?.cidade ?? ''} ${aplicacao.dadosResponsavel?.uf ?? ''}, ${aplicacao.dadosResponsavel != null && dataContratante.isNotEmpty ? "${dataContratante.split("/")[0]} de ${dataContratante.split("/")[1]} de ${dataContratante.split("/")[2]}" : ""}  ',
                      style: pw.TextStyle(fontSize: 11, font: newRoman)),
                ),
                pw.SizedBox(height: 10),
                pw.Padding(
                    padding: const pw.EdgeInsets.symmetric(
                        horizontal: 5, vertical: 1),
                    child: pw.Row(
                        mainAxisAlignment: pw.MainAxisAlignment.spaceBetween,
                        crossAxisAlignment: pw.CrossAxisAlignment.end,
                        children: [
                          pw.Column(
                              crossAxisAlignment: pw.CrossAxisAlignment.start,
                              children: [
                                if (aplicacao.dadosResponsavel
                                        ?.assinaturaResponsavel !=
                                    null)
                                  pw.Container(
                                    height: 22,
                                    width: 100,
                                    child: pw.Image(
                                        pw.MemoryImage(
                                          aplicacao.dadosResponsavel!
                                              .assinaturaResponsavel!,
                                        ),
                                        fit: pw.BoxFit.cover),
                                  ),
                                pw.Text(
                                    aplicacao.dadosResponsavel?.nomeCompleto ??
                                        '',
                                    textAlign: pw.TextAlign.left,
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                                pw.Text(
                                    'DOCUMENTO ${aplicacao.dadosResponsavel?.documento ?? ''}',
                                    textAlign: pw.TextAlign.left,
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                                pw.Text(
                                    'Telefone ${aplicacao.dadosResponsavel?.telefone ?? ''}',
                                    textAlign: pw.TextAlign.left,
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                              ]),
                          pw.Column(
                              crossAxisAlignment: pw.CrossAxisAlignment.start,
                              children: [
                                if (engenheiroSelected?.assinatura != null &&
                                    engenheiroSelected!.assinatura!.isNotEmpty)
                                  pw.Container(
                                    height: 22,
                                    width: 100,
                                    child: pw.Image(
                                        pw.MemoryImage(
                                          base64Decode(
                                              engenheiroSelected.assinatura!),
                                        ),
                                        fit: pw.BoxFit.cover),
                                  ),
                                pw.Text(
                                    engenheiroSelected?.nomeEngenheiro ?? '',
                                    textAlign: pw.TextAlign.left,
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                                pw.Text(
                                    'CREA ${engenheiroSelected?.crea ?? ''}',
                                    textAlign: pw.TextAlign.left,
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                              ]),
                          pw.Column(
                              crossAxisAlignment: pw.CrossAxisAlignment.start,
                              children: [
                                if (pilotoSelected.assinatura != null &&
                                    pilotoSelected.assinatura!.isNotEmpty)
                                  pw.Container(
                                    height: 22,
                                    width: 100,
                                    child: pw.Image(
                                        pw.MemoryImage(
                                          base64Decode(
                                              pilotoSelected.assinatura!),
                                        ),
                                        fit: pw.BoxFit.cover),
                                  ),
                                pw.Text(aplicacao.piloto ?? '',
                                    textAlign: pw.TextAlign.left,
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                                pw.Text('CANAC ${pilotoSelected.cdac ?? ''}',
                                    textAlign: pw.TextAlign.left,
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                              ]),
                          pw.Column(
                              crossAxisAlignment: pw.CrossAxisAlignment.start,
                              children: [
                                if (executorSelected.assinatura != null &&
                                    executorSelected.assinatura!.isNotEmpty)
                                  pw.Container(
                                    height: 22,
                                    width: 100,
                                    child: pw.Image(
                                        pw.MemoryImage(
                                          base64Decode(
                                              executorSelected.assinatura!),
                                        ),
                                        fit: pw.BoxFit.cover),
                                  ),
                                pw.Text(aplicacao.executor ?? '',
                                    textAlign: pw.TextAlign.left,
                                    style: pw.TextStyle(
                                        fontSize: 8, font: newRoman)),
                                pw.Text('CFTA ${executorSelected.cfta ?? ''}',
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
                if (aplicacao.identificacaoAreaTratada?.croquiArea != null)
                  pw.Container(
                    alignment: pw.Alignment.center,
                    margin: const pw.EdgeInsets.all(10),
                    child: pw.Image(
                        pw.MemoryImage(
                            aplicacao.identificacaoAreaTratada!.croquiArea!),
                        fit: pw.BoxFit.fill),
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
                if (aplicacao.caracteristicasProdutoAplicado
                        ?.receiturarioAgronomico !=
                    null)
                  pw.Container(
                    height: 700,
                    width: 700,
                    alignment: pw.Alignment.center,
                    margin: const pw.EdgeInsets.all(10),
                    child: pw.Image(
                      pw.MemoryImage(aplicacao.caracteristicasProdutoAplicado!
                          .receiturarioAgronomico!),
                    ),
                  ),
              ]));
        }));
    if (aplicacoes01 != null) {
      pdf.addPage(pw.Page(
          pageFormat: PdfPageFormat.a4,
          margin: const pw.EdgeInsets.all(10),
          build: (context) {
            return pw.Container(
                decoration: pw.BoxDecoration(
                    border: pw.Border.all(color: PdfColors.black, width: 1.5)),
                child: pw.Column(children: [
                  pw.Text('Condições Climáticas 1',
                      style: pw.TextStyle(
                          fontSize: 16,
                          font: newRomanBold,
                          color: PdfColors.green800,
                          fontWeight: pw.FontWeight.normal)),
                  pw.Divider(height: 1, thickness: 1.5),
                  pw.Row(
                      crossAxisAlignment: pw.CrossAxisAlignment.start,
                      mainAxisAlignment: pw.MainAxisAlignment.start,
                      children: [
                        pw.Padding(
                          padding: const pw.EdgeInsets.only(left: 10, top: 10),
                          child: pw.Text(
                              'Data:  ${aplicacoes01.dataAplicacao != null ? Util.getTodayDate(date: DateTime.fromMillisecondsSinceEpoch(int.tryParse(aplicacoes01.dataAplicacao!)!)) : ''}',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman)),
                        )
                      ]),
                  if (aplicacoes01.imagemCondicaoClimatica != null)
                    pw.Container(
                      alignment: pw.Alignment.center,
                      margin: const pw.EdgeInsets.all(10),
                      child: pw.Image(
                          pw.MemoryImage(aplicacoes01.imagemCondicaoClimatica!),
                          height: 600,
                          width: 560),
                    ),
                ]));
          }));
    }
    if (aplicacoes02 != null) {
      pdf.addPage(pw.Page(
          pageFormat: PdfPageFormat.a4,
          margin: const pw.EdgeInsets.all(10),
          build: (context) {
            return pw.Container(
                decoration: pw.BoxDecoration(
                    border: pw.Border.all(color: PdfColors.black, width: 1.5)),
                child: pw.Column(children: [
                  pw.Text('Condições Climáticas 2',
                      style: pw.TextStyle(
                          fontSize: 16,
                          font: newRomanBold,
                          color: PdfColors.green800,
                          fontWeight: pw.FontWeight.normal)),
                  pw.Divider(height: 1, thickness: 1.5),
                  pw.Row(
                      crossAxisAlignment: pw.CrossAxisAlignment.start,
                      mainAxisAlignment: pw.MainAxisAlignment.start,
                      children: [
                        pw.Padding(
                          padding: const pw.EdgeInsets.only(left: 10, top: 10),
                          child: pw.Text(
                              'Data:  ${aplicacoes02.dataAplicacao != null ? Util.getTodayDate(date: DateTime.fromMillisecondsSinceEpoch(int.tryParse(aplicacoes02.dataAplicacao!)!)) : ''}',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman)),
                        )
                      ]),
                  if (aplicacoes02.imagemCondicaoClimatica != null)
                    pw.Container(
                      alignment: pw.Alignment.center,
                      margin: const pw.EdgeInsets.all(10),
                      child: pw.Image(
                          pw.MemoryImage(aplicacoes02.imagemCondicaoClimatica!),
                          height: 600,
                          width: 560),
                    ),
                ]));
          }));
    }
    if (aplicacoes03 != null) {
      pdf.addPage(pw.Page(
          pageFormat: PdfPageFormat.a4,
          margin: const pw.EdgeInsets.all(10),
          build: (context) {
            return pw.Container(
                decoration: pw.BoxDecoration(
                    border: pw.Border.all(color: PdfColors.black, width: 1.5)),
                child: pw.Column(children: [
                  pw.Text('Condições Climáticas 3',
                      style: pw.TextStyle(
                          fontSize: 16,
                          font: newRomanBold,
                          color: PdfColors.green800,
                          fontWeight: pw.FontWeight.normal)),
                  pw.Divider(height: 1, thickness: 1.5),
                  pw.Row(
                      crossAxisAlignment: pw.CrossAxisAlignment.start,
                      mainAxisAlignment: pw.MainAxisAlignment.start,
                      children: [
                        pw.Padding(
                          padding: const pw.EdgeInsets.only(left: 10, top: 10),
                          child: pw.Text(
                              'Data:  ${aplicacoes03.dataAplicacao != null ? Util.getTodayDate(date: DateTime.fromMillisecondsSinceEpoch(int.tryParse(aplicacoes03.dataAplicacao!)!)) : ''}',
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman)),
                        )
                      ]),
                  if (aplicacoes03.imagemCondicaoClimatica != null)
                    pw.Container(
                      alignment: pw.Alignment.center,
                      margin: const pw.EdgeInsets.all(10),
                      child: pw.Image(
                          pw.MemoryImage(aplicacoes03.imagemCondicaoClimatica!),
                          height: 600,
                          width: 560),
                    ),
                ]));
          }));
    }
    return pdf;
  }

  @override
  Future<Uint8List?> saveDocument({document}) async {
    return await document.save();
  }
}
