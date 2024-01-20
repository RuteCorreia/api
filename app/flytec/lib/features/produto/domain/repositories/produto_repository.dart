import 'package:dartz/dartz.dart';

import '../../../../core/errors/failures.dart';
import '../entities/produto_entity.dart';

abstract class IProdutoRepository {
  Future<Either<Failure, List<ProdutoEntity>>> getProdutos();
}
