import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/core/usecase/usecase.dart';
import 'package:flytec/features/cultura/domain/entities/cultura_entity.dart';
import 'package:flytec/features/executor/domain/repositories/executor_repository.dart';

import '../repositories/executor_repository.dart';

class GetCulturasUseCase extends UseCase<List<CulturaEntity>, NoParams> {
  final ICulturaRepository? iCulturaRepository;
  GetCulturasUseCase({required this.iCulturaRepository});

  @override
  Future<Either<Failure, List<CulturaEntity>>> call(NoParams? params) async {
    final culturas = await iCulturaRepository!.getCultura();

    return culturas;
  }
}
