import 'package:equatable/equatable.dart';

class EquipamentoEntity extends Equatable {
  final int? id;
  final String? nome;

  const EquipamentoEntity({
    this.id,
    this.nome,
  });

  @override
  List<Object?> get props => [id, nome];
}
