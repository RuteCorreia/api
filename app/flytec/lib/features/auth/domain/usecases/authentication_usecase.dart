import 'package:dartz/dartz.dart';
import 'package:equatable/equatable.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/core/usecase/usecase.dart';
import 'package:flytec/features/auth/domain/entities/user_entity.dart';

import '../repositories/authentication_repository.dart';

class AuthenticateUseCase extends UseCase<AuthEntity, AuthParams> {
  final IAuthenticationRepository? _authenticationRepository;
  AuthenticateUseCase(this._authenticationRepository);

  @override
  Future<Either<Failure, AuthEntity>> call(AuthParams? params) async {
    final authResponse = await _authenticationRepository!.authenticate(
      params: AuthParams(username: params!.username, password: params.password),
    );

    return authResponse;
  }
}

class AuthParams extends Equatable {
  final String username;
  final String password;
  const AuthParams({required this.username, required this.password});

  @override
  List<Object> get props => [username, password];
}
