// To parse this JSON data, do
//
//     final relatorioModel = relatorioModelFromJson(jsonString);

import 'dart:convert';

List<RelatorioModel> relatorioModelFromJson(String str) =>
    List<RelatorioModel>.from(
        json.decode(str).map((x) => RelatorioModel.fromJson(x)));

String relatorioModelToJson(List<RelatorioModel> data) =>
    json.encode(List<dynamic>.from(data.map((x) => x.toJson())));

class RelatorioModel {
  String? piloto;
  bool finalizado;
  String? executor;
  Cliente? cliente;
  AreaTratada? areaTratada;
  CarateristicaProduto? carateristicaProduto;
  RecomendacoesTecnicas? recomendacoesTecnicas;
  RelatorioDeAplicacao? relatorioDeAplicacao;
  ContratoServico? contratoServico;
  DadosDoResponsavel? dadosDoResponsavel;

  RelatorioModel({
    this.piloto,
    this.executor,
    this.finalizado = false,
    this.cliente,
    this.areaTratada,
    this.carateristicaProduto,
    this.recomendacoesTecnicas,
    this.relatorioDeAplicacao,
    this.contratoServico,
    this.dadosDoResponsavel,
  });

  factory RelatorioModel.fromJson(Map<String, dynamic> json) => RelatorioModel(
        piloto: json["piloto"],
        finalizado: json["finalizado"],
        executor: json["executor"],
        cliente: json["cliente"] == null
            ? Cliente(
                cpf: "",
                id: "",
                nome: "",
                endereco: "",
                rg: "",
                uf: "",
              )
            : Cliente.fromJson(json["cliente"]),
        areaTratada: json["areaTratada"] == null
            ? AreaTratada(
                cidade: "", cultura: "", extensao: "", localizacao: "", uf: "")
            : AreaTratada.fromJson(json["areaTratada"]),
        carateristicaProduto: json["carateristicaProduto"] == null
            ? CarateristicaProduto(
                adjuvante: "",
                alvoBiologico: "",
                classe: "",
                classificacaoToxicologica: "",
                cultura: "",
                dosePorHectare: "",
                nomeProduto: "",
                tipoFormulacao: "",
                tipoServico: "",
                unidadeHectare: "")
            : CarateristicaProduto.fromJson(json["carateristicaProduto"]),
        recomendacoesTecnicas: json["recomendacoesTecnicas"] == null
            ? RecomendacoesTecnicas(
                aeronave: "",
                alturaDoVoo: "",
                angulo: "",
                equipamento: "",
                larguraDaFaixa: "",
                qtdVeiculante: "",
                temperatura: "",
                tipoProduto: "",
                umidadeRelativaDoAr: "",
                unidadeVolume: "",
                veiculante: "",
                velocidadeDoVento: "",
                volumeDaAplicacao: "")
            : RecomendacoesTecnicas.fromJson(json["recomendacoesTecnicas"]),
        relatorioDeAplicacao: json["relatorioDeAplicacao"] == null
            ? RelatorioDeAplicacao(
                aplicacoes: Aplicacoes(
                    dataDaAplicacao: "",
                    horarioDeInicio: "",
                    horarioDeTermino: "",
                    horimetroFinal: "",
                    horimetroInicial: "",
                    temperaturaFinal: "",
                    temperaturaIncial: "",
                    umidadeRelativaFinal: "",
                    umidadeRelativaInicial: "",
                    ventoFinal: "",
                    ventoInicial: ""),
                cultura: "",
                dosagem: "",
                latitudeSul: "",
                localizacaoPista: "",
                longitudeOeste: "",
                observacoes: "",
                produtoAplicado: "",
                totalAreaAplicada: "",
                unidadeDosagem: "",
                unidadeVolume: "",
                volumeDeAplicacao: "")
            : RelatorioDeAplicacao.fromJson(json["relatorioDeAplicacao"]),
        contratoServico: json["contratoServico"] == null
            ? ContratoServico(
                distanciaDaPista: "",
                executor: "",
                extensao: "",
                nomePiloto: "",
                preco: "",
                tipoPreco: "",
                valorTotal: "",
                vencimento: "")
            : ContratoServico.fromJson(json["contratoServico"]),
        dadosDoResponsavel: json["dadosDoResponsavel"] == null
            ? DadosDoResponsavel(
                assinatura: "",
                cidade: "",
                cpf: "",
                data: "",
                nomeCompleto: "",
                telefone: "",
                uf: "")
            : DadosDoResponsavel.fromJson(json["dadosDoResponsavel"]),
      );

