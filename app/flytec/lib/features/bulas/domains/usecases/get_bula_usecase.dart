import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/core/usecase/usecase.dart';
import 'package:flytec/features/bulas/domains/entities/bula_entity.dart';
import 'package:flytec/features/bulas/domains/repository/bula_repository.dart';
import 'package:flytec/features/executor/domain/repositories/executor_repository.dart';


class GetBulasUseCase extends UseCase<List<BulaEntity>, NoParams> {
  final IBulaRepository? _iBulaRepository;
  GetBulasUseCase(this._iBulaRepository);

  @override
  Future<Either<Failure, List<BulaEntity>>> call(NoParams? params) async {
    final response = await _iBulaRepository!.getBula();

    return response;
  }
}