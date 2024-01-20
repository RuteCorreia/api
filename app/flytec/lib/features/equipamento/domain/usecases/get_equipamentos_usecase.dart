import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/core/usecase/usecase.dart';
import 'package:flytec/features/equipamento/domain/entities/equipamento_entity.dart';
import 'package:flytec/features/executor/domain/repositories/executor_repository.dart';

import '../repositories/equipamento_repository.dart';

class GetEquipamentoUseCase extends UseCase<List<EquipamentoEntity>, NoParams> {
  final IEquipamentoRepository? equipamentoRepository;
  GetEquipamentoUseCase(this.equipamentoRepository);

  @override
  Future<Either<Failure, List<EquipamentoEntity>>> call(
      NoParams? params) async {
    final response = await equipamentoRepository!.getEquipamentos();
    return response;
  }
}
