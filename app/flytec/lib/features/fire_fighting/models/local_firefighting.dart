class LocalFirefighting {
  int? id;
  String? lat;
  String? long;
  String? referencia;

  LocalFirefighting({this.id, this.lat, this.long, this.referencia});

  factory LocalFirefighting.fromJson(Map<String, dynamic>? json) {
    if (json == null) return LocalFirefighting();

    return LocalFirefighting(
        id: json['id'] ?? 0,
        lat: json['lat'] ?? '',
        long: json['long'] ?? '',
        referencia: json['referencia'] ?? '');
  }

  Map<String, dynamic> toJson() =>
      {'lat': lat, 'long': long, 'referencia': referencia};
}
