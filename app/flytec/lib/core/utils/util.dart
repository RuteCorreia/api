import 'package:flutter/material.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:intl/intl.dart';

class Util {
  static String Token = "";

  static String getTodayDate() {
    final now = DateTime.now();

    final formattedDate = DateFormat('dd/MM/yyyy').format(now);

    return formattedDate;
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

  static double converterMetrosPorSegundoParaKmPorHora(
      double velocidadeEmMetrosPorSegundo) {
    return velocidadeEmMetrosPorSegundo * 3.6;
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
