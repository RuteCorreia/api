// ignore_for_file: use_build_context_synchronously

import 'dart:convert';
import 'dart:typed_data';

import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:flytec/features/aplications/components/custom_text.dart';
import 'package:flytec/features/auth/presentation/widgets/custom_login_button.dart';
import 'package:flytec/features/fire_fighting/controller/firefighting_controller.dart';
import 'package:flytec/features/fire_fighting/models/dados_responsavel.dart';
import 'package:flytec/features/fire_fighting/models/firefighting.dart';
import 'package:flytec/features/home/presentation/pages/home_page.dart';
import 'package:go_router/go_router.dart';

class AddFireFightingFourthtepPrivate extends StatefulWidget {
  final FirefightingController? _firefightingController;
  const AddFireFightingFourthtepPrivate(
      {required FirefightingController? firefightingController, super.key})
      : _firefightingController = firefightingController;

  @override
  State<AddFireFightingFourthtepPrivate> createState() =>
      _AddFireFightingSecondStepState();
}

class _AddFireFightingSecondStepState extends State<AddFireFightingFourthtepPrivate> {
  Firefighting? _firefighting;
  TextEditingController _documentoResponsavelController =
      TextEditingController();
  TextEditingController _nomeResponsavelController = TextEditingController();

  String? _assinaturaResponsavel;

  void _onUpdateSignatureResponsavel(Uint8List? signature) {
    _assinaturaResponsavel = base64Encode(signature!);
    setState(() {});
  }

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      setState(() {
        _firefighting = widget._firefightingController?.firefightingSelected;
        _documentoResponsavelController = TextEditingController(
            text: _firefighting?.dadosResponsavel?.documento);
        _nomeResponsavelController =
            TextEditingController(text: _firefighting?.dadosResponsavel?.nome);
        _assinaturaResponsavel = _firefighting?.dadosResponsavel?.assinatura;
      });
    });
  }

  Future<void> _actionFirefighting() async {
    DadosResponsavel dadosResponsavel = DadosResponsavel(
        nome: _nomeResponsavelController.text,
        documento: _documentoResponsavelController.text,
        assinatura: _assinaturaResponsavel);
    await _actionDadosResponsavelOperacional(dadosResponsavel);
  }

  Future<void> _actionDadosResponsavelOperacional(
      DadosResponsavel? dadosResponsavel) async {
    if (dadosResponsavel == null) return;
    if (dadosResponsavel.id == null ||
        (dadosResponsavel.id != null && dadosResponsavel.id! <= 0)) {
      final idDadosResponsavel = await widget._firefightingController
          ?.createElementInTable(
              dadosResponsavel.toJson(), 'DadosResponsavelFirefighting');
      await widget._firefightingController?.updateElementInTable(
          _firefighting!.id!,
          {'dadosResponsavelFirefighting_id': idDadosResponsavel},
          'Firefighting');
      _firefighting?.idDadosResponsavel = idDadosResponsavel;
      setState(() {});
      widget._firefightingController?.setFirefightingSelected(_firefighting!);
      return;
    }
    await widget._firefightingController?.updateElementInTable(
        dadosResponsavel.id!,
        dadosResponsavel.toJson(),
        'DadosResponsavelFirefighting');
    widget._firefightingController?.setFirefightingSelected(_firefighting!);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
          title: const Text("Combate a Incêndio"),
          leading: IconButton(
            icon: const Icon(Icons.arrow_back),
            onPressed: () async {
              await _actionFirefighting();
              Navigator.pop(context);
            },
          )),
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: SingleChildScrollView(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const SizedBox(height: 16),
              const SizedBox(
                width: 328,
                child: Text(
                  'Dados do Responsável',
                  style: TextStyle(
                    color: Color.fromARGB(255, 121, 118, 118),
                    fontSize: 14,
                    fontFamily: 'Inter',
                    fontWeight: FontWeight.w600,
                    height: 0.11,
                  ),
                ),
              ),
              const SizedBox(height: 25),
              const CustomText(text: 'Nome'),
              const SizedBox(height: 14),
              Container(
                width: double.infinity,
                height: 50,
                margin: const EdgeInsets.only(bottom: 20),
                padding:
                    const EdgeInsets.symmetric(horizontal: 16, vertical: 10),
                decoration: ShapeDecoration(
                  shape: RoundedRectangleBorder(
                    side: const BorderSide(width: 1, color: Color(0xFF636363)),
                    borderRadius: BorderRadius.circular(10),
                  ),
                ),
                child: TextField(
                  controller: _nomeResponsavelController,
                  decoration: const InputDecoration(
                      hintText: "Digite aqui",
                      border: InputBorder.none,
                      hintStyle: TextStyle(
                        color: Color.fromARGB(255, 121, 118, 118),
                        fontSize: 16,
                        fontFamily: 'Inter',
                        fontWeight: FontWeight.w500,
                      )),
                ),
              ),
              const CustomText(text: 'Documento'),
              const SizedBox(height: 14),
              Container(
                width: double.infinity,
                height: 50,
                margin: const EdgeInsets.only(bottom: 20),
                padding:
                    const EdgeInsets.symmetric(horizontal: 16, vertical: 10),
                decoration: ShapeDecoration(
                  shape: RoundedRectangleBorder(
                    side: const BorderSide(width: 1, color: Color(0xFF636363)),
                    borderRadius: BorderRadius.circular(10),
                  ),
                ),
                child: TextField(
                  controller: _documentoResponsavelController,
                  decoration: const InputDecoration(
                      hintText: "Digite aqui",
                      border: InputBorder.none,
                      hintStyle: TextStyle(
                        color: Color.fromARGB(255, 121, 118, 118),
                        fontSize: 16,
                        fontFamily: 'Inter',
                        fontWeight: FontWeight.w500,
                      )),
                ),
              ),
             
              AssignmentButton(
                onClick: () {
                  context.push("/addsignature", extra: {
                    "onUpdateSignature": _onUpdateSignatureResponsavel
                  });
                },
              ),
              if (_assinaturaResponsavel != null &&
                  _assinaturaResponsavel!.isNotEmpty)
                Image.memory(
                  base64Decode(_assinaturaResponsavel!),
                  width: MediaQuery.of(context).size.width,
                  height: MediaQuery.of(context).size.height * 0.34,
                  fit: BoxFit.fill,
                ),
              const SizedBox(height: 14),
              Center(
                child: CustomButton(
                  title: "Finalizar",
                  onClick: () async {
                    await _actionFirefighting();
                    Navigator.pushReplacement(
                        context,
                        MaterialPageRoute(
                          builder: (context) => const HomePaga(),
                        ));
                  },
                ),
              )
            ],
          ),
        ),
      ),
    );
  }
}

