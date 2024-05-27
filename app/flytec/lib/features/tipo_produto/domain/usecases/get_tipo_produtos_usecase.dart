import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/core/usecase/usecase.dart';
import 'package:flytec/features/executor/domain/repositories/executor_repository.dart';
import 'package:flytec/features/tipo_produto/domain/entities/tipo_produto_entity.dart';

import '../repositories/tipoproduto_repository.dart';

class GetTipoProdutosUseCase
    extends UseCase<List<TipoProdutoEntity>, NoParams> {
  final ITipoProdutoRepository? repository;
  GetTipoProdutosUseCase(this.repository);

  @override
  Future<Either<Failure, List<TipoProdutoEntity>>> call(
      NoParams? params) async {
    final response = await repository!.getTipoDeProdutos();
    return response;
  }
}
