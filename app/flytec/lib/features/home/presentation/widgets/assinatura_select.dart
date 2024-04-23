import 'dart:typed_data';
import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/save_local_controller.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/components/custom_button.dart';
import 'package:flytec/features/executor/domain/repositories/executor_repository.dart';
import 'package:flytec/features/signature/domain/usecases/get_signature_usecase.dart';
import 'package:flytec/features/signature/domain/usecases/save_signature_usecase.dart';
import 'package:go_router/go_router.dart';

class AssinaturaSelect extends StatelessWidget {
  final Uint8List? assinatura;
  final Function(Uint8List?) updateSignature;
  const AssinaturaSelect(
      {super.key, required this.updateSignature, required this.assinatura});
  bool get _isSaveSignature =>
      base64Encode(assinatura!) != getIt<GlobalConfigVars>().assinatura!;
  @override 
  Widget build(BuildContext context) {
    return SizedBox(
        height: MediaQuery.of(context).size.height *
            (_isSaveSignature ? 0.675 : 0.555),
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
            if (_isSaveSignature)
              CustomButton(
                  onClick: () async {
                    final imageEncoded = base64.encode(assinatura!);
                    await getIt<SaveSignatureUseCase>()
                        .call(imageEncoded)
                        .then((value) {
                      value.fold((l) {
                        Util.toastErro(
                            'Não foi possível salvar sua assinatura');
                      }, (r) async {
                        await getIt<GetSignatureUseCase>()
                            .call(NoParams())
                            .then((value) => value.fold((l) {}, (r) async {
                                  getIt<GlobalConfigVars>()
                                      .setAssinatura(newAssinatura: r);
                                  var preloadData =
                                      getIt<SaveLocalDataController>()
                                          .initializeLocalData();
                                  await getIt<SaveLocalDataController>()
                                      .salvarLocalPreloadData(
                                          preloadData: preloadData);

                                  Util.toastSucesso(
                                      'Assinatura Salva com Sucesso');
                                }));
                      });
                    });

                    // ignore: use_build_context_synchronously
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
