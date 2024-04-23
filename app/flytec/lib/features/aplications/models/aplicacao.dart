import 'package:flytec/core/utils/util.dart';
import 'package:flytec/core/enums/dashboard_state.dart';
import 'package:flytec/features/aplications/models/caracteristicas_produto_aplicado.dart';
import 'package:flytec/features/aplications/models/contratante.dart';
import 'package:flytec/features/aplications/models/contrato_prestacao_servico.dart';
import 'package:flytec/features/aplications/models/dados_responsavel.dart';
import 'package:flytec/features/aplications/models/identificacao_area_tratada.dart';
import 'package:flytec/features/aplications/models/recomendacoes_tecnicas.dart';
import 'package:flytec/features/aplications/models/relatorio_aplicacao.dart';

class Aplicacao {
  String? piloto;
  String? executor;
  int? id;
  String? refDocument;
  DashBoardState? state;
  String? data;
  Contratante? contratante;
  IdentificacaoAreaTratada? identificacaoAreaTratada;
  CaracteristicasProdutoAplicado? caracteristicasProdutoAplicado;
  RecomendacoesTecnicas? recomendacoesTecnicas;
  RelatorioAplicacao? relatorioAplicacao;
  ContratoPrestacaoServico? contratoPrestacaoServico;
  DadosResponsavel? dadosResponsavel;
  String? refUsuario;

  int? contratanteId;
  int? identificacaoAreaTratadaId;
  int? caracteristicasProdutoAplicadoId;
  int? recomendacoesTecnicasId;
  int? relatorioAplicacaoId;
  int? contratoPrestacaoServicoId;
  int? dadosResponsavelId;

  Aplicacao(
      {this.piloto,
      this.executor,
      this.id,
      this.refDocument,
      this.contratante,
      this.identificacaoAreaTratada,
      this.caracteristicasProdutoAplicado,
      this.recomendacoesTecnicas,
      this.relatorioAplicacao,
      this.contratoPrestacaoServico,
      this.dadosResponsavel,
      this.state,
      this.data,
      this.refUsuario,
      this.contratanteId,
      this.identificacaoAreaTratadaId,
      this.caracteristicasProdutoAplicadoId,
      this.recomendacoesTecnicasId,
      this.relatorioAplicacaoId,
      this.contratoPrestacaoServicoId,
      this.dadosResponsavelId});

  Map<String, dynamic> toMap() {
    return {
      'piloto': piloto,
      'executor': executor,
      'state': state?.index ?? DashBoardState.Incompleto.index,
      'data': data,
      'refUsuario': refUsuario
    };
  }

  factory Aplicacao.fromJson(Map<String, dynamic> json) {
    return Aplicacao(
        piloto: json['piloto'] ?? '',
        executor: json['executor'] ?? '',
        id: json['id'] ?? 0,
        refUsuario: json['refUsuario'] ?? "'",
        state: DashBoardState.values.firstWhere(
            (state) => state.index == json['state'],
            orElse: () => DashBoardState.Incompleto),
        data: json['data'] ?? '',
        contratanteId: json['contratante_id'],
        identificacaoAreaTratadaId: json['identificacaoAreaTratada_id'],
        caracteristicasProdutoAplicadoId:
            json['caracteristicasProdutoAplicado_id'],
        recomendacoesTecnicasId: json['recomendacoesTecnicas_id'],
        relatorioAplicacaoId: json['relatorioAplicacao_id'],
        contratoPrestacaoServicoId: json['contratoPrestacaoServico_id'],
        dadosResponsavelId: json['dadosResponsavel_id']);
  }

