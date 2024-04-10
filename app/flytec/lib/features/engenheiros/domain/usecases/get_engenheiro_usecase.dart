import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/core/usecase/usecase.dart';
import 'package:flytec/features/engenheiros/domain/entities/engenheiro_entity.dart';
import 'package:flytec/features/executor/domain/repositories/executor_repository.dart';

import '../repositories/engenheiro_repository.dart';

class GetEngenheiroUseCase extends UseCase<List<EngenheiroEntity>, NoParams> {
  final IEngenheiroRepository? _iEngenheiroRepository;
  GetEngenheiroUseCase(this._iEngenheiroRepository);

  @override
  Future<Either<Failure, List<EngenheiroEntity>>> call(NoParams? params) async {
    final authResponse = await _iEngenheiroRepository!.getEngenheiros();

    return authResponse;
  }
}
