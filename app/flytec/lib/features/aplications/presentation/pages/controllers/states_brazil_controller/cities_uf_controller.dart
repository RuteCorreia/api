import 'package:flytec/features/aplications/presentation/pages/controllers/states_brazil_controller/constants/cities.dart';

class CitiesUfController {
  final _citiesByStatesBrazilConstants = CitiesByStatesBrazilConstants();
  final _statesBrazil = [
    "RO",
    "AC",
    "AM",
    "RR",
    "PA",
    "AP",
    "TO",
    "MA",
    "PI",
    "CE",
    "RN",
    "PB",
    "PE",
    "AL",
    "SE",
    "BA",
    "MG",
    "ES",
    "RJ",
    "SP",
    "PR",
    "SC",
    "RS",
    "MS",
    "MT",
    "GO",
    "DF"
  ];

  List<String> get statesBrazil => _statesBrazil;

  List<String> getCitiesByState(String state) {
    return _citiesByStatesBrazilConstants.statesWithCities
        .firstWhere((element) => element.sigla == state)
        .cities;
  }
}
