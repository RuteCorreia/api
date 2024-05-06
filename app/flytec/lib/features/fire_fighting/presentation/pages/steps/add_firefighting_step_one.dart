// ignore_for_file: use_build_context_synchronously
import 'package:flutter/material.dart';
import 'package:flytec/core/enums/dashboard_state.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/components/components_exports.dart';
import 'package:flytec/features/fire_fighting/controller/firefighting_controller.dart';
import 'package:flytec/features/fire_fighting/models/firefighting.dart';
import 'package:flytec/features/fire_fighting/presentation/pages/steps/add_firefighting_second_step.dart';
import 'package:flytec/features/home/presentation/widgets/custom_dialog_button.dart';
import 'package:go_router/go_router.dart';

class AddFireFightingStepOne extends StatefulWidget {
  final FirefightingController? _firefightingController;
  const AddFireFightingStepOne(
      {required FirefightingController? firefightingController, super.key})
      : _firefightingController = firefightingController;

  @override
  State<AddFireFightingStepOne> createState() => _AddFireFightingStepOneState();
}

class _AddFireFightingStepOneState extends State<AddFireFightingStepOne> {
  void _setPilotOrExecutoz() {
    if (getIt<GlobalConfigVars>().userPayload.role!.contains("Executor") ||
        getIt<GlobalConfigVars>()
            .userPayload
            .role!
            .contains("TecnicoExecutor")) {
      getIt<GlobalConfigVars>().selectedExecutor =
          getIt<GlobalConfigVars>().userPayload.name!;
    } else {
      getIt<GlobalConfigVars>().selectedPilot =
          getIt<GlobalConfigVars>().userPayload.name!;
    }
  }

  @override
  void initState() {
    _setPilotOrExecutoz();
    super.initState();
  }

  Future<void> _createReportFirefighting() async {
    final idUsuario = getIt<GlobalConfigVars>().userPayload.nrUsuario;
    final refUsuario =
        '${idUsuario}_${getIt<GlobalConfigVars>().userPayload.name}';
    final firefighting = Firefighting(
        executor: getIt<GlobalConfigVars>().selectedExecutor,
        piloto: getIt<GlobalConfigVars>().selectedPilot,
        data: DateTime.now().millisecondsSinceEpoch,
        state: DashBoardState.Incompleto,
        refId: refUsuario);
    int? idFirefighting = await widget._firefightingController!
        .createElementInTable(firefighting.toMap(), "Firefighting");
    firefighting.id = idFirefighting;
    firefighting.refId =
        '${getIt<GlobalConfigVars>().userPayload.nrUsuario}_$idFirefighting';
    widget._firefightingController?.setFirefightingSelected(firefighting);
    await widget._firefightingController?.obtainReportsFirefightings();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Combate a incêndio"),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: SingleChildScrollView(
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
                absorbing: getIt<GlobalConfigVars>()
                    .userPayload
                    .role!
                    .contains("Piloto"),
                child: CustomCombo(
                  selectedName: getIt<GlobalConfigVars>()
                          .userPayload
                          .role!
                          .contains("Piloto")
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
                                    getIt<GlobalConfigVars>().selectedPilot =
                                        value;
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
                absorbing: getIt<GlobalConfigVars>()
                    .userPayload
                    .role!
                    .contains("Executor"),
                child: CustomCombo(
                  selectedName: getIt<GlobalConfigVars>()
                              .userPayload
                              .role!
                              .contains("Executor") ||
                          getIt<GlobalConfigVars>()
                              .userPayload
                              .role!
                              .contains("TecnicoExecutor")
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
                                    getIt<GlobalConfigVars>().selectedExecutor =
                                        value;
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
                    } else if (getIt<GlobalConfigVars>()
                        .selectedPilot
                        .isEmpty) {
                      Util.toastAlerta("Selecione o piloto");
                    } else {
                      showAdaptiveDialog<String>(
                          context: context,
                          useSafeArea: true,
                          builder: (BuildContext context) =>
                              AlertDialog.adaptive(
                                insetPadding: const EdgeInsets.all(15),
                                content: SizedBox(
                                  height: 245,
                                  child: SingleChildScrollView(
                                    child: Column(
                                      children: [
                                        const SizedBox(height: 20),
                                        const Text(
                                          'Selecione o contratante',
                                          textAlign: TextAlign.center,
                                          style: TextStyle(
                                            color: Color.fromARGB(
                                                255, 121, 118, 118),
                                            fontSize: 16,
                                            fontFamily: 'Inter',
                                            fontWeight: FontWeight.w500,
                                             
                                          ),
                                        ),
                                        const SizedBox(height: 30),
                                        CustomDialogButton(
                                          showLeftIcon: false,
                                          leftIcon: "",
                                          text: "Orgão Público",
                                          onClick: () async {
                                            await _createReportFirefighting();
                                            context.pop();
                                            Navigator.push(
                                                context,
                                                MaterialPageRoute(
                                                    builder: (context) =>
                                                        AddFireFightingSecondStep(
                                                            firefightingController:
                                                                widget
                                                                    ._firefightingController!)));
                                          },
                                        ),
                                        const SizedBox(height: 10),
                                        CustomDialogButton(
                                          showLeftIcon: false,
                                          leftIcon: "",
                                          onClick: () async {
                                            await _createReportFirefighting();
                                            context.pop();
                                            Navigator.push(
                                                context,
                                                MaterialPageRoute(
                                                    builder: (context) =>
                                                        AddFireFightingSecondStep(
                                                            firefightingController:
                                                                widget
                                                                    ._firefightingController!)));
                                          },
                                          text: "Privado",
                                        )
                                      ],
                                    ),
                                  ),
                                ),
                                actions: const <Widget>[],
                              ));
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
