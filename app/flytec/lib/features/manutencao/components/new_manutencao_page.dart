import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/components/components_exports.dart';
import 'package:flytec/features/manutencao/components/widgets/custom_checklist_card.dart';

class NewManutencaoPage extends StatefulWidget {
  const NewManutencaoPage({super.key});

  @override
  State<NewManutencaoPage> createState() => _NewManutencaoPageState();
}

class _NewManutencaoPageState extends State<NewManutencaoPage> {
  String _selectedAaeronave = '';
  final TextEditingController _horimetro = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Nova Manutenção'),
        centerTitle: true,
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: ListView(
          children: [
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
            const CustomText(text: 'Horímetro inicial'),
            const SizedBox(height: 10),
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
                controller: _horimetro,
                onChanged: (value) {},
                inputFormatters: [
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
                      height: 0.09,
                    )),
              ),
            ),
            const SizedBox(height: 20),
            const CustomCardWithColor(
              title: 'CheckList Revisão',
              isSelected: true,
            ),
            const SizedBox(height: 20),
            const CustomCardWithColor(
              height: 70,
              title: 'Manutenção de Componentes\ne Aeronave',
              isSelected: true,
            )
          ],
        ),
      ),
    );
  }
}
