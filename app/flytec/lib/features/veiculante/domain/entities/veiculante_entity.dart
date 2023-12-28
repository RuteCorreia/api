import 'package:equatable/equatable.dart';

class VeiculanteEntity extends Equatable {
  final int? idVeiculante;
  final String? nome;

  const VeiculanteEntity({
    this.idVeiculante,
    this.nome,
  });

  @override
  // TODO: implement props
  List<Object?> get props => [nome, idVeiculante];
}
