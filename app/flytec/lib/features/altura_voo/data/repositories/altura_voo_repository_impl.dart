import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/exception.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/features/altura_voo/domain/entities/tipo_produto_entity.dart';
import 'package:flytec/features/altura_voo/domain/repositories/altura_voo_repository.dart';

import '../datasources/remote_altura_voo_data_source.dart';

class AlturaVooRepositoryImpl implements IAlturaVooRepository {
  final RemoteAlturVooDataSourceImpl datasource;

  AlturaVooRepositoryImpl({
    required this.datasource,
  });

  @override
  Future<Either<Failure, List<AlturaVooEntity>>> getAlturaVoo() async {
    try {
      final userData = await datasource.getAlturaVoo();
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
