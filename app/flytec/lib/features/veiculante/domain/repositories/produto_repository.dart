import 'package:dartz/dartz.dart';

import '../../../../core/errors/failures.dart';
import '../entities/veiculante_entity.dart';

abstract class IVeiculanteRepository {
  Future<Either<Failure, List<VeiculanteEntity>>> getVeiculantes();
}
