import 'package:flytec/core/infrastructure/network/endpoints.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:http/http.dart' as http;

import '../../../../core/errors/exception.dart';
import '../../../../core/network/network_info.dart';

abstract class IRemoteCompanyDataSource {
  Future<String?> obtainLogoCompany(String companyId);
}

class RemoteCompanyDataSourceImpl implements IRemoteCompanyDataSource {
  final http.Client client;
  final NetWorkInfoImpl? netWorkInfoI;
  RemoteCompanyDataSourceImpl(
      {required this.client, required this.netWorkInfoI});

  @override
  Future<String?> obtainLogoCompany(String companyId) async {
    if (await netWorkInfoI!.isConnected) {
      final response = await client.get(
        Uri.parse(Endpoints.logoCompany(companyId)),
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ${Util.Token}'
        },
      );

      if (response.statusCode == 200) {
        return Future.value(response.body);
      } else if (response.statusCode == 400) {
        throw LoginException(message: "Company incorrecto");
      } else {
        throw ServerException(message: "StatusCode: ${response.statusCode}");
      }
    } else {
      throw NetWorkException(message: "Sem conexão a internet");
    }
  }
}
