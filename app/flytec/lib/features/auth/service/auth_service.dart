import 'dart:convert';

import 'package:shared_preferences/shared_preferences.dart';

class AuthService {
  String token = "";

  Future saveToken(dados) async {
    print("salvando token no cache ...");
    var perf = await SharedPreferences.getInstance();
    return perf.setString("token", json.encode(dados));
  }

  Future<String> getToken() async {
    print("Buscando token localmente ...");
    var perf = await SharedPreferences.getInstance();
    String? token = perf.getString("token") ?? "";
    return Future.value(token);
  }

  Future removeToken() async {
    print("Removendo token do cache ...");
    var perf = await SharedPreferences.getInstance();
    return perf.remove("token");
  }
}
