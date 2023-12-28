import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/core/usecase/usecase.dart';
import 'package:flytec/features/alvo_biologico/domain/entities/alvo_biologico_entity.dart';
import 'package:flytec/features/executor/domain/repositories/executor_repository.dart';

import '../repositories/produto_repository.dart';

class GetAlvoBiologicoUseCase
    extends UseCase<List<AlvoBiologicoEntity>, NoParams> {
  final IAlvoBiologicoRepository? _iAlvoBiologicoRepository;
  GetAlvoBiologicoUseCase(this._iAlvoBiologicoRepository);

  @override
  Future<Either<Failure, List<AlvoBiologicoEntity>>> call(
      NoParams? params) async {
    final response = await _iAlvoBiologicoRepository!.getAlvoBiologico();

    return response;
  }
}