  Map<String, dynamic> toJson() => {
        "piloto": piloto,
        "finalizado": finalizado,
        "executor": executor,
        "cliente": cliente?.toJson(),
        "areaTratada": areaTratada?.toJson(),
        "carateristicaProduto": carateristicaProduto?.toJson(),
        "recomendacoesTecnicas": recomendacoesTecnicas?.toJson(),
        "relatorioDeAplicacao": relatorioDeAplicacao?.toJson(),
        "contratoServico": contratoServico?.toJson(),
        "dadosDoResponsavel": dadosDoResponsavel?.toJson(),
      };
}

class AreaTratada {
  String? uf;
  String? cidade;
  String? localizacao;
  String? cultura;
  String? extensao;
  String? pathImage;

  AreaTratada({
    this.uf,
    this.cidade,
    this.localizacao,
    this.cultura,
    this.extensao,
      this.pathImage
  });

  factory AreaTratada.fromJson(Map<String, dynamic> json) => AreaTratada(
        uf: json["uf"] ?? "",
        cidade: json["cidade"] ?? "",
        localizacao: json["localizacao"] ?? "",
        cultura: json["cultura"] ?? "",
        extensao: json["extensao"] ?? "",
      pathImage: json["pathImage"] ?? ""
      );

  Map<String, dynamic> toJson() => {
        "uf": uf,
        "cidade": cidade,
        "localizacao": localizacao,
        "cultura": cultura,
        "extensao": extensao,
        "pathImage": pathImage,
      };
}

class CarateristicaProduto {
  String? cultura;
  String? nomeProduto;
  String? classificacaoToxicologica;
  String? classe;
  String? tipoFormulacao;
  String? alvoBiologico;
  String? dosePorHectare;
  String? unidadeHectare;
  String? adjuvante;
  String? tipoServico;
  String? pathImage;

  CarateristicaProduto({
    this.cultura,
    this.nomeProduto,
    this.classificacaoToxicologica,
    this.classe,
    this.tipoFormulacao,
    this.alvoBiologico,
    this.dosePorHectare,
    this.unidadeHectare,
    this.adjuvante,
    this.tipoServico,
      this.pathImage
  });

  factory CarateristicaProduto.fromJson(Map<String, dynamic> json) =>
      CarateristicaProduto(
        cultura: json["cultura"] ?? "",
        nomeProduto: json["nomeProduto"] ?? "",
        classificacaoToxicologica: json["classificacaoToxicologica"] ?? "",
        classe: json["classe"] ?? "",
        tipoFormulacao: json["tipoFormulacao"] ?? "",
        alvoBiologico: json["alvoBiologico"] ?? "",
        dosePorHectare: json["dosePorHectare"] ?? "",
        unidadeHectare: json["unidadeHectare"] ?? "",
        adjuvante: json["adjuvante"] ?? "",
        tipoServico: json["tipoServico"] ?? "",
          pathImage: json["pathImage"] ?? ""
      );

