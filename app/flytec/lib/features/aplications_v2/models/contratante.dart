import 'package:flytec/features/aplications/data/models/clientes_model.dart';

class Contratante {
  String? tipoContratante;
  String? nome;
  String? cpf;
  String? endereco;
  String? rg;
  String? uf;
  String? cidade;
  String? cnpj;
  String? inscricaoEstadual;
  String? id;

  Contratante({
    this.tipoContratante,
    this.nome,
    this.cpf,
    this.endereco,
    this.rg,
    this.uf,
    this.cidade,
    this.cnpj,
    this.inscricaoEstadual,
    this.id,
  });

  Map<String,dynamic> toMap() {
    return {
      'tipoContratante': tipoContratante,
      'nome': nome,
      'cpf': cpf,
      'endereco': endereco,
      'rg': rg,
      'uf': uf,
      'id': id,
      'cidade': cidade,
      'cnpj': cnpj,
      'inscricaoEstadual': inscricaoEstadual,
    };
  }
  factory Contratante.fromJson(Map<String, dynamic>? json) {
    if(json==null) return Contratante();
    return Contratante(
      tipoContratante: json['tipoContratante'],
      nome: json['nome'],
      cpf: json['cpf'],
      endereco: json['endereco'],
      rg: json['rg'],
      uf: json['uf'],
      cidade: json['cidade'],
      cnpj: json['cnpj'],
      id: json['id'] ?? '',
      inscricaoEstadual: json['inscricaoEstadual'],
    );
  }

  factory Contratante.fromCliente(ClientesModel cliente) => Contratante(
      tipoContratante:
          cliente.idTipoCliente == 0 ? "Pessoa Física" : "Pessoa Jurídica",
      nome: cliente.nomeCliente,
      cpf: cliente.cpf,
      endereco: cliente.endereco,
      rg: cliente.rg,
      uf: cliente.uf,
      cidade: cliente.cidade,
      cnpj: cliente.cnpj,
      inscricaoEstadual: cliente.inscricaoEstadual,
      id: cliente.idCliente.toString());
}
