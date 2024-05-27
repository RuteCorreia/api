class DMS {
  int degrees;
  int minutes;
  double seconds;

  DMS({required this.degrees, required this.minutes, required this.seconds});

  // Função para converter de DMS para DD
  double toDecimalDegrees() {
    double dd = degrees + (minutes / 60) + (seconds / 3600);
    return dd;
  }
}
