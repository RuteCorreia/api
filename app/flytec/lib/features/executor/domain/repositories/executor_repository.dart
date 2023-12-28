import 'package:dartz/dartz.dart';
import 'package:equatable/equatable.dart';

import '../../../../core/errors/failures.dart';
import '../entities/executor_entity.dart';

abstract class IExecutorRepository {
  Future<Either<Failure, List<ExecutorEntity>>> getExecutores();
}

class NoParams extends Equatable {
  @override
  List<Object?> get props => [];
}
