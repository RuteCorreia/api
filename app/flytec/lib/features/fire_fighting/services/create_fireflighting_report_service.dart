import 'package:flutter/services.dart';

import 'package:pdf/pdf.dart';
import 'package:pdf/widgets.dart' as pw;
import 'package:flytec/core/utils/pdf_generator.dart';

class CreateFireflightingReportService implements PdfGenerator {
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
                    height: 100,
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
                          pw.SizedBox(
                            width: 430,
                            child: pw.Text(
                                'COMBATE A INCÊNCIO EM COBERTURA VEGETAIS\nPLANILHA DE CONTROLE DE VOOS',
                                textAlign: pw.TextAlign.center,
                                style: pw.TextStyle(
                                    fontSize: 16,
                                    fontWeight: pw.FontWeight.bold)),
                          )
                        ])),
                pw.Divider(height: 1, thickness: 1.5),
                pw.SizedBox(
                    height: 100,
                    child: pw.Row(children: [
                      pw.Container(
                          width: 287,
                          decoration: const pw.BoxDecoration(
                              border:
                                  pw.Border(right: pw.BorderSide(width: 1.5))),
                          child: pw.Column(children: [])),
                      pw.Container(
                          width: 287,
                          child: pw.Column(children: [
                            pw.Container(
                                height: 30,
                                width: 287,
                                decoration: const pw.BoxDecoration(
                                    border: pw.Border(
                                        bottom: pw.BorderSide(width: 1.5))),
                                child: pw.Column(children: [
                                  pw.Text('Local do Incêndio',
                                      textAlign: pw.TextAlign.center,
                                      style: pw.TextStyle(
                                          fontSize: 12,
                                          fontWeight: pw.FontWeight.bold)),
                                  pw.Text(''),
                                ])),
                            pw.Container(
                                height: 70,
                                child: pw.Row(children: [
                                  pw.Container(
                                      width: 143.5,
                                      decoration: const pw.BoxDecoration(
                                          border: pw.Border(
                                              right:
                                                  pw.BorderSide(width: 1.5))),
                                      padding: const pw.EdgeInsets.only(
                                          top: 4, bottom: 4),
                                      child: pw.Column(
                                          mainAxisAlignment:
                                              pw.MainAxisAlignment.spaceEvenly,
                                          children: [
                                            pw.Text('Referência',
                                                textAlign: pw.TextAlign.center,
                                                style: pw.TextStyle(
                                                    fontSize: 12,
                                                    fontWeight:
                                                        pw.FontWeight.bold)),
                                            pw.Text(
                                              '',
                                              textAlign: pw.TextAlign.center,
                                            ),
                                          ])),
                                  pw.Container(
                                      width: 143.5,
                                      padding: const pw.EdgeInsets.only(
                                          top: 4, bottom: 4),
                                      child: pw.Column(
                                          mainAxisAlignment:
                                              pw.MainAxisAlignment.spaceEvenly,
                                          children: [
                                            pw.Text('Coordenadas',
                                                textAlign: pw.TextAlign.center,
                                                style: pw.TextStyle(
                                                    fontSize: 12,
                                                    fontWeight:
                                                        pw.FontWeight.bold)),
                                            pw.Text(
                                              'Latitude: \nLongitude: ',
                                              textAlign: pw.TextAlign.center,
                                            ),
                                          ])),
                                ]))
                          ])),
                    ])),
                pw.Divider(height: 1, thickness: 1.5),
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
