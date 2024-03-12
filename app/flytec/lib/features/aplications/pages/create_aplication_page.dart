import 'package:flutter/material.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/components/custom_button.dart';
import 'package:flytec/features/aplications/components/custom_combo.dart';
import 'package:flytec/features/aplications/components/executor_select.dart';
import 'package:flytec/features/aplications/components/pilot_select.dart';
import 'package:flytec/features/aplications/controller/report_aplication_controller.dart';
import 'package:flytec/features/aplications/enums/report_dashboard_state.dart';
import 'package:flytec/features/aplications/models/aplicacao.dart';
import 'package:flytec/features/aplications/pages/menu_aplication_page.dart';

class CreateAplicationPage extends StatefulWidget {
  final ReportAplicationController _reportAplicationController;
  final VoidCallback? _updateView;
  const CreateAplicationPage(
      {required ReportAplicationController reportAplicationController,
      required VoidCallback? updateView,
      super.key})
      : _reportAplicationController = reportAplicationController,
        _updateView = updateView;

  @override
  State<CreateAplicationPage> createState() => _CreateAplicationPageState();
}

class _CreateAplicationPageState extends State<CreateAplicationPage> {
  @override
  void initState() {
    super.initState();
    getIt<GlobalConfigVars>().selectedPilot =
        getIt<GlobalConfigVars>().userPayload.name ?? '';
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
        leading: IconButton(
            onPressed: () async {
              await widget._reportAplicationController
                  .obtainReportsAplications();
              getIt<GlobalConfigVars>().clearGlobalConfigVars();
              widget._updateView!();
              // ignore: use_build_context_synchronously
              Navigator.pop(context);
            },
            icon: const Icon(Icons.arrow_back)),
        title: const Text(
          "Aplicações",
          textAlign: TextAlign.center,
        ),
      ),
      body: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(16.0),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Text(
                'Piloto',
                style: TextStyle(
                  color: Color(0xFF00B45D),
                  fontSize: 14,
                  fontFamily: 'Inter',
                  fontWeight: FontWeight.w700,
                  height: 0.11,
                ),
              ),
              const SizedBox(height: 12),
              AbsorbPointer(
                absorbing:
                    getIt<GlobalConfigVars>().userPayload.role == "Piloto",
                child: CustomCombo(
                  selectedName:
                      getIt<GlobalConfigVars>().userPayload.role == "Piloto"
                          ? getIt<GlobalConfigVars>().userPayload.name ?? ''
                          : getIt<GlobalConfigVars>().selectedPilot.isEmpty
                              ? "Selecione o piloto"
                              : getIt<GlobalConfigVars>().selectedPilot,
                  onTap: () async {
                    Util.closeKeyBoard();
                    await showDialog(
                        context: context,
                        builder: (BuildContext context) {
                          return AlertDialog(
                              backgroundColor: const Color(0xFFF5F5F5),
                              content: SizedBox(
                                width: double.maxFinite,
                                child: PilotSelect(onChanged: (value) {
                                  setState(() {
                                    getIt<GlobalConfigVars>().selectedPilot =
                                        value!;
                                  });
                                }),
                              ));
                        });
                  },
                ),
              ),
              const SizedBox(height: 20),
              const Text(
                'Executor',
                style: TextStyle(
                  color: Color(0xFF00B45D),
                  fontSize: 14,
                  fontFamily: 'Inter',
                  fontWeight: FontWeight.w700,
                  height: 0.11,
                ),
              ),
              const SizedBox(height: 12),
              AbsorbPointer(
                absorbing:
                    getIt<GlobalConfigVars>().userPayload.role == "Executor",
                child: CustomCombo(
                  selectedName:
                      getIt<GlobalConfigVars>().userPayload.role == "Executor"
                          ? getIt<GlobalConfigVars>().userPayload.name!
                          : getIt<GlobalConfigVars>().selectedExecutor.isEmpty
                              ? "Selecione o executor"
                              : getIt<GlobalConfigVars>().selectedExecutor,
                  onTap: () async {
                    Util.closeKeyBoard();
                    await showDialog(
                        context: context,
                        builder: (BuildContext context) {
                          return AlertDialog(
                              backgroundColor: const Color(0xFFF5F5F5),
                              content: SizedBox(
                                width: double.maxFinite,
                                child: ExecutorSelect(onChanged: (value) {
                                  setState(() {
                                    getIt<GlobalConfigVars>().selectedExecutor =
                                        value!;
                                  });
                                }),
                              ));
                        });
                  },
                ),
              ),
              const SizedBox(height: 40),
              Center(
                child: Image.asset(
                  "assets/images/logotipo.png",
                  height: 200,
                ),
              ),
              Center(
                child: CustomButton(
                  title: "Continuar",
                  onClick: () async {
                    if (getIt<GlobalConfigVars>().selectedExecutor.isEmpty) {
                      Util.toastAlerta("Selecione o executor");
                      return;
                    }
                    if (getIt<GlobalConfigVars>().selectedPilot.isEmpty) {
                      Util.toastAlerta("Selecione o piloto");
                      return;
                    }
                    final refUsuario =
                        '${getIt<GlobalConfigVars>().userPayload.nrUsuario}_${getIt<GlobalConfigVars>().userPayload.name}';
                    final aplicacao = Aplicacao(
                        executor: getIt<GlobalConfigVars>().selectedExecutor,
                        piloto: getIt<GlobalConfigVars>().selectedPilot,
                        data: DateTime.now().millisecondsSinceEpoch.toString(),
                        state: ReportDashBoardState.Incompleto,
                        refUsuario: refUsuario);
                    int? idAplicacao = await widget._reportAplicationController
                        .createElementInTable(aplicacao.toMap(), "Aplicacao");
                    aplicacao.id = idAplicacao;
                    widget._reportAplicationController
                        .setAplicacaoSelected(aplicacao);
                    // ignore: use_build_context_synchronously
                    Navigator.push(context,
                        MaterialPageRoute(builder: (context) {
                      return MenuAplicationPage(
                        reportAplicationController:
                            widget._reportAplicationController,
                      );
                    }));
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
