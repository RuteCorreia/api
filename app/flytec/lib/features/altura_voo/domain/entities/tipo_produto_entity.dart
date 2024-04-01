import 'package:equatable/equatable.dart';

class AlturaVooEntity extends Equatable {
  final int? id;
  final String? nome;

  const AlturaVooEntity({
    this.id,
    this.nome,
  });

  @override
  List<Object?> get props => [id, nome];
}
