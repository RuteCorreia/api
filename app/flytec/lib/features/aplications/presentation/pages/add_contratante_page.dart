import 'package:flutter/material.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/data/datasource/clientes_datasource.dart';
import 'package:flytec/features/aplications/presentation/pages/steps/aplication_second_step.dart';
import 'package:go_router/go_router.dart';
import 'package:modal_progress_hud_nsn/modal_progress_hud_nsn.dart';

import '../../../auth/presentation/widgets/custom_login_button.dart';

class AddContratante extends StatefulWidget {
  const AddContratante({super.key});

  @override
  State<AddContratante> createState() => _AddContratanteState();
}

enum TipoPessoa { FISICA, JURIDICA }

class _AddContratanteState extends State<AddContratante> {
  TipoPessoa tipoPessoa = TipoPessoa.FISICA;
  final TextEditingController _nomeClienteController = TextEditingController();
  final TextEditingController _cnpjController = TextEditingController();
  final TextEditingController _inscricaoEstadualController =
      TextEditingController();
  final TextEditingController _enderecoController = TextEditingController();
  final TextEditingController _municipioController = TextEditingController();
  final TextEditingController _ufController = TextEditingController();
  bool isPageLoading = false;
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
        title: const Text(
          "Identificação do contratante",
          textAlign: TextAlign.center,
          style: TextStyle(),
        ),
      ),
      body: ModalProgressHUD(
        inAsyncCall: isPageLoading,
        child: Padding(
          padding: const EdgeInsets.all(16.0),
          child: SingleChildScrollView(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Container(
                  width: double.infinity,
                  height: 27,
                  padding: const EdgeInsets.symmetric(horizontal: 0),
                  child: Row(
                    mainAxisSize: MainAxisSize.min,
                    mainAxisAlignment: MainAxisAlignment.start,
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Row(
                        children: [
                          Checkbox(
                              value: tipoPessoa == TipoPessoa.FISICA,
                              onChanged: (_) {
                                setState(() {
                                  tipoPessoa = TipoPessoa.FISICA;
                                });
                              }),
                          const Text("Pessoa Física")
                        ],
                      ),
                      Row(
                        children: [
                          Checkbox(
                              value: tipoPessoa == TipoPessoa.JURIDICA,
                              onChanged: (_) {
                                setState(() {
                                  tipoPessoa = TipoPessoa.JURIDICA;
                                });
                              }),
                          const Text("Pessoa Jurídica")
                        ],
                      ),
                    ],
                  ),
                ),
                const SizedBox(height: 20),
                tipoPessoa == TipoPessoa.FISICA
                    ? Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          const CustomText(text: 'Nome'),
                          const SizedBox(height: 14),
                          CustomTextField(
                              textEditingController: _nomeClienteController),
                          const CustomText(
                            text: 'CPF',
                          ),
                          const SizedBox(height: 14),
                          CustomTextField(
                            textInputType: TextInputType.number,
                            textEditingController: _cnpjController,
                          ),
                          const CustomText(text: 'Endereço'),
                          const SizedBox(height: 14),
                          CustomTextField(
                            textEditingController: _enderecoController,
                          ),
                          const CustomText(text: 'Município'),
                          const SizedBox(height: 14),
                          CustomTextField(
                            textEditingController: _municipioController,
                          ),
                          const CustomText(text: 'UF'),
                          const SizedBox(height: 14),
                          CustomTextField(
                            textInputType: TextInputType.number,
                            textEditingController: _ufController,
                          ),
                        ],
                      )
                    : Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          const CustomText(text: 'Nome'),
                          const SizedBox(height: 14),
                          CustomTextField(
                            textEditingController: _nomeClienteController,
                          ),
                          const CustomText(text: 'CNPJ'),
                          const SizedBox(height: 14),
                          CustomTextField(
                            textInputType: TextInputType.number,
                            textEditingController: _cnpjController,
                          ),
                          const CustomText(text: 'Inscrição estadual'),
                          const SizedBox(height: 14),
                          CustomTextField(
                            textEditingController: _inscricaoEstadualController,
                          ),
                          const CustomText(text: 'Endereço'),
                          const SizedBox(height: 14),
                          CustomTextField(
                            textEditingController: _enderecoController,
                          ),
                          const CustomText(text: 'Município'),
                          const SizedBox(height: 14),
                          CustomTextField(
                            textEditingController: _municipioController,
                          ),
                          /*  const CustomText(text: 'UF'),
                          const SizedBox(height: 14),
                          CustomTextField(
                            textInputType: TextInputType.number,
                            textEditingController: _ufController,
                          ), */
                        ],
                      ),
                Center(
                  child: CustomButton(
                    title: "OK",
                    onClick: () async {
                      if (tipoPessoa == TipoPessoa.FISICA) {
                        if (_nomeClienteController.text.isEmpty) {
                          Util.toastAlerta("Digite o nome do cliente");
                        } else if (_cnpjController.text.isEmpty) {
                          Util.toastAlerta("Digite o cpf do cliente");
                        } else if (_enderecoController.text.isEmpty) {
                          Util.toastAlerta("Digite o endereço do cliente");
                        } else if (_municipioController.text.isEmpty) {
                          Util.toastAlerta("Digite o município do cliente");
                        } else if (_ufController.text.isEmpty) {
                          Util.toastAlerta("Digite o UF do cliente");
                        } else {
                          setState(() {
                            isPageLoading = true;
                          });
                          final isSucess = await getIt<ClienteDataSourceImpl>()
                              .addCliente(
                                  addClientParams: AddClientParams(
                                      nome: _nomeClienteController.text,
                                      idTipoCliente: 1,
                                      cpf: 0,
                                      rg: 0,
                                      cnpj:
                                          int.tryParse(_cnpjController.text) ??
                                              0,
                                      inscricaoEstadual: int.tryParse(
                                              _inscricaoEstadualController
                                                  .text) ??
                                          0,
                                      endereco: _enderecoController.text,
                                      telefone1: "",
                                      telefone2: "",
                                      email: "",
                                      senha: "",
                                      precificacao: ""));
                          setState(() {
                            isPageLoading = false;
                          });
                          if (isSucess) {
                            Util.toastSucesso("Cliente registado");
                            await getIt<ClienteDataSourceImpl>()
                                .getClients()
                                .then((value) {
                              context.pop();
                            });
                          } else {
                            Util.toastErro("Erro ao registar cliente");
                          }
                        }
                      }
                      if (tipoPessoa == TipoPessoa.JURIDICA) {
                        if (_nomeClienteController.text.isEmpty) {
                          Util.toastAlerta("Digite o nome do cliente");
                        } else if (_cnpjController.text.isEmpty) {
                          Util.toastAlerta("Digite o cpf do cliente");
                        } else if (_enderecoController.text.isEmpty) {
                          Util.toastAlerta("Digite o endereço do cliente");
                        } else if (_municipioController.text.isEmpty) {
                          Util.toastAlerta("Digite o município do cliente");
                        } else {
                          setState(() {
                            isPageLoading = true;
                          });
                          final isSucess = await getIt<ClienteDataSourceImpl>()
                              .addCliente(
                                  addClientParams: AddClientParams(
                                      nome: _nomeClienteController.text,
                                      idTipoCliente: 1,
                                      cpf: 0,
                                      rg: 0,
                                      cnpj:
                                          int.tryParse(_cnpjController.text) ??
                                              0,
                                      inscricaoEstadual: int.tryParse(
                                              _inscricaoEstadualController
                                                  .text) ??
                                          0,
                                      endereco: _enderecoController.text,
                                      telefone1: "",
                                      telefone2: "",
                                      email: "",
                                      senha: "",
                                      precificacao: ""));
                          setState(() {
                            isPageLoading = false;
                          });
                          if (isSucess) {
                            Util.toastSucesso("Cliente registado");
                            await getIt<ClienteDataSourceImpl>()
                                .getClients()
                                .then((value) {
                              context.pop();
                            });
                          } else {
                            Util.toastErro("Erro ao registar cliente");
                          }
                        }
                      }
                    },
                  ),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}

class CustomTextField extends StatelessWidget {
  const CustomTextField(
      {super.key,
      required this.textEditingController,
      this.textInputType = TextInputType.text});
  final TextEditingController? textEditingController;
  final TextInputType? textInputType;
  @override
  Widget build(BuildContext context) {
    return Container(
      width: double.infinity,
      height: 50,
      margin: const EdgeInsets.only(bottom: 20),
      padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 10),
      decoration: ShapeDecoration(
        shape: RoundedRectangleBorder(
          side: const BorderSide(width: 1, color: Color(0xFF636363)),
          borderRadius: BorderRadius.circular(10),
        ),
      ),
      child: TextField(
        keyboardType: textInputType,
        controller: textEditingController,
        decoration: const InputDecoration(
            hintText: "Digite aqui",
            border: InputBorder.none,
            hintStyle: TextStyle(
              color: Color.fromARGB(255, 121, 118, 118),
              fontSize: 16,
              fontFamily: 'Inter',
              fontWeight: FontWeight.w500,
              height: 0.09,
            )),
      ),
    );
  }
}
