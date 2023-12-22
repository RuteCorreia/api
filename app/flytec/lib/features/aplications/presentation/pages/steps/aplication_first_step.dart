import 'package:flutter/material.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:go_router/go_router.dart';
import 'package:modal_bottom_sheet/modal_bottom_sheet.dart';

import '../../../../auth/presentation/widgets/custom_login_button.dart';

class AplicationFirstStep extends StatefulWidget {
  const AplicationFirstStep({super.key});

  @override
  State<AplicationFirstStep> createState() => _AplicationFirstStepState();
}

class _AplicationFirstStepState extends State<AplicationFirstStep> {
  String selectedExecutor = "";
  String selectedPilot = "";
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Aplicações"),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16),
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
              absorbing: getIt<GlobalConfigVars>().userPayload.role == "Piloto",
              child: CustomCombo(
                selectedName:
                    getIt<GlobalConfigVars>().userPayload.role == "Piloto"
                        ? getIt<GlobalConfigVars>().userPayload.uniqueName!
                        : selectedPilot.isEmpty
                            ? "Selecione o piloto"
                            : selectedPilot,
                onTap: () {
                  showMaterialModalBottomSheet(
                    context: context,
                    builder: (context) => SingleChildScrollView(
                      controller: ModalScrollController.of(context),
                      child: Container(
                        height: 400,
                        color: Colors.white,
                        child: Padding(
                          padding: const EdgeInsets.all(0.0),
                          child: SingleChildScrollView(
                            child: Column(
                              crossAxisAlignment: CrossAxisAlignment.start,
                              children: [
                                SizedBox(
                                  height: 500,
                                  child: ListView.builder(
                                      padding: EdgeInsets.zero,
                                      itemCount: getIt<GlobalConfigVars>()
                                          .pilotos
                                          .length,
                                      itemBuilder: (ctx, index) {
                                        final piloto = getIt<GlobalConfigVars>()
                                            .pilotos[index];
                                        return Container(
                                          margin:
                                              const EdgeInsets.only(bottom: 2),
                                          decoration: BoxDecoration(
                                            color: Colors.grey.withOpacity(0.1),
                                          ),
                                          child: ListTile(
                                            onTap: () {
                                              setState(() {
                                                selectedPilot =
                                                    piloto.nomePiloto!;
                                              });
                                              context.pop();
                                            },
                                            style: ListTileStyle.drawer,
                                            title: Text("${piloto.nomePiloto}"),
                                          ),
                                        );
                                      }),
                                )
                              ],
                            ),
                          ),
                        ),
                      ),
                    ),
                  );
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
                        ? getIt<GlobalConfigVars>().userPayload.uniqueName!
                        : selectedExecutor.isEmpty
                            ? "Selecione o executor"
                            : selectedExecutor,
                onTap: () {
                  showMaterialModalBottomSheet(
                    context: context,
                    builder: (context) => SingleChildScrollView(
                      controller: ModalScrollController.of(context),
                      child: Container(
                        height: 400,
                        color: Colors.white,
                        child: Padding(
                          padding: const EdgeInsets.all(0.0),
                          child: SingleChildScrollView(
                            child: Column(
                              crossAxisAlignment: CrossAxisAlignment.start,
                              children: [
                                SizedBox(
                                  height: 500,
                                  child: ListView.builder(
                                      padding: EdgeInsets.zero,
                                      itemCount: getIt<GlobalConfigVars>()
                                          .executores
                                          .length,
                                      itemBuilder: (ctx, index) {
                                        final executor =
                                            getIt<GlobalConfigVars>()
                                                .executores[index];
                                        return Container(
                                          margin:
                                              const EdgeInsets.only(bottom: 2),
                                          decoration: BoxDecoration(
                                            color: Colors.grey.withOpacity(0.1),
                                          ),
                                          child: ListTile(
                                            onTap: () {
                                              setState(() {
                                                selectedExecutor =
                                                    executor.nome!;
                                              });
                                              context.pop();
                                            },
                                            style: ListTileStyle.drawer,
                                            title: Text("${executor.nome}"),
                                          ),
                                        );
                                      }),
                                )
                              ],
                            ),
                          ),
                        ),
                      ),
                    ),
                  );
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
                  context.push(
                    "/aplicationstep2",
                  );
                  /*     showAdaptiveDialog<String>(
                    context: context,
                    useSafeArea: true,
                    builder: (BuildContext context) => AlertDialog.adaptive(
                      insetPadding: const EdgeInsets.all(15),
                      title: const SizedBox(
                        width: 244,
                        height: 30,
                        child: Text(
                          'Selecione o contratante',
                          textAlign: TextAlign.center,
                          style: TextStyle(
                            color: Color.fromARGB(255, 121, 118, 118),
                            fontSize: 16,
                            fontFamily: 'Inter',
                            fontWeight: FontWeight.w500,
                            height: 0.09,
                          ),
                        ),
                      ),
                      content: SizedBox(
                        height: 200,
                        child: Column(
                          children: [
                            CustomDialogButton(
                              showLeftIcon: false,
                              leftIcon: "",
                              text: "Orgão Público",
                              onClick: () {
                                context.pop();
                                context.push(
                                  "/aplicationstep2",
                                );
                              },
                            ),
                            const SizedBox(height: 10),
                            CustomDialogButton(
                              showLeftIcon: false,
                              leftIcon: "",
                              onClick: () {
                                context.pop();
                                context.push(
                                  "/aplicationstep2",
                                );
                              },
                              text: "Privado",
                            )
                          ],
                        ),
                      ),
                      actions: const <Widget>[],
                    ),
                  );
               */
                },
              ),
            ),
          ],
        ),
      ),
    );
  }
}

class CustomCombo extends StatelessWidget {
  const CustomCombo(
      {super.key, required this.selectedName, required this.onTap});
  final String selectedName;
  final VoidCallback onTap;
  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onTap,
      child: Container(
        width: double.infinity,
        height: 40,
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
            Flexible(
              child: Row(
                mainAxisSize: MainAxisSize.min,
                mainAxisAlignment: MainAxisAlignment.start,
                crossAxisAlignment: CrossAxisAlignment.center,
                children: [
                  Flexible(
                    child: Text(
                      selectedName,
                      style: const TextStyle(
                        color: Color.fromARGB(255, 124, 123, 123),
                        fontSize: 16,
                        fontFamily: 'Inter',
                        fontWeight: FontWeight.w500,
                        height: 0.09,
                      ),
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
      ),
    );
  }
}
