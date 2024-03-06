import 'package:flutter/material.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/data/models/clientes_model.dart';
import 'package:flytec/features/aplications_v2/components/custom_button.dart';
import 'package:flytec/features/aplications_v2/components/custom_cliente_card.dart';
import 'package:flytec/features/aplications_v2/controller/report_aplication_controller.dart';
import 'package:flytec/features/aplications_v2/models/aplicacao.dart';
import 'package:flytec/features/aplications_v2/models/contratante.dart';
import 'package:flytec/features/aplications_v2/pages/create_new_contratante_page.dart';

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
  ClientesModel? _selectedContratante;

  Aplicacao get _aplicacao =>
      widget._reportAplicationController.aplicacaoSelected!;

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      if (_aplicacao.contratante?.nome != null) {
        _selectedContratante =
            ClientesModel.fromContratante(_aplicacao.contratante!);
        setState(() {});
      }
    });
  }

  void _onAddContratanteUpdateView() {
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
      ),
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
                        _selectedContratante =
                            getIt<GlobalConfigVars>().clientes[index];
                        setState(() {});
                      },
                      child: CustomContratanteCard(
                        title: getIt<GlobalConfigVars>()
                            .clientes[index]
                            .nomeCliente,
                        isSelected: _selectedContratante?.idCliente ==
                            getIt<GlobalConfigVars>().clientes[index].idCliente,
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
                    if (_aplicacao.contratante?.nome != null) {
                      Navigator.pop(context);
                      return;
                    }
                    try {
                      Contratante contratante = Contratante(
                          cidade: _selectedContratante?.cidade,
                          cnpj: _selectedContratante?.cnpj,
                          cpf: _selectedContratante?.cpf,
                          endereco: _selectedContratante?.endereco,
                          inscricaoEstadual:
                              _selectedContratante?.inscricaoEstadual,
                          nome: _selectedContratante?.nomeCliente,
                          rg: _selectedContratante?.rg,
                          id: _selectedContratante?.idCliente.toString(),
                          tipoContratante:
                              _selectedContratante?.idTipoCliente == 0
                                  ? "Pessoa Física"
                                  : "Pessoa Jurídica");
                      int idContrante = await widget._reportAplicationController
                          .createContrante(contratante);
                      await widget._reportAplicationController
                          .updateContratanteAplicacao(
                              idContrante, _aplicacao.id!);
                      final aplicacao = _aplicacao;
                      aplicacao.contratante = contratante;
                      widget._reportAplicationController
                          .setAplicacaoSelected(aplicacao);
                      Util.toastSucesso("Cliente selecionado");
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
