// To parse this JSON data, do
//
//     final authModel = authModelFromJson(jsonString);

import 'dart:convert';

import 'package:flytec/features/auth/domain/entities/user_entity.dart';

AuthModel authModelFromJson(String str) => AuthModel.fromJson(json.decode(str));

String authModelToJson(AuthModel data) => json.encode(data.toJson());

class AuthModel extends AuthEntity {
  const AuthModel({
    super.success,
    super.token,
  });

  factory AuthModel.fromJson(Map<String, dynamic> json) => AuthModel(
        success: json["success"],
        token: json["token"],
      );

  Map<String, dynamic> toJson() => {
        "success": success,
        "token": token,
      };
}
