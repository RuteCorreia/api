import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/data/models/area_application.dart';
import 'package:flytec/features/aplications/presentation/pages/identificacao_area_page.dart';
import 'package:go_router/go_router.dart';

import '../../../../../core/injections/get_it.dart';

class AplicationSecondStep extends StatefulWidget {
  const AplicationSecondStep({super.key});

  @override
  State<AplicationSecondStep> createState() => _AplicationSecondStepState();
}

class _AplicationSecondStepState extends State<AplicationSecondStep> {
  late DateTime? dataSelecionada = DateTime.now();
  late TimeOfDay? time = const TimeOfDay(hour: 12, minute: 43);
  late TimeOfDay? horimetro = const TimeOfDay(hour: 15, minute: 43);

  final AreaApplication _areaApplication = AreaApplication();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
        title: const Text(
          "Planejamento de\n aplicação de área",
          textAlign: TextAlign.center,
        ),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: SingleChildScrollView(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const SizedBox(height: 16),
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Text(
                    'N° ${getIt<GlobalConfigVars>().userPayload.nrUsuario}1',
                    textAlign: TextAlign.right,
                    style: const TextStyle(
                      color: Color(0xFF00B45D),
                      fontSize: 14,
                      fontFamily: 'Inter',
                      fontWeight: FontWeight.w700,
                      height: 0.11,
                    ),
                  ),
                  Text(
                    'Data ${Util.getTodayDate()}',
                    textAlign: TextAlign.right,
                    style: const TextStyle(
                      color: Color(0xFF00B45D),
                      fontSize: 14,
                      fontFamily: 'Inter',
                      fontWeight: FontWeight.w700,
                      height: 0.11,
                    ),
                  )
                ],
              ),
              const SizedBox(height: 25),
              CustomCardButton(
                title: "Identificação do contratante",
                onTap: () {
                  context.push("/aplicationstep3");
                },
              ),
              CustomCardButton(
                title: "Identificação da área a ser tratada",
                onTap: () {
                  Navigator.push(
                      context,
                      MaterialPageRoute(
                          builder: (context) => IdentificacaoAreaTratamento(
                              identifyAreaProcess:
                                  _areaApplication.identifyAreaProcess,
                              updateIdentifyAreaProcess: (identifyAreaProcess) {
                                _areaApplication.updateIdentifyAreaProcess(
                                    identifyAreaProcess);
                                setState(() {});
                              })));
                },
              ),
              CustomCardButton(
                title: "Características do produto a ser aplicado ",
                onTap: () {
                  context.push("/carateristicaproduto");
                },
              ),
              CustomCardButton(
                title: "Recomendações técnicas para aplicação",
                onTap: () {
                  context.push("/recomendacoestecnicas");
                },
              ),
              CustomCardButton(
                title: "Relatório de aplicação",
                onTap: () {
                  context.push("/relatorioaplicacao");
                },
              ),
              CustomCardButton(
                title: "Contrato de prestação de serviços",
                onTap: () {
                  context.push("/contrato");
                },
              ),
              CustomCardButton(
                title: "Dados do responsável",
                onTap: () {
                  context.push("/responsavel");
                },
              ),
              /*  Center(
                child: CustomButton(
                  title: "Próximo",
                  onClick: () {
                    context.push("/combateIncendioPasso3");
                  },
                ),
              ), */
            ],
          ),
        ),
      ),
    );
  }
}

class CustomCardButton extends StatelessWidget {
  const CustomCardButton({super.key, required this.onTap, required this.title});
  final String? title;
  final VoidCallback? onTap;
  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onTap,
      child: Container(
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
            Expanded(
              child: Container(
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
                            child: Container(
                              child: Column(
                                mainAxisSize: MainAxisSize.min,
                                mainAxisAlignment: MainAxisAlignment.center,
                                crossAxisAlignment: CrossAxisAlignment.center,
                                children: [
                                  SizedBox(
                                    width: double.infinity,
                                    child: Text(
                                      title!,
                                      style: const TextStyle(
                                        color:
                                            Color.fromARGB(255, 121, 118, 118),
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
                          ),
                        ],
                      ),
                    ),
                  ],
                ),
              ),
            ),
            const SizedBox(width: 1),
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
              child: const Column(
                mainAxisSize: MainAxisSize.min,
                mainAxisAlignment: MainAxisAlignment.center,
                crossAxisAlignment: CrossAxisAlignment.center,
                children: [
                  Icon(
                    Icons.arrow_forward_ios_outlined,
                    color: Colors.black,
                  )
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}

class CustomText extends StatelessWidget {
  const CustomText({super.key, required this.text});
  final String text;
  @override
  Widget build(BuildContext context) {
    return Text(
      text,
      style: const TextStyle(
        color: Color(0xFF00B45D),
        fontSize: 14,
        fontFamily: 'Inter',
        fontWeight: FontWeight.w700,
        height: 0.11,
      ),
    );
  }
}

class CustomComboBox extends StatelessWidget {
  const CustomComboBox(
      {super.key, required this.selectedName, required this.onTap});
  final String selectedName;
  final VoidCallback? onTap;
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
            SizedBox(
              child: Row(
                mainAxisSize: MainAxisSize.min,
                mainAxisAlignment: MainAxisAlignment.start,
                crossAxisAlignment: CrossAxisAlignment.center,
                children: [
                  Text(
                    selectedName,
                    style: const TextStyle(
                      color: Color(0xFF636363),
                      fontSize: 14,
                      fontFamily: 'Inter',
                      fontWeight: FontWeight.w600,
                      height: 0.11,
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
      ),
    );
  }
}
