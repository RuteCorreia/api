import 'package:flytec/core/enums/dashboard_state.dart';

class ReportFrotaModel {
  final DashBoardState? state;
  final String? placaVeiculo;
  final int? createdAt;

  ReportFrotaModel(
      {required this.state,
      required this.placaVeiculo,
      required this.createdAt});
}
