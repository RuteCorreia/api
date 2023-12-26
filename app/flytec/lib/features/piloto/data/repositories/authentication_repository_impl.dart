import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/exception.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/features/piloto/domain/entities/piloto_entity.dart';

import '../../domain/repositories/executor_repository.dart';
import '../datasources/remote_piloto_data_source.dart';

class PilotoRepositoryImpl implements IPilotoRepository {
  final RemotePilotoDataSourceImpl remotePilotoDataSourceImpl;

  PilotoRepositoryImpl({
    required this.remotePilotoDataSourceImpl,
  });

  @override
  Future<Either<Failure, List<PilotoEntity>>> getPilotos() async {
    try {
      final userData = await remotePilotoDataSourceImpl.getPilotos();
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
