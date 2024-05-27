class TrackingPoint {
  final double latitude;
  final double longitude;

  TrackingPoint({required this.latitude, required this.longitude});

  factory TrackingPoint.fromJson(Map<String, dynamic> data) {
    return TrackingPoint(
        latitude: data['latitude'], longitude: data['longitude']);
  }
}
