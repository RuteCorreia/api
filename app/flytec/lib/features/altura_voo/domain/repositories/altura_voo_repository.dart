import 'package:dartz/dartz.dart';

import '../../../../core/errors/failures.dart';
import '../entities/tipo_produto_entity.dart';

abstract class IAlturaVooRepository {
  Future<Either<Failure, List<AlturaVooEntity>>> getAlturaVoo();
}