  Map<String, dynamic> toJson() => {
        "cultura": cultura,
        "nomeProduto": nomeProduto,
        "pathImage": pathImage,
        "classificacaoToxicologica": classificacaoToxicologica,
        "classe": classe,
        "tipoFormulacao": tipoFormulacao,
        "alvoBiologico": alvoBiologico,
        "dosePorHectare": dosePorHectare,
        "unidadeHectare": unidadeHectare,
        "adjuvante": adjuvante,
        "tipoServico": tipoServico,
      };
}

class Cliente {
  String? id;
  String? nome;
  String? cpf;
  String? uf;
  String? rg;
  String? cnpj;
  String? endereco;

  Cliente(
      {this.id,
      required this.endereco,
      this.nome,
      this.cpf,
      this.uf,
      this.rg,
      this.cnpj});

  factory Cliente.fromJson(Map<String, dynamic> json) => Cliente(
      id: json["id"] ?? "",
      nome: json["nome"] ?? "",
      endereco: json["endereco"] ?? "",
      cpf: json["cpf"] ?? "",
      rg: json["rg"] ?? "",
      uf: json["uf"] ?? "",
      cnpj: json["cnpj"] ?? "");

  Map<String, dynamic> toJson() => {
        "id": id ?? "",
        "nome": nome ?? "",
        "cpf": cpf ?? "",
        "rg": rg ?? "",
        "uf": uf ?? "",
        "cnpj": cnpj ?? ""
      };
}

class ContratoServico {
  String? distanciaDaPista;
  String? preco;
  String? tipoPreco;
  String? extensao;
  String? valorTotal;
  String? vencimento;
  String? nomePiloto;
  String? executor;

  ContratoServico({
    this.distanciaDaPista,
    this.preco,
    this.tipoPreco,
    this.extensao,
    this.valorTotal,
    this.vencimento,
    this.nomePiloto,
    this.executor,
  });

  factory ContratoServico.fromJson(Map<String, dynamic> json) =>
      ContratoServico(
        distanciaDaPista: json["distanciaDaPista"] ?? "",
        preco: json["preco"] ?? "",
        tipoPreco: json["tipoPreco"] ?? "",
        extensao: json["extensao"] ?? "",
        valorTotal: json["valorTotal"] ?? "",
        vencimento: json["vencimento"] ?? "",
        nomePiloto: json["nomePiloto"] ?? "",
        executor: json["executor"] ?? "",
      );

  Map<String, dynamic> toJson() => {
        "distanciaDaPista": distanciaDaPista,
        "preco": preco,
        "tipoPreco": tipoPreco,
        "extensao": extensao,
        "valorTotal": valorTotal,
        "vencimento": vencimento,
        "nomePiloto": nomePiloto,
        "executor": executor,
      };
}

class DadosDoResponsavel {
  String? data;
  String? uf;
  String? cidade;
  String? nomeCompleto;
  String? cpf;
  String? telefone;
  String? assinatura;

  DadosDoResponsavel({
    this.data,
    this.uf,
    this.cidade,
    this.nomeCompleto,
    this.cpf,
    this.telefone,
    this.assinatura,
  });

  factory DadosDoResponsavel.fromJson(Map<String, dynamic> json) =>
      DadosDoResponsavel(
        data: json["data"] ?? "",
        uf: json["uf"] ?? "",
        cidade: json["cidade"] ?? "",
        nomeCompleto: json["nomeCompleto"] ?? "",
        cpf: json["cpf"] ?? "",
        telefone: json["telefone"] ?? "",
        assinatura: json["assinatura"] ?? "",
      );

  Map<String, dynamic> toJson() => {
        "data": data,
        "uf": uf,
        "cidade": cidade,
        "nomeCompleto": nomeCompleto,
        "cpf": cpf,
        "telefone": telefone,
        "assinatura": assinatura,
      };
}

class RecomendacoesTecnicas {
  String? veiculante;
  String? qtdVeiculante;
  String? larguraDaFaixa;
  String? volumeDaAplicacao;
  String? unidadeVolume;
  String? aeronave;
  String? alturaDoVoo;
  String? temperatura;
  String? umidadeRelativaDoAr;
  String? velocidadeDoVento;
  String? tipoProduto;
  String? equipamento;
  String? angulo;

