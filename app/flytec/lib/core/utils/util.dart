import 'dart:io';
import 'dart:math';
import 'dart:ui' as ui;
import 'package:file_picker/file_picker.dart';
import 'package:flutter/material.dart';
import 'package:flutter/rendering.dart';
import 'package:flutter/services.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:flytec/core/models/archive_import.dart';
import 'package:flytec/features/home/presentation/widgets/custom_dialog_button.dart';
import 'package:image_picker/image_picker.dart';
import 'package:intl/intl.dart';

class Util {
  static String Token = "";
  static final ImagePicker _imagePicker = ImagePicker();

  static String getTodayDate({DateTime? date}) {
    final now = date ?? DateTime.now();

    final formattedDate = DateFormat('dd/MM/yyyy').format(now);

    return formattedDate;
  }

  static String getRandomString(int length) {
    const chars =
        'AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz1234567890';
    Random rnd = Random();
    return String.fromCharCodes(Iterable.generate(
        length, (_) => chars.codeUnitAt(rnd.nextInt(chars.length))));
  }

  static Future<ArchiveImport> obtainImagePathMaps(BuildContext context,
      {bool isPdf = true}) async {
    ArchiveImport archive = ArchiveImport();
    await showAdaptiveDialog<String>(
      context: context,
      useSafeArea: true,
      builder: (BuildContext context) => AlertDialog.adaptive(
        insetPadding: const EdgeInsets.all(32),
        title: const SizedBox(
          width: 244,
          height: 50,
          child: Text(
            'Selecione de onde o arquivo',
            textAlign: TextAlign.center,
            style: TextStyle(
              color: Color.fromARGB(255, 121, 118, 118),
              fontSize: 16,
              fontFamily: 'Inter',
              fontWeight: FontWeight.w500,
            ),
          ),
        ),
        content: SizedBox(
          height: 300,
          child: Column(children: [
            CustomDialogButton(
              text: "Galeria",
              icon: Icons.image,
              onClick: () async {
                try {
                  final XFile? image = await _imagePicker.pickImage(
                      source: ImageSource.gallery,
                      imageQuality: 65,
                      maxHeight: 800,
                      requestFullMetadata: Platform.isAndroid,
                      maxWidth: 800);
                  archive.path = image!.path;
                  // ignore: use_build_context_synchronously
                  Navigator.pop(context);
                  Util.toastSucesso('Imagem adicionada com sucesso');
                } catch (e) {
                  Util.toastErro(
                      'Não foi possível adicionar a imagem. Por Favor, tente novamente');
                }
              },
            ),
            const SizedBox(height: 10),
            CustomDialogButton(
              icon: Icons.camera_alt_outlined,
              onClick: () async {
                try {
                  final XFile? image = await _imagePicker.pickImage(
                      source: ImageSource.camera,
                      imageQuality: 65,
                      maxHeight: 800,
                      maxWidth: 800);
                  archive.path = image!.path;
                  // ignore: use_build_context_synchronously
                  Navigator.pop(context);
                  Util.toastSucesso('Imagem adicionada com sucesso');
                } catch (e) {
                  Util.toastErro(
                      'Não foi possível adicionar a imagem. Por Favor, tente novamente');
                }
              },
              text: "Câmera",
            ),
            if (isPdf) ...[
              const SizedBox(height: 10),
              CustomDialogButton(
                text: "Arquivos",
                icon: Icons.archive,
                onClick: () async {
                  try {
                    FilePickerResult? result =
                        await FilePicker.platform.pickFiles(
                      type: FileType.custom,
                      allowedExtensions: ['pdf', 'doc'],
                    );
                    archive.path = result!.files.single.path!;
                    archive.isImage = false;
                    // ignore: use_build_context_synchronously
                    Navigator.pop(context);
                    Util.toastSucesso('Arquivo adicionada com sucesso');
                  } catch (e) {
                    Util.toastErro(
                        'Não foi possível adicionar o arquivo. Por Favor, tente novamente');
                  }
                },
              ),
            ]
          ]),
        ),
        actions: const <Widget>[],
      ),
    );
    return archive;
  }

