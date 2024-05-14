import 'dart:convert';
import 'dart:io';
import 'dart:typed_data';

import 'package:flutter/material.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/components/components_exports.dart';
import 'package:flytec/features/aplications/pages/images/upload_foto.dart';
import 'package:flytec/features/manutencao/components/manutencao_componentes_list_page.dart';
import 'package:flytec/features/manutencao/components/widgets/componente_select_widget.dart';

class ManutencaoComponentesPage extends StatefulWidget {
  const ManutencaoComponentesPage({super.key});

  @override
  State<ManutencaoComponentesPage> createState() =>
      _ManutencaoComponentesPageState();
}

class _ManutencaoComponentesPageState extends State<ManutencaoComponentesPage> {
  final TextEditingController _observationTextField = TextEditingController();
  String? _componenteSelecionado = 'Selecione';
  final ScrollController _scrollController = ScrollController();
  final List<String> _images = [];

  final List<String> _componentes = [
    'Componente 1',
    'Componente 2',
    'Componente 3'
  ];

  void _addNewComponente(String componente) {
    _componentes.add(componente);
    setState(() {});
  }
  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: const Text('Nova Manutenção'),
          centerTitle: true,
        ),
        body: Padding(
            padding: const EdgeInsets.all(16.0),
            child: ListView(
              controller: _scrollController,
              children: [
                const Text(
                  'Aeronave PTX-123',
                  style: TextStyle(fontSize: 14, fontWeight: FontWeight.w500),
                ),
                const SizedBox(height: 10),
                const CustomText(text: 'Componente'),
                const SizedBox(height: 10),
                CustomComboBoxExpanded(
                  selectedName: _componenteSelecionado ?? "Selecione",
                  onTap: () async {
                    Util.closeKeyBoard();
                    await showDialog(
                        context: context,
                        builder: (BuildContext context) {
                          return AlertDialog(
                              backgroundColor: const Color(0xFFF5F5F5),
                              content: SizedBox(
                                width: double.maxFinite,
                                child: ComponenteNameSelect(
                                  onAddComponente: _addNewComponente,
                                  componentes: _componentes,
                                  onChangeComponente: (componente) {
                                    _componenteSelecionado = componente;
                                    setState(() {});
                                  },
                                ),
                              ));
                        });
                  },
                ),
                const SizedBox(height: 10),
                const CustomText(text: "Observações "),
                const SizedBox(height: 10),
                Container(
                  width: double.infinity,
                  height: 100,
                  margin: const EdgeInsets.only(bottom: 20),
                  padding:
                      const EdgeInsets.symmetric(horizontal: 16, vertical: 10),
                  decoration: ShapeDecoration(
                    shape: RoundedRectangleBorder(
                      side:
                          const BorderSide(width: 1, color: Color(0xFF636363)),
                      borderRadius: BorderRadius.circular(10),
                    ),
                  ),
                  child: TextField(
                    minLines: 4,
                    maxLines: 4,
                    controller: _observationTextField,
                    onChanged: (value) {},
                    textInputAction: TextInputAction.done,
                    onSubmitted: (value) {
                      if (_observationTextField.text.isEmpty) return;
                      try {
                        _observationTextField.clear();
                        setState(() {});
                        Util.toastSucesso("Observação adicionada com sucesso!");
                      } catch (e) {
                        Util.toastErro("Observação não foi adicionada");
                      }
                    },
                    decoration: const InputDecoration(
                        hintText: "-",
                        border: InputBorder.none,
                        hintStyle: TextStyle(
                          color: Color.fromARGB(255, 121, 118, 118),
                          fontSize: 16,
                          fontFamily: 'Inter',
                          fontWeight: FontWeight.w500,
                           
                        )),
                  ),
                ),
                const CustomText(text: "Imagens"),
                const SizedBox(height: 10),
                InkWell(
                    onTap: () async {
                      final archive =
                          await Util.obtainImagePathMaps(context,isPdf: false);
                      Uint8List? imageData =
                          await File(archive.path!).readAsBytes();
                      if (!archive.isImage) return;

                      // ignore: use_build_context_synchronously
                      Navigator.push(
                          // ignore: use_build_context_synchronously
                          context,
                          MaterialPageRoute(
                              builder: (context) => UploadFotos(
                                    updateImageData: (data) {
                                      _images.add(base64Encode(data));
                                      setState(() {});
                                    },
                                    imageData: imageData,
                                    onOkButton: () {
                                      Navigator.pop(context);
                                    },
                                  )));
                    },
                    child: Container(
                      height: 60,
                      color: const Color(0xFFECEAEA),
                      padding: const EdgeInsets.symmetric(
                          horizontal: 20, vertical: 10),
                      child: Row(
                        mainAxisAlignment: MainAxisAlignment.spaceBetween,
                        children: [
                          const Icon(Icons.close),
                          const SizedBox(width: 10),
                          Container(
                            width: 35,
                            height: 35,
                            decoration: BoxDecoration(
                                color: Colors.blue,
                                borderRadius: BorderRadius.circular(10)),
                            child: const Icon(
                              Icons.photo_camera,
                              size: 20,
                              color: Colors.white,
                            ),
                          ),
                          const SizedBox(width: 8),
                          const Text(
                            'Adicionar nova imagem \ndo componente',
                            style: TextStyle(
                              color: Color(0xFF151515),
                              fontSize: 13,
                              fontFamily: 'Inter',
                              fontWeight: FontWeight.w600,
                            ),
                          ),
                          const Icon(Icons.arrow_forward_ios_sharp)
                        ],
                      ),
                    )),
                const SizedBox(height: 10),
                ListView.builder(
                  controller: _scrollController,
                  itemCount: _images.length,
                  shrinkWrap: true,
                  itemBuilder: (context, index) => Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      CustomText(text: 'Foto ${index + 1}'),
                      const SizedBox(height: 10),
                      Container(
                        height: 300,
                        width: MediaQuery.of(context).size.width,
                        decoration: BoxDecoration(
                            image: DecorationImage(
                                image:
                                    MemoryImage(base64Decode(_images[index])),
                                fit: BoxFit.fill)),
                      ),
                    ],
                  ),
                ),
                CustomButton(
                    onClick: () async {
                      Navigator.push(
                          context,
                          MaterialPageRoute(
                            builder: (context) =>
                                const ManutencaoComponentesListPage(),
                          ));
                    },
                    title: 'Salvar'),
              ],
            )));
  }
}
