part of 'authentication_bloc.dart';

abstract class AuthenticationState extends Equatable {
  const AuthenticationState();

  @override
  List<Object> get props => [];
}

class AuthenticationInitial extends AuthenticationState {}

class AuthenticationSuccessState extends AuthenticationState {
  final AuthModel? authModel;
  const AuthenticationSuccessState({required this.authModel});
}

class AuthenticationLoadingState extends AuthenticationState {}

class AuthenticationError extends AuthenticationState {
  final String? message;
  const AuthenticationError({required this.message});
}

class AuthenticationValidatorState extends AuthenticationState {
  final String message;
  const AuthenticationValidatorState({required this.message});
}

class UpdateNotificationTimerState extends AuthenticationState {}
