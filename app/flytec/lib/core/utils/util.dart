import 'dart:math';

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:flytec/features/home/presentation/widgets/custom_dialog_button.dart';
import 'package:image_picker/image_picker.dart';
import 'package:intl/intl.dart';

class Util {
  static String Token = "";
  static final ImagePicker _imagePicker = ImagePicker();

  static String getTodayDate() {
    final now = DateTime.now();

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

  static Future<String> obtainImagePathMaps(BuildContext context) async {
    String pathImage = "";
    await showAdaptiveDialog<String>(
      context: context,
      useSafeArea: true,
      builder: (BuildContext context) => AlertDialog.adaptive(
        insetPadding: const EdgeInsets.all(32),
        title: const SizedBox(
          width: 244,
          height: 30,
          child: Text(
            'Selecione de onde vem a imagem',
            textAlign: TextAlign.center,
            style: TextStyle(
              color: Color.fromARGB(255, 121, 118, 118),
              fontSize: 16,
              fontFamily: 'Inter',
              fontWeight: FontWeight.w500,
              height: 0.09,
            ),
          ),
        ),
        content: SizedBox(
          height: 200,
          child: Column(
            children: [
              CustomDialogButton(
                text: "Galeria",
                icon: Icons.image,
                onClick: () async {
                  try {
                    final XFile? image = await _imagePicker.pickImage(
                        source: ImageSource.gallery);
                    pathImage = image!.path;
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
                        source: ImageSource.camera);
                    pathImage = image!.path;
                    Navigator.pop(context);
                    Util.toastSucesso('Imagem adicionada com sucesso');
                  } catch (e) {
                    Util.toastErro(
                        'Não foi possível adicionar a imagem. Por Favor, tente novamente');
                  }
                },
                text: "Câmera",
              )
            ],
          ),
        ),
        actions: const <Widget>[],
      ),
    );
    return pathImage;
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

  static toastAlerta(txt) {
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
}

/// Formata o valor do campo com a mascara kg,g (ex: 103,8)
class FormatarHorimetro extends TextInputFormatter {
  @override
  TextEditingValue formatEditUpdate(
      TextEditingValue oldValue, TextEditingValue newValue) {
    // verifica o tamanho máximo do campo
    if (newValue.text.length > 4) return oldValue;

    var posicaoCursor = newValue.selection.end;
    var substrIndex = 0;
    final valorFinal = StringBuffer();

    switch (newValue.text.length) {
      case 3:
        valorFinal.write('${newValue.text.substring(0, substrIndex = 2)}.');
        if (newValue.selection.end >= 3) posicaoCursor++;
        break;
      case 4:
        valorFinal.write('${newValue.text.substring(0, substrIndex = 3)}.');
        if (newValue.selection.end >= 4) posicaoCursor++;
        break;
    }

    if (newValue.text.length >= substrIndex) {
      valorFinal.write(newValue.text.substring(substrIndex));
    }

    return TextEditingValue(
      text: valorFinal.toString(),
      selection: TextSelection.collapsed(offset: posicaoCursor),
    );
  }
}
