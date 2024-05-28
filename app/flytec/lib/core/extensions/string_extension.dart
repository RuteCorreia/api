extension StringExtension on String {
  String get toCNPJ {
    if (length != 14) {
      return this;
    }

    String parte1 = substring(0, 2);
    String parte2 = substring(2, 5);
    String parte3 = substring(5, 8);
    String parte4 = substring(8, 12);
    String parte5 = substring(12, 14);

    return '$parte1.$parte2.$parte3/$parte4-$parte5';
  }
}
