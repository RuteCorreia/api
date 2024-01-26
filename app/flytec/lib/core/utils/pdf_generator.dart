import 'dart:typed_data';

abstract class PdfGenerator {
  Future<dynamic> generatePdf({dynamic parameters});
  Future<Uint8List?> saveDocument({dynamic document});
}
