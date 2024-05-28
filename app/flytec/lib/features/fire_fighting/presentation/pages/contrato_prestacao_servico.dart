// ignore_for_file: use_build_context_synchronously
import 'package:brasil_fields/brasil_fields.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/fire_fighting/models/contrato_prestacao_servico.dart';
import 'package:flytec/features/fire_fighting/models/firefighting.dart';

import 'package:flytec/features/fire_fighting/presentation/pages/steps/add_firefighthing_fourth_step_private.dart';
import 'package:flytec/features/fire_fighting/presentation/pages/steps/add_firefighting_fourth_step_public.dart';
import 'package:flytec/features/aplications/components/components_exports.dart';
import 'package:flytec/features/fire_fighting/controller/firefighting_controller.dart';
import 'package:intl/intl.dart';

class ContratoPrestacaoServicoPage extends StatefulWidget {
  final FirefightingController? _firefightingController;
  const ContratoPrestacaoServicoPage(
      {required FirefightingController firefightingController, super.key})
      : _firefightingController = firefightingController;

  @override
  State<ContratoPrestacaoServicoPage> createState() =>
      _ContratoPrestacaoServicoPageState();
}

class _ContratoPrestacaoServicoPageState
    extends State<ContratoPrestacaoServicoPage> {
  Firefighting? _firefighting;

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

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) async {
      _firefighting = widget._firefightingController?.firefightingSelected;

      if (_firefighting!.idContratoPrestacaoServico != null) {
        final contrato = await widget._firefightingController?.getElementById(
            _firefighting!.idContratoPrestacaoServico!,
            'ContratoPrestacaoServicoFirefighting');
        _firefighting?.contratoPrestacaoServico =
            ContratoPrestacaoServico.fromJson(contrato);
        _precoController.text =
            _firefighting!.contratoPrestacaoServico?.preco ?? '';
        _extensaoController.text =
            _firefighting!.contratoPrestacaoServico?.extensao ?? '';
        _valorTotalController.text =
            _firefighting!.contratoPrestacaoServico?.valorTotal ?? '';
        _vencimentoController =
            _firefighting!.contratoPrestacaoServico?.vencimento != null
                ? DateTime.fromMillisecondsSinceEpoch(
                    _firefighting!.contratoPrestacaoServico!.vencimento!)
                : DateTime.now();
        _distancia.text =
            _firefighting!.contratoPrestacaoServico?.distanciaPista ?? '';
        _precoUnidade = 'hora';
      }
      setState(() {});
    });
  }

  Future<void> _actionContratoPrestacaoServico() async {
    ContratoPrestacaoServico contratoPrestacaoServico =
        ContratoPrestacaoServico(
      distanciaPista: _distancia.text,
      preco: _precoController.text,
      extensao: _extensaoController.text,
      valorTotal: _valorTotalController.text,
      vencimento: _vencimentoController?.millisecondsSinceEpoch,
      nomePiloto: getIt<GlobalConfigVars>().selectedPilot,
      executor: getIt<GlobalConfigVars>().selectedExecutor,
    );
    if (contratoPrestacaoServico.id == null ||
        (contratoPrestacaoServico.id != null &&
            contratoPrestacaoServico.id! <= 0)) {
      final idContrato = await widget._firefightingController
          ?.createElementInTable(contratoPrestacaoServico.toJson(),
              'ContratoPrestacaoServicoFirefighting');

      await widget._firefightingController?.updateElementInTable(
          _firefighting!.id!,
          {'contratoPrestacaoServicoFirefighting_id': idContrato},
          'Firefighting');

      _firefighting?.idContratoPrestacaoServico = idContrato;
      contratoPrestacaoServico.id = idContrato;
      _firefighting?.contratoPrestacaoServico = contratoPrestacaoServico;
      widget._firefightingController?.setFirefightingSelected(_firefighting!);
      return;
    }
    await widget._firefightingController?.updateElementInTable(
        contratoPrestacaoServico.id!,
        contratoPrestacaoServico.toJson(),
        'ContratoPrestacaoServicoFirefighting');
    _firefighting?.idContratoPrestacaoServico = contratoPrestacaoServico.id;
    contratoPrestacaoServico.id = contratoPrestacaoServico.id;
    _firefighting?.contratoPrestacaoServico = contratoPrestacaoServico;
    widget._firefightingController?.setFirefightingSelected(_firefighting!);
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
              await _actionContratoPrestacaoServico();
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
              children: [
                const Text("Preço por hora"),
                Checkbox(
                    materialTapTargetSize: MaterialTapTargetSize.shrinkWrap,
                    value: _precoUnidade == "hora",
                    onChanged: (value) {
                      setState(() {
                        _precoUnidade = "hora";
                      });
                    }),
              ],
            ),
            const SizedBox(height: 10),
            const CustomText(text: 'Extensão em horas'),
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
                title: "Próximo",
                onClick: () async {
                  await _actionContratoPrestacaoServico();
                  if (_firefighting!.privado!) {
                    Navigator.push(
                        context,
                        MaterialPageRoute(
                            builder: (context) =>
                                AddFireFightingFourthtepPrivate(
                                  firefightingController:
                                      widget._firefightingController,
                                )));
                    return;
                  }
                  Navigator.push(
                      context,
                      MaterialPageRoute(
                          builder: (context) => AddFireFightingFourthtepPublic(
                                firefightingController:
                                    widget._firefightingController,
                              )));
                },
              ),
            ),
          ],
        ),
      ),
    );
  }
}
