import 'package:dartz/dartz.dart';

import '../../../../core/errors/failures.dart';
import '../entities/aeronave_entity.dart';

abstract class IAeroNaveRepository {
  Future<Either<Failure, List<AeroNaveEntity>>> getAeroNaves();
}
