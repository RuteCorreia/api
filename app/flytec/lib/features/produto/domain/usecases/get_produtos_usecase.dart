import 'package:dartz/dartz.dart';
import 'package:equatable/equatable.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/core/usecase/usecase.dart';
import 'package:flytec/features/executor/domain/repositories/executor_repository.dart';
import 'package:flytec/features/produto/domain/entities/produto_entity.dart';

import '../repositories/produto_repository.dart';

class GetProdutosUseCase extends UseCase<List<ProdutoEntity>, NoParams> {
  final IProdutoRepository? _iProdutoRepository;
  GetProdutosUseCase(this._iProdutoRepository);

  @override
  Future<Either<Failure, List<ProdutoEntity>>> call(NoParams? params) async {
    final authResponse = await _iProdutoRepository!.getProdutos();

    return authResponse;
  }
}

class AuthParams extends Equatable {
  final String username;
  final String password;
  const AuthParams({required this.username, required this.password});

  @override
  List<Object> get props => [username, password];
}
