import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/exception.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/features/veiculante/domain/entities/veiculante_entity.dart';
import 'package:flytec/features/veiculante/domain/repositories/produto_repository.dart';

import '../datasources/remote_veiculante_data_source.dart';

class VeiculanteRepositoryImpl implements IVeiculanteRepository {
  final RemoteVeiculanteDataSourceImpl remoteVeiculanteDataSourceImpl;

  VeiculanteRepositoryImpl({
    required this.remoteVeiculanteDataSourceImpl,
  });

  @override
  Future<Either<Failure, List<VeiculanteEntity>>> getVeiculantes() async {
    try {
      final userData = await remoteVeiculanteDataSourceImpl.getVeiculantes();
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
