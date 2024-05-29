import 'package:dartz/dartz.dart';
import 'package:flytec/features/auth/domain/entities/user_entity.dart';

import '../../../../core/errors/exception.dart';
import '../../../../core/errors/failures.dart';
import '../../domain/repositories/authentication_repository.dart';
import '../../domain/usecases/authentication_usecase.dart';
import '../datasources/remote_authentication_data_source.dart';

class AuthenticationRepositoryImpl implements IAuthenticationRepository {
  final RemoteAuthenticationDataSourceImpl remoteAuthenticationDataSourceImpl;

  AuthenticationRepositoryImpl({
    required this.remoteAuthenticationDataSourceImpl,
  });

  @override
  Future<Either<Failure, AuthEntity>> authenticate({AuthParams? params}) async {
    try {
      final userData = await remoteAuthenticationDataSourceImpl.authenticate(
        AuthParams(username: params!.username, password: params.password),
      );
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
