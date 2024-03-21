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
      case 'Clear':
        return 'Tempo Limpo';
      case 'Partly cloudy':
        return 'Parcialmente Nublado';
      case 'Overcast':
        return 'Nublado';
      case 'Mist':
        return 'Névoa';
      case 'Patchy rain possible':
        return 'Possibilidade de Chuva';
      case 'Patchy snow possible':
        return 'Possibilidade de Neve';
      case 'Patchy sleet possible':
        return 'Possibilidade de Neve';
      case 'Patchy freezing drizzle possible':
        return 'Possibilidade de Chuva Congelante';
      case 'Thundery outbreaks possible':
        return 'Possibilidade de Trovoada';
      case 'Blowing snow':
        return 'Neve';
      case 'Blizzard':
        return 'Nevasca';
      case 'Fog':
        return 'Nevoeiro';
      case 'Freezing fog':
        return 'Nevoeiro Congelante';
      case 'Patchy light drizzle':
        return 'Possibilidade de Chuvisco';
      case 'Light drizzle':
        return 'Chuvisco';
      case 'Freezing drizzle':
        return 'Chuva Congelante';
      case 'Heavy freezing drizzle':
        return 'Chuva Congelante Pesada';
      case 'Patchy light rain':
        return 'Possibilidade de Chuva Leve';
      case 'Light rain':
        return 'Chuva Leve';
      case 'Moderate rain at times':
        return 'Chuva Moderada';
      case 'Moderate rain':
        return 'Chuva Moderada';
      case 'Heavy rain':
        return 'Chuva Pesada';
      case 'Light freezing rain':
        return 'Chuva Congelante Leve';
      case 'Moderate or heavy freezing rain':
        return 'Chuva Congelante Moderada ou Pesada';
      case 'Light sleet':
        return 'Neve Leve';
      case 'Moderate or heavy sleet':
        return 'Neve Moderada ou Pesada';
      case 'Patchy light snow':
        return 'Possibilidade de Neve Leve';
      case 'Light snow':
        return 'Neve Leve';
      case 'Patchy moderate snow':
        return 'Possibilidade de Neve Moderada';
      case 'Moderate snow':
        return 'Neve Moderada';
      case 'Patchy heavy snow':
        return 'Possibilidade de Neve Pesada';
      case 'Heavy snow':
        return 'Neve Pesada';
      case 'Ice pellets':
        return 'Granizo';
      case 'Light rain shower':
        return 'Chuva Leve';
      case 'Moderate or heavy rain shower':
        return 'Chuva Moderada ou Pesada';
      case 'Torrential rain shower':
        return 'Chuva Torrencial';
      case 'Light sleet showers':
        return 'Granizo Leve';
      case 'Moderate or heavy sleet showers':
        return 'Granizo Moderado ou Pesado';
      case "Light snow showers":
        return "Poucas nuvens com neve leve";
      case "Moderate or heavy snow showers":
        return "Neve moderada ou intensa";
      case "Light showers of ice pellets":
        return "Pouca chuva de granizo";
      case "Moderate or heavy showers of ice pellets":
        return "Chuva moderada ou intensa de granizo";
      case "Patchy light rain with thunder":
        return "Chuva leve com trovoadas";
      case "Moderate or heavy rain with thunder":
        return "Chuva moderada ou intensa com trovoadas";
      case "Patchy light snow with thunder":
        return "Neve leve com trovoadas";
      case "Moderate or heavy snow with thunder":
        return "Neve moderada ou intensa com trovoadas";
      case "Patchy rain nearby":
        return "Chuva irregular nas proximidades";
      case "Patchy snow nearby":
        return "Neve irregular nas proximidades";
      case "Patchy sleet nearby":
        return "Granizo irregular nas proximidades";  
      default:
        return text;
    }
  }
}
