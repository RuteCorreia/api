import 'dart:io';
import 'package:flytec/core/utils/util.dart';
import 'package:pick_or_save/pick_or_save.dart';
import 'package:share_plus/share_plus.dart';
import 'package:flutter/material.dart';
import 'package:flutter_pdfview/flutter_pdfview.dart';

class ReportAplicationsPage extends StatefulWidget {
  final File? report;
  const ReportAplicationsPage({super.key, this.report});

  @override
  State<ReportAplicationsPage> createState() => _ReportAplicationsPageState();
}

class _ReportAplicationsPageState extends State<ReportAplicationsPage> {
  final _pickOrSavePlugin = PickOrSave();

  Future<void> _downloadReportInPdf() async {
    final nameFile = "relatorio_${Util.getRandomString(10)}.pdf";
    final bytes = await widget.report!.readAsBytes();
    await _pickOrSavePlugin.fileSaver(
        params: FileSaverParams(
            saveFiles: [SaveFileInfo(fileName: nameFile, fileData: bytes)]));
  }

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
      floatingActionButton: Column(
        mainAxisAlignment: MainAxisAlignment.end,
        children: [
          FloatingActionButton(
            heroTag: "Shared Report",
            onPressed: () {
              Share.shareXFiles([XFile(widget.report!.path)],
                  text: 'Relatório');
            },
            child: const Icon(
              Icons.share,
              color: Colors.white,
            ),
          ),
          const SizedBox(
            height: 10,
          ),
          FloatingActionButton(
            heroTag: "Download Report",
            onPressed: () async {
              try {
                await _downloadReportInPdf();
                Util.toastSucesso(
                    'Download do Relatório concluído e se encontra nos seus documentos!');
              } catch (e) {
                Util.toastErro(
                    'Erro ao fazer download do relatório. Tente novamente mais tarde');
              }
            },
            child: const Icon(
              Icons.save_alt,
              color: Colors.white,
            ),
          ),
        ],
      ),
      body: Center(
        child: Builder(builder: (context) {
          if (widget.report == null) {
            return const Text('Relatório não encontrado');
          }
          
          return PDFView(
            filePath: widget.report!.path,
            enableSwipe: true,
            swipeHorizontal: true,
            autoSpacing: false,
            pageFling: false,
          );
        }),
      ),
    );
  }
}
