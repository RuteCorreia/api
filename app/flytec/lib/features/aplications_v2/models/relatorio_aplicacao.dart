import 'package:flytec/features/aplications_v2/enums/report_dashboard_state.dart';
import 'package:flytec/features/aplications_v2/models/aplicacao.dart';
import 'package:flytec/features/aplications_v2/models/caracteristicas_produto.dart';
import 'package:flytec/features/aplications_v2/models/contrato_prestacao_servico.dart';
import 'package:flytec/features/aplications_v2/models/dados_responsavel.dart';
import 'package:flytec/features/aplications_v2/models/identificacao_area.dart';
import 'package:flytec/features/aplications_v2/models/identificacao_contratante.dart';

class RelatorioAplicacoes {
  String? piloto;
  String? executor;
  DateTime data;
  int? identificadorRelatorio;
  ReportDashBoardState? state;

  IdentificacaoContratante? identificacaoContratante;
  IdentificacaoArea? identificacaoArea;
  CaracteristicasProduto? caracteristicasProduto;
  Aplicacao? aplicacaoRelatorio;
  ContratoPrestacaoServicos? contratoPrestacaoServicos;
  DadosResponsavel? dadosResponsavel;

  RelatorioAplicacoes({
    this.piloto,
    this.executor,
    required this.data,
    required this.state,
    this.identificadorRelatorio,
    this.identificacaoContratante,
    this.identificacaoArea,
    this.caracteristicasProduto,
    this.aplicacaoRelatorio,
    this.contratoPrestacaoServicos,
    this.dadosResponsavel,
  });

  Map<String, dynamic> toMap() {
    return {
      'piloto': piloto,
      'executor': executor,
      'data': data.millisecondsSinceEpoch.toString(),
      'state': state?.name,
    };
  }

  factory RelatorioAplicacoes.fromJson(Map<String, dynamic> json) {
    return RelatorioAplicacoes(
      data: DateTime.fromMillisecondsSinceEpoch(int.tryParse(json['data'])!),
      state: json['state'] != null
          ? ReportDashBoardState.values
              .firstWhere((element) => element.name == json['state'])
          : ReportDashBoardState.Incompleto,
    );
  }

  // RelatorioModel toRelatorioModel() {
  //   return RelatorioModel(
  //     piloto: piloto,
  //     executor: executor,
  //     finalizado: state == ReportDashBoardState.Pronto,
  //     areaTratada: AreaTratada(
  //         cidade: identificacaoArea?.cidade,
  //         uf: identificacaoArea?.uf,
  //         cultura: identificacaoArea?.cultura,
  //         extensao: identificacaoArea?.extensao,
  //         localizacao: identificacaoArea?.localizacao,
  //         pathImage: null),
  //     carateristicaProduto: CarateristicaProduto(
  //         adjuvante: caracteristicasProduto?.adjuvante,
  //         alvoBiologico: caracteristicasProduto?.alvoBiologico,
  //         classe: caracteristicasProduto?.classe,
  //         cultura: caracteristicasProduto?.cultura,
  //         classificacaoToxicologica:
  //             caracteristicasProduto?.classificacaoToxicologica,
  //         dosePorHectare: caracteristicasProduto?.doseProdutoHectare,
  //         nomeProduto: caracteristicasProduto?.nomeProduto,
  //         pathImage: null,
  //         tipoFormulacao: caracteristicasProduto?.tipoFormulacao,
  //         tipoServico: caracteristicasProduto?.tipoServico,
  //         unidadeHectare: caracteristicasProduto?.unidadeDoseProdutoHectare),
  //     cliente: Cliente(
  //         endereco: identificacaoContratante?.endereco,
  //         nome: identificacaoContratante?.nomeContratante,
  //         cidade: identificacaoContratante?.cidade,
  //         uf: identificacaoContratante?.uf,
  //         cnpj: identificacaoContratante?.documento,
  //         cpf: identificacaoContratante?.documento,
  //         id: null,
  //         inscricaoEstadual: identificacaoContratante?.inscricaoEstadual,
  //         rg: identificacaoContratante?.documento),
  //     contratoServico: ContratoServico(
  //         distanciaDaPista:
  //             contratoPrestacaoServicos?.distanciaPista.toString(),
  //         executor: contratoPrestacaoServicos?.executor,
  //         extensao: contratoPrestacaoServicos?.extensaoHorasHa,
  //         nomePiloto: contratoPrestacaoServicos?.nomePiloto,
  //         preco: contratoPrestacaoServicos?.preco.toString(),
  //         tipoPreco: contratoPrestacaoServicos?.precoUnidade,
  //         valorTotal: contratoPrestacaoServicos?.valorTotal,
  //         vencimento: contratoPrestacaoServicos
  //             ?.vencimento.millisecondsSinceEpoch
  //             .toString()),
  //     dadosDoResponsavel: DadosDoResponsavel(),
  //     dashBoardState: DashBoardState.values
  //         .firstWhere((element) => element.name == state?.name),
  //     numeroRelatorio: identificadorRelatorio,
  //     recomendacoesTecnicas: RecomendacoesTecnicas(aeronave: re ,alturaDoVoo: ,angulo: ,equipamento: ,larguraDaFaixa: ,qtdVeiculante: ,temperatura: ,tipoProduto: ,umidadeRelativaDoAr: ,unidadeVolume: ,veiculante: ,velocidadeDoVento: ,volumeDaAplicacao: ),
  //     relatorioDeAplicacao: RelatorioDeAplicacao(aplicacoes: ,),
  //   );
  // }

}