  RecomendacoesTecnicas({
    this.veiculante,
    this.qtdVeiculante,
    this.larguraDaFaixa,
    this.volumeDaAplicacao,
    this.unidadeVolume,
    this.aeronave,
    this.alturaDoVoo,
    this.temperatura,
    this.umidadeRelativaDoAr,
    this.velocidadeDoVento,
    this.tipoProduto,
    this.equipamento,
    this.angulo,
  });

  factory RecomendacoesTecnicas.fromJson(Map<String, dynamic> json) =>
      RecomendacoesTecnicas(
        veiculante: json["veiculante"] ?? "",
        qtdVeiculante: json["qtdVeiculante"] ?? "",
        larguraDaFaixa: json["larguraDaFaixa"] ?? "",
        volumeDaAplicacao: json["volumeDaAplicacao"] ?? "",
        unidadeVolume: json["unidadeVolume"] ?? "",
        aeronave: json["aeronave"] ?? "",
        alturaDoVoo: json["alturaDoVoo"] ?? "",
        temperatura: json["temperatura"] ?? "",
        umidadeRelativaDoAr: json["umidadeRelativaDoAr"] ?? "",
        velocidadeDoVento: json["velocidadeDoVento"] ?? "",
        tipoProduto: json["tipoProduto"] ?? "",
        equipamento: json["equipamento"] ?? "",
        angulo: json["angulo"] ?? "",
      );

  Map<String, dynamic> toJson() => {
        "veiculante": veiculante,
        "qtdVeiculante": qtdVeiculante,
        "larguraDaFaixa": larguraDaFaixa,
        "volumeDaAplicacao": volumeDaAplicacao,
        "unidadeVolume": unidadeVolume,
        "aeronave": aeronave,
        "alturaDoVoo": alturaDoVoo,
        "temperatura": temperatura,
        "umidadeRelativaDoAr": umidadeRelativaDoAr,
        "velocidadeDoVento": velocidadeDoVento,
        "tipoProduto": tipoProduto,
        "equipamento": equipamento,
        "angulo": angulo,
      };
}

class RelatorioDeAplicacao {
  String? cultura;
  String? produtoAplicado;
  String? dosagem;
  String? unidadeDosagem;
  String? volumeDeAplicacao;
  String? unidadeVolume;
  String? totalAreaAplicada;
  String? localizacaoPista;
  String? latitudeSul;
  String? longitudeOeste;
  String? observacoes;
  String? densidade;
  String? log;

  Aplicacoes? aplicacoes;

  RelatorioDeAplicacao({
    this.cultura,
    this.produtoAplicado,
    this.dosagem,
    this.unidadeDosagem,
    this.volumeDeAplicacao,
    this.unidadeVolume,
    this.totalAreaAplicada,
    this.localizacaoPista,
    this.densidade,
    this.latitudeSul,
    this.longitudeOeste,
    this.observacoes,
    this.log,
    this.aplicacoes,
  });

  factory RelatorioDeAplicacao.fromJson(Map<String, dynamic> json) =>
      RelatorioDeAplicacao(
        cultura: json["cultura"] ?? "",
        densidade: json["densidade"] ?? "",
        produtoAplicado: json["produtoAplicado"] ?? "",
        dosagem: json["dosagem"] ?? "",
        log: json["log"] ?? "",
        unidadeDosagem: json["unidadeDosagem"] ?? "",
        volumeDeAplicacao: json["volumeDeAplicacao"] ?? "",
        unidadeVolume: json["unidadeVolume"] ?? "",
        totalAreaAplicada: json["totalAreaAplicada"] ?? "",
        localizacaoPista: json["localizacaoPista"] ?? "",
        latitudeSul: json["latitudeSul"] ?? "",
        longitudeOeste: json["longitudeOeste"] ?? "",
        observacoes: json["observacoes"] ?? "",
        aplicacoes: json["aplicacoes"] == null
            ? Aplicacoes(
                dataDaAplicacao: "",
                horarioDeInicio: "",
                horarioDeTermino: "",
                horimetroFinal: "",
                horimetroInicial: "",
                temperaturaFinal: "",
                temperaturaIncial: "",
                umidadeRelativaFinal: "",
                umidadeRelativaInicial: "",
                ventoFinal: "",
                ventoInicial: "",
              )
            : Aplicacoes.fromJson(json["aplicacoes"]),
      );

