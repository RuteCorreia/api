import 'package:equatable/equatable.dart';

class ProdutoEntity extends Equatable {
  final int? id;
  final int? idCultura;
  final String? nome;
  final String? classificacaoToxicologica;
  final String? classe;
  final String? tipoDeFormulacao;
  final String? tipoServico;

  const ProdutoEntity({
    this.id,
    this.idCultura,
    this.nome,
    this.classificacaoToxicologica,
    this.classe,
    this.tipoDeFormulacao,
    this.tipoServico,
  });

  @override
  List<Object?> get props => [
        id,
        idCultura,
        nome,
        classificacaoToxicologica,
        classe,
        tipoDeFormulacao,
        tipoServico
      ];
}
