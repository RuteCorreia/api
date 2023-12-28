import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/core/usecase/usecase.dart';
import 'package:flytec/features/aeronave/domain/entities/aeronave_entity.dart';
import 'package:flytec/features/executor/domain/repositories/executor_repository.dart';

import '../repositories/aeronave_repository.dart';

class GetAeroNaveUseCase extends UseCase<List<AeroNaveEntity>, NoParams> {
  final IAeroNaveRepository? aeroNaveRepository;
  GetAeroNaveUseCase(this.aeroNaveRepository);

  @override
  Future<Either<Failure, List<AeroNaveEntity>>> call(NoParams? params) async {
    final response = await aeroNaveRepository!.getAeroNaves();

    return response;
  }
}
