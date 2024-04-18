import 'package:flutter/services.dart';

import 'package:pdf/pdf.dart';
import 'package:pdf/widgets.dart' as pw;
import 'package:flytec/core/utils/pdf_generator.dart';

class CreateFirefightingReportService implements PdfGenerator {
  @override
  Future generatePdf({parameters}) async {
    final pdf = pw.Document();

    final logoImage = (await rootBundle.load('assets/images/logo-light.png'))
        .buffer
        .asUint8List();

    pdf.addPage(
      pw.Page(
        pageFormat: PdfPageFormat.a4,
        margin: const pw.EdgeInsets.all(10),
        build: (context) {
          return pw.Container(
              decoration: pw.BoxDecoration(
                  border: pw.Border.all(color: PdfColors.black, width: 1.5)),
              child: pw.Column(children: [
                pw.SizedBox(
                    height: 110,
                    child: pw.Row(
                        mainAxisAlignment: pw.MainAxisAlignment.center,
                        children: [
                          pw.Container(
                            height: 150,
                            width: 150,
                            margin: const pw.EdgeInsets.only(
                                left: 4, top: 4, bottom: 4),
                            child: pw.Image(pw.MemoryImage(logoImage),
                                fit: pw.BoxFit.contain),
                          ),
                          pw.SizedBox(width: 10),
                          pw.Column(
                              crossAxisAlignment: pw.CrossAxisAlignment.start,
                              mainAxisAlignment: pw.MainAxisAlignment.start,
                              children: [
                                pw.SizedBox(height: 5),
                                pw.Container(
                                    padding: pw.EdgeInsets.only(right: 10.0),
                                    height: 25,
                                    width: 420,
                                    child: pw.Text(
                                        'COMBATE A INCÊNCIO EM COBERTURA VEGETAIS PLANILHA DE CONTROLE DE VOOS',
                                        maxLines: 2,
                                        textAlign: pw.TextAlign.center,
                                        style: pw.TextStyle(
                                            fontSize: 16,
                                            fontWeight: pw.FontWeight.bold))),
                                pw.SizedBox(height: 20),
                                pw.Column(
                                    crossAxisAlignment:
                                        pw.CrossAxisAlignment.start,
                                    mainAxisAlignment:
                                        pw.MainAxisAlignment.start,
                                    children: [
                                      pw.SizedBox(
                                          child: pw.Text(
                                              'IMAGEM AVIAÇÃO AGRÍCOLA LTDA.',
                                              style: pw.TextStyle(
                                                  fontSize: 14,
                                                  fontWeight:
                                                      pw.FontWeight.normal))),
                                      pw.Text(
                                          'MA SP - 000000-0 - CNPJ 00.000.000/0000-00 - Inscr. Est. 000.000.000.000',
                                          maxLines: 1,
                                          style: pw.TextStyle(
                                              fontSize: 8,
                                              fontWeight:
                                                  pw.FontWeight.normal)),
                                      pw.Text('FONE: (XX) XXXX-XXXX',
                                          style: pw.TextStyle(
                                              fontSize: 14,
                                              fontWeight: pw.FontWeight.bold)),
                                      pw.Text(
                                          'RUA PARANÁ, 000 - CENTRO - CEP 00000-000 - MONÇÕES - EST.SÃO PAULO',
                                          style: pw.TextStyle(
                                              fontSize: 8,
                                              fontWeight:
                                                  pw.FontWeight.normal)),
                                    ])
                              ]),
                        ])),
                pw.Divider(height: 1, thickness: 1.5),
                pw.SizedBox(
                    height: 75,
                    child: pw.Row(children: [
                      pw.Container(
                          width: 287,
                          decoration: const pw.BoxDecoration(
                              border:
                                  pw.Border(right: pw.BorderSide(width: 1.5))),
                          child: pw.Column(children: [
                            pw.Row(children: [
                              pw.Container(
                                  height: 25,
                                  width: 143.5,
                                  decoration: const pw.BoxDecoration(
                                      border: pw.Border(
                                          right: pw.BorderSide(width: 1.5),
                                          bottom: pw.BorderSide(width: 1.5))),
                                  child: pw.Column(children: [
                                    pw.Text('N Aviso',
                                        textAlign: pw.TextAlign.center,
                                        style: const pw.TextStyle(
                                          fontSize: 12,
                                        )),
                                    pw.Text(''),
                                  ])),
                              pw.Container(
                                  height: 25,
                                  width: 143.5,
                                  decoration: const pw.BoxDecoration(
                                      border: pw.Border(
                                          bottom: pw.BorderSide(width: 1.5))),
                                  child: pw.Column(children: [
                                    pw.Text('HORÍMETRO DE ACIONAMENTO',
                                        textAlign: pw.TextAlign.center,
                                        style: const pw.TextStyle(
                                          fontSize: 8,
                                        )),
                                    pw.Text(''),
                                  ])),
                            ]),
                            pw.Container(
                                height: 25,
                                width: 287,
                                decoration: const pw.BoxDecoration(
                                    border: pw.Border(
                                        right: pw.BorderSide(width: 1.5),
                                        bottom: pw.BorderSide(width: 1.5))),
                                child: pw.Column(children: [
                                  pw.Text('Pista de Operação',
                                      textAlign: pw.TextAlign.center,
                                      style: const pw.TextStyle(
                                        fontSize: 12,
                                      )),
                                  pw.Text(''),
                                ])),
                            pw.Row(children: [
                              pw.Container(
                                  height: 25,
                                  width: 95.6,
                                  decoration: const pw.BoxDecoration(
                                      border: pw.Border(
                                    right: pw.BorderSide(width: 1.5),
                                  )),
                                  child: pw.Column(children: [
                                    pw.Text('Código ICAO',
                                        textAlign: pw.TextAlign.center,
                                        style: const pw.TextStyle(
                                          fontSize: 12,
                                        )),
                                    pw.Text(''),
                                  ])),
                              pw.Container(
                                  height: 25,
                                  width: 95.6,
                                  decoration: const pw.BoxDecoration(
                                      border: pw.Border(
                                    right: pw.BorderSide(width: 1.5),
                                  )),
                                  child: pw.Column(children: [
                                    pw.Text('Nome',
                                        textAlign: pw.TextAlign.center,
                                        style: const pw.TextStyle(
                                          fontSize: 12,
                                        )),
                                    pw.Text(''),
                                  ])),
                              pw.Container(
                                  height: 25,
                                  width: 95.6,
                                  child: pw.Column(children: [
                                    pw.Text('Coordenadas',
                                        textAlign: pw.TextAlign.center,
                                        style: const pw.TextStyle(
                                          fontSize: 12,
                                        )),
                                    pw.Text(''),
                                  ])),
                            ])
                          ])),
                      pw.Container(
                          width: 287,
                          child: pw.Column(children: [
                            pw.Container(
                                height: 25,
                                width: 287,
                                decoration: const pw.BoxDecoration(
                                    border: pw.Border(
                                        bottom: pw.BorderSide(width: 1.5))),
                                child: pw.Column(children: [
                                  pw.Text('Local do Incêndio',
                                      textAlign: pw.TextAlign.center,
                                      style: const pw.TextStyle(
                                        fontSize: 12,
                                      )),
                                  pw.Text(''),
                                ])),
                            pw.Container(
                                height: 50,
                                child: pw.Row(children: [
                                  pw.Container(
                                      width: 143.5,
                                      decoration: const pw.BoxDecoration(
                                          border: pw.Border(
                                              right:
                                                  pw.BorderSide(width: 1.5))),
                                      child: pw.Column(
                                          mainAxisAlignment:
                                              pw.MainAxisAlignment.spaceEvenly,
                                          children: [
                                            pw.Text('Referência',
                                                textAlign: pw.TextAlign.center,
                                                style: const pw.TextStyle(
                                                  fontSize: 12,
                                                )),
                                            pw.Text(
                                              '',
                                              textAlign: pw.TextAlign.center,
                                            ),
                                          ])),
                                  pw.Container(
                                      width: 143.5,
                                      child: pw.Column(
                                          mainAxisAlignment:
                                              pw.MainAxisAlignment.spaceEvenly,
                                          children: [
                                            pw.Text('Coordenadas',
                                                textAlign: pw.TextAlign.center,
                                                style: const pw.TextStyle(
                                                  fontSize: 12,
                                                )),
                                            pw.Text(
                                              '',
                                              textAlign: pw.TextAlign.center,
                                            ),
                                          ])),
                                ]))
                          ])),
                    ])),
                pw.Divider(height: 1, thickness: 1.5),
                pw.SizedBox(
                    height: 25,
                    child: pw.Row(children: [
                      pw.Container(
                          height: 25,
                          width: 191.3,
                          decoration: const pw.BoxDecoration(
                              border:
                                  pw.Border(right: pw.BorderSide(width: 1.5))),
                          child: pw.Column(children: [
                            pw.Text('Horário de chegada na pista',
                                textAlign: pw.TextAlign.center,
                                style: const pw.TextStyle(
                                  fontSize: 12,
                                )),
                            pw.Text(''),
                          ])),
                      pw.Container(
                          height: 25,
                          width: 191.3,
                          decoration: const pw.BoxDecoration(
                              border:
                                  pw.Border(right: pw.BorderSide(width: 1.5))),
                          child: pw.Column(children: [
                            pw.Text('Horímetro de chegada na pista',
                                textAlign: pw.TextAlign.center,
                                style: const pw.TextStyle(
                                  fontSize: 12,
                                )),
                            pw.Text(''),
                          ])),
                      pw.Container(
                          height: 25,
                          width: 191.3,
                          child: pw.Column(children: [
                            pw.Text('Prefixo da Aeronave',
                                textAlign: pw.TextAlign.center,
                                style: const pw.TextStyle(
                                  fontSize: 12,
                                )),
                            pw.Text(''),
                          ])),
                    ])),
                pw.Divider(height: 1, thickness: 1.5),
                pw.Row(children: [
                  pw.Column(children: [
                    pw.Container(
                        height: 15,
                        width: 287,
                        alignment: pw.Alignment.center,
                        decoration: const pw.BoxDecoration(
                            border: pw.Border(
                                right: pw.BorderSide(width: 1.5),
                                bottom: pw.BorderSide(width: 1.5))),
                        child: pw.Text('Decolagem',
                            textAlign: pw.TextAlign.center)),
                    pw.Row(children: [
                      pw.Container(
                          height: 15,
                          width: 143.5,
                          alignment: pw.Alignment.center,
                          decoration: const pw.BoxDecoration(
                              border: pw.Border(
                                  right: pw.BorderSide(width: 1.5),
                                  bottom: pw.BorderSide(width: 1.5))),
                          child: pw.Text('Horário',
                              textAlign: pw.TextAlign.center)),
                      pw.Container(
                          height: 15,
                          width: 143.5,
                          alignment: pw.Alignment.center,
                          decoration: const pw.BoxDecoration(
                              border: pw.Border(
                                  right: pw.BorderSide(width: 1.5),
                                  bottom: pw.BorderSide(width: 1.5))),
                          child: pw.Text('Horímetro',
                              textAlign: pw.TextAlign.center)),
                    ]),
                    pw.Container(
                        width: 287,
                        height: 320,
                        decoration: const pw.BoxDecoration(
                            border:
                                pw.Border(right: pw.BorderSide(width: 1.5))),
                        child: pw.ListView.separated(
                          itemBuilder: (context, index) => pw.SizedBox(
                              width: 287,
                              height: 15,
                              child: pw.Row(children: [
                                pw.Container(
                                    width: 143.5,
                                    alignment: pw.Alignment.centerLeft,
                                    decoration: const pw.BoxDecoration(
                                        border: pw.Border(
                                            right: pw.BorderSide(width: 1.5))),
                                    child: pw.Text('${index + 1} ',
                                        textAlign: pw.TextAlign.center)),
                                pw.SizedBox(
                                    width: 143.5,
                                    child: pw.Text('',
                                        textAlign: pw.TextAlign.center)),
                              ])),
                          itemCount: 20,
                          separatorBuilder: (context, index) =>
                              pw.Divider(height: 1, thickness: 1.5),
                        )),
                  ]),
                  pw.Column(children: [
                    pw.Container(
                        height: 15,
                        width: 287,
                        alignment: pw.Alignment.center,
                        decoration: const pw.BoxDecoration(
                            border: pw.Border(
                                right: pw.BorderSide(width: 1.5),
                                bottom: pw.BorderSide(width: 1.5))),
                        child:
                            pw.Text('Pouso', textAlign: pw.TextAlign.center)),
                    pw.Row(children: [
                      pw.Container(
                          height: 15,
                          width: 143.5,
                          alignment: pw.Alignment.center,
                          decoration: const pw.BoxDecoration(
                              border: pw.Border(
                                  right: pw.BorderSide(width: 1.5),
                                  bottom: pw.BorderSide(width: 1.5))),
                          child: pw.Text('Horário',
                              textAlign: pw.TextAlign.center)),
                      pw.Container(
                          height: 15,
                          width: 143.5,
                          alignment: pw.Alignment.center,
                          decoration: const pw.BoxDecoration(
                              border: pw.Border(
                                  right: pw.BorderSide(width: 1.5),
                                  bottom: pw.BorderSide(width: 1.5))),
                          child: pw.Text('Horímetro',
                              textAlign: pw.TextAlign.center)),
                    ]),
                    pw.SizedBox(
                        width: 287,
                        height: 320,
                        child: pw.ListView.separated(
                          itemBuilder: (context, index) => pw.SizedBox(
                              width: 287,
                              height: 15,
                              child: pw.Row(children: [
                                pw.Container(
                                    width: 143.5,
                                    alignment: pw.Alignment.centerLeft,
                                    decoration: const pw.BoxDecoration(
                                        border: pw.Border(
                                            right: pw.BorderSide(width: 1.5))),
                                    child: pw.Text('${index + 1}',
                                        textAlign: pw.TextAlign.center)),
                                pw.SizedBox(
                                    width: 143.5,
                                    child: pw.Text('',
                                        textAlign: pw.TextAlign.center)),
                              ])),
                          itemCount: 20,
                          separatorBuilder: (context, index) =>
                              pw.Divider(height: 1, thickness: 1.5),
                        ))
                  ])
                ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.SizedBox(width: 574, height: 35),
                pw.Divider(height: 1, thickness: 1.5),
                pw.SizedBox(
                    width: 574,
                    height: 20,
                    child: pw.Row(children: [
                      pw.Container(
                          width: 191.3,
                          decoration: const pw.BoxDecoration(
                              border:
                                  pw.Border(right: pw.BorderSide(width: 1.5))),
                          child: pw.Column(children: [
                            pw.Container(
                              width: 191.3,
                              height: 20,
                              child: pw.Text('Horário de Término:',
                                  style: const pw.TextStyle(fontSize: 10)),
                            ),
                            pw.Container(
                              width: 191.3,
                              height: 20,
                              child: pw.Text('Horário de Corte:',
                                  style: const pw.TextStyle(fontSize: 10)),
                            )
                          ])),
                      pw.Container(
                          width: 191.3,
                          decoration: const pw.BoxDecoration(
                              border:
                                  pw.Border(right: pw.BorderSide(width: 1.5))),
                          child: pw.Column(children: [
                            pw.Container(
                              width: 191.3,
                              height: 20,
                              child: pw.Text('Horímetro de Término:',
                                  style: const pw.TextStyle(fontSize: 10)),
                            ),
                            pw.Container(
                              width: 191.3,
                              height: 20,
                              child: pw.Text('Horímetro de Corte:',
                                  style: const pw.TextStyle(fontSize: 10)),
                            )
                          ])),
                      pw.Container(
                        width: 191.3,
                        decoration: const pw.BoxDecoration(
                            border:
                                pw.Border(right: pw.BorderSide(width: 1.5))),
                        child: pw.Text('N de lançamentos:',
                            style: const pw.TextStyle(fontSize: 10)),
                      )
                    ])),
                pw.Divider(height: 1, thickness: 1.5),
                pw.SizedBox(
                    width: 574,
                    child: pw.Row(children: [
                      pw.Container(
                        width: 287,
                        height: 20,
                        child: pw.Text('Capacidade de Carga da Aeronave:',
                            style: const pw.TextStyle(fontSize: 10)),
                        decoration: const pw.BoxDecoration(
                            border: pw.Border(
                                bottom: pw.BorderSide(width: 1.5),
                                right: pw.BorderSide(width: 1.5))),
                      ),
                      pw.Container(
                          width: 287,
                          height: 20,
                          child: pw.Text('Capacidade de Carga da Aeronave:',
                              style: const pw.TextStyle(fontSize: 10)),
                          decoration: const pw.BoxDecoration(
                              border: pw.Border(
                                  bottom: pw.BorderSide(width: 1.5)))),
                    ])),
                pw.SizedBox(
                    width: 574,
                    child: pw.Row(children: [
                      pw.Container(
                        width: 287,
                        height: 20,
                        child: pw.Text('Coordenador da Base Operacional',
                            textAlign: pw.TextAlign.start,
                            style: const pw.TextStyle(fontSize: 10)),
                        decoration: const pw.BoxDecoration(
                            border: pw.Border(
                                bottom: pw.BorderSide(width: 1.5),
                                right: pw.BorderSide(width: 1.5))),
                      ),
                      pw.Container(
                          width: 287,
                          height: 20,
                          child: pw.Text('Comandante da Ocorrência',
                              textAlign: pw.TextAlign.start,
                              style: const pw.TextStyle(fontSize: 10)),
                          decoration: const pw.BoxDecoration(
                              border: pw.Border(
                                  bottom: pw.BorderSide(width: 1.5)))),
                    ])),
                pw.SizedBox(
                    width: 574,
                    child: pw.Row(children: [
                      pw.Container(
                        width: 287,
                        height: 20,
                        child: pw.Text('Nome:',
                            style: const pw.TextStyle(fontSize: 10)),
                        decoration: const pw.BoxDecoration(
                            border: pw.Border(
                                bottom: pw.BorderSide(width: 1.5),
                                right: pw.BorderSide(width: 1.5))),
                      ),
                      pw.Container(
                          width: 287,
                          height: 20,
                          child: pw.Text('Nome:',
                              style: const pw.TextStyle(fontSize: 10)),
                          decoration: const pw.BoxDecoration(
                              border: pw.Border(
                                  bottom: pw.BorderSide(width: 1.5)))),
                    ])),
                pw.SizedBox(
                    width: 574,
                    height: 20,
                    child: pw.Row(children: [
                      pw.Row(children: [
                        pw.Container(
                          width: 143.5,
                          height: 20,
                          child: pw.Text('Posto/Grad:',
                              style: const pw.TextStyle(fontSize: 10)),
                          decoration: const pw.BoxDecoration(
                              border: pw.Border(
                                  bottom: pw.BorderSide(width: 1.5),
                                  right: pw.BorderSide(width: 1.5))),
                        ),
                        pw.Container(
                          width: 143.5,
                          height: 20,
                          child: pw.Text('RE:',
                              style: const pw.TextStyle(fontSize: 10)),
                          decoration: const pw.BoxDecoration(
                              border: pw.Border(
                                  bottom: pw.BorderSide(width: 1.5),
                                  right: pw.BorderSide(width: 1.5))),
                        ),
                      ]),
                      pw.Row(children: [
                        pw.Container(
                          width: 143.5,
                          height: 20,
                          child: pw.Text('Posto/Grad:',
                              style: const pw.TextStyle(fontSize: 10)),
                          decoration: const pw.BoxDecoration(
                              border: pw.Border(
                                  bottom: pw.BorderSide(width: 1.5),
                                  right: pw.BorderSide(width: 1.5))),
                        ),
                        pw.Container(
                          width: 143.5,
                          height: 20,
                          child: pw.Text('RE:',
                              style: const pw.TextStyle(fontSize: 10)),
                          decoration: const pw.BoxDecoration(
                              border: pw.Border(
                                  bottom: pw.BorderSide(width: 1.5),
                                  right: pw.BorderSide(width: 1.5))),
                        ),
                      ]),
                    ])),
                pw.SizedBox(
                    width: 574,
                    child: pw.Row(children: [
                      pw.Container(
                        width: 287,
                        height: 30,
                        child: pw.Text('Assinatura:',
                            style: const pw.TextStyle(fontSize: 10)),
                        decoration: const pw.BoxDecoration(
                            border: pw.Border(
                                bottom: pw.BorderSide(width: 1.5),
                                right: pw.BorderSide(width: 1.5))),
                      ),
                      pw.Container(
                          width: 287,
                          height: 30,
                          child: pw.Text('Assinatura:',
                              style: const pw.TextStyle(fontSize: 10)),
                          decoration: const pw.BoxDecoration(
                              border: pw.Border(
                                  bottom: pw.BorderSide(width: 1.5)))),
                    ])),
                pw.SizedBox(width: 574, height: 40),
                pw.Divider(height: 1, thickness: 1.5),
                pw.SizedBox(
                    width: 574,
                    child: pw.Row(
                        mainAxisAlignment: pw.MainAxisAlignment.center,
                        crossAxisAlignment: pw.CrossAxisAlignment.center,
                        children: [
                          pw.Container(
                            width: 191.3,
                            child: pw.Column(
                              mainAxisAlignment: pw.MainAxisAlignment.center,
                              crossAxisAlignment: pw.CrossAxisAlignment.start,
                              children: [
                                pw.Text(" "),
                                pw.Divider(height: 0.5, thickness: 1.0),
                                pw.Text('Executor',
                                    textAlign: pw.TextAlign.left,
                                    style: const pw.TextStyle(fontSize: 10)),
                                pw.Text('CFTA ',
                                    textAlign: pw.TextAlign.left,
                                    style: const pw.TextStyle(fontSize: 10)),
                              ],
                            ),
                          ),
                          pw.Container(
                            width: 191.3,
                            child: pw.Column(
                              mainAxisAlignment: pw.MainAxisAlignment.center,
                              crossAxisAlignment: pw.CrossAxisAlignment.start,
                              children: [
                                pw.Text(" "),
                                pw.Divider(height: 0.5, thickness: 1.0),
                                pw.Text('Executor',
                                    textAlign: pw.TextAlign.left,
                                    style: const pw.TextStyle(fontSize: 10)),
                                pw.Text('CFTA ',
                                    textAlign: pw.TextAlign.left,
                                    style: const pw.TextStyle(fontSize: 10)),
                              ],
                            ),
                          ),
                          pw.Container(
                            width: 191.3,
                            child: pw.Column(
                              mainAxisAlignment: pw.MainAxisAlignment.center,
                              crossAxisAlignment: pw.CrossAxisAlignment.start,
                              children: [
                                pw.Text(" "),
                                pw.Divider(height: 0.5, thickness: 1.0),
                                pw.Text('Executor',
                                    textAlign: pw.TextAlign.left,
                                    style: const pw.TextStyle(fontSize: 10)),
                                pw.Text('CFTA ',
                                    textAlign: pw.TextAlign.left,
                                    style: const pw.TextStyle(fontSize: 10)),
                              ],
                            ),
                          ),
                        ])),
              ]));
        },
      ),
    );
    return pdf;
  }

  @override
  Future<Uint8List?> saveDocument({document}) async {
    return await document.save();
  }
}
