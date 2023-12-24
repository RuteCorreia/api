import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

import '../../../auth/presentation/widgets/custom_login_button.dart';
import 'my_activity_page.dart';
import 'relatorio_aplicacao_page.dart';
import 'package:image_picker/image_picker.dart';

class AplicacoesPage extends StatelessWidget {
  AplicacoesPage({super.key});
  final ImagePicker picker = ImagePicker();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
        title: const Text(
          "Aplicações",
          textAlign: TextAlign.center,
          style: TextStyle(
            fontSize: 15,
          ),
        ),
      ),
      body: Padding(
        padding: const EdgeInsets.all(8.0),
        child: SingleChildScrollView(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const SizedBox(height: 16),
              const CustomText(text: 'Data da aplicação'),
              const SizedBox(height: 14),
              const SizedBox(
                  width: double.infinity,
                  child: ComboBox(selectedName: "Selecione")),
              const SizedBox(height: 20),
              const CustomText(text: 'Horário de início'),
              const SizedBox(height: 14),
              const SizedBox(
                width: double.infinity,
                child: ComboBox(selectedName: "Selecione"),
              ),
              const SizedBox(height: 14),
              const CustomText(text: 'Horímetro inicial'),
              const SizedBox(height: 14),
              const CustomTextField(),
              const SizedBox(height: 14),
              const CustomText(text: 'Horário de término'),
              const SizedBox(height: 14),
              const SizedBox(
                  width: double.infinity,
                  child: ComboBox(selectedName: "Selecione")),
              const SizedBox(height: 20),
              const CustomText(text: "Horímetro final"),
              const SizedBox(height: 14),
              const CustomTextField(),
              const SizedBox(height: 20),
              InkWell(
                  onTap: () async {
                    final XFile? image =
                        await picker.pickImage(source: ImageSource.gallery);
                  },
                  child: const UploadButton()),
              const SizedBox(height: 24),
              const Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [],
              ),
             
              const Center(
                child: Text(
                  'Condições climáticas durante a aplicação',
                  textAlign: TextAlign.center,
                  style: TextStyle(
                    color: Color(0xFF151515),
                    fontSize: 14,
                    fontFamily: 'Inter',
                    fontWeight: FontWeight.w600,
                    height: 0.11,
                  ),
                ),
              ),
              const SizedBox(height: 40),
              const Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Column(
                    children: [
                      CustomText(text: "INICIAL"),
                      Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          SizedBox(height: 40),
                          CustomText(text: "Temperatura (°C)"),
                          SizedBox(height: 12),
                          ComboBox(selectedName: "Selecione")
                        ],
                      ),
                    ],
                  ),
                  Column(
                    children: [
                      CustomText(text: "FINAL"),
                      Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          SizedBox(height: 40),
                          CustomText(text: "Temperatura (°C)"),
                          SizedBox(height: 12),
                          ComboBox(selectedName: "Selecione")
                        ],
                      ),
                    ],
                  ),
                ],
              ),
              const SizedBox(height: 30),
              const Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      CustomText(text: "U.R do ar (%)"),
                      SizedBox(height: 12),
                      ComboBox(selectedName: "Selecione")
                    ],
                  ),
                  Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      CustomText(text: "U.R do ar (%)"),
                      SizedBox(height: 12),
                      ComboBox(selectedName: "Selecione")
                    ],
                  )
                ],
              ),
              const SizedBox(height: 30),
              const Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      CustomText(text: "Vento (km/h)"),
                      SizedBox(height: 12),
                      ComboBox(selectedName: "Selecione")
                    ],
                  ),
                  Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      CustomText(text: "Vento (km/h)"),
                      SizedBox(height: 12),
                      ComboBox(selectedName: "Selecione")
                    ],
                  )
                ],
              ),
              const SizedBox(height: 20),
              Center(
                child: CustomButton(
                  title: "OK",
                  onClick: () {
                    context.pop();
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
}

class UploadButton extends StatelessWidget {
  const UploadButton({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 60,
      color: const Color(0xFFECEAEA),
      padding: const EdgeInsets.symmetric(horizontal: 20, vertical: 10),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          const Icon(Icons.close),
          const SizedBox(width: 10),
          Container(
            width: 35,
            height: 35,
            decoration: BoxDecoration(
                color: Colors.blue, borderRadius: BorderRadius.circular(10)),
            child: const Icon(
              Icons.photo_camera,
              size: 20,
              color: Colors.white,
            ),
          ),
          const SizedBox(width: 8),
          const Text(
            'Imagens/Print condições\nclimáticas',
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
    );
  }
}
