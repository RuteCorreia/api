import 'package:equatable/equatable.dart';

class ExecutorEntity extends Equatable {
  final int? idExecutor;
  final int? idEmpresa;
  final String? nome;
  final String? email;
  final String? senha;
  final String? cfta;
  final String? assinatura;

  const ExecutorEntity({
    this.idExecutor,
    this.idEmpresa,
    this.nome,
    this.email,
    this.senha,
    this.cfta,
    this.assinatura,
  });

  @override
  List<Object?> get props =>
      [idExecutor, idEmpresa, nome, email, senha, cfta, assinatura];
}
