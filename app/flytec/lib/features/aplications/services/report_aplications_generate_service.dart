import 'dart:typed_data';
import 'package:intl/intl.dart';
import 'package:pdf/pdf.dart';
import 'package:pdf/widgets.dart' as pw;
import 'package:flytec/core/utils/pdf_generator.dart';

class ReportAplicationsGenerate implements PdfGenerator {
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
                                    pw.Text('______________ '),
                                    pw.Text(' Data '),
                                    pw.Text(
                                        DateFormat('dd/MM/yyyy')
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
                          child: pw.Text('Nome',
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
                                  child: pw.Text('CNPJ/CPF ',
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
                          child: pw.Text('Endereço',
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
                        child: pw.Padding(
                          padding: const pw.EdgeInsets.only(left: 10, top: 2),
                          child: pw.Text('Localização',
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
                            child: pw.Text('Cultura ',
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
                                  child: pw.Text('Extensão (Ha) ',
                                      style: pw.TextStyle(
                                          fontSize: 12, font: newRoman)),
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
                        width: 80,
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
                          child: pw.Text('Veiculante',
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
                          child: pw.Text('Quant. Veic.',
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
                          child: pw.Text('Vazão L/Ha',
                              textAlign: pw.TextAlign.left,
                              style:
                                  pw.TextStyle(fontSize: 12, font: newRoman))),
                      pw.Container(
                          width: 137,
                          height: 28,
                          padding: const pw.EdgeInsets.only(top: 2),
                          alignment: pw.Alignment.topCenter,
                          child: pw.Text('Largura Faixa',
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
                          child: pw.Text('Aeronave',
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
                          child: pw.Text('Altura Vôo',
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
                          child: pw.Text('Temp. C°',
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
                          child: pw.Text('U.R% do Ar',
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
                          child: pw.Text('Veloc. Vento',
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
                                pw.Text(
                                    '_____________________________________________________________',
                                    textAlign: pw.TextAlign.left,
                                    style: pw.TextStyle(
                                        fontSize: 10, font: newRoman)),
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
                    child: pw.Text('Localização da Pista',
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
                    child: pw.Text('Inicial:',
                        style: pw.TextStyle(fontSize: 12, font: newRoman)),
                  ),
                  pw.Container(
                    height: 25,
                    width: 120,
                    padding: const pw.EdgeInsets.only(left: 2, top: 2),
                    alignment: pw.Alignment.topLeft,
                    child: pw.Text('Final:',
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
                    child: pw.Text('Alterações do Planejamento:',
                        style: pw.TextStyle(fontSize: 12, font: newRoman)),
                  ),
                  pw.Container(
                    height: 28,
                    width: 240,
                    padding: const pw.EdgeInsets.only(left: 2),
                    alignment: pw.Alignment.centerLeft,
                    child: pw.Text('Emitiu relatório do DGPS',
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
                                      'Preço Ha: ____________________',
                                      style: pw.TextStyle(
                                          fontSize: 12, font: newRoman)),
                                ),
                                pw.Container(
                                  height: 20,
                                  alignment: pw.Alignment.bottomCenter,
                                  child: pw.Text(
                                      'Valor Total: _________________',
                                      style: pw.TextStyle(
                                          fontSize: 12, font: newRoman)),
                                ),
                                pw.Container(
                                  height: 20,
                                  alignment: pw.Alignment.bottomCenter,
                                  child: pw.Text('Vencimento: ____/____/____',
                                      style: pw.TextStyle(
                                          fontSize: 12, font: newRoman)),
                                ),
                              ]))
                    ])),
                pw.Container(
                  height: 15,
                  margin:
                      const pw.EdgeInsets.only(left: 4.0, bottom: 4, right: 4),
                  alignment: pw.Alignment.center,
                  child: pw.Text(
                      '         O contratante declara estar plenamente de acordo com os serviços executados, área, valor e forma de pagamento expressa neste contrato, tendo o mesmo valor como comprovante de entrega dos serviços efetudos',
                      style: pw.TextStyle(fontSize: 10, font: newRoman)),
                ),
                pw.Container(
                  height: 15,
                  margin: const pw.EdgeInsets.only(left: 4.0, top: 4, right: 4),
                  alignment: pw.Alignment.center,
                  child: pw.Text(
                      '         E, por estarem de acordo com todas as cláusulas, itens e demais condições estabelecidas neste contrato, inclusive as constantes no verso, as partes firmam o presente, tendo valor como testemunhas as assinaturas do Piloto e Eng. Agrônomo.',
                      style: pw.TextStyle(fontSize: 10, font: newRoman)),
                ),
                pw.Container(
                  height: 15,
                  margin:
                      const pw.EdgeInsets.only(left: 4.0, right: 4, top: 12),
                  alignment: pw.Alignment.centerRight,
                  child: pw.Text(
                      '__________________________________,_________ de ______________ de ______',
                      style: pw.TextStyle(fontSize: 10, font: newRoman)),
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
                                pw.Text('RG',
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
