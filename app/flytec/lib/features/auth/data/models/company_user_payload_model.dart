class CompanyUserPayloadModel {
  String? idEmpresa;
  String? nomeEmpresa;
  String? telefoneEmpresa;
  String? emailEmpresa;
  String? inscricaoEstadualEmpresa;
  String? nrCDAEmpresa;
  String? registroMapaEmpresa;
  String? cepEmpresa;
  String? enderecoEmpresa;
  String? numeroEmpresa;
  String? cidadeEmpresa;
  String? estadoEmpresa;
  String? logoEmpresa;

  CompanyUserPayloadModel({
    this.idEmpresa,
    this.nomeEmpresa,
    this.telefoneEmpresa,
    this.emailEmpresa,
    this.inscricaoEstadualEmpresa,
    this.nrCDAEmpresa,
    this.registroMapaEmpresa,
    this.cepEmpresa,
    this.enderecoEmpresa,
    this.numeroEmpresa,
    this.cidadeEmpresa,
    this.estadoEmpresa,
    this.logoEmpresa,
  });

  factory CompanyUserPayloadModel.fromJson(Map<String, dynamic> json) {
    return CompanyUserPayloadModel(
      idEmpresa: json["IdEmpresa"].toString().isEmpty
          ? null
          : json["IdEmpresa"]?.toString(),
      nomeEmpresa: json["NomeEmpresa"].toString().isEmpty
          ? null
          : json["NomeEmpresa"]?.toString(),
      telefoneEmpresa: json["TelefoneEmpresa"].toString().isEmpty
          ? null
          : json["TelefoneEmpresa"]?.toString(),
      emailEmpresa: json["EmailEmpresa"].toString().isEmpty
          ? null
          : json["EmailEmpresa"]?.toString(),
      inscricaoEstadualEmpresa:
          json["inscricaoEstadualEmpresa"].toString().isEmpty
              ? null
              : json["inscricaoEstadualEmpresa"]?.toString(),
      nrCDAEmpresa: json["nrCDAEmpresa"].toString().isEmpty
          ? null
          : json["nrCDAEmpresa"]?.toString(),
      registroMapaEmpresa: json["registroMapaEmpresa"].toString().isEmpty
          ? null
          : json["registroMapaEmpresa"]?.toString(),
      cepEmpresa: json["cepEmpresa"].toString().isEmpty
          ? null
          : json["cepEmpresa"]?.toString(),
      enderecoEmpresa: json["enderecoEmpresa"].toString().isEmpty
          ? null
          : json["enderecoEmpresa"]?.toString(),
      numeroEmpresa: json["numeroEmpresa"].toString().isEmpty
          ? null
          : json["numeroEmpresa"]?.toString(),
      cidadeEmpresa: json["cidadeEmpresa"].toString().isEmpty
          ? null
          : json["cidadeEmpresa"]?.toString(),
      estadoEmpresa: json["estadoEmpresa"].toString().isEmpty
          ? null
          : json["estadoEmpresa"]?.toString(),
      logoEmpresa: json["logoEmpresa"].toString().isEmpty
          ? null
          : json["logoEmpresa"]?.toString(),
    );
  }

  Map<String, dynamic> toJson() => {
        "IdEmpresa": idEmpresa,
        "NomeEmpresa": nomeEmpresa,
        "TelefoneEmpresa": telefoneEmpresa,
        "EmailEmpresa": emailEmpresa,
        "inscricaoEstadualEmpresa": inscricaoEstadualEmpresa,
        "nrCDAEmpresa": nrCDAEmpresa,
        "registroMapaEmpresa": registroMapaEmpresa,
        "cepEmpresa": cepEmpresa,
        "enderecoEmpresa": enderecoEmpresa,
        "numeroEmpresa": numeroEmpresa,
        "cidadeEmpresa": cidadeEmpresa,
        "estadoEmpresa": estadoEmpresa,
        "logoEmpresa": logoEmpresa,
      };
}
