import 'package:flutter/material.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications_v2/components/components_exports.dart';
import 'package:flytec/features/aplications_v2/controller/report_aplication_controller.dart';
import 'package:flytec/features/aplications_v2/models/aplicacao.dart';
import 'package:flytec/features/aplications_v2/models/recomendacoes_tecnicas.dart';

class RecomendacoesTecnicasPage extends StatefulWidget {
  final ReportAplicationController _reportAplicationController;
  const RecomendacoesTecnicasPage(
      {required ReportAplicationController reportAplicationController,
      super.key})
      : _reportAplicationController = reportAplicationController;

  @override
  State<RecomendacoesTecnicasPage> createState() =>
      _RecomendacoesTecnicasPageState();
}

class _RecomendacoesTecnicasPageState extends State<RecomendacoesTecnicasPage> {
  RecomendacoesTecnicas? _recomendacoesTecnicas;

  Aplicacao get _aplicacao =>
      widget._reportAplicationController.aplicacaoSelected!;

  String _productType = "Selecione";
  String _unidade = "";
  String _degree = "Selecione";
  String _veiculanteType = "Selecione";
  String _humiditySelected = '+55%';
  String _temperatureSelected = "20.0°C";
  String _flightHeight = "Selecione";
  String _speedWind = "Selecione";
  String _selectedEquipment = "";
  String _selectedAaeronave = "";
  final TextEditingController _qtdVeiculante = TextEditingController();
  final TextEditingController _larguraDaFaixa = TextEditingController();
  final TextEditingController _volumeDeAplicacao = TextEditingController();

