import 'package:brasil_fields/brasil_fields.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/data/models/relatorio_model.dart';
import 'package:flytec/features/auth/presentation/widgets/custom_login_button.dart';
import 'package:go_router/go_router.dart';
import 'package:intl/intl.dart';
import 'package:mask_text_input_formatter/mask_text_input_formatter.dart';

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
  final TextEditingController _precoController = TextEditingController();
  final TextEditingController _extensaoController = TextEditingController();
  final TextEditingController _valorTotalController = TextEditingController();
  final TextEditingController _vencimentoController = TextEditingController();
  var maskFormatter = MaskTextInputFormatter(
      mask: '##/##/####', filter: {"#": RegExp(r'[0-9]')});

  final TextEditingController _distancia = TextEditingController();

  String _changePrice() {
    final price = double.parse(_precoController.text
        .replaceAll("R\$", "")
        .replaceAll(".", "")
        .replaceAll(",", '.'));

    _valorTotalController.text =
        NumberFormat.currency(locale: 'pt_BR', symbol: 'R\$').format((price *
            double.parse(_extensaoController.text.isEmpty
                ? "0.0"
                : _extensaoController.text)));
    return _valorTotalController.text;
  }

  bool verifyFields() {
    // if (_precoController.text.isEmpty) {
    //   Util.toastAlerta("Digite o preço");
    //   return false;
    // } else if (_extensaoController.text.isEmpty) {
    //   Util.toastAlerta("Digite a extensão");
    //   return false;
    // }
    // if (_valorTotalController.text.isEmpty) {
    //   Util.toastAlerta("Digite o valor total");
    //   return false;
    // }
    // if (_vencimentoController.text.isEmpty) {
    //   Util.toastAlerta("Digite o vencimento");
    //   return false;
    // }
    // if (_vencimentoController.text.length < 10) {
    //   Util.toastAlerta("Digite uma data válida");
    //   return false;
    // } else {
    getIt<GlobalConfigVars>().reportList.last.contratoServico = ContratoServico(
        executor: getIt<GlobalConfigVars>().selectedExecutor,
        nomePiloto: getIt<GlobalConfigVars>().selectedPilot,
        extensao: _extensaoController.text,
        vencimento: _vencimentoController.text,
        preco: _precoController.text,
        valorTotal: _valorTotalController.text,
        tipoPreco: _preco == Preco.Ha ? "ha" : "hora",
        distanciaDaPista: _distancia.text);

    Util.toastSucesso("Dados inseridos com sucesso");
    context.pop();
    return true;
    //}
  }

  @override
  void initState() {
    var data = getIt<GlobalConfigVars>().reportList.last.contratoServico;
    _distancia.text = data!.distanciaDaPista!;
    _precoController.text = data.preco!;
    _extensaoController.text = data.extensao!;
    _valorTotalController.text = data.valorTotal!;
    _vencimentoController.text = data.vencimento!;
    data.nomePiloto = getIt<GlobalConfigVars>().selectedPilot;
    data.executor = getIt<GlobalConfigVars>().selectedExecutor;

    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    var data = getIt<GlobalConfigVars>().reportList.last.contratoServico;

    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
        title: const Text(
          "Contrato de prestação de\n serviços",
          textAlign: TextAlign.center,
        ),
      ),
      body: Padding(
        padding: const EdgeInsets.all(12),
        child: ListView(
          children: [
            const SizedBox(height: 16),
            const CustomText(text: 'Selecione a distância da pista'),
            const SizedBox(height: 14),
            CustomTextField(
              onChanged: (value) {
                data!.distanciaDaPista = value;
              },
              keyboardType: TextInputType.number,
              controller: _distancia,
              text: "Digite aqui",
            ),
            //const ComboBox(selectedName: "Selecione"),
            const SizedBox(height: 20),
            const CustomText(text: 'Preço'),
            const SizedBox(height: 14),
            CustomTextField(
              onChanged: (value) {
                data!.preco = value;
                data.valorTotal = _changePrice();
              },
              formater: [
                FilteringTextInputFormatter.digitsOnly,
                CentavosInputFormatter(moeda: true)
              ],
              keyboardType: TextInputType.number,
              controller: _precoController,
              text: "Digite aqui",
            ),
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
            CustomText(
                text: 'Extensão ${_preco == Preco.Ha ? "ha" : "em horas"}'),
            const SizedBox(height: 14),
            CustomTextField(
              onChanged: (value) {
                data!.extensao = value;
                data.valorTotal = _changePrice();
              },
              controller: _extensaoController,
              keyboardType: TextInputType.number,
              text: "Digite aqui",
            ),
            const SizedBox(height: 14),
            const CustomText(text: 'Valor total'),
            const SizedBox(height: 14),
            CustomTextField(
              onChanged: (value) {
                data!.valorTotal = value;
              },
              formater: [
                FilteringTextInputFormatter.digitsOnly,
                CentavosInputFormatter(moeda: true)
              ],
              controller: _valorTotalController,
              keyboardType: TextInputType.number,
              text: "Digite aqui",
            ),
            const SizedBox(height: 14),
            const CustomText(text: 'Vencimento'),
            const SizedBox(height: 14),
            CustomTextField(
              onChanged: (value) {
                data!.vencimento = value;
              },
              controller: _vencimentoController,
              keyboardType: TextInputType.number,
              formater: [
                maskFormatter,
              ],
              text: "DD/MM/YY",
            ),
            const SizedBox(height: 14),
            const CustomText(text: 'Nome do piloto'),
            const SizedBox(height: 14),
            CustomTextField(
              onChanged: (value) {},
              disabled: true,
              controller: null,
              text: getIt<GlobalConfigVars>().selectedPilot,
            ),
            const SizedBox(height: 14),
            const CustomText(text: 'Executor'),
            const SizedBox(height: 14),
            CustomTextField(
              onChanged: (v) {},
              disabled: true,
              controller: null,
              text: getIt<GlobalConfigVars>().selectedExecutor,
            ),
            Center(
              child: CustomButton(
                title: "OK",
                onClick: () {
                  verifyFields();
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
      this.disabled = false,
      this.formater = const [],
      required this.onChanged,
      this.keyboardType = TextInputType.text,
      required this.controller});
  final TextEditingController? controller;
  final String text;
  final TextInputType keyboardType;
  final bool disabled;
  final List<TextInputFormatter> formater;
  final Function(String value) onChanged;
  @override
  Widget build(BuildContext context) {
    return AbsorbPointer(
      absorbing: disabled,
      child: Container(
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
          controller: controller,
          onChanged: onChanged,
          inputFormatters: formater,
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
