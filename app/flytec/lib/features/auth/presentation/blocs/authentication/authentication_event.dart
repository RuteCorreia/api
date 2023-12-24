part of 'authentication_bloc.dart';

abstract class AuthenticationEvent extends Equatable {
  const AuthenticationEvent();

  @override
  List<Object> get props => [];
}

class LoginEvent extends AuthenticationEvent {
  final String username;
  final String password;
  final BuildContext context;
  const LoginEvent(
      {required this.password, required this.username, required this.context});

  @override
  List<Object> get props => [username, password];
}

class UpdateNotificationEvent extends AuthenticationEvent {
  final int randomNumber;
  const UpdateNotificationEvent({required this.randomNumber});
}
