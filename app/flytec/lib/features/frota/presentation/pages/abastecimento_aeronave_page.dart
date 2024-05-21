import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/core/widgets/combo_box.dart';
import 'package:flytec/core/widgets/custom_text.dart';
import 'package:flytec/features/aplications/components/aircraft_select.dart';
import 'package:flytec/features/aplications/components/custom_button.dart';
import 'package:flytec/features/aplications/components/custom_combo.dart';
import 'package:flytec/features/aplications/components/custom_text_field.dart';
import 'package:flytec/features/aplications/components/executor_select.dart';
import 'package:flytec/features/aplications/components/pilot_select.dart';
import 'package:flytec/features/fire_fighting/presentation/pages/steps/add_firefighting_third_step.dart';
import 'package:flytec/features/frota/presentation/components/veiculo_select.dart';
import 'package:intl/intl.dart';

class AbastecimentoAeronavePage extends StatefulWidget {
  const AbastecimentoAeronavePage({super.key});

  @override
  State<AbastecimentoAeronavePage> createState() =>
      _AbastecimentoAeronavePageState();
}

class _AbastecimentoAeronavePageState extends State<AbastecimentoAeronavePage> {
  String _selectedVeiculo = '';
  final TextEditingController _kmInicial = TextEditingController();
  final TextEditingController _kmFinal = TextEditingController();
  final TextEditingController _horimetroInicial = TextEditingController();
  final TextEditingController _horimetroFinal = TextEditingController();
  final TextEditingController _gasolinaInicial =
      TextEditingController(text: '0');
  final TextEditingController _gasolinaFinal = TextEditingController(text: '0');
  final TextEditingController _totalDeLitros = TextEditingController();
  final TextEditingController _extensaoController = TextEditingController();
  DateTime? _dataSelecionada;

  void _obtainTotalLitros() {
    double? gasolinaInicial = double.parse(_gasolinaInicial.text);
    double? gasolinaFinal = double.parse(_gasolinaFinal.text);
    if (gasolinaFinal < gasolinaInicial) {
      _totalDeLitros.text = '0';
      return;
    }
    double? totalLitros = gasolinaFinal - gasolinaInicial;
    _totalDeLitros.text = totalLitros.toString();
  }

