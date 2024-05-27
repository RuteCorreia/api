class DecolagemPousoFirefighting {
  final int? id;
  final int? idFirefighting;
  final int? horarioDecolagem;
  final int? horarioPouso;
  final String? horimetroDecolagem;
  final String? horimetroPouso;

  DecolagemPousoFirefighting(
      {this.id,
      this.horarioDecolagem,
      this.horarioPouso,
      this.idFirefighting,
      this.horimetroDecolagem,
      this.horimetroPouso});

  factory DecolagemPousoFirefighting.fromJson(Map<String, dynamic>? json) {
    if (json == null) return DecolagemPousoFirefighting();

    return DecolagemPousoFirefighting(
        id: json['id'] ?? 0,
        horarioDecolagem: json['horarioDecolagem'] ?? 0,
        horarioPouso: json['horarioPouso'] ?? 0,
        idFirefighting: json['firefightingId'] ?? 0,
        horimetroDecolagem: json['horimetroDecolagem'] ?? '',
        horimetroPouso: json['horimetroPouso'] ?? '');
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'horarioDecolagem': horarioDecolagem,
      'horarioPouso': horarioPouso,
      'horimetroDecolagem': horimetroDecolagem,
      'horimetroPouso': horimetroPouso
    };
  }
}
