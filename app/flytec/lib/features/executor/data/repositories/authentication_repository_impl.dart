import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/exception.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/features/executor/domain/entities/executor_entity.dart';

import '../../domain/repositories/executor_repository.dart';
import '../datasources/remote_authentication_data_source.dart';

class ExecutorRepositoryImpl implements IExecutorRepository {
  final RemoteExecutorDataSourceImpl remoteExecutorDataSourceImpl;

  ExecutorRepositoryImpl({
    required this.remoteExecutorDataSourceImpl,
  });

  @override
  Future<Either<Failure, List<ExecutorEntity>>> getExecutores() async {
    try {
      final userData = await remoteExecutorDataSourceImpl.getExecutores();
      return Right(userData);
    } on ServerException {
      return Left(ServerFailure(message: "Ocorreu um erro ao fazer o login"));
    } on LoginException {
      return Left(LoginFailure());
    } on NetWorkException {
      return Left(NetWorkFailure());
    }
  }
}