  Map<String, dynamic> toJson() => {
        "cultura": cultura ?? "",
        "produtoAplicado": produtoAplicado ?? "",
        "dosagem": dosagem ?? "",
        "unidadeDosagem": unidadeDosagem ?? "",
        "volumeDeAplicacao": volumeDeAplicacao ?? "",
        "unidadeVolume": unidadeVolume ?? "",
        "totalAreaAplicada": totalAreaAplicada ?? "",
        "localizacaoPista": localizacaoPista ?? "",
        "latitudeSul": latitudeSul ?? "",
        "longitudeOeste": longitudeOeste ?? "",
        "observacoes": observacoes ?? "",
        "aplicacoes": aplicacoes?.toJson(),
        "densidade": densidade ?? "",
        "log": log ?? ""
      };
}

class Aplicacoes {
  String? dataDaAplicacao;
  String? horarioDeInicio;
  String? horimetroInicial;
  String? horarioDeTermino;
  String? horimetroFinal;
  String? temperaturaIncial;
  String? temperaturaFinal;
  String? umidadeRelativaInicial;
  String? umidadeRelativaFinal;
  String? ventoInicial;
  String? ventoFinal;
  String? pathImage;

  Aplicacoes({
    this.dataDaAplicacao,
    this.horarioDeInicio,
    this.horimetroInicial,
    this.horarioDeTermino,
    this.horimetroFinal,
    this.temperaturaIncial,
    this.temperaturaFinal,
    this.umidadeRelativaInicial,
    this.umidadeRelativaFinal,
    this.ventoInicial,
    this.ventoFinal,
      this.pathImage
  });

  factory Aplicacoes.fromJson(Map<String, dynamic> json) => Aplicacoes(
        dataDaAplicacao: json["dataDaAplicacao"] ?? "",
        horarioDeInicio: json["horarioDeInicio"] ?? "",
        horimetroInicial: json["horimetroInicial"] ?? "",
        horarioDeTermino: json["horarioDeTermino"] ?? "",
        horimetroFinal: json["horimetroFinal"] ?? "",
        temperaturaIncial: json["temperaturaIncial"] ?? "",
        temperaturaFinal: json["temperaturaFinal"] ?? "",
        umidadeRelativaInicial: json["umidadeRelativaInicial"] ?? "",
        umidadeRelativaFinal: json["umidadeRelativaFinal"] ?? "",
        ventoInicial: json["ventoInicial"] ?? "",
        ventoFinal: json["ventoFinal"] ?? "",
      pathImage: json["pathImage"] ?? ""
      );

  Map<String, dynamic> toJson() => {
        "dataDaAplicacao": dataDaAplicacao,
        "horarioDeInicio": horarioDeInicio,
        "horimetroInicial": horimetroInicial,
        "horarioDeTermino": horarioDeTermino,
        "horimetroFinal": horimetroFinal,
        "temperaturaIncial": temperaturaIncial,
        "temperaturaFinal": temperaturaFinal,
        "umidadeRelativaInicial": umidadeRelativaInicial,
        "umidadeRelativaFinal": umidadeRelativaFinal,
        "ventoInicial": ventoInicial,
        "ventoFinal": ventoFinal,
        "pathImage": pathImage,
      };
}
