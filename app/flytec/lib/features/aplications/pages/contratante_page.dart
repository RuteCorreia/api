import 'package:flutter/material.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/components/custom_button.dart';
import 'package:flytec/features/aplications/components/custom_cliente_card.dart';
import 'package:flytec/features/aplications/controller/report_aplication_controller.dart';
import 'package:flytec/features/aplications/models/aplicacao.dart';
import 'package:flytec/features/aplications/models/contratante.dart';
import 'package:flytec/features/aplications/pages/create_new_contratante_page.dart';

class ContrantePage extends StatefulWidget {
  final ReportAplicationController _reportAplicationController;
  const ContrantePage(
      {required ReportAplicationController reportAplicationController,
      super.key})
      : _reportAplicationController = reportAplicationController;

  @override
  State<ContrantePage> createState() => _ContrantePageState();
}

class _ContrantePageState extends State<ContrantePage> {
  Contratante? _selectedContratante;

  Aplicacao get _aplicacao =>
      widget._reportAplicationController.aplicacaoSelected!;

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      if (_aplicacao.contratante?.id != null) {
        _selectedContratante = _aplicacao.contratante!;
        setState(() {});
      }
    });
  }

  void _onAddContratanteUpdateView() {
    setState(() {});
  }

  Future<void> _contratanteAction() async {
    if (_aplicacao.contratante?.id == null || _aplicacao.contratante!.id! <= 0) {
      int? idContratante = await widget._reportAplicationController
          .createElementInTable(_selectedContratante!.toMap(), 'Contratante');
      await widget._reportAplicationController.updateElementInTable(
          _aplicacao.id!, {'contratante_id': idContratante}, 'Aplicacao');
      await _updateContrante(idContratante!);
      return;
    }
    int? idContratante = _aplicacao.contratante!.id;
    await widget._reportAplicationController.updateElementInTable(
        idContratante!, _selectedContratante!.toMap(), 'Contratante');
    await _updateContrante(idContratante);
  }

  Future<void> _updateContrante(int id) async {
    final element = await widget._reportAplicationController
        .getElementById(id, 'Contratante');
    final identificacaoContratante = Contratante.fromJson(element);
    _selectedContratante = identificacaoContratante;
    setState(() {});
    _aplicacao.contratante = identificacaoContratante;
    widget._reportAplicationController.setAplicacaoSelected(_aplicacao);
    await widget._reportAplicationController.obtainReportsAplications();
    setState(() {});
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
          centerTitle: true,
          title: const Text(
            "Selecionar Cliente",
            textAlign: TextAlign.center,
            style: TextStyle(),
          ),
          leading: IconButton(
            icon: const Icon(Icons.arrow_back),
            onPressed: () async {
              await _contratanteAction();
              // ignore: use_build_context_synchronously
              Navigator.pop(context);
            },
          )),
      floatingActionButton: FloatingActionButton(
        onPressed: () {
          Navigator.push(context, MaterialPageRoute(builder: (context) {
            return CreateNewContratantePage(
              onAddContratanteUpdateView: _onAddContratanteUpdateView,
            );
          }));
        },
        child: const Icon(Icons.add, color: Colors.white),
      ),
      body: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(16),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              ListView.builder(
                  shrinkWrap: true,
                  physics: const NeverScrollableScrollPhysics(),
                  itemCount: getIt<GlobalConfigVars>().clientes.length,
                  itemBuilder: (ctx, index) {
                    return InkWell(
                      onTap: () {
                        _selectedContratante = Contratante.fromCliente(
                            getIt<GlobalConfigVars>().clientes[index]);
                        setState(() {});
                      },
                      child: CustomContratanteCard(
                        title: getIt<GlobalConfigVars>()
                            .clientes[index]
                            .nomeCliente,
                        isSelected: _selectedContratante?.idContrante ==
                            getIt<GlobalConfigVars>()
                                .clientes[index]
                                .idCliente
                                .toString(),
                      ),
                    );
                  }),
              const SizedBox(height: 20),
              Center(
                child: CustomButton(
                  title: "OK",
                  onClick: () async {
                    if (_selectedContratante == null) {
                      Util.toastAlerta("Selecione o cliente");
                      return;
                    }
                   
                    try {
                      await _contratanteAction();
                      widget._reportAplicationController.updateView!();
                      // ignore: use_build_context_synchronously
                      Navigator.pop(context);
                    } catch (e) {
                      Util.toastAlerta('Não foi possivel criar o cliente');
                    }
                  },
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
