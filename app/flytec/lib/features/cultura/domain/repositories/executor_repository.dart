import 'package:dartz/dartz.dart';
import 'package:flytec/features/cultura/domain/entities/cultura_entity.dart';

import '../../../../core/errors/failures.dart';

abstract class ICulturaRepository {
  Future<Either<Failure, List<CulturaEntity>>> getCultura();
}
