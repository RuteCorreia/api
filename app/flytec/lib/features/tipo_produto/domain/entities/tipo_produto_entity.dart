import 'package:equatable/equatable.dart';

class TipoProdutoEntity extends Equatable {
  final int? id;
  final String? nome;

  const TipoProdutoEntity({
    this.id,
    this.nome,
  });

  @override
  // TODO: implement props
  List<Object?> get props => [id, nome];
}
