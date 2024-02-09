import 'package:flutter/material.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/data/models/relatorio_model.dart';
import 'package:flytec/features/auth/presentation/widgets/custom_login_button.dart';
import 'package:go_router/go_router.dart';

class ClienteModel {
  ClienteModel({required this.nome, this.isSelected = false});
  final String nome;
  bool isSelected;
}

class AplicationThirdStep extends StatefulWidget {
  const AplicationThirdStep({super.key});

  @override
  State<AplicationThirdStep> createState() => _AplicationThirdStepState();
}

class _AplicationThirdStepState extends State<AplicationThirdStep> {
  late DateTime? dataSelecionada = DateTime.now();
  late TimeOfDay? time = const TimeOfDay(hour: 12, minute: 43);
  late TimeOfDay? horimetro = const TimeOfDay(hour: 15, minute: 43);
  String selectedClient = "";
  @override
  void initState() {
    super.initState();
    setState(() {
    });
  }

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
  }

  @override
  void dispose() {
    super.dispose();
  }

  void _onAddContratante() => setState(() {});

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
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: SingleChildScrollView(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const SizedBox(height: 25),
              SizedBox(
                child: ListView.builder(
                    shrinkWrap: true,
                    physics: const NeverScrollableScrollPhysics(),
                    itemCount: getIt<GlobalConfigVars>().clientes.length,
                    itemBuilder: (ctx, index) {
                      return InkWell(
                        onTap: () {
                          setState(() {
                            for (var element
                                in getIt<GlobalConfigVars>().clientes) {
                              element.isSelected = false;
                              selectedClient = getIt<GlobalConfigVars>()
                                  .clientes[index]
                                  .nomeCliente!;
                            }
                            getIt<GlobalConfigVars>()
                                .clientes[index]
                                .isSelected = true;
                            getIt<GlobalConfigVars>().reportList.last.cliente =
                                Cliente(
                              endereco: getIt<GlobalConfigVars>()
                                  .clientes[index]
                                  .endereco,
                              cpf:
                                  getIt<GlobalConfigVars>().clientes[index].cpf,
                              rg: getIt<GlobalConfigVars>().clientes[index].rg,
                              cidade: getIt<GlobalConfigVars>()
                                  .clientes[index]
                                  .cidade,
                              uf: getIt<GlobalConfigVars>().clientes[index].uf,
                              id: getIt<GlobalConfigVars>()
                                  .clientes[index]
                                  .idCliente
                                  .toString(),
                              nome: getIt<GlobalConfigVars>()
                                  .clientes[index]
                                  .nomeCliente,
                              cnpj: getIt<GlobalConfigVars>()
                                  .clientes[index]
                                  .cnpj,
                            );
                          });
                        },
                        child: CustomClientCard(
                          title: getIt<GlobalConfigVars>()
                              .clientes[index]
                              .nomeCliente,
                          isSelected: getIt<GlobalConfigVars>()
                              .clientes[index]
                              .isSelected,
                        ),
                      );
                    }),
              ),
              const SizedBox(height: 20),

              /*  Center(
                child: CustomButton(
                  title: "Próximo",
                  onClick: () {
                    context.push("/combateIncendioPasso3");
                  },
                ),
              ), */
              Center(
                child: CustomButton(
                  title: "OK",
                  onClick: () {
                    if (selectedClient.isEmpty) {
                      Util.toastAlerta("Selecione o cliente");
                      return;
                    } else {
                      Util.toastSucesso("Cliente selecionado");
                      context.pop();
                    }
                  },
                ),
              ),
            ],
          ),
        ),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () {
          context.push("/addcontratante",
              extra: {"onAddContratante": _onAddContratante});
        },
        child: const Icon(
          Icons.add,
          color: Colors.white,
        ),
      ),
    );
  }
}

// ignore: must_be_immutable
class CustomClientCard extends StatefulWidget {
  CustomClientCard({super.key, required this.title, this.isSelected = false});
  final String? title;
  bool? isSelected;

  @override
  State<CustomClientCard> createState() => _CustomClientCardState();
}

class _CustomClientCardState extends State<CustomClientCard> {
  @override
  Widget build(BuildContext context) {
    return Container(
      width: double.infinity,
      height: 67,
      margin: const EdgeInsets.symmetric(vertical: 10),
      padding: const EdgeInsets.all(16),
      clipBehavior: Clip.antiAlias,
      decoration: ShapeDecoration(
        color: Colors.white,
        shape: RoundedRectangleBorder(
          side: const BorderSide(width: 2, color: Color(0xFF00B45D)),
          borderRadius: BorderRadius.circular(8),
        ),
      ),
      child: Row(
        mainAxisSize: MainAxisSize.min,
        mainAxisAlignment: MainAxisAlignment.start,
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
          Container(
            width: 30,
            height: 30,
            padding: const EdgeInsets.all(1),
            clipBehavior: Clip.antiAlias,
            decoration: ShapeDecoration(
              shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(3.49),
              ),
            ),
            child: Column(
              mainAxisSize: MainAxisSize.min,
              mainAxisAlignment: MainAxisAlignment.center,
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                widget.isSelected!
                    ? const Icon(
                        Icons.check,
                        color: Colors.green,
                      )
                    : const SizedBox()
              ],
            ),
          ),
          const SizedBox(width: 1),
          Expanded(
            child: Column(
              mainAxisSize: MainAxisSize.min,
              mainAxisAlignment: MainAxisAlignment.center,
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                SizedBox(
                  width: double.infinity,
                  child: Row(
                    mainAxisSize: MainAxisSize.min,
                    mainAxisAlignment: MainAxisAlignment.center,
                    crossAxisAlignment: CrossAxisAlignment.center,
                    children: [
                      Expanded(
                        child: Column(
                          mainAxisSize: MainAxisSize.min,
                          mainAxisAlignment: MainAxisAlignment.center,
                          crossAxisAlignment: CrossAxisAlignment.center,
                          children: [
                            SizedBox(
                              width: double.infinity,
                              child: Text(
                                widget.title!,
                                style: const TextStyle(
                                  color: Color.fromARGB(255, 121, 118, 118),
                                  fontSize: 13,
                                  fontFamily: 'Inter',
                                  fontWeight: FontWeight.w600,
                                  height: 0.09,
                                ),
                              ),
                            ),
                          ],
                        ),
                      ),
                    ],
                  ),
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}
