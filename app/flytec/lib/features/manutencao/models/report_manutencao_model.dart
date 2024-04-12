import 'package:flytec/core/enums/dashboard_state.dart';

class ReportManutencaoModel {
  final DashBoardState? state;
  final String? prefAeronave;
  final int? createdAt;

  ReportManutencaoModel(
      {required this.state,
      required this.prefAeronave,
      required this.createdAt});
}
