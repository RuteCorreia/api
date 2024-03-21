import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/exception.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/features/bulas/data/datasource/remote_bula_datasource.dart';
import 'package:flytec/features/bulas/domains/entities/bula_entity.dart';
import 'package:flytec/features/bulas/domains/repository/bula_repository.dart';


class BulaRepositoryImpl implements IBulaRepository {
  final RemoteBulaDataSourceImpl remoteBulaDataSourceImpl;

  BulaRepositoryImpl({
    required this.remoteBulaDataSourceImpl,
  });

  @override
  Future<Either<Failure, List<BulaEntity>>> getBula() async {
    try {
      final userData = await remoteBulaDataSourceImpl.getBulas();
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