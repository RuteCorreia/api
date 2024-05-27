import 'package:dartz/dartz.dart';
import 'package:equatable/equatable.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/core/usecase/usecase.dart';
import 'package:flytec/features/executor/domain/entities/executor_entity.dart';

import '../repositories/executor_repository.dart';

class GetExecutoresUseCase extends UseCase<List<ExecutorEntity>, NoParams> {
  final IExecutorRepository? _executorRepository;
  GetExecutoresUseCase(this._executorRepository);

  @override
  Future<Either<Failure, List<ExecutorEntity>>> call(NoParams? params) async {
    final authResponse = await _executorRepository!.getExecutores();

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
