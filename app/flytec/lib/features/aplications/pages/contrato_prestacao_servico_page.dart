import 'package:brasil_fields/brasil_fields.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/controller/report_aplication_controller.dart';
import 'package:flytec/features/aplications/models/aplicacao.dart';
import 'package:flytec/features/aplications/models/contrato_prestacao_servico.dart';
import 'package:flytec/features/aplications/components/components_exports.dart';
import 'package:intl/intl.dart';

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
  DateTime? _vencimentoController;
  final TextEditingController _distancia = TextEditingController();
  String? _precoUnidade = "";

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
      await _contratoPrestacaoServicoAction();
      Util.toastSucesso("Dados inseridos com sucesso");
      // ignore: use_build_context_synchronously
      Navigator.pop(context);
      return true;
    }
  }

  Future<void> _contratoPrestacaoServicoAction() async {
    _contratoPrestacaoServico = ContratoPrestacaoServico(
        preco: _precoController.text,
        extensao: _extensaoController.text,
        valorTotal: _valorTotalController.text,
        vencimento: _vencimentoController?.millisecondsSinceEpoch.toString(),
        distanciaPista: _distancia.text,
        executor: getIt<GlobalConfigVars>().selectedExecutor,
        nomePiloto: getIt<GlobalConfigVars>().selectedPilot,
        unidadePreco: _precoUnidade);
    if (_aplicacao.contratoPrestacaoServico?.id == null ||
        _aplicacao.contratoPrestacaoServico!.id! <= 0) {
      int? idContratoPrestacaoServico = await widget._reportAplicationController
          .createElementInTable(
              _contratoPrestacaoServico!.toMap(), 'ContratoPrestacaoServico');
      await widget._reportAplicationController.updateElementInTable(
          _aplicacao.id!,
          {'contratoPrestacaoServico_id': idContratoPrestacaoServico},
          'Aplicacao');
      await _updateContratoPrestacaoServico(idContratoPrestacaoServico!);
      return;
    }
    int? idContratoPrestacaoServico = _aplicacao.contratoPrestacaoServico?.id;
    await widget._reportAplicationController.updateElementInTable(
        idContratoPrestacaoServico!,
        _contratoPrestacaoServico!.toMap(),
        'ContratoPrestacaoServico');
    await _updateContratoPrestacaoServico(idContratoPrestacaoServico);
  }

  Future<void> _updateContratoPrestacaoServico(int id) async {
    final element = await widget._reportAplicationController
        .getElementById(id, 'ContratoPrestacaoServico');

    final contratoPrestacaoServico = ContratoPrestacaoServico.fromJson(element);
    _contratoPrestacaoServico = contratoPrestacaoServico;
    setState(() {});
    _aplicacao.contratoPrestacaoServico = contratoPrestacaoServico;
    widget._reportAplicationController.setAplicacaoSelected(_aplicacao);
    setState(() {});
  }

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      if (_aplicacao.contratoPrestacaoServico?.id != null) {
        _contratoPrestacaoServico = _aplicacao.contratoPrestacaoServico;
        _distancia.text = _contratoPrestacaoServico?.distanciaPista ?? "";
        _precoController.text = _contratoPrestacaoServico?.preco ?? "";
        _extensaoController.text = _contratoPrestacaoServico?.extensao ?? "";
        _valorTotalController.text =
            _contratoPrestacaoServico?.valorTotal ?? "";
        final epoch = int.tryParse(_contratoPrestacaoServico!.vencimento!);
        _vencimentoController =
            epoch != null ? DateTime.fromMillisecondsSinceEpoch(epoch) : null;

        _precoUnidade = _contratoPrestacaoServico?.unidadePreco;
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
          leading: IconButton(
            icon: const Icon(Icons.arrow_back),
            onPressed: () async {
              await _contratoPrestacaoServicoAction();
              // ignore: use_build_context_synchronously
              Navigator.pop(context);
            },
          )),
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
                          value: _precoUnidade == "ha",
                          onChanged: (value) {
                            setState(() {
                              _precoUnidade = "ha";
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
                          value: _precoUnidade == "hora",
                          onChanged: (value) {
                            setState(() {
                              _precoUnidade = "hora";
                            });
                          }),
                    ],
                  ),
                )
              ],
            ),
            const SizedBox(height: 10),
            CustomText(
                text: 'Extensão ${_precoUnidade == "ha" ? "ha" : "em horas"}'),
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
            CustomCombo(
              selectedName: _vencimentoController == null
                  ? "Selecione"
                  : DateFormat('dd/MM/yyyy').format(_vencimentoController!),
              onTap: () async {
                final dataS = await showDatePicker(
                  confirmText: "Selecionar data",
                  cancelText: "Cancelar",
                  helpText: "",
                  context: context,
                  //locale: const Locale("pt"),
                  initialDate: DateTime.now(),
                  firstDate: DateTime(2024),
                  lastDate: DateTime(2028),
                );
                Util.closeKeyBoard();

                setState(() {
                  _vencimentoController = dataS;
                });
              },
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
