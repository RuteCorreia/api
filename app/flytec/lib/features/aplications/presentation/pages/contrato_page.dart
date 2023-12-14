import 'package:flutter/material.dart';
import 'package:flytec/features/auth/presentation/widgets/custom_login_button.dart';
import 'package:go_router/go_router.dart';

import 'my_activity_page.dart';

class ContratoPrestacaoService extends StatefulWidget {
  const ContratoPrestacaoService({super.key});

  @override
  State<ContratoPrestacaoService> createState() =>
      _ContratoPrestacaoServiceState();
}

enum Preco { Hora, Ha, Nenhum }

class _ContratoPrestacaoServiceState extends State<ContratoPrestacaoService> {
  Preco _preco = Preco.Nenhum;
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
        title: const Text(
          "Contrato de prestação de\n serviços",
          textAlign: TextAlign.center,
          style: TextStyle(
            fontSize: 15,
          ),
        ),
      ),
      body: Padding(
        padding: const EdgeInsets.all(12),
        child: ListView(
          children: [
            const SizedBox(height: 16),
            const CustomText(text: 'Selecione a distância da pista'),
            const SizedBox(height: 14),
            const ComboBox(selectedName: "Selecione"),
            const SizedBox(height: 20),
            const CustomText(text: 'Preço'),
            const SizedBox(height: 14),
            const ComboBox(selectedName: "-"),
            const SizedBox(height: 5),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Flexible(
                  child: Row(
                    children: [
                      const Text("Preço por ha"),
                      Checkbox(
                          materialTapTargetSize:
                              MaterialTapTargetSize.shrinkWrap,
                          value: _preco == Preco.Ha,
                          onChanged: (value) {
                            setState(() {
                              _preco = Preco.Ha;
                            });
                          }),
                    ],
                  ),
                ),
                Flexible(
                  child: Row(
                    children: [
                      const Text("Preço por hora"),
                      Checkbox(
                          materialTapTargetSize:
                              MaterialTapTargetSize.shrinkWrap,
                          value: _preco == Preco.Hora,
                          onChanged: (value) {
                            setState(() {
                              _preco = Preco.Hora;
                            });
                          }),
                    ],
                  ),
                )
              ],
            ),
            const SizedBox(height: 20),
            const CustomText(text: 'Extensão ha'),
            const SizedBox(height: 14),
            const CustomTextField(
              text: "-",
              keyboardType: TextInputType.number,
            ),
            const SizedBox(height: 14),
            const CustomText(text: 'Valor total'),
            const SizedBox(height: 14),
            const CustomTextField(
              text: "-",
            ),
            const SizedBox(height: 14),
            const CustomText(text: 'Vencimento'),
            const SizedBox(height: 14),
            const CustomTextField(
              text: "-",
            ),
            const SizedBox(height: 14),
            const CustomText(text: 'Nome do piloto'),
            const SizedBox(height: 14),
            const CustomTextField(
              text: "-",
            ),
            const SizedBox(height: 14),
            const CustomText(text: 'Executor'),
            const SizedBox(height: 14),
            const CustomTextField(
              text: "-",
            ),
            Center(
              child: CustomButton(
                title: "OK",
                onClick: () {
                  context.pop();
                },
              ),
            ),
          ],
        ),
      ),
    );
  }
}

class CustomTextField extends StatelessWidget {
  const CustomTextField(
      {super.key,
      this.text = "Digite aqui",
      this.keyboardType = TextInputType.text});
  final String text;
  final TextInputType keyboardType;
  @override
  Widget build(BuildContext context) {
    return Container(
      width: double.infinity,
      height: 50,
      margin: const EdgeInsets.only(bottom: 10),
      padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 10),
      decoration: ShapeDecoration(
        shape: RoundedRectangleBorder(
          side: const BorderSide(width: 1, color: Color(0xFF636363)),
          borderRadius: BorderRadius.circular(10),
        ),
      ),
      child: TextField(
        keyboardType: keyboardType,
        decoration: InputDecoration(
            hintText: text,
            border: InputBorder.none,
            hintStyle: const TextStyle(
              color: Color.fromARGB(255, 121, 118, 118),
              fontSize: 16,
              fontFamily: 'Inter',
              fontWeight: FontWeight.w500,
              height: 0.09,
            )),
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
                    height: 0.09,
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
