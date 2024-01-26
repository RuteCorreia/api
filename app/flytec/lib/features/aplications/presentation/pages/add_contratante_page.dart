import 'package:dropdown_button2/dropdown_button2.dart';
import 'package:flutter/material.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/data/datasource/clientes_datasource.dart';
import 'package:flytec/features/aplications/presentation/pages/controllers/maps_informations_controller.dart';
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
  final MapsInformationsController _mapsInformationsController =
      MapsInformationsControllerBrazil();
  final TextEditingController _nomeClienteController = TextEditingController();
  final TextEditingController _cnpjController = TextEditingController();
  final TextEditingController _inscricaoEstadualController =
      TextEditingController();
  final TextEditingController _enderecoController = TextEditingController();
  final TextEditingController _municipioController = TextEditingController();
  final TextEditingController _ufController = TextEditingController();
  bool isPageLoading = false;
  List<String> _statesOfBrazil = [];
  void _obtainStatesOfBrazil() {
    _statesOfBrazil = _mapsInformationsController.getStatesBrazil;
    setState(() {});
  }

  String _uf = 'SP';
  late String _cityOfUf = '';
  final List<String> _citiesNamesUfBrazil = [];

  Future<void> _obtainCitiesOfUfBrazil(String uf) async {
    _citiesNamesUfBrazil.clear();
    List<String> cities =
        _mapsInformationsController.obtainCitiesFromStateBrazil(uf);
    _cityOfUf = cities.first;
    setState(() {});
    _citiesNamesUfBrazil.addAll(cities);
    setState(() {});
  }

  @override
  void initState() {
    super.initState();
    _obtainStatesOfBrazil();
    _obtainCitiesOfUfBrazil('SP');
  }

  final TextEditingController _citySearchControllerJuridica =
      TextEditingController();
  final TextEditingController _citySearchControllerFisica =
      TextEditingController();

  @override
  void dispose() {
    _citySearchControllerJuridica.dispose();
    _citySearchControllerFisica.dispose();
    super.dispose();
  }

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
                            textEditingController: _nomeClienteController,
                            onChanged: (String value) {},
                          ),
                          const CustomText(
                            text: 'CPF',
                          ),
                          const SizedBox(height: 14),
                          CustomTextField(
                            textInputType: TextInputType.number,
                            textEditingController: _cnpjController,
                            onChanged: (String value) {},
                          ),
                          const CustomText(text: 'Endereço'),
                          const SizedBox(height: 14),
                          CustomTextField(
                            textEditingController: _enderecoController,
                            onChanged: (String value) {},
                          ),
                          Row(
                            mainAxisAlignment: MainAxisAlignment.spaceBetween,
                            children: [
                              Column(
                                crossAxisAlignment: CrossAxisAlignment.start,
                                children: [
                                  const CustomText(text: "UF"),
                                  const SizedBox(height: 10),
                                  Container(
                                      decoration: BoxDecoration(
                                        border: Border.all(
                                            width: 1,
                                            color: const Color(0xFF636363)),
                                        borderRadius:
                                            BorderRadius.circular(10.0),
                                      ),
                                      alignment: Alignment.center,
                                      width: 100,
                                      height: 40,
                                      padding: const EdgeInsets.symmetric(
                                          horizontal: 8.0),
                                      child: DropdownButton<String>(
                                        onChanged: (regiaoSelecionada) {
                                          _uf = regiaoSelecionada!;
                                          _obtainCitiesOfUfBrazil(
                                              regiaoSelecionada);
                                          setState(() {});
                                        },
                                        alignment: Alignment.center,
                                        disabledHint: const SizedBox.shrink(),
                                        underline: const SizedBox.shrink(),
                                        value: _uf,
                                        icon: const Icon(
                                          Icons.keyboard_arrow_down,
                                          color: Colors.black,
                                        ),
                                        items: _statesOfBrazil
                                            .map((String regiao) {
                                          return DropdownMenuItem(
                                            value: regiao,
                                            child: Text(
                                              regiao,
                                              style: const TextStyle(
                                                  color: Color(0xFF636363)),
                                            ),
                                          );
                                        }).toList(),
                                      ))
                                ],
                              ),
                              Builder(
                                builder: (context) {
                                  if (_citiesNamesUfBrazil.isNotEmpty &&
                                      _cityOfUf.isNotEmpty) {
                                    return Column(
                                      crossAxisAlignment:
                                          CrossAxisAlignment.start,
                                      children: [
                                        const CustomText(text: "Cidade"),
                                        const SizedBox(height: 10),
                                        DropdownButtonHideUnderline(
                                          child: DropdownButton2<String>(
                                            isExpanded: true,
                                            items: _citiesNamesUfBrazil
                                                .map((item) => DropdownMenuItem(
                                                      value: item,
                                                      child: Text(
                                                        item,
                                                        style: const TextStyle(
                                                          fontSize: 14,
                                                        ),
                                                      ),
                                                    ))
                                                .toList(),
                                            value: _cityOfUf,
                                            onChanged: (value) {
                                              setState(() {
                                                _cityOfUf = value!;
                                              });
                                            },
                                            buttonStyleData: ButtonStyleData(
                                              padding:
                                                  const EdgeInsets.symmetric(
                                                      horizontal: 12.0),
                                              height: 40,
                                              decoration: BoxDecoration(
                                                border: Border.all(
                                                    width: 1,
                                                    color: const Color(
                                                        0xFF636363)),
                                                borderRadius:
                                                    BorderRadius.circular(10.0),
                                              ),
                                              width: 200,
                                            ),
                                            dropdownStyleData:
                                                const DropdownStyleData(
                                              maxHeight: 200,
                                              padding: EdgeInsets.all(0),
                                            ),
                                            menuItemStyleData:
                                                const MenuItemStyleData(
                                              height: 40,
                                            ),
                                            dropdownSearchData:
                                                DropdownSearchData(
                                              searchController:
                                                  _citySearchControllerFisica,
                                              searchInnerWidgetHeight: 50,
                                              searchInnerWidget: Container(
                                                height: 50,
                                                padding: const EdgeInsets.only(
                                                  right: 8,
                                                  top: 4.0,
                                                  bottom: 4.0,
                                                  left: 8,
                                                ),
                                                child: TextFormField(
                                                  controller:
                                                      _citySearchControllerFisica,
                                                  decoration: InputDecoration(
                                                    isDense: true,
                                                    hintText: 'Digite a cidade',
                                                    hintStyle: const TextStyle(
                                                        fontSize: 12),
                                                    border: OutlineInputBorder(
                                                      borderRadius:
                                                          BorderRadius.circular(
                                                              8),
                                                    ),
                                                  ),
                                                ),
                                              ),
                                              searchMatchFn:
                                                  (item, searchValue) {
                                                return item.value
                                                    .toString()
                                                    .toLowerCase()
                                                    .contains(searchValue
                                                        .toLowerCase());
                                              },
                                            ),
                                            onMenuStateChange: (isOpen) {
                                              if (!isOpen) {
                                                _citySearchControllerFisica
                                                    .clear();
                                              }
                                            },
                                          ),
                                        ),
                                      ],
                                    );
                                  }
                                  return const SizedBox.shrink();
                                },
                              ),
                            ],
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
                            onChanged: (String value) {},
                          ),
                          const CustomText(text: 'CNPJ'),
                          const SizedBox(height: 14),
                          CustomTextField(
                            textInputType: TextInputType.number,
                            textEditingController: _cnpjController,
                            onChanged: (String value) {},
                          ),
                          const CustomText(text: 'Inscrição estadual'),
                          const SizedBox(height: 14),
                          CustomTextField(
                            textEditingController: _inscricaoEstadualController,
                            onChanged: (String value) {},
                          ),
                          const CustomText(text: 'Endereço'),
                          const SizedBox(height: 14),
                          CustomTextField(
                            textEditingController: _enderecoController,
                            onChanged: (String value) {},
                          ),
                          Row(
                            mainAxisAlignment: MainAxisAlignment.spaceBetween,
                            children: [
                              Column(
                                crossAxisAlignment: CrossAxisAlignment.start,
                                children: [
                                  const CustomText(text: "UF"),
                                  const SizedBox(height: 10),
                                  Container(
                                      decoration: BoxDecoration(
                                        border: Border.all(
                                            width: 1,
                                            color: const Color(0xFF636363)),
                                        borderRadius:
                                            BorderRadius.circular(10.0),
                                      ),
                                      alignment: Alignment.center,
                                      width: 100,
                                      height: 40,
                                      padding: const EdgeInsets.symmetric(
                                          horizontal: 8.0),
                                      child: DropdownButton<String>(
                                        onChanged: (regiaoSelecionada) async {
                                          _uf = regiaoSelecionada!;
                                          await _obtainCitiesOfUfBrazil(
                                              regiaoSelecionada);
                                          setState(() {});
                                        },
                                        alignment: Alignment.center,
                                        disabledHint: const SizedBox.shrink(),
                                        underline: const SizedBox.shrink(),
                                        value: _uf,
                                        icon: const Icon(
                                          Icons.keyboard_arrow_down,
                                          color: Colors.black,
                                        ),
                                        items: _statesOfBrazil
                                            .map((String regiao) {
                                          return DropdownMenuItem(
                                            value: regiao,
                                            child: Text(
                                              regiao,
                                              style: const TextStyle(
                                                  color: Color(0xFF636363)),
                                            ),
                                          );
                                        }).toList(),
                                      ))
                                ],
                              ),
                              Builder(
                                builder: (context) {
                                  if (_citiesNamesUfBrazil.isNotEmpty &&
                                      _cityOfUf.isNotEmpty) {
                                    return Column(
                                      crossAxisAlignment:
                                          CrossAxisAlignment.start,
                                      children: [
                                        const CustomText(text: "Cidade"),
                                        const SizedBox(height: 10),
                                        DropdownButtonHideUnderline(
                                          child: DropdownButton2<String>(
                                            isExpanded: true,
                                            items: _citiesNamesUfBrazil
                                                .map((item) => DropdownMenuItem(
                                                      value: item,
                                                      child: Text(
                                                        item,
                                                        style: const TextStyle(
                                                          fontSize: 14,
                                                        ),
                                                      ),
                                                    ))
                                                .toList(),
                                            value: _cityOfUf,
                                            onChanged: (value) {
                                              setState(() {
                                                _cityOfUf = value!;
                                              });
                                            },
                                            buttonStyleData: ButtonStyleData(
                                              padding:
                                                  const EdgeInsets.symmetric(
                                                      horizontal: 12.0),
                                              height: 40,
                                              decoration: BoxDecoration(
                                                border: Border.all(
                                                    width: 1,
                                                    color: const Color(
                                                        0xFF636363)),
                                                borderRadius:
                                                    BorderRadius.circular(10.0),
                                              ),
                                              width: 200,
                                            ),
                                            dropdownStyleData:
                                                const DropdownStyleData(
                                              maxHeight: 200,
                                              padding: EdgeInsets.all(0),
                                            ),
                                            menuItemStyleData:
                                                const MenuItemStyleData(
                                              height: 40,
                                            ),
                                            dropdownSearchData:
                                                DropdownSearchData(
                                              searchController:
                                                  _citySearchControllerFisica,
                                              searchInnerWidgetHeight: 50,
                                              searchInnerWidget: Container(
                                                height: 50,
                                                padding: const EdgeInsets.only(
                                                  right: 8,
                                                  top: 4.0,
                                                  bottom: 4.0,
                                                  left: 8,
                                                ),
                                                child: TextFormField(
                                                  controller:
                                                      _citySearchControllerFisica,
                                                  decoration: InputDecoration(
                                                    isDense: true,
                                                    hintText: 'Digite a cidade',
                                                    hintStyle: const TextStyle(
                                                        fontSize: 12),
                                                    border: OutlineInputBorder(
                                                      borderRadius:
                                                          BorderRadius.circular(
                                                              8),
                                                    ),
                                                  ),
                                                ),
                                              ),
                                              searchMatchFn:
                                                  (item, searchValue) {
                                                return item.value
                                                    .toString()
                                                    .toLowerCase()
                                                    .contains(searchValue
                                                        .toLowerCase());
                                              },
                                            ),
                                            onMenuStateChange: (isOpen) {
                                              if (!isOpen) {
                                                _citySearchControllerFisica
                                                    .clear();
                                              }
                                            },
                                          ),
                                        ),
                                      ],
                                    );
                                  }
                                  return const SizedBox.shrink();
                                },
                              ),
                            ],
                          ),
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
                        } else if (_uf.isEmpty) {
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
      required this.onChanged,
      this.textInputType = TextInputType.text});
  final TextEditingController? textEditingController;
  final TextInputType? textInputType;
  final Function(String value) onChanged;
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
        onChanged: onChanged,
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
