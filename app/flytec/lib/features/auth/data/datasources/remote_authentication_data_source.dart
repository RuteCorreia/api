import 'dart:convert';

import 'package:flytec/core/infrastructure/network/endpoints.dart';
import 'package:flytec/features/auth/data/models/auth_model.dart';
import 'package:http/http.dart' as http;

import '../../../../core/errors/exception.dart';
import '../../../../core/network/network_info.dart';
import '../../domain/usecases/authentication_usecase.dart';

abstract class IRemoteAuthenticationDataSource {
  Future<AuthModel> authenticate(AuthParams authParams);
}

class RemoteAuthenticationDataSourceImpl
    implements IRemoteAuthenticationDataSource {
  final http.Client client;
  final NetWorkInfoImpl? netWorkInfoI;
  RemoteAuthenticationDataSourceImpl(
      {required this.client, required this.netWorkInfoI});

  @override
  Future<AuthModel> authenticate(AuthParams authParams) async {
    if (await netWorkInfoI!.isConnected) {
      final response = await client.post(Uri.parse(Endpoints.login),
          headers: {
            'Content-Type': 'application/json',
          },
          body: jsonEncode({
            "email": authParams.username,
            "password": authParams.password,
          }));

      if (response.statusCode == 200) {
        return Future.value(
          authModelFromJson(response.body),
        );
      } else if (response.statusCode == 400) {
        final errorMessage = response.body.isNotEmpty
            ? response.body.replaceAll('"', '')
            : "Login incorrecto";
        throw LoginException(message: errorMessage);
      } else {
        throw ServerException(message: "StatusCode: ${response.statusCode}");
      }
    } else {
      throw NetWorkException(message: "Sem conexão a internet");
    }
  }
}
