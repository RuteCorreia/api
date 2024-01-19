import 'dart:io';

import 'package:flutter/material.dart';
import 'package:flutter_pdfview/flutter_pdfview.dart';

class ReportAplicationsPage extends StatefulWidget {
  final File? report;
  const ReportAplicationsPage({super.key, this.report});

  @override
  State<ReportAplicationsPage> createState() => _ReportAplicationsPageState();
}

class _ReportAplicationsPageState extends State<ReportAplicationsPage> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
        title: const Text(
          "Relatório de Aplicações",
          textAlign: TextAlign.center,
        ),
      ),
      body: Center(
        child: Builder(builder: (context) {
          if (widget.report == null) {
            return const Text('Press the button to generate a PDF');
          }
          return PDFView(
            filePath: widget.report!.path,
            enableSwipe: true,
            swipeHorizontal: true,
            autoSpacing: false,
            pageFling: false,
            onRender: (_pages) {
              // setState(() {
              //   pages = _pages;
              //   isReady = true;
              // });
            },
            onError: (error) {
              print(error.toString());
            },
            onPageError: (page, error) {
              print('$page: ${error.toString()}');
            },
            onViewCreated: (PDFViewController pdfViewController) {
              //_controller.complete(pdfViewController);
            },
          );
        }),
      ),
    );
  }
}
