class PistaFirefighting {
  int? id;
  int? horarioChegadaPista;
  String? horimetroChegadaPista;
  String? codigoICAOPista;
  String? nomePista;
  String? latPista;
  String? longPista;

  PistaFirefighting(
      {this.id,
      this.horarioChegadaPista,
      this.horimetroChegadaPista,
      this.codigoICAOPista,
      this.nomePista,
      this.latPista,
      this.longPista});

  factory PistaFirefighting.fromJson(Map<String, dynamic>? json) {
    if (json == null) return PistaFirefighting();
    return PistaFirefighting(
        id: json['id'] ?? 0,
        horarioChegadaPista: json['horarioChegadaPista'] ?? 0,
        horimetroChegadaPista: json['horimetroChegadaPista'] ?? '',
        codigoICAOPista: json['codigoICAOPista'] ?? '',
        nomePista: json['nomePista'] ?? '',
        latPista: json['latPista'] ?? '',
        longPista: json['longPista'] ?? '');
  }

  Map<String, dynamic> toJson() {
    return {
      'horarioChegadaPista': horarioChegadaPista??0,
      'horimetroChegadaPista': horimetroChegadaPista??'',
      'codigoICAOPista': codigoICAOPista??'',
      'nomePista': nomePista??'',
      'latPista': latPista??'',
      'longPista': longPista??''
    };
  }
}
