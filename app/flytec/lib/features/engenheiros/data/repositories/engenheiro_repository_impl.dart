import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/exception.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/features/engenheiros/domain/entities/engenheiro_entity.dart';

import '../../domain/repositories/engenheiro_repository.dart';
import '../datasources/remote_engenheiro_data_source.dart';

class EngenheiroRepositoryImpl implements IEngenheiroRepository {
  final RemoteEngenheiroDataSourceImpl remoteEngenheiroDataSourceImpl;

  EngenheiroRepositoryImpl({
    required this.remoteEngenheiroDataSourceImpl,
  });

  @override
  Future<Either<Failure, List<EngenheiroEntity>>> getEngenheiros() async {
    try {
      final userData = await remoteEngenheiroDataSourceImpl.getEngenheiros();
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
