class LocalFirefighting {
  final int? id;
  final String? lat;
  final String? long;
  final String? referencia;

  LocalFirefighting({this.id, this.lat, this.long, this.referencia});

  factory LocalFirefighting.fromJson(Map<String, dynamic> json) {
    return LocalFirefighting(
        id: json['id'] ?? 0,
        lat: json['lat'] ?? '',
        long: json['long'] ?? '',
        referencia: json['referencia'] ?? '');
  }
}
