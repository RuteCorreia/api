// To parse this JSON data, do
//
//     final userPayloadModel = userPayloadModelFromJson(jsonString);

import 'dart:convert';

UserPayloadModel userPayloadModelFromJson(String str) =>
    UserPayloadModel.fromJson(json.decode(str));

String userPayloadModelToJson(UserPayloadModel data) =>
    json.encode(data.toJson());

class UserPayloadModel {
  final String? nrUsuario;
  final String? sub;
  final String? name;
  final String? email;
  final String? jti;
  final String? idUsuario;
  final int? nbf;
  final int? iat;
  final List<String?>? role;
  final int? exp;
  final String? iss;
  final String? aud;

  UserPayloadModel({
    this.nrUsuario,
    this.sub,
    this.name,
    this.email,
    this.jti,
    this.idUsuario,
    this.nbf,
    this.iat,
    this.role,
    this.exp,
    this.iss,
    this.aud,
  });

  factory UserPayloadModel.fromJson(Map<String, dynamic> json) =>
      UserPayloadModel(
        nrUsuario: json["NrUsuario"],
        sub: json["sub"],
        name: json["name"],
        email: json["email"],
        jti: json["jti"],
        nbf: json["nbf"],
        iat: json["iat"],
        role: json["role"] is String
            ? [json["role"].toString()]
            : (json["role"] as List).map((e) => e.toString()).toList(),
        idUsuario: json["IdUsuario"],
        exp: json["exp"],
        iss: json["iss"],
        aud: json["aud"],
      );

  Map<String, dynamic> toJson() => {
        "NrUsuario": nrUsuario,
        "sub": sub,
        "name": name,
        "email": email,
        "jti": jti,
        "nbf": nbf,
        "iat": iat,
        "role": role,
        "idUsuario": idUsuario,
        "exp": exp,
        "iss": iss,
        "aud": aud,
      };
}
