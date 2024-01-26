import 'package:flutter/material.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/data/models/relatorio_model.dart';
import 'package:flytec/features/aplications/presentation/widgets/executor_select.dart';
import 'package:flytec/features/aplications/presentation/widgets/pilot_select.dart';
import 'package:go_router/go_router.dart';

import '../../../../auth/presentation/widgets/custom_login_button.dart';

class AplicationFirstStep extends StatefulWidget {
  const AplicationFirstStep({super.key});

  @override
  State<AplicationFirstStep> createState() => _AplicationFirstStepState();
}

class _AplicationFirstStepState extends State<AplicationFirstStep> {
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
        title: const Text("Aplicações"),
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
                      getIt<GlobalConfigVars>().reportList.add(RelatorioModel(
                          cliente: Cliente(
                            cnpj: "",
                            endereco: "",
                            cpf: "",
                            id: "",
                            nome: "",
                            rg: "",
                            uf: "",
                          ),
                          areaTratada: AreaTratada(
                              cidade: "",
                              cultura: "",
                              extensao: "",
                              localizacao: "",
                              uf: ""),
                          carateristicaProduto: CarateristicaProduto(
                            adjuvante: "",
                            alvoBiologico: "",
                            classe: "",
                            classificacaoToxicologica: "",
                            cultura: "",
                            dosePorHectare: "",
                            nomeProduto: "",
                            tipoFormulacao: "",
                            tipoServico: "",
                            unidadeHectare: "",
                          ),
                          recomendacoesTecnicas: RecomendacoesTecnicas(
                            aeronave: "",
                            alturaDoVoo: "",
                            volumeDaAplicacao: "",
                            angulo: "",
                            equipamento: "",
                            larguraDaFaixa: "",
                            qtdVeiculante: "",
                            temperatura: "20.0°C",
                            tipoProduto: "",
                            umidadeRelativaDoAr: "",
                            unidadeVolume: "",
                            veiculante: "",
                            velocidadeDoVento: "",
                          ),
                          relatorioDeAplicacao: RelatorioDeAplicacao(
                              cultura: "",
                              dosagem: "",
                              latitudeSul: "",
                              localizacaoPista: "",
                              densidade: "",
                              log: "",
                              longitudeOeste: "",
                              observacoes: "",
                              totalAreaAplicada: "",
                              produtoAplicado: "",
                              unidadeDosagem: "",
                              unidadeVolume: "",
                              volumeDeAplicacao: "",
                              aplicacoes: Aplicacoes(
                                dataDaAplicacao: "",
                                horarioDeInicio: "",
                                horarioDeTermino: "",
                                horimetroFinal: "",
                                horimetroInicial: "",
                                temperaturaFinal: "",
                                temperaturaIncial: "",
                                umidadeRelativaFinal: "",
                                umidadeRelativaInicial: "",
                                ventoFinal: "",
                                ventoInicial: "",
                              )),
                          contratoServico: ContratoServico(
                              distanciaDaPista: "",
                              executor: "",
                              extensao: "",
                              nomePiloto: "",
                              preco: "",
                              tipoPreco: "",
                              valorTotal: "",
                              vencimento: ""),
                          dadosDoResponsavel: DadosDoResponsavel(
                            assinatura: "",
                            cidade: "",
                            cpf: "",
                            data: "",
                            nomeCompleto: "",
                            telefone: "",
                            uf: "",
                          ),
                          piloto: getIt<GlobalConfigVars>().selectedPilot,
                          executor:
                              getIt<GlobalConfigVars>().selectedExecutor));
                      context.push(
                        "/aplicationstep2",
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
        height: 50,
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
