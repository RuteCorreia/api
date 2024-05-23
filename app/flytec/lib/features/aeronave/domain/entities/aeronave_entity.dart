import 'package:equatable/equatable.dart';
import 'package:flytec/features/aeronave/data/models/tipo_aeronave_enum.dart';

class AeroNaveEntity extends Equatable {
  final int? id;
  final int? idEmpresa;
  final String? prefixo;
  final String? combustivel;
  final int? capacidadeDeCarga;
  final String? horimetro;
  final String? modelo;
  final String? serialNumber;
  final int? tipo;


  const AeroNaveEntity({
    this.id,
    this.idEmpresa,
    this.prefixo,
    this.combustivel,
    this.capacidadeDeCarga,
    this.horimetro,
    this.modelo,
    this.serialNumber,
    this.tipo
  });

  @override
  List<Object?> get props =>
      [id, idEmpresa, prefixo, combustivel, capacidadeDeCarga, horimetro, tipo];
}
