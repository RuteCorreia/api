import 'package:dartz/dartz.dart';

import '../../../../core/errors/failures.dart';
import '../entities/alvo_biologico_entity.dart';

abstract class IAlvoBiologicoRepository {
  Future<Either<Failure, List<AlvoBiologicoEntity>>> getAlvoBiologico();
}
