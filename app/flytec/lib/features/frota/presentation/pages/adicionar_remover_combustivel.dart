import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/core/widgets/combo_box.dart';
import 'package:flytec/core/widgets/custom_text.dart';
import 'package:flytec/features/aplications/components/custom_button.dart';
import 'package:flytec/features/fire_fighting/presentation/pages/steps/add_firefighting_third_step.dart';
import 'package:flytec/features/frota/presentation/components/veiculo_select.dart';
import 'package:intl/intl.dart';

class AdicionarRemoverCombustivelAeronavePage extends StatefulWidget {
  const AdicionarRemoverCombustivelAeronavePage({super.key});

  @override
  State<AdicionarRemoverCombustivelAeronavePage> createState() =>
      _AdicionarRemoverCombustivelAeronavePageState();
}

class _AdicionarRemoverCombustivelAeronavePageState
    extends State<AdicionarRemoverCombustivelAeronavePage> {
  String _selectedVeiculo = '';
  final TextEditingController _inserirCombustivel = TextEditingController();
  final TextEditingController _removerCombustivel = TextEditingController();
  DateTime? _dataSelecionada;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Inserir ou remover\n combustível'),
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
            const CustomText(text: 'Inserir Combustível (Litros)'),
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
                controller: _inserirCombustivel,
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
            const CustomText(text: 'Remover Combustível (Litros)'),
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
                controller: _removerCombustivel,
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
