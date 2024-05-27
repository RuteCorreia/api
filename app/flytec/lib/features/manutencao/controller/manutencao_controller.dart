import 'package:flytec/features/manutencao/models/report_manutencao_model.dart';

class ManutencaoController {
  ReportManutencaoModel? _reportManutencaoModel;

  ReportManutencaoModel? get reportManutencaoModel => _reportManutencaoModel;

  void setReportManutencaoModel(ReportManutencaoModel reportManutencaoModel) {
    _reportManutencaoModel = reportManutencaoModel;
  }

}
