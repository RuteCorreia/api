import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/exception.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/features/alvo_biologico/domain/entities/alvo_biologico_entity.dart';
import 'package:flytec/features/alvo_biologico/domain/repositories/produto_repository.dart';

import '../datasources/remote_alvo_biologico_data_source.dart';

class AlvoBiologicoRepositoryImpl implements IAlvoBiologicoRepository {
  final RemoteAlvoBilogicoDataSourceImpl remoteAlvoBilogicoDataSourceImpl;

  AlvoBiologicoRepositoryImpl({
    required this.remoteAlvoBilogicoDataSourceImpl,
  });

  @override
  Future<Either<Failure, List<AlvoBiologicoEntity>>> getAlvoBiologico() async {
    try {
      final userData =
          await remoteAlvoBilogicoDataSourceImpl.getAlvosBiologicos();
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
