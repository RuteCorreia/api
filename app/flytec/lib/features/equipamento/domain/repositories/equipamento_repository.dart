import 'package:dartz/dartz.dart';

import '../../../../core/errors/failures.dart';
import '../entities/equipamento_entity.dart';

abstract class IEquipamentoRepository {
  Future<Either<Failure, List<EquipamentoEntity>>> getEquipamentos();
}
