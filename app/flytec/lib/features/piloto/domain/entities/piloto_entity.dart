import 'package:equatable/equatable.dart';

class PilotoEntity extends Equatable {
  final int? idPiloto;
  final int? idEmpresa;
  final String? nomePiloto;
  final String? email;
  final String? senha;
  final String? cdac;
  final String? assinatura;
  final String? porcentagemComissao;

  const PilotoEntity({
    this.idPiloto,
    this.idEmpresa,
    this.nomePiloto,
    this.email,
    this.senha,
    this.cdac,
    this.assinatura,
    this.porcentagemComissao,
  });

  @override
  List<Object?> get props => [
        porcentagemComissao,
        assinatura,
        cdac,
        senha,
        email,
        nomePiloto,
        idEmpresa,
        idPiloto
      ];
}
