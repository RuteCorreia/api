import 'dart:convert';

import 'package:flytec/core/infrastructure/network/endpoints.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/data/models/report_aplications_model.dart';
import 'package:http/http.dart' as http;

import '../../../../core/errors/exception.dart';
import '../../../../core/network/network_info.dart';

abstract class RemoteReportAplicationsDatasource {
  Future<bool> sendReportAplication(
      ReportAplicationsModel? reportAplicationsModel);
}

class RemoteReportAplicationsDatasourceImpl
    implements RemoteReportAplicationsDatasource {
  final http.Client client;
  final NetWorkInfoImpl? netWorkInfoI;
  RemoteReportAplicationsDatasourceImpl(
      {required this.client, required this.netWorkInfoI});

  @override
  Future<bool> sendReportAplication(
      ReportAplicationsModel? reportAplicationsModel) async {
    if (!(await netWorkInfoI!.isConnected)) {
      throw NetWorkException(message: "Sem conexão a internet");
    }
    try {
      final response = await client.post(
        Uri.parse(Endpoints.reportAplications),
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ${Util.Token}',
        },
        body: jsonEncode(reportAplicationsModel?.toJson()),
      );
      if (response.statusCode == 200) {
        return Future.value(true);
      }
      throw ServerException(message: "StatusCode: ${response.statusCode}");
    } catch (e) {
      rethrow;
    }
  }
}
