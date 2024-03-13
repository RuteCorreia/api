import 'package:flutter/material.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/util.dart';

import 'package:flytec/features/aplications/components/components_exports.dart';
import 'package:go_router/go_router.dart';


class AddFireFightingStepOne extends StatefulWidget {
  const AddFireFightingStepOne({super.key});

  @override
  State<AddFireFightingStepOne> createState() => _AddFireFightingStepOneState();
}

class _AddFireFightingStepOneState extends State<AddFireFightingStepOne> {
  void setPilotOrExecutoz() {
    if (getIt<GlobalConfigVars>().userPayload.role == "Executor") {
      getIt<GlobalConfigVars>().selectedExecutor =
          getIt<GlobalConfigVars>().userPayload.name!;
    } else {
      getIt<GlobalConfigVars>().selectedPilot =
          getIt<GlobalConfigVars>().userPayload.name!;
    }
  }

  @override
  void initState() {
    setPilotOrExecutoz();
    super.initState();
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
                  onClick: () {
                    if (getIt<GlobalConfigVars>().selectedExecutor.isEmpty) {
                      Util.toastAlerta("Selecione o executor");
                    } else if (getIt<GlobalConfigVars>()
                        .selectedPilot
                        .isEmpty) {
                      Util.toastAlerta("Selecione o piloto");
                    } else {
                      context.push(
                        "/combateIncendioPasso2",
                      );
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
