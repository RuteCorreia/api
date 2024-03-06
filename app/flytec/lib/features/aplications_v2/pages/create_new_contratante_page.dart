import 'package:dropdown_button2/dropdown_button2.dart';
import 'package:flutter/material.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/data/datasource/clientes_datasource.dart';
import 'package:flytec/features/aplications/presentation/pages/controllers/maps_informations_controller.dart';
import 'package:flytec/features/aplications_v2/components/custom_button.dart';
import 'package:flytec/features/aplications_v2/components/custom_text.dart';
import 'package:flytec/features/aplications_v2/components/custom_text_field.dart';
import 'package:modal_progress_hud_nsn/modal_progress_hud_nsn.dart';

class CreateNewContratantePage extends StatefulWidget {
  final VoidCallback? _onAddContratanteUpdateView;
  const CreateNewContratantePage(
      {required VoidCallback? onAddContratanteUpdateView, super.key})
      : _onAddContratanteUpdateView = onAddContratanteUpdateView;

  @override
  State<CreateNewContratantePage> createState() =>
      _CreateNewContratantePageState();
}

class _CreateNewContratantePageState extends State<CreateNewContratantePage> {
  bool _isPageLoading = false;
  int _tipoPessoa = 0;
  final MapsInformationsController _mapsInformationsController =
      MapsInformationsControllerBrazil();
  final TextEditingController _nomeClienteController = TextEditingController();
  final TextEditingController _cnpjController = TextEditingController();
  final TextEditingController _inscricaoEstadualController =
      TextEditingController();
  final TextEditingController _enderecoController = TextEditingController();
  final TextEditingController _rgController = TextEditingController();
  final TextEditingController _citySearchControllerJuridica =
      TextEditingController();
  final TextEditingController _citySearchControllerFisica =
      TextEditingController();
  String _uf = 'SP';
  late String _cityOfUf = '';
  final List<String> _citiesNamesUfBrazil = [];
  List<String> _statesOfBrazil = [];

