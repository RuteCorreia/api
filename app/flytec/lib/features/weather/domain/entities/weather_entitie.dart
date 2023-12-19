class WeatherEntityEntity {
  final CoordEntity? coordEntity;
  final List<WeatherEntity>? weatherEntity;
  final String? base;
  final MainEntity? mainEntity;
  final int? visibility;
  final WindEntity? windEntity;
  final RainEntity? rainEntity;
  final CloudsEntity? cloudsEntity;
  final int? dt;
  final SysEntity? sysEntity;
  final int? timezone;
  final int? id;
  final String? name;
  final int? cod;

  WeatherEntityEntity({
    this.coordEntity,
    this.weatherEntity,
    this.base,
    this.mainEntity,
    this.visibility,
    this.windEntity,
    this.rainEntity,
    this.cloudsEntity,
    this.dt,
    this.sysEntity,
    this.timezone,
    this.id,
    this.name,
    this.cod,
  });
}

class CloudsEntity {
  final int? all;

  CloudsEntity({
    this.all,
  });
}

class CoordEntity {
  final double? lon;
  final double? lat;

  CoordEntity({
    this.lon,
    this.lat,
  });
}

class MainEntity {
  final double? temp;
  final double? feelsLike;
  final double? tempMin;
  final double? tempMax;
  final int? pressure;
  final int? humidity;

  MainEntity({
    this.temp,
    this.feelsLike,
    this.tempMin,
    this.tempMax,
    this.pressure,
    this.humidity,
  });
}

class RainEntity {
  final double? the1H;

  RainEntity({
    this.the1H,
  });
}

class SysEntity {
  final int? type;
  final int? id;
  final String? country;
  final int? sunrise;
  final int? sunset;

  SysEntity({
    this.type,
    this.id,
    this.country,
    this.sunrise,
    this.sunset,
  });
}

class WeatherEntity {
  final int? id;
  final String? mainEntity;
  final String? description;
  final String? icon;

  WeatherEntity({
    this.id,
    this.mainEntity,
    this.description,
    this.icon,
  });
}

class WindEntity {
  final double? speed;
  final int? deg;

  WindEntity({
    this.speed,
    this.deg,
  });
}
