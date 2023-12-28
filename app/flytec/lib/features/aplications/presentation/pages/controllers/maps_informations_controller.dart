import 'package:estados_municipios/estados_municipios.dart';

abstract class MapsInformationsController {
  Future<List<String>> getUfBrazil();
  Future<List<String>> obtainCitiesOfUfBrazil(String uf);
}

class MapsInformationsControllerBrazil implements MapsInformationsController {
  final _controllerMapStates = EstadosMunicipiosController();

  @override
  Future<List<String>> getUfBrazil() async {
    List<Estado> states = await _controllerMapStates.buscaTodosEstados();
    return states.map((state) => state.sigla).toList();
  }

  @override
  Future<List<String>> obtainCitiesOfUfBrazil(String uf) async {
    List<Municipio> cities =
        await _controllerMapStates.buscaMunicipiosPorEstado(uf);
    return cities.map((city) => city.nome).toList();
  }
}