  void _obtainStatesOfBrazil() {
    _statesOfBrazil = _mapsInformationsController.getStatesBrazil;
    setState(() {});
  }

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
    WidgetsBinding.instance.addPostFrameCallback((_) {
      _obtainStatesOfBrazil();
      _obtainCitiesOfUfBrazil('SP');
      widget._onAddContratanteUpdateView!();
      ;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
        title: const Text(
          "Identificação do contratante",
          textAlign: TextAlign.center,
        ),
      ),
      body: ModalProgressHUD(
        inAsyncCall: _isPageLoading,
        child: SingleChildScrollView(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Padding(
                padding: const EdgeInsets.all(8.0),
                child: Row(
                  children: [
                    Row(
                      children: [
                        Checkbox(
                            value: _tipoPessoa == 0,
                            onChanged: (_) {
                              _tipoPessoa = 0;
                              setState(() {});
                            }),
                        const Text("Pessoa Física")
                      ],
                    ),
                    Row(
                      children: [
                        Checkbox(
                            value: _tipoPessoa == 1,
                            onChanged: (_) {
                              _tipoPessoa = 1;
                              setState(() {});
                            }),
                        const Text("Pessoa Jurídica")
                      ],
                    ),
                  ],
                ),
              ),
              Padding(
                padding: const EdgeInsets.all(16.0),
                child: Builder(builder: (context) {
                  if (_tipoPessoa == 0) {
                    return Column(
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
                        const CustomText(text: 'Rg'),
                        const SizedBox(height: 14),
                        CustomTextField(
                          textInputType: TextInputType.number,
                          textEditingController: _rgController,
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
                                      borderRadius: BorderRadius.circular(10.0),
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
                                      items:
                                          _statesOfBrazil.map((String regiao) {
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
                                            padding: const EdgeInsets.symmetric(
                                                horizontal: 10.0),
                                            height: 40,
                                            decoration: BoxDecoration(
                                              border: Border.all(
                                                  width: 1,
                                                  color:
                                                      const Color(0xFF636363)),
                                              borderRadius:
                                                  BorderRadius.circular(10.0),
                                            ),
                                            width: 180,
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
                                            searchMatchFn: (item, searchValue) {
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
                    );
                  }
                  return Column(
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
                                    borderRadius: BorderRadius.circular(10.0),
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
                                    items: _statesOfBrazil.map((String regiao) {
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
                                  crossAxisAlignment: CrossAxisAlignment.start,
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
                                          padding: const EdgeInsets.symmetric(
                                              horizontal: 12.0),
                                          height: 40,
                                          decoration: BoxDecoration(
                                            border: Border.all(
                                                width: 1,
                                                color: const Color(0xFF636363)),
                                            borderRadius:
                                                BorderRadius.circular(10.0),
                                          ),
                                          width: 180,
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
                                        dropdownSearchData: DropdownSearchData(
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
                                                      BorderRadius.circular(8),
                                                ),
                                              ),
                                            ),
                                          ),
                                          searchMatchFn: (item, searchValue) {
                                            return item.value
                                                .toString()
                                                .toLowerCase()
                                                .contains(
                                                    searchValue.toLowerCase());
                                          },
                                        ),
                                        onMenuStateChange: (isOpen) {
                                          if (!isOpen) {
                                            _citySearchControllerFisica.clear();
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
                  );
                }),
              ),
              Center(
                child: CustomButton(
                  title: "OK",
                  onClick: () async {
                    if (_tipoPessoa == 0) {
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
                          _isPageLoading = true;
                        });
                        final isSucess = await getIt<ClienteDataSourceImpl>()
                            .addCliente(
                                addClientParams: AddClientParams(
                                    nome: _nomeClienteController.text,
                                    idTipoCliente: 1,
                                    cpf: 0,
                                    uf: _uf,
                                    cidade: _cityOfUf,
                                    rg: int.tryParse(_rgController.text) ?? 0,
                                    cnpj:
                                        int.tryParse(_cnpjController.text) ?? 0,
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
                          _isPageLoading = false;
                        });
                        if (isSucess) {
                          Util.toastSucesso("Cliente registado");
                          await getIt<ClienteDataSourceImpl>()
                              .getClients()
                              .then((value) {
                            widget._onAddContratanteUpdateView!();
                            ;
                            Navigator.pop(context);
                          });
                        } else {
                          Util.toastErro("Erro ao registar cliente");
                        }
                      }
                    }
                    if (_tipoPessoa == 1) {
                      if (_nomeClienteController.text.isEmpty) {
                        Util.toastAlerta("Digite o nome do cliente");
                      } else if (_cnpjController.text.isEmpty) {
                        Util.toastAlerta("Digite o cpf do cliente");
                      } else if (_enderecoController.text.isEmpty) {
                        Util.toastAlerta("Digite o endereço do cliente");
                      } else {
                        setState(() {
                          _isPageLoading = true;
                        });
                        final isSucess = await getIt<ClienteDataSourceImpl>()
                            .addCliente(
                                addClientParams: AddClientParams(
                                    nome: _nomeClienteController.text,
                                    idTipoCliente: 1,
                                    uf: _uf,
                                    cpf: 0,
                                    rg: 0,
                                    cidade: _cityOfUf,
                                    cnpj:
                                        int.tryParse(_cnpjController.text) ?? 0,
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
                          _isPageLoading = false;
                        });
                        if (isSucess) {
                          Util.toastSucesso("Cliente registado");
                          await getIt<ClienteDataSourceImpl>()
                              .getClients()
                              .then((value) {
                            widget._onAddContratanteUpdateView!();
                            ;
                            Navigator.pop(context);
                          });
                        } else {
                          Util.toastErro("Erro ao registar cliente");
                        }
                      }
                    }
                  },
                ),
              ),
              const SizedBox(height: 20),
            ],
          ),
        ),
      ),
    );
  }

  @override
  void dispose() {
    _citySearchControllerJuridica.dispose();
    _citySearchControllerFisica.dispose();
    super.dispose();
  }
}
