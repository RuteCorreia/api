import 'package:dartz/dartz.dart';

import '../../../../core/errors/failures.dart';
import '../entities/piloto_entity.dart';

abstract class IPilotoRepository {
  Future<Either<Failure, List<PilotoEntity>>> getPilotos();
}
