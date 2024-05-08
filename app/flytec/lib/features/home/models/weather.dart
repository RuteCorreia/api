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
      case "Sunny":
        return "Ensolarado";
      case "Partly Cloudy":
        return "Parcialmente nublado";
      case "Cloudy":
        return "Nublado";
      case "Overcast":
        return "Encoberto";
      case "Mist":
        return "Neblina";
      case "Patchy Rain Possible":
        return "Possibilidade de chuva";
      case "Patchy Rain Nearby":
        return "Possibilidade de chuva próxima";
      case "Patchy Snow Possible":
        return "Possibilidade de neve";
      case "Patchy Sleet Possible":
        return "Possibilidade de chuva congelante";
      case "Patchy Freezing Drizzle Possible":
        return "Possibilidade de chuva congelante leve";
      case "Thundery Outbreaks Possible":
        return "Possibilidade de surtos de trovoadas";
      case "Blowing Snow":
        return "Nevasca";
      case "Blizzard":
        return "Tempestade de neve";
      case "Fog":
        return "Nevoeiro";
      case "Freezing Fog":
        return "Nevoeiro congelante";
      case "Patchy Light Drizzle":
        return "Possibilidade de chuvisco leve";
      case "Light Drizzle":
        return "Chuvisco leve";
      case "Freezing Drizzle":
        return "Chuvisco congelante";
      case "Heavy Freezing Drizzle":
        return "Chuvisco congelante pesado";
      case "Patchy Light Rain":
        return "Possibilidade de chuva leve";
      case "Light Rain":
        return "Chuva Leve";
      case "Moderate Rain At Times":
        return "Chuva moderada às vezes";
      case "Moderate Rain":
        return "Chuva moderada";
      case "Heavy Rain At Times":
        return "Chuva pesada às vezes";
      case "Heavy Rain":
        return "Chuva pesada";
      case "Light Freezing Rain":
        return "Chuva congelante leve";
      case "Moderate Or Heavy Freezing Rain":
        return "Chuva congelante moderada ou pesada";
      case "Light Sleet":
        return "Garoa leve";
      case "Moderate Or Heavy Sleet":
        return "Garoa moderada ou pesada";
      case "Patchy Light Snow":
        return "Possibilidade de neve leve";
      case "Light Snow":
        return "Neve leve";
      case "Patchy Moderate Snow":
        return "Possibilidade de neve moderada";
      case "Moderate Snow":
        return "Neve moderada";
      case "Patchy Heavy Snow":
        return "Possibilidade de neve pesada";
      case "Heavy Snow":
        return "Neve pesada";
      case "Ice Pellets":
        return "Granizo";
      case "Light Rain Shower":
        return "Chuva leve";
      case "Moderate Or Heavy Rain Shower":
        return "Chuva moderada ou pesada";
      case "Torrential Rain Shower":
        return "Chuva torrencial";
      case "Light Sleet Showers":
        return "Garoa leve";
      case "Moderate Or Heavy Sleet Showers":
        return "Garoa moderada ou pesada";
      case "Light Snow Showers":
        return "Neve leve";
      case "Moderate Or Heavy Snow Showers":
        return "Neve moderada ou pesada";
      case "Light Showers Of Ice Pellets":
        return "Granizo leve";
      case "Moderate Or Heavy Showers Of Ice Pellets":
        return "Granizo moderado ou pesado";
      case "Patchy Light Rain With Thunder":
        return "Possibilidade de chuva leve com trovões";
      case "Moderate Or Heavy Rain With Thunder":
        return "Chuva moderada ou pesada com trovões";
      case "Patchy Light Snow With Thunder":
        return "Possibilidade de neve leve com trovões";
      case "Moderate Or Heavy Snow With Thunder":
        return "Neve moderada ou pesada com trovões";
      case "Clear":
        return "Limpo";
      default:
        return text;
    }
  }
}
