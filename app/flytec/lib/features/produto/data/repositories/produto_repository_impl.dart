import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/exception.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/features/produto/domain/entities/produto_entity.dart';
import 'package:flytec/features/produto/domain/repositories/produto_repository.dart';

import '../datasources/remote_produtos_data_source.dart';

class ProdutoRepositoryImpl implements IProdutoRepository {
  final RemoteProdutoDataSourceImpl remoteProdutoDataSourceImpl;

  ProdutoRepositoryImpl({
    required this.remoteProdutoDataSourceImpl,
  });

  @override
  Future<Either<Failure, List<ProdutoEntity>>> getProdutos() async {
    try {
      final userData = await remoteProdutoDataSourceImpl.getProdutos();
      return Right(userData);
    } on ServerException {
      return Left(ServerFailure(message: "Ocorreu um erro  no servidor"));
    } on LoginException {
      return Left(LoginFailure());
    } on NetWorkException {
      return Left(NetWorkFailure());
    }
  }
}
