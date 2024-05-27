import 'package:dartz/dartz.dart';

import '../../../../core/errors/failures.dart';
import '../entities/user_entity.dart';
import '../usecases/authentication_usecase.dart';

abstract class IAuthenticationRepository {
  Future<Either<Failure, AuthEntity>> authenticate({
    AuthParams? params,
  });
}
