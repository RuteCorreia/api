import 'package:dartz/dartz.dart';

import '../../../../core/errors/failures.dart';
import '../entities/engenheiro_entity.dart';

abstract class IEngenheiroRepository {
  Future<Either<Failure, List<EngenheiroEntity>>> getEngenheiros();
}
