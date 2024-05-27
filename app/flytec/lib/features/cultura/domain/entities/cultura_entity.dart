import 'package:equatable/equatable.dart';

class CulturaEntity extends Equatable {
  final int? idCultura;
  final String? nome;
  final String? alvoBiologico;

  const CulturaEntity({
    this.idCultura,
    this.nome,
    this.alvoBiologico,
  });

  @override
  List<Object?> get props => [idCultura, nome, alvoBiologico];
}
