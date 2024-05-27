import 'package:dartz/dartz.dart';
import 'package:flytec/features/bulas/domains/entities/bula_entity.dart';

import '../../../../core/errors/failures.dart';

abstract class IBulaRepository {
  Future<Either<Failure, List<BulaEntity>>> getBula();
}
