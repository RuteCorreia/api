import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/exception.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/features/tipo_produto/domain/entities/tipo_produto_entity.dart';
import 'package:flytec/features/tipo_produto/domain/repositories/tipoproduto_repository.dart';

import '../datasources/remote_tipo_produto_data_source.dart';

class TipoProdutoRepositoryImpl implements ITipoProdutoRepository {
  final RemoteTipoProdutoDataSourceImpl datasource;

  TipoProdutoRepositoryImpl({
    required this.datasource,
  });

  @override
  Future<Either<Failure, List<TipoProdutoEntity>>> getTipoDeProdutos() async {
    try {
      final userData = await datasource.getTipoDeProdutos();
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
