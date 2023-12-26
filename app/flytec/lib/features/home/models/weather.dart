class Weather {
  final double tempC;
  final double windKph;
  final Condition condition;

  Weather({
    required this.tempC,
    required this.windKph,
    required this.condition,
  });

  factory Weather.fromJson(Map<String, dynamic> json) {
    return Weather(
      tempC: json['current']['temp_c'],
      windKph: json['current']['wind_kph'],
      condition: Condition.fromJson(json['current']['condition']),
    );
  }
}

class Condition {
  final String text;
  final String icon;

  Condition({required this.text, required this.icon});

  factory Condition.fromJson(Map<String, dynamic> json) {
    return Condition(
      text: json['text'],
      icon: json['icon'],
    );
  }

  String get translationConditionWeather {
    switch (text) {
      case 'Sunny':
        return 'Tempo Ensolarado';
      default:
        return text;
    }
  }
}
