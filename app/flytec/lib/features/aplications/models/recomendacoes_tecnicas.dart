class RecomendacoesTecnicas{
	String? veiculante;
	String? qtdVeiculante;
	String? larguraFaixa;
	String? volumeAplicacao;
	String? unidadevolumeAplicacao;
	String? aeronave;
	String? alturaVoo;
	String? temperatura;
	String? umidadeRelativaAr;
	String? velocidadeVento;
	String? tipoProduto;
	String? equipamento;
	String? angulo;
  int? id;

  RecomendacoesTecnicas({
    this.veiculante,
    this.qtdVeiculante,
    this.larguraFaixa,
    this.volumeAplicacao,
    this.unidadevolumeAplicacao,
    this.aeronave,
    this.alturaVoo,
    this.temperatura,
    this.umidadeRelativaAr,
    this.velocidadeVento,
    this.tipoProduto,
    this.equipamento,
    this.angulo,
      this.id
  });

  Map<String, dynamic> toMap() {
    return {
      'veiculante': veiculante,
      'qtdVeiculante': qtdVeiculante,
      'larguraFaixa': larguraFaixa,
      'volumeAplicacao': volumeAplicacao,
      'unidadevolumeAplicacao': unidadevolumeAplicacao,
      'aeronave': aeronave,
      'alturaVoo': alturaVoo,
      'temperatura': temperatura,
      'umidadeRelativaAr': umidadeRelativaAr,
      'velocidadeVento': velocidadeVento,
      'tipoProduto': tipoProduto,
      'equipamento': equipamento,
      'angulo': angulo,
    };
  }

  factory RecomendacoesTecnicas.fromJson(Map<String, dynamic>? json) {
    if(json==null) return RecomendacoesTecnicas();
    return RecomendacoesTecnicas(
      veiculante: json['veiculante'],
      qtdVeiculante: json['qtdVeiculante'],
      larguraFaixa: json['larguraFaixa'],
      volumeAplicacao: json['volumeAplicacao'],
      unidadevolumeAplicacao: json['unidadevolumeAplicacao'],
      aeronave: json['aeronave'],
      alturaVoo: json['alturaVoo'],
      temperatura: json['temperatura'],
      umidadeRelativaAr: json['umidadeRelativaAr'],
      velocidadeVento: json['velocidadeVento'],
      tipoProduto: json['tipoProduto'],
      equipamento: json['equipamento'],
      angulo: json['angulo'],
      id: json['id'],
    );
  }
}