import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/core/usecase/usecase.dart';
import 'package:flytec/features/executor/domain/repositories/executor_repository.dart';
import 'package:flytec/features/veiculante/domain/entities/veiculante_entity.dart';

import '../repositories/produto_repository.dart';

class GetVeiculanteUseCase extends UseCase<List<VeiculanteEntity>, NoParams> {
  final IVeiculanteRepository? _iAlvoBiologicoRepository;
  GetVeiculanteUseCase(this._iAlvoBiologicoRepository);

  @override
  Future<Either<Failure, List<VeiculanteEntity>>> call(NoParams? params) async {
    final response = await _iAlvoBiologicoRepository!.getVeiculantes();

    return response;
  }
}