  String _selectedAaeronave = '';
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Abastecimento\n de Aeronave'),
        centerTitle: true,
      ),
      body: Padding(
        padding: const EdgeInsets.only(left: 10.0, right: 10.0, top: 16.0),
        child: ListView(
          children: [
            const SizedBox(height: 20),
            const CustomText(text: 'Veiculo'),
            const SizedBox(height: 10),
            CustomComboBoxExpanded(
                selectedName:
                    _selectedVeiculo.isEmpty ? "Selecione" : _selectedVeiculo,
                onTap: () async {
                  Util.closeKeyBoard();
                  await showDialog(
                      context: context,
                      builder: (BuildContext context) {
                        return AlertDialog(
                            backgroundColor: Colors.grey[100],
                            content: SizedBox(
                              width: double.maxFinite,
                              child: VeiculoSelect(onChanged: (value) {
                                setState(() {
                                  _selectedVeiculo = value;
                                });
                              }),
                            ));
                      });
                }),
            const SizedBox(height: 20),
            const CustomText(text: 'Km Inicial'),
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
                controller: _kmInicial,
                keyboardType:
                    const TextInputType.numberWithOptions(decimal: false),
                inputFormatters: <TextInputFormatter>[
                  FilteringTextInputFormatter.digitsOnly,
                  CustomNumberFormatterTho()
                ],
                decoration: const InputDecoration(
                    hintText: "Digite aqui",
                    border: InputBorder.none,
                    hintStyle: TextStyle(
                      color: Color.fromARGB(255, 121, 118, 118),
                      fontSize: 16,
                      fontFamily: 'Inter',
                      fontWeight: FontWeight.w500,
                    )),
              ),
            ),
            const CustomText(text: 'Km Final'),
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
                controller: _kmFinal,
                keyboardType:
                    const TextInputType.numberWithOptions(decimal: false),
                inputFormatters: <TextInputFormatter>[
                  FilteringTextInputFormatter.digitsOnly,
                  CustomNumberFormatterTho()
                ],
                decoration: const InputDecoration(
                    hintText: "Digite aqui",
                    border: InputBorder.none,
                    hintStyle: TextStyle(
                      color: Color.fromARGB(255, 121, 118, 118),
                      fontSize: 16,
                      fontFamily: 'Inter',
                      fontWeight: FontWeight.w500,
                    )),
              ),
            ),
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
            const SizedBox(height: 20),
            const CustomText(text: 'Horímetro inicial'),
            const SizedBox(height: 14),
            Container(
                width: (MediaQuery.of(context).size.width / 2) - 25,
                height: 50,
                padding:
                    const EdgeInsets.symmetric(horizontal: 16, vertical: 0),
                decoration: ShapeDecoration(
                  shape: RoundedRectangleBorder(
                    side: const BorderSide(width: 1, color: Color(0xFF636363)),
                    borderRadius: BorderRadius.circular(10),
                  ),
                ),
                child: TextField(
                  controller: _horimetroInicial,
                  onChanged: (value) {},
                  inputFormatters: [
                    // obrigatório
                    FilteringTextInputFormatter.digitsOnly,
                    CustomNumberFormatter()
                  ],
                  keyboardType: TextInputType.datetime,
                  decoration: const InputDecoration(
                      hintText: "Digite aqui",
                      border: InputBorder.none,
                      hintStyle: TextStyle(
                        color: Color.fromARGB(255, 121, 118, 118),
                        fontSize: 16,
                        fontFamily: 'Inter',
                        fontWeight: FontWeight.w500,
                      )),
                )),
            const SizedBox(height: 20),
            const CustomText(text: 'Horímetro Final'),
            const SizedBox(height: 14),
            Container(
              width: (MediaQuery.of(context).size.width / 2) - 25,
              height: 50,
              padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 0),
              decoration: ShapeDecoration(
                shape: RoundedRectangleBorder(
                  side: const BorderSide(width: 1, color: Color(0xFF636363)),
                  borderRadius: BorderRadius.circular(10),
                ),
              ),
              child: TextField(
                controller: _horimetroFinal,
                onChanged: (value) {},
                inputFormatters: [
                  // obrigatório
                  FilteringTextInputFormatter.digitsOnly,
                  CustomNumberFormatter()
                ],
                keyboardType: TextInputType.datetime,
                decoration: const InputDecoration(
                    hintText: "Digite aqui",
                    border: InputBorder.none,
                    hintStyle: TextStyle(
                      color: Color.fromARGB(255, 121, 118, 118),
                      fontSize: 16,
                      fontFamily: 'Inter',
                      fontWeight: FontWeight.w500,
                    )),
              ),
            ),
            const SizedBox(height: 20),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const CustomText(text: 'Combustível inicial'),
                    const SizedBox(height: 14),
                    Container(
                      width: (MediaQuery.of(context).size.width / 2) - 25,
                      height: 50,
                      padding: const EdgeInsets.symmetric(
                          horizontal: 16, vertical: 0),
                      decoration: ShapeDecoration(
                        shape: RoundedRectangleBorder(
                          side: const BorderSide(
                              width: 1, color: Color(0xFF636363)),
                          borderRadius: BorderRadius.circular(10),
                        ),
                      ),
                      child: TextField(
                        controller: _gasolinaInicial,
                        onChanged: (value) {
                          _obtainTotalLitros();
                          setState(() {});
                        },
                        keyboardType: TextInputType.datetime,
                        decoration: const InputDecoration(
                            hintText: "Digite aqui",
                            border: InputBorder.none,
                            hintStyle: TextStyle(
                              color: Color.fromARGB(255, 121, 118, 118),
                              fontSize: 16,
                              fontFamily: 'Inter',
                              fontWeight: FontWeight.w500,
                            )),
                      ),
                    ),
                  ],
                ),
                Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const CustomText(text: 'Combustível Final'),
                    const SizedBox(height: 14),
                    Container(
                      width: (MediaQuery.of(context).size.width / 2) - 25,
                      height: 50,
                      padding: const EdgeInsets.symmetric(
                          horizontal: 16, vertical: 0),
                      decoration: ShapeDecoration(
                        shape: RoundedRectangleBorder(
                          side: const BorderSide(
                              width: 1, color: Color(0xFF636363)),
                          borderRadius: BorderRadius.circular(10),
                        ),
                      ),
                      child: TextField(
                        controller: _gasolinaFinal,
                        onChanged: (value) {
                          _obtainTotalLitros();
                          setState(() {});
                        },
                        keyboardType: TextInputType.datetime,
                        decoration: const InputDecoration(
                            hintText: "Digite aqui",
                            border: InputBorder.none,
                            hintStyle: TextStyle(
                              color: Color.fromARGB(255, 121, 118, 118),
                              fontSize: 16,
                              fontFamily: 'Inter',
                              fontWeight: FontWeight.w500,
                            )),
                      ),
                    ),
                  ],
                )
              ],
            ),
            const SizedBox(height: 20),
            const CustomText(text: 'Total de Litros'),
            const SizedBox(height: 14),
            Container(
              width: (MediaQuery.of(context).size.width / 2) - 25,
              height: 50,
              padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 0),
              decoration: ShapeDecoration(
                shape: RoundedRectangleBorder(
                  side: const BorderSide(width: 1, color: Color(0xFF636363)),
                  borderRadius: BorderRadius.circular(10),
                ),
              ),
              child: TextField(
                controller: _totalDeLitros,
                onChanged: (value) {},
                keyboardType: TextInputType.datetime,
                decoration: const InputDecoration(
                    hintText: "Digite aqui",
                    border: InputBorder.none,
                    hintStyle: TextStyle(
                      color: Color.fromARGB(255, 121, 118, 118),
                      fontSize: 16,
                      fontFamily: 'Inter',
                      fontWeight: FontWeight.w500,
                    )),
              ),
            ),
            const SizedBox(height: 20),
            const CustomText(text: "Extensão(ha)"),
            const SizedBox(height: 10),
            CustomTextField(
              textEditingController: _extensaoController,
              onChanged: (value) {},
              textInputType: TextInputType.number,
            ),
            const Text(
              'Piloto',
              style: TextStyle(
                color: Color(0xFF00B45D),
                fontSize: 14,
                fontFamily: 'Inter',
                fontWeight: FontWeight.w700,
                height: 0.11,
              ),
            ),
            const SizedBox(height: 12),
            AbsorbPointer(
              absorbing: getIt<GlobalConfigVars>()
                  .userPayload
                  .role!
                  .contains("Piloto"),
              child: CustomCombo(
                selectedName: getIt<GlobalConfigVars>()
                        .userPayload
                        .role!
                        .contains("Piloto")
                    ? getIt<GlobalConfigVars>().userPayload.name ?? ''
                    : getIt<GlobalConfigVars>().selectedPilot.isEmpty
                        ? "Selecione o piloto"
                        : getIt<GlobalConfigVars>().selectedPilot,
                onTap: () async {
                  Util.closeKeyBoard();
                  await showDialog(
                      context: context,
                      builder: (BuildContext context) {
                        return AlertDialog(
                            backgroundColor: const Color(0xFFF5F5F5),
                            content: SizedBox(
                              width: double.maxFinite,
                              child: PilotSelect(onChanged: (value) {
                                setState(() {
                                  getIt<GlobalConfigVars>().selectedPilot =
                                      value!;
                                });
                              }),
                            ));
                      });
                },
              ),
            ),
            const SizedBox(height: 20),
            const Text(
              'Executor',
              style: TextStyle(
                color: Color(0xFF00B45D),
                fontSize: 14,
                fontFamily: 'Inter',
                fontWeight: FontWeight.w700,
                height: 0.11,
              ),
            ),
            const SizedBox(height: 12),
            AbsorbPointer(
              absorbing: getIt<GlobalConfigVars>()
                      .userPayload
                      .role!
                      .contains("Executor") ||
                  getIt<GlobalConfigVars>()
                      .userPayload
                      .role!
                      .contains("TecnicoExecutor"),
              child: CustomCombo(
                selectedName: getIt<GlobalConfigVars>()
                            .userPayload
                            .role!
                            .contains("Executor") ||
                        getIt<GlobalConfigVars>()
                            .userPayload
                            .role!
                            .contains("TecnicoExecutor")
                    ? getIt<GlobalConfigVars>().userPayload.name!
                    : getIt<GlobalConfigVars>().selectedExecutor.isEmpty
                        ? "Selecione o executor"
                        : getIt<GlobalConfigVars>().selectedExecutor,
                onTap: () async {
                  Util.closeKeyBoard();
                  await showDialog(
                      context: context,
                      builder: (BuildContext context) {
                        return AlertDialog(
                            backgroundColor: const Color(0xFFF5F5F5),
                            content: SizedBox(
                              width: double.maxFinite,
                              child: ExecutorSelect(onChanged: (value) {
                                setState(() {
                                  getIt<GlobalConfigVars>().selectedExecutor =
                                      value!;
                                });
                              }),
                            ));
                      });
                },
              ),
            ),
            const SizedBox(height: 20),
            const CustomText(text: 'Data'),
            const SizedBox(height: 10),
            CustomComboBoxExpanded(
              selectedName: _dataSelecionada == null
                  ? "Selecione"
                  : DateFormat('dd/MM/yyyy').format(_dataSelecionada!),
              onTap: () async {
                final data = await showDatePicker(
                  confirmText: "Selecionar data",
                  cancelText: "Cancelar",
                  helpText: "",
                  context: context,
                  initialDate: DateTime.now(),
                  firstDate: DateTime(2024),
                  lastDate: DateTime(2028),
                );
                Util.closeKeyBoard();

                _dataSelecionada = data;
                setState(() {});
              },
            ),
            Center(
              child: CustomButton(
                title: "Finalizar",
                onClick: () async {},
              ),
            ),
            const SizedBox(height: 20),
          ],
        ),
      ),
    );
  }
}
