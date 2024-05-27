import 'package:equatable/equatable.dart';

class EngenheiroEntity extends Equatable {
  final String? idEngenheiro;
  final String? nomeEngenheiro;
  final String? email;
  final String? senha;
  final String? crea;
  final String? assinatura;

  const EngenheiroEntity({
    this.idEngenheiro,
    this.nomeEngenheiro,
    this.email,
    this.senha,
    this.crea,
    this.assinatura,
  });

  @override
  List<Object?> get props => [
        assinatura,
        crea,
        senha,
        email,
        nomeEngenheiro,
        idEngenheiro
      ];
}
