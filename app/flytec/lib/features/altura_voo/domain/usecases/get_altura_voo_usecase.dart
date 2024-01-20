import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/core/usecase/usecase.dart';
import 'package:flytec/features/altura_voo/domain/entities/tipo_produto_entity.dart';
import 'package:flytec/features/executor/domain/repositories/executor_repository.dart';

import '../repositories/altura_voo_repository.dart';

class GetAlturaVooUseCase extends UseCase<List<AlturaVooEntity>, NoParams> {
  final IAlturaVooRepository? repository;
  GetAlturaVooUseCase(this.repository);

  @override
  Future<Either<Failure, List<AlturaVooEntity>>> call(NoParams? params) async {
    final response = await repository!.getAlturaVoo();
    return response;
  }
}
