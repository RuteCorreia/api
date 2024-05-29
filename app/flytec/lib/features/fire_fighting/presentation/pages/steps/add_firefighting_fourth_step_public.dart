// ignore_for_file: use_build_context_synchronously

import 'dart:convert';
import 'dart:typed_data';

import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:flytec/features/aplications/components/custom_text.dart';
import 'package:flytec/features/fire_fighting/controller/firefighting_controller.dart';
import 'package:flytec/features/fire_fighting/models/comandante_ocorrencia.dart';
import 'package:flytec/features/fire_fighting/models/coordenador_base_operacional.dart';
import 'package:flytec/features/fire_fighting/models/firefighting.dart';
import 'package:flytec/features/home/presentation/pages/home_page.dart';
import 'package:go_router/go_router.dart';

import '../../../../auth/presentation/widgets/custom_login_button.dart';

class AddFireFightingFourthtepPublic extends StatefulWidget {
  final FirefightingController? _firefightingController;
  const AddFireFightingFourthtepPublic(
      {required FirefightingController? firefightingController, super.key})
      : _firefightingController = firefightingController;

  @override
  State<AddFireFightingFourthtepPublic> createState() =>
      _AddFireFightingSecondStepState();
}

class _AddFireFightingSecondStepState extends State<AddFireFightingFourthtepPublic> {
  Firefighting? _firefighting;
  TextEditingController _nomeCoordenadorController = TextEditingController();
  TextEditingController _postoCoordenadorController = TextEditingController();
  TextEditingController _reCoordenadorController = TextEditingController();
  TextEditingController _nomeComandanteController = TextEditingController();
  TextEditingController _postoComandanteController = TextEditingController();
  TextEditingController _reComandanteController = TextEditingController();

  String? _assinaturaCoordenador;
  String? _assinaturaComandante;

  void _onUpdateSignatureCoordenador(Uint8List? signature) {
    _assinaturaCoordenador = base64Encode(signature!);
    setState(() {});
  }

  void _onUpdateSignatureComandante(Uint8List? signature) {
    _assinaturaComandante = base64Encode(signature!);
    setState(() {});
  }

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      setState(() {
        _firefighting = widget._firefightingController?.firefightingSelected;
        _nomeCoordenadorController = TextEditingController(
            text: _firefighting?.coordenadorBaseOperacional?.nome);
        _postoCoordenadorController = TextEditingController(
            text: _firefighting?.coordenadorBaseOperacional?.postoGraduacao);
        _reCoordenadorController = TextEditingController(
            text: _firefighting?.coordenadorBaseOperacional?.re);
        _nomeComandanteController = TextEditingController(
            text: _firefighting?.comandanteOcorrencia?.nome);
        _postoComandanteController = TextEditingController(
            text: _firefighting?.comandanteOcorrencia?.postoGraduacao);
        _reComandanteController = TextEditingController(
            text: _firefighting?.comandanteOcorrencia?.re);
        _assinaturaComandante = _firefighting?.comandanteOcorrencia?.assinatura;
        _assinaturaCoordenador =
            _firefighting?.coordenadorBaseOperacional?.assinatura;
      });
    });
  }

  Future<void> _actionFirefighting() async {
    CoordenadorBaseOperacional coordenadorBaseOperacional =
        CoordenadorBaseOperacional(
            nome: _nomeCoordenadorController.text,
            postoGraduacao: _postoCoordenadorController.text,
            re: _reCoordenadorController.text,
            assinatura: _assinaturaCoordenador);
    await _actionCoordenadorBaseOperacional(coordenadorBaseOperacional);

    ComandanteOcorrencia comandanteOcorrencia = ComandanteOcorrencia(
        nome: _nomeComandanteController.text,
        postoGraduacao: _postoComandanteController.text,
        re: _reComandanteController.text,
        assinatura: _assinaturaComandante);
    await _actionComandanteOcorrencia(comandanteOcorrencia);
  }

  Future<void> _actionCoordenadorBaseOperacional(
      CoordenadorBaseOperacional? coordenadorBaseOperacional) async {
    if (coordenadorBaseOperacional == null) return;
    if (coordenadorBaseOperacional.id == null ||
        (coordenadorBaseOperacional.id != null &&
            coordenadorBaseOperacional.id! <= 0)) {
      final idCoordenador = await widget._firefightingController
          ?.createElementInTable(coordenadorBaseOperacional.toJson(),
              'CoordenadorBaseOperacionalFirefighting');
      await widget._firefightingController?.updateElementInTable(
          _firefighting!.id!,
          {'coordenadorBaseOperacional_id': idCoordenador},
          'Firefighting');
      widget._firefightingController?.setFirefightingSelected(_firefighting!);
      return;
    }
    await widget._firefightingController?.updateElementInTable(
        coordenadorBaseOperacional.id!,
        coordenadorBaseOperacional.toJson(),
        'CoordenadorBaseOperacionalFirefighting');
    widget._firefightingController?.setFirefightingSelected(_firefighting!);
  }

  Future<void> _actionComandanteOcorrencia(
      ComandanteOcorrencia? comandante) async {
    if (comandante == null) return;
    if (comandante.id == null ||
        (comandante.id != null && comandante.id! <= 0)) {
      final idComandante = await widget._firefightingController
          ?.createElementInTable(
              comandante.toJson(), 'ComandanteOcorrenciaFirefighting');
      await widget._firefightingController?.updateElementInTable(
          _firefighting!.id!,
          {'comandanteOcorrencia_id': idComandante},
          'Firefighting');
      _firefighting?.idComandanteOcorrencia = idComandante;
      _firefighting?.comandanteOcorrencia = comandante;
      widget._firefightingController?.setFirefightingSelected(_firefighting!);
      return;
    }
    await widget._firefightingController?.updateElementInTable(comandante.id!,
        comandante.toJson(), 'ComandanteOcorrenciaFirefighting');
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
                  'Coordenador da base operacional',
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
                  controller: _nomeCoordenadorController,
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
              const CustomText(text: 'Posto / Graduação'),
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
                  controller: _postoCoordenadorController,
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
              const CustomText(text: 'RE'),
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
                  controller: _reCoordenadorController,
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
              const SizedBox(height: 8),
              AssignmentButton(
                onClick: () {
                  context.push("/addsignature", extra: {
                    "onUpdateSignature": _onUpdateSignatureCoordenador
                  });
                },
              ),
               if (_assinaturaCoordenador != null &&
                  _assinaturaCoordenador!.isNotEmpty)
                Image.memory(
                  base64Decode(_assinaturaCoordenador!),
                  width: MediaQuery.of(context).size.width,
                  height: MediaQuery.of(context).size.height * 0.34,
                  fit: BoxFit.fill,
                ),
              const SizedBox(height: 14),
              const SizedBox(height: 20),
              const SizedBox(
                width: 328,
                child: Text(
                  'Comandante da ocorrência',
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
                  controller: _nomeComandanteController,
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
              const CustomText(text: 'Posto / Graduação'),
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
                  controller: _postoComandanteController,
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
              const CustomText(text: 'RE'),
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
                  controller: _reComandanteController,
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
                    "onUpdateSignature": _onUpdateSignatureComandante
                  });
                },
              ),
              if (_assinaturaComandante != null &&
                  _assinaturaComandante!.isNotEmpty)
                Image.memory(
                  base64Decode(_assinaturaComandante!),
                  width: MediaQuery.of(context).size.width,
                  height: MediaQuery.of(context).size.height * 0.34,
                  fit: BoxFit.fill,
                ),
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
              ),
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
