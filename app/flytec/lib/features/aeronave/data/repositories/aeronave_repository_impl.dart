import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/exception.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/features/aeronave/domain/entities/aeronave_entity.dart';
import 'package:flytec/features/aeronave/domain/repositories/aeronave_repository.dart';

import '../datasources/remote_aeronave_data_source.dart';

class AeroNaveRepositoryImpl implements IAeroNaveRepository {
  final RemoteAeroNaveDataSourceImpl remoteAeroNaveRepository;

  AeroNaveRepositoryImpl({
    required this.remoteAeroNaveRepository,
  });

  @override
  Future<Either<Failure, List<AeroNaveEntity>>> getAeroNaves() async {
    try {
      final userData = await remoteAeroNaveRepository.getAeroNaves();
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