class AssignmentButton extends StatelessWidget {
  const AssignmentButton({super.key, required this.onClick});
  final VoidCallback? onClick;

  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onClick,
      child: Container(
        width: 328,
        height: 56,
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
              child: Column(
                mainAxisSize: MainAxisSize.min,
                mainAxisAlignment: MainAxisAlignment.center,
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Container(
                    width: double.infinity,
                    padding: const EdgeInsets.only(left: 8),
                    child: const Row(
                      mainAxisSize: MainAxisSize.min,
                      mainAxisAlignment: MainAxisAlignment.center,
                      crossAxisAlignment: CrossAxisAlignment.center,
                      children: [
                        Expanded(
                          child: Column(
                            mainAxisSize: MainAxisSize.min,
                            mainAxisAlignment: MainAxisAlignment.center,
                            crossAxisAlignment: CrossAxisAlignment.center,
                            children: [
                              SizedBox(
                                width: double.infinity,
                                child: Text(
                                  'Assinatura',
                                  style: TextStyle(
                                    color: Color.fromARGB(255, 121, 118, 118),
                                    fontSize: 16,
                                    fontFamily: 'Inter',
                                    fontWeight: FontWeight.w600,
                                  ),
                                ),
                              ),
                            ],
                          ),
                        ),
                      ],
                    ),
                  ),
                ],
              ),
            ),
            const SizedBox(width: 16),
            Container(
              width: 24,
              height: 24,
              clipBehavior: Clip.antiAlias,
              decoration: const BoxDecoration(),
              child: Stack(
                  children: [SvgPicture.asset("assets/images/arrow.svg")]),
            ),
          ],
        ),
      ),
    );
  }
}
