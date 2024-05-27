import 'package:dartz/dartz.dart';
import 'package:equatable/equatable.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/core/usecase/usecase.dart';
import 'package:flytec/features/executor/domain/repositories/executor_repository.dart';
import 'package:flytec/features/piloto/domain/entities/piloto_entity.dart';

import '../repositories/executor_repository.dart';

class GetPilotosUseCase extends UseCase<List<PilotoEntity>, NoParams> {
  final IPilotoRepository? _iPilotoRepository;
  GetPilotosUseCase(this._iPilotoRepository);

  @override
  Future<Either<Failure, List<PilotoEntity>>> call(NoParams? params) async {
    final authResponse = await _iPilotoRepository!.getPilotos();

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
