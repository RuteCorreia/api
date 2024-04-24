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
      case "Partly cloudy":
        return "Parcialmente nublado";
      case "Cloudy":
        return "Nublado";
      case "Overcast":
        return "Encoberto";
      case "Mist":
        return "Neblina";
      case "Patchy rain possible":
        return "Possibilidade de chuva";
      case "Patchy rain nearby":
        return "Possibilidade de chuva próxima";
      case "Patchy snow possible":
        return "Possibilidade de neve";
      case "Patchy sleet possible":
        return "Possibilidade de chuva congelante";
      case "Patchy freezing drizzle possible":
        return "Possibilidade de chuva congelante leve";
      case "Thundery outbreaks possible":
        return "Possibilidade de surtos de trovoadas";
      case "Blowing snow":
        return "Nevasca";
      case "Blizzard":
        return "Tempestade de neve";
      case "Fog":
        return "Nevoeiro";
      case "Freezing fog":
        return "Nevoeiro congelante";
      case "Patchy light drizzle":
        return "Possibilidade de chuvisco leve";
      case "Light drizzle":
        return "Chuvisco leve";
      case "Freezing drizzle":
        return "Chuvisco congelante";
      case "Heavy freezing drizzle":
        return "Chuvisco congelante pesado";
      case "Patchy light rain":
        return "Possibilidade de chuva leve";
      case "Light rain":
        return "Chuva leve";
      case "Moderate rain at times":
        return "Chuva moderada às vezes";
      case "Moderate rain":
        return "Chuva moderada";
      case "Heavy rain at times":
        return "Chuva pesada às vezes";
      case "Heavy rain":
        return "Chuva pesada";
      case "Light freezing rain":
        return "Chuva congelante leve";
      case "Moderate or heavy freezing rain":
        return "Chuva congelante moderada ou pesada";
      case "Light sleet":
        return "Garoa leve";
      case "Moderate or heavy sleet":
        return "Garoa moderada ou pesada";
      case "Patchy light snow":
        return "Possibilidade de neve leve";
      case "Light snow":
        return "Neve leve";
      case "Patchy moderate snow":
        return "Possibilidade de neve moderada";
      case "Moderate snow":
        return "Neve moderada";
      case "Patchy heavy snow":
        return "Possibilidade de neve pesada";
      case "Heavy snow":
        return "Neve pesada";
      case "Ice pellets":
        return "Granizo";
      case "Light rain shower":
        return "Chuva leve";
      case "Moderate or heavy rain shower":
        return "Chuva moderada ou pesada";
      case "Torrential rain shower":
        return "Chuva torrencial";
      case "Light sleet showers":
        return "Garoa leve";
      case "Moderate or heavy sleet showers":
        return "Garoa moderada ou pesada";
      case "Light snow showers":
        return "Neve leve";
      case "Moderate or heavy snow showers":
        return "Neve moderada ou pesada";
      case "Light showers of ice pellets":
        return "Granizo leve";
      case "Moderate or heavy showers of ice pellets":
        return "Granizo moderado ou pesado";
      case "Patchy light rain with thunder":
        return "Possibilidade de chuva leve com trovões";
      case "Moderate or heavy rain with thunder":
        return "Chuva moderada ou pesada com trovões";
      case "Patchy light snow with thunder":
        return "Possibilidade de neve leve com trovões";
      case "Moderate or heavy snow with thunder":
        return "Neve moderada ou pesada com trovões";
      case "Clear":
        return "Limpo";
      default:
        return text;
    }
  }
}
