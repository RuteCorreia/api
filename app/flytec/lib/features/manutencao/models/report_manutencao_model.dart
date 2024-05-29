import 'package:flytec/core/enums/dashboard_state.dart';
import 'package:flytec/features/manutencao/models/manutencao_componente_aeronave_model.dart';

class ReportManutencaoModel {
  final DashBoardState? state;
  final String? prefAeronave;
  final int? createdAt;
  final String? horimetroInicial;
  final ManutencaoComponenteAeronaveModel? manutencao;

  ReportManutencaoModel(
      {this.state,
      this.prefAeronave,
      this.horimetroInicial,
      this.manutencao,
      this.createdAt});
}
