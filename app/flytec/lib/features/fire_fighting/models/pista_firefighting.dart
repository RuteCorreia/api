class PistaFirefighting {
  final int? id;
  final int? horarioChegadaPista;
  final String? horimetroChegadaPista;
  final String? codigoICAOPista;
  final String? nomePista;
  final String? latPista;
  final String? longPista;

  PistaFirefighting(
      {this.id,
      this.horarioChegadaPista,
      this.horimetroChegadaPista,
      this.codigoICAOPista,
      this.nomePista,
      this.latPista,
      this.longPista});

  factory PistaFirefighting.fromJson(Map<String, dynamic> json) {
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
      'id': id,
      'horarioChegadaPista': horarioChegadaPista,
      'horimetroChegadaPista': horimetroChegadaPista,
      'codigoICAOPista': codigoICAOPista,
      'nomePista': nomePista,
      'latPista': latPista,
      'longPista': longPista
    };
  }
}
