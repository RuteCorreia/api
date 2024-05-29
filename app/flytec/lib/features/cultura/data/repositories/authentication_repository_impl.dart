import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/exception.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/features/cultura/domain/entities/cultura_entity.dart';

import '../../domain/repositories/executor_repository.dart';
import '../datasources/remote_piloto_data_source.dart';

class CulturaRepositoryImpl implements ICulturaRepository {
  final RemoteCulturaDataSourceImpl remoteCulturaDataSourceImpl;

  CulturaRepositoryImpl({
    required this.remoteCulturaDataSourceImpl,
  });

  @override
  Future<Either<Failure, List<CulturaEntity>>> getCultura() async {
    try {
      final userData = await remoteCulturaDataSourceImpl.getCulturas();
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