  static double converterMetrosPorSegundoParaKmPorHora(
      double velocidadeEmMetrosPorSegundo) {
    return velocidadeEmMetrosPorSegundo * 3.6;
  }

  static closeKeyBoard() {
    FocusManager.instance.primaryFocus?.unfocus();
  }

  static toastSucesso(txt) {
    return Fluttertoast.showToast(
      msg: txt,
      toastLength: Toast.LENGTH_SHORT,
      gravity: ToastGravity.BOTTOM,
      timeInSecForIosWeb: 4,
      backgroundColor: Colors.green,
      textColor: Colors.white,
      fontSize: 16.0,
    );
  }

  static toastErro(txt) {
    return Fluttertoast.showToast(
      msg: txt,
      toastLength: Toast.LENGTH_SHORT,
      gravity: ToastGravity.BOTTOM,
      timeInSecForIosWeb: 4,
      backgroundColor: Colors.red,
      textColor: Colors.white,
      fontSize: 16.0,
    );
  }

  static toastAlerta(txt, {bool showToast = true}) {
    if (!showToast) return;
    return Fluttertoast.showToast(
      msg: txt,
      toastLength: Toast.LENGTH_SHORT,
      gravity: ToastGravity.BOTTOM,
      timeInSecForIosWeb: 4,
      backgroundColor: Colors.orange,
      textColor: Colors.white,
      fontSize: 16.0,
    );
  }

  static Future<Uint8List> createImageFromWidget(Widget widget,
      {Size? logicalSize,
      required Duration waitToRender,
      Size? imageSize}) async {
    final RenderRepaintBoundary repaintBoundary = RenderRepaintBoundary();
    final view = ui.PlatformDispatcher.instance.views.first;
    logicalSize ??= view.physicalSize / view.devicePixelRatio;
    imageSize ??= view.physicalSize;

    // assert(logicalSize.aspectRatio == imageSize.aspectRatio);

    final RenderView renderView = RenderView(
      view: view,
      child: RenderPositionedBox(
          alignment: Alignment.center, child: repaintBoundary),
      configuration: ViewConfiguration(
        size: logicalSize,
        devicePixelRatio: 1.0,
      ),
    );

    final PipelineOwner pipelineOwner = PipelineOwner();
    final BuildOwner buildOwner = BuildOwner(focusManager: FocusManager());

    pipelineOwner.rootNode = renderView;
    renderView.prepareInitialFrame();

    final RenderObjectToWidgetElement<RenderBox> rootElement =
        RenderObjectToWidgetAdapter<RenderBox>(
      container: repaintBoundary,
      child: widget,
    ).attachToRenderTree(buildOwner);

    buildOwner.buildScope(rootElement);

    await Future.delayed(waitToRender);

    buildOwner.buildScope(rootElement);
    buildOwner.finalizeTree();

    pipelineOwner.flushLayout();
    pipelineOwner.flushCompositingBits();
    pipelineOwner.flushPaint();

    final ui.Image image = await repaintBoundary.toImage(
        pixelRatio: imageSize.width / logicalSize.width);
    final ByteData? byteData =
        await image.toByteData(format: ui.ImageByteFormat.png);

    return byteData!.buffer.asUint8List();
  }
}

class CustomNumberFormatter extends TextInputFormatter {
  @override
  TextEditingValue formatEditUpdate(
      TextEditingValue oldValue, TextEditingValue newValue) {
    String newText = newValue.text.replaceAll(RegExp(r'[^0-9]'), '');

    if (newText.length > 9) {
      newText = newText.substring(0, 9);
    }

    if (newText.length > 1) {
      newText =
          '${newText.substring(0, newText.length - 1)}.${newText.substring(newText.length - 1)}';
    }

    return newValue.copyWith(
      text: newText,
      selection: TextSelection.collapsed(offset: newText.length),
    );
  }
}
