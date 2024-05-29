import 'package:equatable/equatable.dart';

class AuthEntity extends Equatable {
  final bool? success;
  final String? token;

  const AuthEntity({
    this.success,
    this.token,
  });

  @override
  List<Object?> get props => [success, token];
}
