// To parse this JSON data, do
//
//     final userPayloadModel = userPayloadModelFromJson(jsonString);

import 'dart:convert';

UserPayloadModel userPayloadModelFromJson(String str) =>
    UserPayloadModel.fromJson(json.decode(str));

String userPayloadModelToJson(UserPayloadModel data) =>
    json.encode(data.toJson());

class UserPayloadModel {
  final String? uniqueName;
  final String? role;
  final int? nbf;
  final int? exp;
  final int? iat;
  final String? iss;
  final String? aud;

  UserPayloadModel({
    this.uniqueName,
    this.role,
    this.nbf,
    this.exp,
    this.iat,
    this.iss,
    this.aud,
  });

  factory UserPayloadModel.fromJson(Map<String, dynamic> json) =>
      UserPayloadModel(
        uniqueName: json["unique_name"],
        role: json["role"],
        nbf: json["nbf"],
        exp: json["exp"],
        iat: json["iat"],
        iss: json["iss"],
        aud: json["aud"],
      );

  Map<String, dynamic> toJson() => {
        "unique_name": uniqueName,
        "role": role,
        "nbf": nbf,
        "exp": exp,
        "iat": iat,
        "iss": iss,
        "aud": aud,
      };
}
