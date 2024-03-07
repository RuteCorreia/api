import 'package:brasil_fields/brasil_fields.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications_v2/controller/report_aplication_controller.dart';
import 'package:flytec/features/aplications_v2/models/aplicacao.dart';
import 'package:flytec/features/aplications_v2/models/contrato_prestacao_servico.dart';
import 'package:flytec/features/aplications_v2/components/components_exports.dart';
import 'package:intl/intl.dart';
import 'package:mask_text_input_formatter/mask_text_input_formatter.dart';

class ContratoPrestacaoServicoPage extends StatefulWidget {
  final ReportAplicationController _reportAplicationController;
  const ContratoPrestacaoServicoPage(
      {required ReportAplicationController reportAplicationController,
      super.key})
      : _reportAplicationController = reportAplicationController;

  @override
  State<ContratoPrestacaoServicoPage> createState() =>
      _ContratoPrestacaoServicoPageState();
}

class _ContratoPrestacaoServicoPageState
    extends State<ContratoPrestacaoServicoPage> {
  ContratoPrestacaoServico? _contratoPrestacaoServico;

  Aplicacao get _aplicacao =>
      widget._reportAplicationController.aplicacaoSelected!;

  final TextEditingController _precoController = TextEditingController();
  final TextEditingController _extensaoController = TextEditingController();
  final TextEditingController _valorTotalController = TextEditingController();
  final TextEditingController _vencimentoController = TextEditingController();
  final TextEditingController _distancia = TextEditingController();
  String? _preco = "";

  final _maskFormatter = MaskTextInputFormatter(
      mask: '##/##/####', filter: {"#": RegExp(r'[0-9]')});

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

  Future<bool> _verifyFields() async {
    if (_distancia.text.isEmpty) {
      Util.toastAlerta("Digite a distância da pista");
      return false;
    } else if (_precoController.text.isEmpty) {
      Util.toastAlerta("Digite o preço");
      return false;
    } else if (_extensaoController.text.isEmpty) {
      Util.toastAlerta("Digite a extensão");
      return false;
    }
    if (_valorTotalController.text.isEmpty) {
      Util.toastAlerta("Digite o valor total");
      return false;
    } else {
      Util.toastSucesso("Dados inseridos com sucesso");
      Navigator.pop(context);
      return true;
    }
  }

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      if (_aplicacao.contratoPrestacaoServico?.executor != null) {
        _contratoPrestacaoServico = _aplicacao.contratoPrestacaoServico!;
        _contratoPrestacaoServico!.preco = _precoController.text;
        _contratoPrestacaoServico!.extensao = _extensaoController.text;
        _contratoPrestacaoServico!.valorTotal = _valorTotalController.text;
        _contratoPrestacaoServico!.vencimento = _vencimentoController.text;
        _contratoPrestacaoServico!.distanciaPista = _distancia.text;
        _contratoPrestacaoServico!.preco = _preco;
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
          "Contrato de prestação de\n serviços",
          textAlign: TextAlign.center,
        ),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: ListView(
          children: [
            const CustomText(text: 'Selecione a distância da pista'),
            const SizedBox(height: 10),
            CustomTextField(
              onChanged: (value) {},
              textInputType: TextInputType.number,
              textEditingController: _distancia,
            ),
            const SizedBox(height: 10),
            const CustomText(text: 'Preço'),
            const SizedBox(height: 10),
            CustomTextField(
              onChanged: (value) {
                _changePrice();
              },
              formater: [
                FilteringTextInputFormatter.digitsOnly,
                CentavosInputFormatter(moeda: true)
              ],
              textInputType: TextInputType.number,
              textEditingController: _precoController,
              text: "Digite aqui",
            ),
            const SizedBox(height: 10),
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
                          value: _preco == "ha",
                          onChanged: (value) {
                            setState(() {
                              _preco = "ha";
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
                          value: _preco == "h",
                          onChanged: (value) {
                            setState(() {
                              _preco = "h";
                            });
                          }),
                    ],
                  ),
                )
              ],
            ),
            const SizedBox(height: 10),
            CustomText(text: 'Extensão ${_preco == "ha" ? "ha" : "em horas"}'),
            const SizedBox(height: 10),
            CustomTextField(
              onChanged: (value) {
                _changePrice();
              },
              textEditingController: _extensaoController,
              textInputType: TextInputType.number,
              text: "Digite aqui",
            ),
            const SizedBox(height: 10),
            const CustomText(text: 'Valor total'),
            const SizedBox(height: 10),
            CustomTextField(
              onChanged: (value) {},
              formater: [
                FilteringTextInputFormatter.digitsOnly,
                CentavosInputFormatter(moeda: true)
              ],
              textEditingController: _valorTotalController,
              textInputType: TextInputType.number,
              text: "Digite aqui",
            ),
            const SizedBox(height: 10),
            const CustomText(text: 'Vencimento'),
            const SizedBox(height: 10),
            CustomTextField(
              onChanged: (value) {},
              textEditingController: _vencimentoController,
              textInputType: TextInputType.number,
              formater: [
                _maskFormatter,
              ],
              text: "DD/MM/YY",
            ),
            const SizedBox(height: 10),
            const CustomText(text: 'Nome do piloto'),
            const SizedBox(height: 10),
            CustomTextField(
              onChanged: (value) {},
              disabled: true,
              textEditingController: null,
              text: getIt<GlobalConfigVars>().selectedPilot,
            ),
            const SizedBox(height: 10),
            const CustomText(text: 'Executor'),
            const SizedBox(height: 10),
            CustomTextField(
              onChanged: (v) {},
              disabled: true,
              textEditingController: null,
              text: getIt<GlobalConfigVars>().selectedExecutor,
            ),
            Center(
              child: CustomButton(
                title: "OK",
                onClick: () async {
                  await _verifyFields();
                },
              ),
            ),
          ],
        ),
      ),
    );
  }
}
