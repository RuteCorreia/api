import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';
import 'package:flutter/cupertino.dart';
import 'package:flytec/core/injections/get_it.dart';

import '../../../../../core/errors/failures.dart';
import '../../../../../core/messages/messages.dart';
import '../../../data/models/auth_model.dart';
import '../../../domain/usecases/authentication_usecase.dart';

part 'authentication_event.dart';
part 'authentication_state.dart';

class AuthenticationBloc
    extends Bloc<AuthenticationEvent, AuthenticationState> {
  AuthenticationBloc() : super(AuthenticationInitial()) {
    String _mapFailureToMessage(Failure failure) {
      switch (failure.runtimeType) {
        case ServerFailure:
          return serverFailureMessage;
        case LoginFailure:
          final loginFailure = failure as LoginFailure;
          return loginFailure.message.isNotEmpty
              ? loginFailure.message
              : loginFailureMessage;
        case NetWorkFailure:
          return netWorkFailureMessage;
        default:
          return 'Unexpected Error';
      }
    }

    on<LoginEvent>((event, emit) async {
      emit(AuthenticationInitial());
      if (event.username.isEmpty) {
        emit(const AuthenticationValidatorState(
            message: "Por favor digite o seu email"));
      } else if (event.password.isEmpty) {
        emit(const AuthenticationValidatorState(
            message: "Por favor digite a sua password"));
      } else if (event.password.length < 6) {
        emit(const AuthenticationValidatorState(
            message: "A password deve ter pelo menos 6 dígitos"));
      } else {
        emit(AuthenticationLoadingState());

        final failureOrSuccess = await getIt<AuthenticateUseCase>().call(
          AuthParams(username: event.username, password: event.password),
        );
        failureOrSuccess.fold((left) {
          emit(AuthenticationError(message: _mapFailureToMessage(left)));
        }, (right) {
          emit(AuthenticationSuccessState(authModel: right as AuthModel));
        });
      }
    });
  }
}
