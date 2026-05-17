import 'package:equatable/equatable.dart';

abstract class Failure extends Equatable {}

// General failures
class ServerFailure extends Failure {
  final String message;
  ServerFailure({required this.message});
  @override
  List<Object?> get props => [];
}

class CacheFailure extends Failure {
  @override
  List<Object?> get props => [];
}

class EmptyFailure extends Failure {
  final String message;
  EmptyFailure({required this.message});
  @override
  List<Object?> get props => [message];
}

class DuplicateFailure extends Failure {
  @override
  List<Object?> get props => [];
}

class NetWorkFailure extends Failure {
  @override
  List<Object?> get props => [];
}

class LoginFailure extends Failure {
  final String message;
  LoginFailure({this.message = ''});
  @override
  List<Object?> get props => [message];
}
