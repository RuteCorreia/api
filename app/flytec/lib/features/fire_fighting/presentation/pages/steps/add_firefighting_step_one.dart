import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:go_router/go_router.dart';

import '../../../../auth/presentation/widgets/custom_login_button.dart';
import '../../../../home/presentation/widgets/custom_dialog_button.dart';

class AddFireFightingStepOne extends StatelessWidget {
  const AddFireFightingStepOne({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Combate a Incêndio"),
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
            const CustomCombo(
              selectedName: "Rodrigo Doto",
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
            const CustomCombo(selectedName: "Higor"),
            const SizedBox(height: 40),
            Image.asset(
              "assets/images/logotipo.png",
              height: 200,
            ),
            Center(
              child: CustomButton(
                title: "Continuar",
                onClick: () {
                  showAdaptiveDialog<String>(
                      context: context,
                      useSafeArea: true,
                      builder: (BuildContext context) => AlertDialog.adaptive(
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
                                        color:
                                            Color.fromARGB(255, 121, 118, 118),
                                        fontSize: 16,
                                        fontFamily: 'Inter',
                                        fontWeight: FontWeight.w500,
                                        height: 0.09,
                                      ),
                                    ),
                                    const SizedBox(height: 30),
                                    CustomDialogButton(
                                      showLeftIcon: false,
                                      leftIcon: "",
                                      text: "Orgão Público",
                                      onClick: () {
                                        context.pop();
                                        context.push(
                                          "/combateIncendioPasso2",
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
                                          "/combateIncendioPasso2",
                                        );
                                      },
                                      text: "Privado",
                                    )
                                  ],
                                ),
                              ),
                            ),
                            actions: const <Widget>[],
                          ));
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
  const CustomCombo({super.key, required this.selectedName});
  final String selectedName;

  @override
  Widget build(BuildContext context) {
    return Container(
      width: 328,
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
          SizedBox(
            child: Row(
              mainAxisSize: MainAxisSize.min,
              mainAxisAlignment: MainAxisAlignment.start,
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                Text(
                  selectedName,
                  style: const TextStyle(
                    color: Color.fromARGB(255, 121, 118, 118),
                    fontSize: 16,
                    fontFamily: 'Inter',
                    fontWeight: FontWeight.w600,
                    height: 0.09,
                  ),
                ),
              ],
            ),
          ),
          Container(
            width: 16,
            height: 16,
            clipBehavior: Clip.antiAlias,
            decoration: const BoxDecoration(),
            child: Stack(children: [
              SvgPicture.asset(
                "assets/images/arrow.svg",
              )
            ]),
          ),
        ],
      ),
    );
  }
}
