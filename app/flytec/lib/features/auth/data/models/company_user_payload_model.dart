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
      idEmpresa: json["IdEmpresa"]?.toString() ?? '',
      nomeEmpresa: json["NomeEmpresa"]?.toString() ?? '',
      telefoneEmpresa: json["TelefoneEmpresa"]?.toString() ?? '',
      emailEmpresa: json["EmailEmpresa"]?.toString() ?? '',
      inscricaoEstadualEmpresa:
          json["inscricaoEstadualEmpresa"]?.toString() ?? '',
      nrCDAEmpresa: json["nrCDAEmpresa"]?.toString() ?? '',
      registroMapaEmpresa: json["registroMapaEmpresa"]?.toString() ?? '',
      cepEmpresa: json["cepEmpresa"]?.toString() ?? '',
      enderecoEmpresa: json["enderecoEmpresa"]?.toString() ?? '',
      numeroEmpresa: json["numeroEmpresa"]?.toString() ?? '',
      cidadeEmpresa: json["cidadeEmpresa"]?.toString() ?? '',
      estadoEmpresa: json["estadoEmpresa"]?.toString() ?? '',
      logoEmpresa: json["logoEmpresa"]?.toString() ?? '',
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
