import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flytec/core/extensions/time_of_day_extension.dart';
import 'package:flytec/features/fire_fighting/models/firefighting.dart';

import 'package:pdf/pdf.dart';
import 'package:pdf/widgets.dart' as pw;
import 'package:flytec/core/utils/pdf_generator.dart';

class CreateFirefightingReportService implements PdfGenerator {
  final Firefighting _firefighting;
  CreateFirefightingReportService(this._firefighting);

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
                                    padding:
                                        const pw.EdgeInsets.only(right: 10.0),
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
                                  child: pw.Column(
                                      mainAxisAlignment:
                                          pw.MainAxisAlignment.center,
                                      children: [
                                        pw.Text('N Aviso',
                                            textAlign: pw.TextAlign.center,
                                            style: const pw.TextStyle(
                                              fontSize: 10,
                                            )),
                                        pw.Text(
                                            _firefighting.numeroAviso
                                                    ?.toString() ??
                                                '',
                                            textAlign: pw.TextAlign.center,
                                            style: const pw.TextStyle(
                                              fontSize: 10,
                                            )),
                                      ])),
                              pw.Container(
                                  height: 25,
                                  width: 143.5,
                                  decoration: const pw.BoxDecoration(
                                      border: pw.Border(
                                          bottom: pw.BorderSide(width: 1.5))),
                                  child: pw.Column(
                                      mainAxisAlignment:
                                          pw.MainAxisAlignment.center,
                                      children: [
                                        pw.Text('HORÍMETRO DE ACIONAMENTO',
                                            textAlign: pw.TextAlign.center,
                                            style: const pw.TextStyle(
                                              fontSize: 8,
                                            )),
                                        pw.Text(_firefighting
                                                .horimetroAcionamento ??
                                            ''),
                                      ])),
                            ]),
                            pw.Container(
                                height: 15,
                                width: 287,
                                decoration: const pw.BoxDecoration(
                                    border: pw.Border(
                                        right: pw.BorderSide(width: 1.5),
                                        bottom: pw.BorderSide(width: 1.5))),
                                child: pw.Text('Pista de Operação',
                                    textAlign: pw.TextAlign.center,
                                    style: const pw.TextStyle(
                                      fontSize: 10,
                                    ))),
                            pw.Row(children: [
                              pw.Container(
                                  height: 35,
                                  width: 95.6,
                                  decoration: const pw.BoxDecoration(
                                      border: pw.Border(
                                    right: pw.BorderSide(width: 1.5),
                                  )),
                                  child: pw.Column(
                                      mainAxisAlignment:
                                          pw.MainAxisAlignment.center,
                                      children: [
                                        pw.Text('Código ICAO',
                                            textAlign: pw.TextAlign.center,
                                            style: const pw.TextStyle(
                                              fontSize: 10,
                                            )),
                                        pw.Text(
                                            _firefighting
                                                    .pista?.codigoICAOPista ??
                                                '',
                                            textAlign: pw.TextAlign.center,
                                            style: const pw.TextStyle(
                                              fontSize: 12,
                                            )),
                                      ])),
                              pw.Container(
                                  height: 35,
                                  width: 90.6,
                                  decoration: const pw.BoxDecoration(
                                      border: pw.Border(
                                    right: pw.BorderSide(width: 1.5),
                                  )),
                                  child: pw.Column(
                                      crossAxisAlignment:
                                          pw.CrossAxisAlignment.center,
                                      mainAxisAlignment:
                                          pw.MainAxisAlignment.center,
                                      children: [
                                        pw.Text('Nome',
                                            textAlign: pw.TextAlign.center,
                                            style: const pw.TextStyle(
                                              fontSize: 10,
                                            )),
                                        pw.Text(
                                            _firefighting.pista?.nomePista ??
                                                '',
                                            textAlign: pw.TextAlign.center,
                                            style: const pw.TextStyle(
                                              fontSize: 10,
                                            )),
                                      ])),
                              pw.Container(
                                  height: 35,
                                  width: 100.6,
                                  child: pw.Column(
                                      mainAxisAlignment:
                                          pw.MainAxisAlignment.center,
                                      children: [
                                        pw.Text('Coordenadas',
                                            textAlign: pw.TextAlign.center,
                                            style: const pw.TextStyle(
                                              fontSize: 10,
                                            )),
                                        pw.Text(
                                            'Latitude: ${_firefighting.pista?.latPista ?? ''}\nLongitude: ${_firefighting.pista?.longPista ?? ''}',
                                            textAlign: pw.TextAlign.center,
                                            style: const pw.TextStyle(
                                              fontSize: 10,
                                            )),
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
                                child: pw.Text('Local do Incêndio',
                                    textAlign: pw.TextAlign.center,
                                    style: const pw.TextStyle(
                                      fontSize: 12,
                                    ))),
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
                                            pw.Text(_firefighting.localIncendio
                                                    ?.referencia ??
                                                ''),
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
                                                'Latitude: ${_firefighting.localIncendio?.lat ?? ''}\nLongitude: ${_firefighting.localIncendio?.long ?? ''}'),
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
                                  fontSize: 10,
                                )),
                            pw.Text(
                                _firefighting.pista != null &&
                                        _firefighting
                                                .pista!.horarioChegadaPista !=
                                            null
                                    ? TimeOfDay.fromDateTime(
                                            DateTime.fromMillisecondsSinceEpoch(
                                                _firefighting.pista!
                                                    .horarioChegadaPista!))
                                        .to24hours()
                                        .toString()
                                    : '',
                                textAlign: pw.TextAlign.center,
                                style: const pw.TextStyle(
                                  fontSize: 10,
                                )),
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
                                  fontSize: 10,
                                )),
                            pw.Text(
                                _firefighting.pista?.horimetroChegadaPista ??
                                    '',
                                textAlign: pw.TextAlign.center,
                                style: const pw.TextStyle(
                                  fontSize: 10,
                                )),
                          ])),
                      pw.Container(
                          height: 25,
                          width: 191.3,
                          child: pw.Column(children: [
                            pw.Text('Prefixo da Aeronave',
                                textAlign: pw.TextAlign.center,
                                style: const pw.TextStyle(
                                  fontSize: 10,
                                )),
                            pw.Text(_firefighting.prefixoAeronave ?? '',
                                textAlign: pw.TextAlign.center,
                                style: const pw.TextStyle(
                                  fontSize: 10,
                                )),
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
                                    child: pw.Text(
                                        '${index + 1} ${_firefighting.decolagemPousoFirefightingList != null && _firefighting.decolagemPousoFirefightingList!.isNotEmpty && index < _firefighting.decolagemPousoFirefightingList!.length ? _firefighting.decolagemPousoFirefightingList![index]?.horarioDecolagem != null ? TimeOfDay.fromDateTime(DateTime.fromMillisecondsSinceEpoch(_firefighting.decolagemPousoFirefightingList![index]!.horarioDecolagem!)).to24hours().toString() : '' : ''}',
                                        textAlign: pw.TextAlign.center)),
                                pw.SizedBox(
                                    width: 143.5,
                                    child: pw.Text(
                                        '${_firefighting.decolagemPousoFirefightingList != null && _firefighting.decolagemPousoFirefightingList!.isNotEmpty && index < _firefighting.decolagemPousoFirefightingList!.length ? _firefighting.decolagemPousoFirefightingList![index]?.horimetroDecolagem : ''}',
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
                                    child: pw.Text(
                                        '${index + 1} ${_firefighting.decolagemPousoFirefightingList != null && _firefighting.decolagemPousoFirefightingList!.isNotEmpty && index < _firefighting.decolagemPousoFirefightingList!.length ? _firefighting.decolagemPousoFirefightingList![index]?.horarioPouso != null ? TimeOfDay.fromDateTime(DateTime.fromMillisecondsSinceEpoch(_firefighting.decolagemPousoFirefightingList![index]!.horarioPouso!)).to24hours().toString() : '' : ''}',
                                        textAlign: pw.TextAlign.center)),
                                pw.SizedBox(
                                    width: 143.5,
                                    child: pw.Text(
                                        '${_firefighting.decolagemPousoFirefightingList != null && _firefighting.decolagemPousoFirefightingList!.isNotEmpty && index < _firefighting.decolagemPousoFirefightingList!.length ? _firefighting.decolagemPousoFirefightingList![index]?.horimetroPouso : ''}',
                                        textAlign: pw.TextAlign.center)),
                              ])),
                          itemCount: 20,
                          separatorBuilder: (context, index) =>
                              pw.Divider(height: 1, thickness: 1.5),
                        ))
                  ])
                ]),
                pw.Divider(height: 1, thickness: 1.5),
                pw.SizedBox(
                    width: 574,
                    height: 35,
                    child: pw.Text(_firefighting.observacao ?? '',
                        textAlign: pw.TextAlign.center)),
                pw.Divider(height: 1, thickness: 1.5),
                pw.SizedBox(
                    width: 574,
                    height: 30,
                    child: pw.Row(children: [
                      pw.Container(
                          width: 191.3,
                          decoration: const pw.BoxDecoration(
                              border:
                                  pw.Border(right: pw.BorderSide(width: 1.5))),
                          child: pw.Column(children: [
                            pw.Container(
                              width: 191.3,
                              height: 15,
                              child: pw.Text(
                                  'Horário de Término: ${_firefighting.horarioFinalOperacao != null ? TimeOfDay.fromDateTime(DateTime.fromMillisecondsSinceEpoch(_firefighting.horarioFinalOperacao!)).to24hours().toString() : ''}',
                                  style: const pw.TextStyle(fontSize: 10)),
                            ),
                            pw.Container(
                              width: 191.3,
                              height: 15,
                              child: pw.Text(
                                  'Horário de Corte: ${_firefighting.horarioCorte != null ? TimeOfDay.fromDateTime(DateTime.fromMillisecondsSinceEpoch(_firefighting.horarioCorte!)).to24hours().toString() : ''}',
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
                              height: 15,
                              child: pw.Text(
                                  'Horímetro de Término: ${_firefighting.horimetroFinalOperacao}',
                                  style: const pw.TextStyle(fontSize: 10)),
                            ),
                            pw.Container(
                              width: 191.3,
                              height: 15,
                              child: pw.Text(
                                  'Horímetro de Corte:  ${_firefighting.horimetroCorte}',
                                  style: const pw.TextStyle(fontSize: 10)),
                            )
                          ])),
                      pw.Container(
                        width: 191.3,
                        decoration: const pw.BoxDecoration(
                            border:
                                pw.Border(right: pw.BorderSide(width: 1.5))),
                        child: pw.Text(
                            'N de lançamentos: ${_firefighting.decolagemPousoFirefightingList != null && _firefighting.decolagemPousoFirefightingList!.isNotEmpty ? _firefighting.decolagemPousoFirefightingList!.length : '0'}',
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
                        child: pw.Text(
                            'Capacidade de Carga da Aeronave: ${_firefighting.capacidadeCargaAeronave}',
                            style: const pw.TextStyle(fontSize: 10)),
                        decoration: const pw.BoxDecoration(
                            border: pw.Border(
                                bottom: pw.BorderSide(width: 1.5),
                                right: pw.BorderSide(width: 1.5))),
                      ),
                      pw.Container(
                          width: 287,
                          height: 20,
                          child: pw.Text(
                              'Total de água utilizada (capacidade x n° lançamentos): ${(_firefighting.totalAguaUtilizadaOperacao != null ? double.tryParse(_firefighting.totalAguaUtilizadaOperacao!) : 0) ?? 0 * (_firefighting.decolagemPousoFirefightingList != null ? _firefighting.decolagemPousoFirefightingList!.length : 0)}',
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
                        height: 15,
                        child: pw.Text('Coordenador da Base Operacional',
                            textAlign: pw.TextAlign.center,
                            style: const pw.TextStyle(fontSize: 10)),
                        decoration: const pw.BoxDecoration(
                            border: pw.Border(
                                bottom: pw.BorderSide(width: 1.5),
                                right: pw.BorderSide(width: 1.5))),
                      ),
                      pw.Container(
                          width: 287,
                          height: 15,
                          child: pw.Text('Comandante da Ocorrência',
                              textAlign: pw.TextAlign.center,
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
                        child: pw.Text(
                            'Nome: ${_firefighting.coordenadorBaseOperacional?.nome ?? ''}',
                            style: const pw.TextStyle(fontSize: 10)),
                        decoration: const pw.BoxDecoration(
                            border: pw.Border(
                                bottom: pw.BorderSide(width: 1.5),
                                right: pw.BorderSide(width: 1.5))),
                      ),
                      pw.Container(
                          width: 287,
                          height: 20,
                          child: pw.Text(
                              'Nome: ${_firefighting.comandanteOcorrencia?.nome ?? ''}',
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
                          child: pw.Text(
                              'Posto/Grad: ${_firefighting.coordenadorBaseOperacional?.postoGraduacao ?? ''}',
                              style: const pw.TextStyle(fontSize: 10)),
                          decoration: const pw.BoxDecoration(
                              border: pw.Border(
                                  bottom: pw.BorderSide(width: 1.5),
                                  right: pw.BorderSide(width: 1.5))),
                        ),
                        pw.Container(
                          width: 143.5,
                          height: 20,
                          child: pw.Text(
                              'RE: ${_firefighting.coordenadorBaseOperacional?.re ?? ''}',
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
                          child: pw.Text(
                              'Posto/Grad: ${_firefighting.comandanteOcorrencia?.postoGraduacao ?? ''}',
                              style: const pw.TextStyle(fontSize: 10)),
                          decoration: const pw.BoxDecoration(
                              border: pw.Border(
                                  bottom: pw.BorderSide(width: 1.5),
                                  right: pw.BorderSide(width: 1.5))),
                        ),
                        pw.Container(
                          width: 143.5,
                          height: 20,
                          child: pw.Text(
                              'RE: ${_firefighting.comandanteOcorrencia?.re ?? ''}',
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
                        child: pw.Row(children: [
                          pw.Text('Assinatura:',
                              style: const pw.TextStyle(fontSize: 10)),
                          if (_firefighting
                                      .coordenadorBaseOperacional?.assinatura !=
                                  null &&
                              _firefighting.coordenadorBaseOperacional!
                                  .assinatura!.isNotEmpty)
                            pw.Image(
                                pw.MemoryImage(
                                  base64Decode(_firefighting
                                      .coordenadorBaseOperacional!.assinatura!),
                                ),
                                fit: pw.BoxFit.fill,
                                height: 100,
                                width: 200),
                        ]),
                        decoration: const pw.BoxDecoration(
                            border: pw.Border(
                                bottom: pw.BorderSide(width: 1.5),
                                right: pw.BorderSide(width: 1.5))),
                      ),
                      pw.Container(
                        width: 287,
                        height: 30,
                        child: pw.Row(children: [
                          pw.Text('Assinatura:',
                              style: const pw.TextStyle(fontSize: 10)),
                          if (_firefighting.comandanteOcorrencia?.assinatura !=
                                  null &&
                              _firefighting
                                  .comandanteOcorrencia!.assinatura!.isNotEmpty)
                            pw.Image(
                                pw.MemoryImage(
                                  base64Decode(_firefighting
                                      .comandanteOcorrencia!.assinatura!),
                                ),
                                fit: pw.BoxFit.fill,
                                height: 100,
                                width: 200),
                        ]),
                        decoration: const pw.BoxDecoration(
                            border: pw.Border(
                                bottom: pw.BorderSide(width: 1.5),
                                right: pw.BorderSide(width: 1.5))),
                      ),
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
                                pw.Text('Eng Agrônomo',
                                    textAlign: pw.TextAlign.left,
                                    style: const pw.TextStyle(fontSize: 10)),
                                pw.Text('CREA ',
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
                                pw.Text('Técnico Executor',
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
                                pw.Text('Piloto',
                                    textAlign: pw.TextAlign.left,
                                    style: const pw.TextStyle(fontSize: 10)),
                                pw.Text('CANAC ',
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
