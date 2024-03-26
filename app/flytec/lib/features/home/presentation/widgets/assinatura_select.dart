import 'dart:typed_data';

import 'package:flutter/material.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/components/custom_button.dart';
import 'package:go_router/go_router.dart';

class AssinaturaSelect extends StatelessWidget {
  final Uint8List? assinatura;
  final Function(Uint8List?) updateSignature;
  const AssinaturaSelect(
      {super.key, required this.updateSignature, required this.assinatura});

  @override
  Widget build(BuildContext context) {
    return SizedBox(
        height: MediaQuery.of(context).size.height * 0.64,
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                const Text(
                  'Assinatura',
                  style: TextStyle(
                    color: Colors.black,
                    fontSize: 14,
                    fontFamily: 'Inter',
                    fontWeight: FontWeight.w700,
                  ),
                ),
                IconButton(
                    onPressed: () => context.pop(),
                    icon: const Icon(Icons.close, size: 18))
              ],
            ),
            Image.memory(
              assinatura!,
              width: MediaQuery.of(context).size.width,
              height: MediaQuery.of(context).size.height * 0.34,
              fit: BoxFit.fill,
            ),
            CustomButton(
                onClick: () {
                  Util.toastSucesso('Assinatura Salva com Sucesso');
                  context.pop();
                },
                title: 'Salvar'),
            CustomButton(
                onClick: () async {
                  await context.push('/addsignature', extra: {
                    'onUpdateSignature': updateSignature,
                  });
                },
                title: 'Editar')
          ],
        ));
  }
}