  Future<bool> _verifyFiels() async {
    if (_veiculanteType.isEmpty || _veiculanteType == "Selecione") {
      Util.toastAlerta("Selecione o veiculante");
      return false;
    } else if (_larguraDaFaixa.text.isEmpty) {
      Util.toastAlerta("Digite a largura da faixa");
      return false;
    } else if (_volumeDeAplicacao.text.isEmpty) {
      Util.toastAlerta("Digite o volume de aplicação");
      return false;
    } else if (_unidade.isEmpty) {
      Util.toastAlerta("Selecione a unidade do volume de aplicação");
      return false;
    } else if (_selectedAaeronave.isEmpty) {
      Util.toastAlerta("Selecione a aeronave");
      return false;
    } else if (_flightHeight.isEmpty || _flightHeight == "Selecione") {
      Util.toastAlerta("Selecione a altura do voo");
      return false;
    } else if (_temperatureSelected.isEmpty) {
      Util.toastAlerta("Selecione a temperatura");
      return false;
    } else if (_humiditySelected.isEmpty || _humiditySelected == "Selecione") {
      Util.toastAlerta("Selecione a umidade relativa do ar");
      return false;
    } else if (_speedWind.isEmpty || _speedWind == "Selecione") {
      Util.toastAlerta("Selecione a velocidade do vento");
      return false;
    } else if (_productType.isEmpty || _productType == "Selecione") {
      Util.toastAlerta("Selecione o tipo de produto");
      return false;
    } else if (_selectedEquipment.isEmpty ||
        _selectedEquipment == "Selecione") {
      Util.toastAlerta("Selecione o equipamento");
      return false;
    } else if (_degree.isEmpty || _degree == "Selecione") {
      Util.toastAlerta("Selecione o ângulo");
      return false;
    } else {
      _recomendacoesTecnicas = RecomendacoesTecnicas(
          tipoProduto: _productType,
          unidadevolumeAplicacao: _unidade,
          angulo: _degree,
          veiculante: _veiculanteType,
          umidadeRelativaAr: _humiditySelected,
          temperatura: _temperatureSelected,
          alturaVoo: _flightHeight,
          velocidadeVento: _speedWind,
          equipamento: _selectedEquipment,
          aeronave: _selectedAaeronave,
          qtdVeiculante: _qtdVeiculante.text,
          larguraFaixa: _larguraDaFaixa.text,
          volumeAplicacao: _volumeDeAplicacao.text);
      Util.toastSucesso("Dados inseridos com sucesso");
      Navigator.pop(context);
      return true;
    }
  }

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      if (_aplicacao.recomendacoesTecnicas?.angulo != null) {
        _recomendacoesTecnicas = _aplicacao.recomendacoesTecnicas!;
        _productType = _recomendacoesTecnicas!.tipoProduto!;
        _unidade = _recomendacoesTecnicas!.unidadevolumeAplicacao!;
        _degree = _recomendacoesTecnicas!.angulo!;
        _veiculanteType = _recomendacoesTecnicas!.veiculante!;
        _humiditySelected = _recomendacoesTecnicas!.umidadeRelativaAr!;
        _temperatureSelected = _recomendacoesTecnicas!.temperatura!;
        _flightHeight = _recomendacoesTecnicas!.alturaVoo!;
        _speedWind = _recomendacoesTecnicas!.velocidadeVento!;
        _selectedEquipment = _recomendacoesTecnicas!.equipamento!;
        _selectedAaeronave = _recomendacoesTecnicas!.aeronave!;
        _qtdVeiculante.text = _recomendacoesTecnicas!.qtdVeiculante!;
        _larguraDaFaixa.text = _recomendacoesTecnicas!.larguraFaixa!;
        _volumeDeAplicacao.text = _recomendacoesTecnicas!.volumeAplicacao!;
        setState(() {});
      }
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
        title: const Text(
          "Recomendações técnicas",
          textAlign: TextAlign.center,
        ),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: ListView(
          children: [
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const CustomText(text: 'Veiculante'),
                    const SizedBox(height: 10),
                    InkWell(
                        onTap: () async {
                          Util.closeKeyBoard();
                          await showDialog(
                              context: context,
                              builder: (BuildContext context) {
                                return AlertDialog(
                                    backgroundColor: Colors.grey[100],
                                    content: SizedBox(
                                      width: double.maxFinite,
                                      child: VeiculanteSelect(
                                          onChangeVeiculanteType: (value) {
                                        setState(() {
                                          _veiculanteType = value;
                                        });
                                      }),
                                    ));
                              });
                        },
                        child: ComboBox(
                            selectedName: _veiculanteType.isEmpty
                                ? "Selecione"
                                : _veiculanteType)),
                  ],
                ),
                Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const CustomText(text: 'Qtde. Veiculante (L)'),
                    const SizedBox(height: 10),
                    Container(
                      width: (MediaQuery.of(context).size.width / 2) - 25,
                      height: 50,
                      padding: const EdgeInsets.symmetric(
                          horizontal: 16, vertical: 10),
                      decoration: ShapeDecoration(
                        shape: RoundedRectangleBorder(
                          side: const BorderSide(
                              width: 1, color: Color(0xFF636363)),
                          borderRadius: BorderRadius.circular(10),
                        ),
                      ),
                      child: TextField(
                        controller: _qtdVeiculante,
                        onChanged: (value) {},
                        keyboardType: TextInputType.number,
                        decoration: const InputDecoration(
                            hintText: "Digite aqui",
                            border: InputBorder.none,
                            hintStyle: TextStyle(
                              color: Color.fromARGB(255, 121, 118, 118),
                              fontSize: 16,
                              fontFamily: 'Inter',
                              fontWeight: FontWeight.w500,
                              height: 0.09,
                            )),
                      ),
                    ),
                  ],
                )
              ],
            ),
            const SizedBox(height: 10),
            const CustomText(text: 'Largura da faixa (m)'),
            const SizedBox(height: 10),
            Container(
              width: double.infinity,
              height: 50,
              margin: const EdgeInsets.only(bottom: 20),
              padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 10),
              decoration: ShapeDecoration(
                shape: RoundedRectangleBorder(
                  side: const BorderSide(width: 1, color: Color(0xFF636363)),
                  borderRadius: BorderRadius.circular(10),
                ),
              ),
              child: TextField(
                controller: _larguraDaFaixa,
                onChanged: (value) {},
                keyboardType: TextInputType.number,
                decoration: const InputDecoration(
                    hintText: "Digite aqui",
                    border: InputBorder.none,
                    hintStyle: TextStyle(
                      color: Color.fromARGB(255, 121, 118, 118),
                      fontSize: 16,
                      fontFamily: 'Inter',
                      fontWeight: FontWeight.w500,
                      height: 0.09,
                    )),
              ),
            ),
            const SizedBox(height: 10),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const CustomText(text: "Volume de aplicação"),
                    const SizedBox(height: 10),
                    Container(
                      width: (MediaQuery.of(context).size.width / 2) - 40,
                      height: 50,
                      padding: const EdgeInsets.symmetric(
                          horizontal: 16, vertical: 10),
                      decoration: ShapeDecoration(
                        shape: RoundedRectangleBorder(
                          side: const BorderSide(
                              width: 1, color: Color(0xFF636363)),
                          borderRadius: BorderRadius.circular(10),
                        ),
                      ),
                      child: TextField(
                        controller: _volumeDeAplicacao,
                        onChanged: (value) {},
                        keyboardType: TextInputType.number,
                        decoration: const InputDecoration(
                            hintText: "Digite aqui",
                            border: InputBorder.none,
                            hintStyle: TextStyle(
                              color: Color.fromARGB(255, 121, 118, 118),
                              fontSize: 16,
                              fontFamily: 'Inter',
                              fontWeight: FontWeight.w500,
                              height: 0.09,
                            )),
                      ),
                    ),
                  ],
                ),
                Flexible(
                  child: Row(
                    children: [
                      const Text("Kg/ha"),
                      Checkbox(
                          value: _unidade == "Kg/ha",
                          onChanged: (value) {
                            setState(() {
                              _unidade = "Kg/ha";
                            });
                          }),
                    ],
                  ),
                ),
                Flexible(
                  child: Row(
                    children: [
                      const Text("L/ha"),
                      Checkbox(
                          value: _unidade == "L/ha",
                          onChanged: (value) {
                            setState(() {
                              _unidade = "L/ha";
                            });
                          }),
                    ],
                  ),
                )
              ],
            ),
            const SizedBox(height: 10),
            const CustomText(text: 'Aeronave'),
            const SizedBox(height: 10),
            CustomComboBoxExpanded(
                selectedName: _selectedAaeronave.isEmpty
                    ? "Selecione"
                    : _selectedAaeronave,
                onTap: () async {
                  Util.closeKeyBoard();
                  await showDialog(
                      context: context,
                      builder: (BuildContext context) {
                        return AlertDialog(
                            backgroundColor: Colors.grey[100],
                            content: SizedBox(
                              width: double.maxFinite,
                              child: AirCraftSelect(onChanged: (value) {
                                setState(() {
                                  _selectedAaeronave = value;
                                });
                              }),
                            ));
                      });
                }),
            const SizedBox(height: 10),
            const CustomText(text: 'Altura do voo (m)'),
            const SizedBox(height: 10),
            InkWell(
                onTap: () async {
                  Util.closeKeyBoard();
                  await showDialog(
                      context: context,
                      builder: (BuildContext context) {
                        return AlertDialog(
                            backgroundColor: Colors.grey[100],
                            content: SizedBox(
                              width: double.maxFinite,
                              child: FlightHeightSelect(
                                  onChangedFlightHeight: (value) {
                                setState(() {
                                  _flightHeight = value;
                                });
                              }),
                            ));
                      });
                },
                child: ComboBox(
                    selectedName:
                        _flightHeight.isEmpty ? "Selecione" : _flightHeight)),
            const SizedBox(height: 10),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const CustomText(text: 'Temperatura (ºC)'),
                    const SizedBox(height: 14),
                    InkWell(
                        onTap: () async {
                          Util.closeKeyBoard();
                          await showDialog(
                              context: context,
                              builder: (BuildContext context) {
                                return AlertDialog(
                                    backgroundColor: Colors.grey[100],
                                    content: SizedBox(
                                      width: double.maxFinite,
                                      child: TemperatureSelect(
                                          scrollTheList: true,
                                          scrollToIndex: 19,
                                          onChangedTemperature: (value) {
                                            setState(() {
                                              _temperatureSelected = value;
                                            });
                                          }),
                                    ));
                              });
                        },
                        child: ComboBox(selectedName: _temperatureSelected))
                  ],
                ),
                Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const CustomText(text: 'U.R do ar(%)'),
                    const SizedBox(height: 14),
                    InkWell(
                        onTap: () async {
                          Util.closeKeyBoard();
                          await showDialog(
                              context: context,
                              builder: (BuildContext context) {
                                return AlertDialog(
                                    backgroundColor: Colors.grey[100],
                                    content: SizedBox(
                                      width: double.maxFinite,
                                      child: RelativeHumiditySelect(
                                          scrollTheList: true,
                                          scrollToIndex: 54,
                                          onChangedHumidity: (value) {
                                            setState(() {
                                              _humiditySelected = value;
                                            });
                                          }),
                                    ));
                              });
                        },
                        child: ComboBox(
                            selectedName: _humiditySelected.isEmpty
                                ? "Selecione"
                                : _humiditySelected))
                  ],
                )
              ],
            ),
            const SizedBox(height: 10),
            const CustomText(text: 'Velocidade do vento'),
            const SizedBox(height: 10),
            InkWell(
                onTap: () async {
                  Util.closeKeyBoard();
                  await showDialog(
                      context: context,
                      builder: (BuildContext context) {
                        return AlertDialog(
                            backgroundColor: Colors.grey[100],
                            content: SizedBox(
                              width: double.maxFinite,
                              child:
                                  SpeedWindSelect(onChangedSpeedWind: (value) {
                                setState(() {
                                  _speedWind = value;
                                });
                              }),
                            ));
                      });
                },
                child: ComboBox(
                    selectedName:
                        _speedWind.isEmpty ? "Selecione" : _speedWind)),
            const SizedBox(height: 10),
            const CustomText(text: 'Tipo de produto'),
            const SizedBox(height: 10),
            InkWell(
                onTap: () async {
                  Util.closeKeyBoard();
                  await showDialog(
                      context: context,
                      builder: (BuildContext context) {
                        return AlertDialog(
                            backgroundColor: Colors.grey[100],
                            content: SizedBox(
                              width: double.maxFinite,
                              child: ProductTypeSelect(
                                  onChangedProductType: (value) {
                                setState(() {
                                  _productType = value;
                                });
                              }),
                            ));
                      });
                },
                child: ComboBox(
                    selectedName:
                        _productType.isEmpty ? "Selecione" : _productType)),
            const SizedBox(height: 10),
            const CustomText(text: 'Equipamento'),
            const SizedBox(height: 10),
            CustomComboBoxExpanded(
              selectedName:
                  _selectedEquipment.isEmpty ? "Selecione" : _selectedEquipment,
              onTap: () async {
                await showDialog(
                    context: context,
                    builder: (BuildContext context) {
                      Util.closeKeyBoard();
                      return AlertDialog(
                          backgroundColor: const Color(0xFFF5F5F5),
                          content: SizedBox(
                            width: double.maxFinite,
                            child: EquipmentSelect(onChanged: (value) {
                              setState(() {
                                _selectedEquipment = value;
                              });
                            }),
                          ));
                    });
              },
            ),
            const SizedBox(height: 10),
            const CustomText(text: 'Ângulo'),
            const SizedBox(height: 10),
            InkWell(
                onTap: () async {
                  await showDialog(
                      context: context,
                      builder: (BuildContext context) {
                        Util.closeKeyBoard();
                        return AlertDialog(
                            backgroundColor: Colors.grey[100],
                            content: SizedBox(
                              width: double.maxFinite,
                              child: DegreeSelect(onChangeDegree: (value) {
                                setState(() {
                                  _degree = value;
                                });
                              }),
                            ));
                      });
                },
                child: ComboBox(
                    selectedName: _degree.isEmpty ? "Selecione" : _degree)),
            Center(
              child: CustomButton(
                title: "OK",
                onClick: () {
                  _verifyFiels();
                },
              ),
            ),
          ],
        ),
      ),
    );
  }
}

class ComboBox extends StatelessWidget {
  const ComboBox({super.key, required this.selectedName});
  final String selectedName;

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 50,
      width: (MediaQuery.of(context).size.width / 2) - 25,
      padding: const EdgeInsets.all(8),
      decoration: ShapeDecoration(
        shape: RoundedRectangleBorder(
          side: const BorderSide(width: 1, color: Color(0xFF636363)),
          borderRadius: BorderRadius.circular(8),
        ),
      ),
      child: Row(
        mainAxisSize: MainAxisSize.min,
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
          SizedBox(
            child: Row(
              mainAxisSize: MainAxisSize.min,
              mainAxisAlignment: MainAxisAlignment.start,
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                Text(
                  selectedName,
                  style: const TextStyle(
                    color: Color.fromARGB(255, 124, 123, 123),
                    fontSize: 16,
                    fontFamily: 'Inter',
                    fontWeight: FontWeight.w500,
                  ),
                ),
              ],
            ),
          ),
          const Icon(
            Icons.keyboard_arrow_down,
          )
        ],
      ),
    );
  }
}
