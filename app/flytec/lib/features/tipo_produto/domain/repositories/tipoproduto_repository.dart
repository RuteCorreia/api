import 'package:dartz/dartz.dart';

import '../../../../core/errors/failures.dart';
import '../entities/tipo_produto_entity.dart';

abstract class ITipoProdutoRepository {
  Future<Either<Failure, List<TipoProdutoEntity>>> getTipoDeProdutos();
}
