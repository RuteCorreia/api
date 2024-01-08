import 'package:flytec/features/aplications/presentation/pages/controllers/states_brazil_controller/states_brazil_controller.dart';

abstract class MapsInformationsController {
  List<String> get getStatesBrazil;
  List<String> obtainCitiesFromStateBrazil(String uf);
}

class MapsInformationsControllerBrazil implements MapsInformationsController {
  final _controllerMapStates = StatesBrazilController();

  @override
  List<String> get getStatesBrazil => _controllerMapStates.statesBrazil;
  

  @override
  List<String> obtainCitiesFromStateBrazil(String uf) {
    return _controllerMapStates.getCitiesByState(uf);
  
  }
}