  bool verifyFieldsMandatory({bool showToast = true}) {
    if (identificacaoAreaTratada == null ||
        identificacaoAreaTratada?.id == null) {
      Util.toastAlerta('Identificação da área tratada é obrigatório',
          showToast: showToast);
      return false;
    }
    if (identificacaoAreaTratada?.localizacao == null ||
        identificacaoAreaTratada!.localizacao!.isEmpty) {
      Util.toastAlerta('Localização da área tratada é obrigatório',
          showToast: showToast);
      return false;
    }
    if (identificacaoAreaTratada?.cultura == null ||
        identificacaoAreaTratada!.cultura!.isEmpty) {
      Util.toastAlerta('Cultura da área tratada é obrigatório',
          showToast: showToast);
      return false;
    }
    if (identificacaoAreaTratada?.extensao == null ||
        identificacaoAreaTratada!.extensao!.isEmpty) {
      Util.toastAlerta('Extensão da área tratada é obrigatório',
          showToast: showToast);
      return false;
    }
    if (identificacaoAreaTratada?.croquiArea == null) {
      Util.toastAlerta('Imagem da área tratada é obrigatório',
          showToast: showToast);
      return false;
    }

    if (caracteristicasProdutoAplicado == null ||
        caracteristicasProdutoAplicado?.id == null) {
      Util.toastAlerta('Caracteristicas do produto aplicado é obrigatório',
          showToast: showToast);
      return false;
    }
    if (caracteristicasProdutoAplicado?.cultura == null ||
        caracteristicasProdutoAplicado!.cultura!.isEmpty) {
      Util.toastAlerta(
          'Cultura das Caracteristicas do produto aplicado é obrigatório',
          showToast: showToast);
      return false;
    }
    if (caracteristicasProdutoAplicado?.nomeProduto == null ||
        caracteristicasProdutoAplicado!.nomeProduto!.isEmpty) {
      Util.toastAlerta(
          'Produto das Caracteristicas do produto aplicado é obrigatório',
          showToast: showToast);
      return false;
    }
    if (caracteristicasProdutoAplicado?.classificacaoToxicologica == null) {
      Util.toastAlerta(
          'Classificação Toxicológica das Caracteristicas do produto aplicado é obrigatório',
          showToast: showToast);
      return false;
    }
    if (caracteristicasProdutoAplicado?.classe == null ||
        caracteristicasProdutoAplicado!.classe!.isEmpty) {
      Util.toastAlerta(
          'Classe das Caracteristicas do produto aplicado é obrigatório',
          showToast: showToast);
      return false;
    }
    if (caracteristicasProdutoAplicado?.tipoFormulacao == null ||
        caracteristicasProdutoAplicado!.tipoFormulacao!.isEmpty) {
      Util.toastAlerta(
          'Tipo da Formulação das Caracteristicas do produto aplicado é obrigatório',
          showToast: showToast);
      return false;
    }
    if (caracteristicasProdutoAplicado?.doseProdutoHectare == null ||
        caracteristicasProdutoAplicado!.doseProdutoHectare!.isEmpty) {
      Util.toastAlerta(
          'Dose do Produto das Caracteristicas do produto aplicado é obrigatório',
          showToast: showToast);
      return false;
    }
    if (caracteristicasProdutoAplicado?.unidadeDoseProdutoHectare == null ||
        caracteristicasProdutoAplicado!.unidadeDoseProdutoHectare!.isEmpty) {
      Util.toastAlerta(
          'Unidade da Dose do Produto das Caracteristicas do produto aplicado é obrigatório',
          showToast: showToast);
      return false;
    }
    if (caracteristicasProdutoAplicado?.tipoServico == null ||
        caracteristicasProdutoAplicado!.tipoServico!.isEmpty) {
      Util.toastAlerta(
          'Tipo do Serviço das Caracteristicas do produto aplicado é obrigatório',
          showToast: showToast);
      return false;
    }
    if (caracteristicasProdutoAplicado?.receiturarioAgronomico == null) {
      Util.toastAlerta(
          'Imagem do Receituário das Caracteristicas do produto aplicado é obrigatório',
          showToast: showToast);
      return false;
    }
    if (caracteristicasProdutoAplicado?.dataEmissao == null ||
        caracteristicasProdutoAplicado!.dataEmissao!.isEmpty) {
      Util.toastAlerta(
          'Data de Emissão do Receituário das Caracteristicas do produto aplicado é obrigatório',
          showToast: showToast);
      return false;
    }

    if (caracteristicasProdutoAplicado?.numeroReceituarioAgronomico == null ||
        caracteristicasProdutoAplicado!.numeroReceituarioAgronomico!.isEmpty) {
      Util.toastAlerta(
          'Número do Receituário das Caracteristicas do produto aplicado é obrigatório',
          showToast: showToast);
      return false;
    }

    if (contratante == null || contratante?.id == null) {
      Util.toastAlerta('Contratante é obrigatório', showToast: showToast);
      return false;
    }

    if (contratoPrestacaoServico == null ||
        contratoPrestacaoServico?.id == null) {
      Util.toastAlerta('Contrato da Prestação do Serviço é obrigatório',
          showToast: showToast);
      return false;
    }
    if (contratoPrestacaoServico?.distanciaPista == null ||
        contratoPrestacaoServico!.distanciaPista!.isEmpty) {
      Util.toastAlerta(
          'Distância da Pista do Contrato da Prestação do Serviço é obrigatório',
          showToast: showToast);
      return false;
    }

    if (contratoPrestacaoServico?.preco == null ||
        contratoPrestacaoServico!.preco!.isEmpty) {
      Util.toastAlerta(
          'Preço do Contrato da Prestação do Serviço é obrigatório',
          showToast: showToast);
      return false;
    }

    if (contratoPrestacaoServico?.extensao == null ||
        contratoPrestacaoServico!.extensao!.isEmpty) {
      Util.toastAlerta(
          'Extensão do Contrato da Prestação do Serviço é obrigatório',
          showToast: showToast);
      return false;
    }

    if (contratoPrestacaoServico?.valorTotal == null ||
        contratoPrestacaoServico!.valorTotal!.isEmpty) {
      Util.toastAlerta(
          'Valor Total do Contrato da Prestação do Serviço é obrigatório',
          showToast: showToast);
      return false;
    }

    if (executor == null || executor!.isEmpty) {
      Util.toastAlerta('Executor é obrigatório', showToast: showToast);
      return false;
    }

    if (piloto == null || piloto!.isEmpty) {
      Util.toastAlerta('Piloto é obrigatório', showToast: showToast);
      return false;
    }

    if (recomendacoesTecnicas == null || recomendacoesTecnicas!.id == null) {
      Util.toastAlerta('Recomendações Técnicas é obrigatório',
          showToast: showToast);
      return false;
    }

    if (recomendacoesTecnicas?.veiculante == null ||
        recomendacoesTecnicas!.veiculante!.isEmpty) {
      Util.toastAlerta('Veiculante das Recomendações Técnicas é obrigatório',
          showToast: showToast);
      return false;
    }

    if (recomendacoesTecnicas?.larguraFaixa == null ||
        recomendacoesTecnicas!.larguraFaixa!.isEmpty) {
      Util.toastAlerta(
          'Largura da Faixa das Recomendações Técnicas é obrigatório',
          showToast: showToast);
      return false;
    }

    if (recomendacoesTecnicas?.volumeAplicacao == null ||
        recomendacoesTecnicas!.volumeAplicacao!.isEmpty) {
      Util.toastAlerta(
          'Volume Aplicação das Recomendações Técnicas é obrigatório',
          showToast: showToast);
      return false;
    }

    if (recomendacoesTecnicas?.unidadevolumeAplicacao == null ||
        recomendacoesTecnicas!.unidadevolumeAplicacao!.isEmpty) {
      Util.toastAlerta(
          'Unidade do Volume Aplicação das Recomendações Técnicas é obrigatório',
          showToast: showToast);
      return false;
    }

    if (recomendacoesTecnicas?.aeronave == null ||
        recomendacoesTecnicas!.aeronave!.isEmpty) {
      Util.toastAlerta('Aeronave das Recomendações Técnicas é obrigatório',
          showToast: showToast);
      return false;
    }

    if (recomendacoesTecnicas?.alturaVoo == null ||
        recomendacoesTecnicas!.alturaVoo!.isEmpty) {
      Util.toastAlerta('Altura do Voo das Recomendações Técnicas é obrigatório',
          showToast: showToast);
      return false;
    }

    if (recomendacoesTecnicas?.temperatura == null ||
        recomendacoesTecnicas!.temperatura!.isEmpty) {
      Util.toastAlerta('Temperatura das Recomendações Técnicas é obrigatório',
          showToast: showToast);
      return false;
    }

    if (recomendacoesTecnicas?.umidadeRelativaAr == null ||
        recomendacoesTecnicas!.umidadeRelativaAr!.isEmpty) {
      Util.toastAlerta(
          'Umidade Relativa do Ar das Recomendações Técnicas é obrigatório',
          showToast: showToast);
      return false;
    }

    if (recomendacoesTecnicas?.velocidadeVento == null ||
        recomendacoesTecnicas!.umidadeRelativaAr!.isEmpty) {
      Util.toastAlerta(
          'Velocidade do Vento das Recomendações Técnicas é obrigatório',
          showToast: showToast);
      return false;
    }

    if (recomendacoesTecnicas?.tipoProduto == null ||
        recomendacoesTecnicas!.tipoProduto!.isEmpty) {
      Util.toastAlerta(
          'Tipo do Produto das Recomendações Técnicas é obrigatório',
          showToast: showToast);
      return false;
    }

    if (recomendacoesTecnicas?.equipamento == null ||
        recomendacoesTecnicas!.equipamento!.isEmpty) {
      Util.toastAlerta('Equipamento das Recomendações Técnicas é obrigatório',
          showToast: showToast);
      return false;
    }

    if (recomendacoesTecnicas?.angulo == null ||
        recomendacoesTecnicas!.angulo!.isEmpty) {
      Util.toastAlerta('Ângulo das Recomendações Técnicas é obrigatório',
          showToast: showToast);
      return false;
    }

    return true;
  }
}
