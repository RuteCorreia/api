import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/exception.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/features/equipamento/domain/entities/equipamento_entity.dart';
import 'package:flytec/features/equipamento/domain/repositories/equipamento_repository.dart';

import '../datasources/remote_equipamento_data_source.dart';

class EquipamentoRepositoryImpl implements IEquipamentoRepository {
  final RemoteEquipamentoDataSourceImpl datasource;

  EquipamentoRepositoryImpl({
    required this.datasource,
  });

  @override
  Future<Either<Failure, List<EquipamentoEntity>>> getEquipamentos() async {
    try {
      final userData = await datasource.getEquipamentos();
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
